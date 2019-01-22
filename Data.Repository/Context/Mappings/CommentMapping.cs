using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Context.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //table
            builder.ToTable("Comments");
            // Primary key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Message).HasColumnName("Message");
            builder.Property(x => x.State).HasColumnName("State");
        }
    }
}
