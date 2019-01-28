using Application.Dto;
using Domain.Model.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.CommentService
{
    public interface ICommentService
    {
        Task<CommentDto> AddComment(CommentDto comment, AuthorType authorType, int userId);

        Task<CommentDto> EditComment(CommentDto comment, AuthorType authorType, int userId);

        Task<bool> DeleteComment(CommentDto comment, AuthorType authorType, int userId);

        Task<CommentDto> ReplyToComment(CommentDto comment, AuthorType authorType, int parentId, int userId);

        Task<CommentDto> GetComment(int id);

        Task<List<CommentDto>> GetAllComments();
    }
}
