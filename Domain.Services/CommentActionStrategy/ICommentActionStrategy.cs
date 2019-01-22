using Domain.Model;
using Domain.Model.Enums;

namespace Domain.Services.CommentActionStrategy
{
    public interface ICommentActionStrategy
    {
        void CalculateSate(Comment comment, int CurrentUserId, ActiontType actionType);
    }
}
