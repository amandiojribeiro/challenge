using Domain.Model;
using Domain.Model.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services.CommentActionStrategy.Strategies
{
    public abstract class BaseStrategy
    {
        public abstract Dictionary<ActiontType, CommentState> AvailableStates { get; }

        protected void CalculateCommentState(Comment comment, ActiontType type )
        {
            if (!this.AvailableStates.ContainsKey(type))
            {
                comment.State = CommentState.Rejected;
            }

            comment.State = this.AvailableStates[type];
        }
    }
}
