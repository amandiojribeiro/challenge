using Data.Repository.Context.Mappings;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Context
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Comment>(new CommentMapping());

            modelBuilder.ApplyConfiguration<CommentActions>(new CommentActionsMapping());

            modelBuilder.ApplyConfiguration<RelatedComments>(new RelatedCommentsMapping());
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CommentActions> CommentActions { get; set; }

        public DbSet<RelatedComments> RelatedComments { get; set; }
    }
}
