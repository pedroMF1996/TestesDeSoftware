
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

        protected void AdicionarErros(ValidationResult validationResult) 
        {
            validationResult.Errors.Select(e => e.ErrorMessage).ToList().ForEach(AddErro);
        }

        protected async Task<ValidationResult> LancarErrosDeValidacao(Command message, CancellationToken cancellationToken)
        {
            AdicionarErros(message.ValidationResult);
            return await LancarErros(message.MessageType, cancellationToken);
        }
        
        protected async Task<ValidationResult> LancarErrosDeValidacao(string origin, ValidationResult validationResult, CancellationToken cancellationToken)
        {
            AdicionarErros(validationResult);
            return await LancarErros(origin, cancellationToken);
        }

        protected async Task<ValidationResult> LancarErros(string origin, CancellationToken cancellationToken)
        {
            foreach (var erro in ValidationResult.Errors.Select(e => e.ErrorMessage))
            {
                await _mediator.Publish(new DomainNotification(origin, erro), cancellationToken);
            }
            return ValidationResult;
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
