using FluentValidation.Results;

using NYCSS.Utils.Data;

namespace NYCSS.Utils.MessageBus.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddError("An error ocurred persisting data.");

            return ValidationResult;
        }
    }
}