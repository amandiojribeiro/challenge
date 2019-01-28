using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Context.Mappings
{
    public class RelatedCommentsMapping : IEntityTypeConfiguration<RelatedComments>
    {
        public void Configure(EntityTypeBuilder<RelatedComments> builder)
        {
            //table
            builder.ToTable("RelatedComments");
            builder.HasKey(x => new { x.CommentId, x.RelatedCommentId });
            // Properties
            builder.Property(x => x.CommentId).HasColumnName("CommentId");
            builder.Property(x => x.RelatedCommentId).HasColumnName("RelatedCommentId");
        }
    }
}
