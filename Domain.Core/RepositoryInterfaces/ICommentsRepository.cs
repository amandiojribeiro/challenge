using Domain.Model;
using SharpRepository.Repository;
using System.Threading.Tasks;

namespace Domain.Core.RepositoryInterfaces
{
    public interface ICommentsRepository : IRepository<Comment>
    {
        Task<Comment> GetCommentById(int id);
    }
}
