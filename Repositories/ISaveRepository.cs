using System.Threading.Tasks;

namespace PetsMicroservice.Repositories
{
    public interface ISaveRepository
    {
        Task CompleteAsync();
    }
}
