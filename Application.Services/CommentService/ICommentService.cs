using Application.Dto.Comment;
using System.Threading.Tasks;

namespace Application.Services.CommentService
{
    public interface ICommentService
    {
        Task<CommentDto> AddComment(CommentDto comment);

        Task<CommentDto> EditComment(CommentDto comment);

        Task<bool> DeleteComment(CommentDto comment);

        Task<CommentDto> ReplyToComment(CommentDto comment);
    }
}
