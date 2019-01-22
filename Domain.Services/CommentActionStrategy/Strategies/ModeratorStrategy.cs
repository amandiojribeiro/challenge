using Domain.Model;
using Domain.Model.Enums;
using System.Collections.Generic;

namespace Domain.Services.CommentActionStrategy.Strategies
{
    public class ModeratorStrategy : BaseStrategy, ICommentActionStrategy
    {
        private readonly Dictionary<ActiontType, CommentState> _availableStates;

        public sealed override Dictionary<ActiontType, CommentState> AvailableStates { get { return _availableStates; } }

        public ModeratorStrategy()
        {
            this._availableStates = new Dictionary<ActiontType, CommentState>()
            {
                {ActiontType.Add, CommentState.Rejected },
                {ActiontType.Edit, CommentState.Rejected },
                {ActiontType.Delete, CommentState.Accepted },
            };
        }

        public void CalculateSate(Comment comment, int CurrentUserId, ActiontType actionType)
        {
            this.CalculateCommentState(comment, actionType);
        }
    }
}
