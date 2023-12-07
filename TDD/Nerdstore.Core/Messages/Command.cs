using FluentValidation.Results;
using MediatR;

namespace Nerdstore.Core.Messages
{
    public abstract class Command : Message, 
                                    IRequest<ValidationResult>
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