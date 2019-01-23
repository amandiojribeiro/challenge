using Data.Repository.Context;
using Domain.Core.RepositoryInterfaces;
using Domain.Model;
using SharpRepository.EfCoreRepository;

namespace Data.Repository.Repositories
{
    public class CommentActionsRepository : EfCoreRepository<CommentActions>, ICommentActionsRepository
    {
        public CommentActionsRepository(ChallengeContext context)
          : base(context)
        {
        }
    }
}
