using Domain.Model;
using Domain.Model.Enums;
using Domain.Services.CommentActionStrategy;

namespace Domain.Services.Services
{
    public class CommentStateService : ICommentStateService
    {
        private readonly CommentActionStrategyFactory _commentActionStrategyFactory;

        public CommentStateService(CommentActionStrategyFactory commentActionStrategyFactory)
        {
            this._commentActionStrategyFactory = commentActionStrategyFactory;
        }
        public void CalculateState(Comment comment, int currentUserId, ActiontType actionType, AuthorType authorType)
        {
            this._commentActionStrategyFactory.GetRequestStateStrategy(authorType).CalculateSate(comment, currentUserId, actionType);
        }
    }
}
