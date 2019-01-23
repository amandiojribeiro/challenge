using Data.Repository.Context;
using Domain.Core.RepositoryInterfaces;
using Domain.Model;
using SharpRepository.EfCoreRepository;

namespace Data.Repository.Repositories
{
    public class RelatedCommentsRepository : EfCoreRepository<RelatedComments>, IRelatedCommentsRepository
    {
        public RelatedCommentsRepository(ChallengeContext context)
          : base(context)
        {
        }
    }
}
