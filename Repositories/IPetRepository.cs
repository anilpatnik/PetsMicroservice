using System.Collections.Generic;
using System.Threading.Tasks;
using PetsMicroservice.Models;

namespace PetsMicroservice.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> ListAsync();

        Task<Pet> FindByIdAsync(int id);

        void Add(Pet payload);

        void Update(Pet payload);

        void Remove(Pet payload);
    }
}
