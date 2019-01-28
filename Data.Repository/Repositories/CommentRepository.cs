using System.Linq;
using System.Threading.Tasks;
using Data.Repository.Context;
using Domain.Core.RepositoryInterfaces;
using Domain.Model;
using SharpRepository.EfCoreRepository;
using SharpRepository.Repository.FetchStrategies;
using SharpRepository.Repository.Specifications;

namespace Data.Repository.Repositories
{
    public class CommentRepository : EfCoreRepository<Comment> , ICommentsRepository
    {
        public CommentRepository(ChallengeContext context)
           : base(context)
        {
        }

        public async Task<Comment> GetCommentById(int id)
        {
            //this is only needed if we want to use lazy loading
            var spec = new Specification<Comment>(obj => obj.Id == id)
            {
                FetchStrategy = new GenericFetchStrategy<Comment>()
            };

            spec.FetchStrategy
                            .Include(x => x.Actions)
                            .Include(x => x.ChildComments);


            var result = this.Find(spec);

            //for normal eager loading just use and don't use the above code att all
            //this.Find(x => x.Id == id);

            return await Task.FromResult<Comment>(result);
        }
    }
}
