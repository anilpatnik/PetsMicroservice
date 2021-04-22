using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetsMicroservice.Models;
using PetsMicroservice.Repositories.Context;

namespace PetsMicroservice.Repositories
{
    public class PetRepository : BaseRepository, IPetRepository
    {
        public PetRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Pet>> ListAsync()
        {
            return await _context.Pets.AsNoTracking().ToListAsync();
        }

        public async Task<Pet> FindByIdAsync(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public void Add(Pet payload)
        {
            _context.Pets.Add(payload);
        }

        public void Update(Pet payload)
        {
            _context.Pets.Update(payload);
        }

        public void Remove(Pet payload)
        {
            _context.Pets.Remove(payload);
        }
    }
}
