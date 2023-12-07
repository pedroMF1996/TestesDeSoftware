using FluentValidation.Results;

namespace Nerdstore.Core.Messages
{
    public abstract class Command : Message
    {
        public ValidationResult ValidationResult { get; set; }
        public DateTime TimeStamp { get; private set; }
        public Command() : base(nameof(Command), Guid.NewGuid())
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            return ValidationResult.IsValid;
        }
    }
}