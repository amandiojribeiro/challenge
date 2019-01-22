using System;
using System.Collections.Generic;
using Domain.Model;
using Domain.Model.Enums;

namespace Domain.Services.CommentActionStrategy.Strategies
{
    public class AdministratorStrategy :BaseStrategy, ICommentActionStrategy
    {
        private readonly Dictionary<ActiontType, CommentState> _availableStates;

        public sealed override Dictionary<ActiontType, CommentState> AvailableStates { get { return _availableStates; } }

        public AdministratorStrategy()
        {
            this._availableStates = new Dictionary<ActiontType, CommentState>()
            {
                {ActiontType.Add, CommentState.Accepted },
                {ActiontType.Edit, CommentState.Accepted },
                {ActiontType.Delete, CommentState.Accepted },
            };
        }

        public void CalculateSate(Comment comment, int CurrentUserId, ActiontType actionType)
        {
            this.CalculateCommentState(comment, actionType);
        }
    }
}
