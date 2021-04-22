using System.Collections.Generic;
using System.Threading.Tasks;
using PetsMicroservice.Models;

namespace PetsMicroservice.Services
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> ListAsync();

        Task<Response<Pet>> FindByIdAsync(int id);

        Task<Response<Pet>> SaveAsync(Pet payload);

        Task<Response<Pet>> UpdateAsync(int id, Pet payload);

        Task<Response<Pet>> DeleteAsync(int id);
    }
}
