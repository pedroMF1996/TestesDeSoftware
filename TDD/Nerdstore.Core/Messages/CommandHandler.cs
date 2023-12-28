
using FluentValidation.Results;
using MediatR;
using Nerdstore.Core.Data;
using Nerdstore.Core.DomainObjects;

namespace Nerdstore.Core.Messages
{
    public abstract class CommandHandler
    {
        public ValidationResult ValidationResult { get; private set; }
        private IMediator _mediator;
        protected CommandHandler(IMediator mediator)
        {
            ValidationResult = new ValidationResult();
            _mediator = mediator;
        }

        protected void AddErro(string msg)
        {
            ValidationResult.Errors.Add(new ValidationFailure(nameof(CommandHandler), msg));
        }

        protected async Task<ValidationResult> LancarErrosDeValidacao(Command message, CancellationToken cancellationToken)
        {
            foreach (var erro in message.ValidationResult.Errors.Select(e => e.ErrorMessage))
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, erro), cancellationToken);
            }

            return message.ValidationResult;
        }

        protected async Task<ValidationResult> Commit<T>(IRepository<T> repository) where T : IAggregateRoot
        {
            if(await repository.UnitOfWork.Commit())
                return ValidationResult;
            
            AddErro("Erro no salvamento");
            return ValidationResult;
        }
    }
}
