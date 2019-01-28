using Domain.Model.Enums;
using System;

namespace Domain.Model
{
    public class CommentActions
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public ActiontType Action { get; set; }

        public DateTime? Date { get; set; }

        public int UserId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
