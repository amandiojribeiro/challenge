using Domain.Model.Enums;

namespace Application.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public CommentState State { get; set; }
    }
}
