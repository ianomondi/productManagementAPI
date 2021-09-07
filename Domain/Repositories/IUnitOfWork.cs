using System.Threading.Tasks;

namespace ProductManagement.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}