using Domain.Model.Enums;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public CommentState State { get; set; }

        public ICollection<CommentActions> Actions { get; set; }

        public ICollection<RelatedComments> ChildComments { get; set; }
    }
}
