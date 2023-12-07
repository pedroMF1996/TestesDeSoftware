

using FluentValidation.Results;

namespace Nerdstore.Core.Messages
{
    public abstract class CommandHandler
    {
        public ValidationResult ValidationResult { get; private set; }

        public void AddErro(string msg)
        {
            ValidationResult.Errors.Add(new ValidationFailure("commandHandler", msg));
        }
    }
}
