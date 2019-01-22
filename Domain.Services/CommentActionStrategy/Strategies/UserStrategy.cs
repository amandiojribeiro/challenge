using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Domain.Model.Enums;

namespace Domain.Services.CommentActionStrategy.Strategies
{
    public class UserStrategy : BaseStrategy, ICommentActionStrategy
    {
        private readonly Dictionary<ActiontType, CommentState> _availableStates;

        public sealed override Dictionary<ActiontType, CommentState> AvailableStates { get { return _availableStates; } }

        public UserStrategy()
        {
            this._availableStates = new Dictionary<ActiontType, CommentState>()
            {
                {ActiontType.Add, CommentState.Accepted },
                {ActiontType.Edit, CommentState.Accepted },
                {ActiontType.Delete, CommentState.Rejected },
            };
        }

        public void CalculateSate(Comment comment, int CurrentUserId, ActiontType type)
        {
            switch (type)
            {
                case ActiontType.Edit:
                    var authorId = comment.Actions.Where(x => x.Type == ActiontType.Add).FirstOrDefault().UserId;
                    if (CurrentUserId != authorId)
                    {
                        comment.State = CommentState.Rejected;
                    }
                    break;
                default:
                    this.CalculateCommentState(comment, type);
                    break;
            }
        }
    }
}
