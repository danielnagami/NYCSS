using MediatR;

namespace NYCSS.UserApi.Application.Events
{
    public class UserEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}