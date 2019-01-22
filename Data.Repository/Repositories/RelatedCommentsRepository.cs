using Data.Repository.Context;
using Domain.Model;
using SharpRepository.EfCoreRepository;

namespace Data.Repository.Repositories
{
    public class RelatedCommentsRepository : EfCoreRepository<RelatedComments>
    {
        public RelatedCommentsRepository(ChallengeContext context)
          : base(context)
        {
        }
    }
}
