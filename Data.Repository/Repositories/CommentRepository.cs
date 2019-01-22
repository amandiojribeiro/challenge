using Data.Repository.Context;
using Domain.Core.RepositoryInterfaces;
using Domain.Model;
using SharpRepository.EfCoreRepository;

namespace Data.Repository.Repositories
{
    public class CommentRepository : EfCoreRepository<Comment> , ICommentsRepository
    {
        public CommentRepository(ChallengeContext context)
           : base(context)
        {
        }
    }
}
