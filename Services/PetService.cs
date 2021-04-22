using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PetsMicroservice.Models;
using PetsMicroservice.Repositories;

namespace PetsMicroservice.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly ISaveRepository _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<PetService> _logger;

        public PetService(
            IPetRepository petRepository
            , ISaveRepository unitOfWork
            , IMemoryCache cache
            , ILogger<PetService> logger)
        {
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<Pet>> ListAsync()
        {
            // Get Pets from the memory cache.
            // If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire 12 hours ahead and
            // returning the Task that lists Pets from the repository.
            var payload = await _cache.GetOrCreateAsync(CacheKeys.Pets, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12);

                return _petRepository.ListAsync();
            });

            return payload;
        }

        public async Task<Response<Pet>> FindByIdAsync(int id)
        {
            var payload = await _petRepository.FindByIdAsync(id);

            return payload == null ?
                new Response<Pet>("Pet not found.") :
                new Response<Pet>(payload);
        }

        public async Task<Response<Pet>> SaveAsync(Pet payload)
        {
            try
            {
                _petRepository.Add(payload);

                await _unitOfWork.CompleteAsync();

                return new Response<Pet>(payload);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PetService)} {nameof(SaveAsync)} {ex.Message}");

                return new Response<Pet>(ex.Message);
            }
        }

        public async Task<Response<Pet>> UpdateAsync(int id, Pet payload)
        {
            var existingPet = await _petRepository.FindByIdAsync(id);

            if (existingPet == null)
            {
                return new Response<Pet>("Pet not found.");
            }

            existingPet.Name = payload.Name;

            try
            {
                _petRepository.Update(existingPet);

                await _unitOfWork.CompleteAsync();

                return new Response<Pet>(existingPet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PetService)} {nameof(UpdateAsync)} {ex.Message}");

                return new Response<Pet>(ex.Message);
            }
        }

        public async Task<Response<Pet>> DeleteAsync(int id)
        {
            var existingPet = await _petRepository.FindByIdAsync(id);

            if (existingPet == null)
            {
                return new Response<Pet>("Pet not found.");
            }

            try
            {
                _petRepository.Remove(existingPet);

                await _unitOfWork.CompleteAsync();

                return new Response<Pet>(existingPet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PetService)} {nameof(DeleteAsync)} {ex.Message}");

                return new Response<Pet>(ex.Message);
            }
        }
    }
}
