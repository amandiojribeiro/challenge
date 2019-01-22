using System;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Core.RepositoryInterfaces;
using Domain.Model;
using Domain.Model.Enums;
using Domain.Services.Services;
using Infrastructure.Crosscuting;

namespace Application.Services.CommentService
{
    //TODO
    //Unit Testing
    //None of this code was tested yet, so expect it to fail miserably...
    public class CommentService : ICommentService
    {
        private readonly ICommentStateService _commentStateService;

        private readonly ICommentsRepository _commentRepository;

        private readonly ICommentActionsRepository _commentActionsRepository;

        private readonly IRelatedCommentsRepository _relatedCommentsRepository;

        public CommentService(ICommentStateService commentStateService, 
            ICommentsRepository commentRepository,
            ICommentActionsRepository commentActionsRepository,
            IRelatedCommentsRepository relatedCommentsRepository)
        {
            this._commentStateService = commentStateService;
            this._commentRepository = commentRepository;
            this._commentActionsRepository = commentActionsRepository;
            this._relatedCommentsRepository = relatedCommentsRepository;
        }
        
        public async Task<CommentDto> AddComment(CommentDto comment, AuthorType authorType)
        {
            var commentFromDto = TypeAdapterHelper.Adapt<Comment>(comment);

            this._commentStateService.CalculateState(commentFromDto, comment.UserId, ActiontType.Add, authorType);

            if (commentFromDto.State == CommentState.Accepted)
            {
                this._commentRepository.Add(commentFromDto);
                var action = new CommentActions() { CommentId = commentFromDto.Id, Action = ActiontType.Add, UserId = comment.UserId };
                this._commentActionsRepository.Add(action);
            }

            return await Task.FromResult<CommentDto>(TypeAdapterHelper.Adapt<CommentDto>(commentFromDto));
        }

        public async Task<bool> DeleteComment(CommentDto comment, AuthorType authorType)
        {
            var operationResult = true;
            var result = this._commentRepository.Find(x => x.Id == comment.Id);

            if (result == null)
            {
                result = new Comment() { State = CommentState.Cancelled };
                operationResult = false;
            }
            else
            {
                this._commentStateService.CalculateState(result, comment.UserId, ActiontType.Delete, authorType);

                if (result.State == CommentState.Accepted)
                {
                    this._commentRepository.Delete(result);
                    var action = new CommentActions() { CommentId = comment.Id, Action = ActiontType.Delete, UserId = comment.UserId };
                    this._commentActionsRepository.Add(action);
                }
            }

            return await Task.FromResult<bool>(operationResult);
        }

        public async Task<CommentDto> EditComment(CommentDto comment, AuthorType authorType)
        {
            var result = this._commentRepository.Find(x => x.Id == comment.Id);

            if( result == null)
            {
                result = new Comment() { State = CommentState.Cancelled };
            }
            else
            {
                 this._commentStateService.CalculateState(result, comment.UserId, ActiontType.Edit, authorType);

                if (result.State == CommentState.Accepted)
                {
                    result.Message = comment.Message;
                    this._commentRepository.Update(result);
                    var action = new CommentActions() { CommentId = result.Id, Action = ActiontType.Edit, UserId = comment.UserId };
                    this._commentActionsRepository.Add(action);
                }
            }

            return await Task.FromResult<CommentDto>(TypeAdapterHelper.Adapt<CommentDto>(result));
        }

        public async Task<CommentDto> ReplyToComment(CommentDto comment, AuthorType authorType, int parentId)
        {
            var commentFromDto = TypeAdapterHelper.Adapt<Comment>(comment);
            var result = this._commentRepository.Find(x => x.Id == parentId);

            if (result == null)
            {
                commentFromDto = new Comment() { State = CommentState.Cancelled };
            }
            else
            {
                this._commentStateService.CalculateState(commentFromDto, comment.UserId, ActiontType.Add, authorType);

                if (commentFromDto.State == CommentState.Accepted)
                {
                    this._commentRepository.Add(commentFromDto);
                    var action = new CommentActions() { CommentId = commentFromDto.Id, Action = ActiontType.Add, UserId = comment.UserId };
                    this._commentActionsRepository.Add(action);
                    var relatedComment = new RelatedComments() { Id = parentId, RelatedId = commentFromDto.Id };
                    this._relatedCommentsRepository.Add(relatedComment);
                }
            }

            return await Task.FromResult<CommentDto>(TypeAdapterHelper.Adapt<CommentDto>(commentFromDto));
        }
    }
}
