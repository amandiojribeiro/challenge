using Data.Repository.Context;
using Domain.Model;
using SharpRepository.EfCoreRepository;

namespace Data.Repository.Repositories
{
    public class CommentActionsRepository : EfCoreRepository<CommentActions>
    {
        public CommentActionsRepository(ChallengeContext context)
          : base(context)
        {
        }
    }
}
