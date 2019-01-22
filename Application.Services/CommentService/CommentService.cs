using System;
using System.Threading.Tasks;
using Application.Dto.Comment;
using Domain.Core.RepositoryInterfaces;
using Domain.Model;
using Domain.Services.Services;

namespace Application.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentStateService _commentStateService;

        private readonly ICommentsRepository _commentRepository;

        public CommentService(ICommentStateService commentStateService)
        {
            this._commentStateService = commentStateService;
        }

        public async Task<CommentDto> AddComment(CommentDto comment)
        {
            var newComment = new Comment() { Message = comment.Message };
            return null;

        }

        public Task<bool> DeleteComment(CommentDto comment)
        {
            throw new NotImplementedException();
        }

        public Task<CommentDto> EditComment(CommentDto comment)
        {
            throw new NotImplementedException();
        }

        public Task<CommentDto> ReplyToComment(CommentDto comment)
        {
            throw new NotImplementedException();
        }
    }
}
