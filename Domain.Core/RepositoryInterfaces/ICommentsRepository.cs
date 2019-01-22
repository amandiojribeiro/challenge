using Domain.Model;
using SharpRepository.Repository;

namespace Domain.Core.RepositoryInterfaces
{
    public interface ICommentsRepository : IRepository<Comment>
    {
    }
}
