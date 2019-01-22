using Application.Dto;
using Domain.Model.Enums;
using System.Threading.Tasks;

namespace Application.Services.CommentService
{
    public interface ICommentService
    {
        Task<CommentDto> AddComment(CommentDto comment, AuthorType authorType);

        Task<CommentDto> EditComment(CommentDto comment, AuthorType authorType);

        Task<bool> DeleteComment(CommentDto comment, AuthorType authorType);

        Task<CommentDto> ReplyToComment(CommentDto comment, AuthorType authorType, int parentId);
    }
}
