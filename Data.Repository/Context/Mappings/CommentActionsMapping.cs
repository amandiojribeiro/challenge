using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Context.Mappings
{
    public class CommentActionsMapping : IEntityTypeConfiguration<CommentActions>
    {
        public void Configure(EntityTypeBuilder<CommentActions> builder)
        {
            //table
            builder.ToTable("CommentActions");
            // Primary key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.CommentId).HasColumnName("CommentId");
            builder.Property(x => x.Action).HasColumnName("Type");
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.Date).HasColumnName("Date");
        }
    }
}
