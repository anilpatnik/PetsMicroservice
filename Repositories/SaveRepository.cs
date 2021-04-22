using System.Threading.Tasks;
using PetsMicroservice.Repositories.Context;

namespace PetsMicroservice.Repositories
{
    public class SaveRepository : BaseRepository, ISaveRepository
    {
        public SaveRepository(AppDbContext context) : base(context) { }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
