using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Context.Mappings
{
    class RelatedCommentsMapping : IEntityTypeConfiguration<RelatedComments>
    {
        public void Configure(EntityTypeBuilder<RelatedComments> builder)
        {
            //table
            builder.ToTable("RelatedComments");
            // Primary key
            builder.HasKey(x => x.Id);
            builder.HasKey(x => x.RelatedId);

            // Properties
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.RelatedId).HasColumnName("CommentId");
        }
    }
}
