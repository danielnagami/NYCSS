using FluentValidation.Results;

using NYCSS.Utils.MessageBus.Messages;

namespace NYCSS.Utils.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}