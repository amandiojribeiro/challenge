using Domain.Model;
using Domain.Model.Enums;

namespace Domain.Services.Services
{
    public interface ICommentStateService
    {
        void CalculateState(Comment comment, int currentUserId, ActiontType actionType, AuthorType authorType);
    }
}
