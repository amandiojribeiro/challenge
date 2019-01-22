using Domain.Model.Enums;
using Domain.Services.CommentActionStrategy.Strategies;
using System;
using System.Collections.Generic;

namespace Domain.Services.CommentActionStrategy
{

    public class CommentActionStrategyFactory
    {
        private readonly Dictionary<AuthorType, ICommentActionStrategy> availableStrategies;

        public CommentActionStrategyFactory()
        {
            this.availableStrategies = new Dictionary<AuthorType, ICommentActionStrategy>
            {
                { AuthorType.User, new UserStrategy() },
                { AuthorType.Moderator, new ModeratorStrategy() },
                { AuthorType.Administrator, new AdministratorStrategy() },
            };
        }

        public virtual ICommentActionStrategy GetRequestStateStrategy(AuthorType authorType)
        {
            if (!this.availableStrategies.ContainsKey(authorType))
            {
                throw new ArgumentException($"Request type not supported: {authorType}");
            }

            return this.availableStrategies[authorType];
        }
    }
}