
using FluentValidation.Results;
using Nerdstore.Core.Data;
using Nerdstore.Core.DomainObjects;

namespace Nerdstore.Core.Messages
{
    public abstract class CommandHandler
    {
        public ValidationResult ValidationResult { get; private set; }

        protected CommandHandler() 
        {
            ValidationResult = new ValidationResult();
        }

        public void AddErro(string msg)
        {
            ValidationResult.Errors.Add(new ValidationFailure("commandHandler", msg));
        }

        public async Task<ValidationResult> Commit<T>(IRepository<T> repository) where T : IAggregateRoot
        {
            if(await repository.UnitOfWork.Commit())
                return ValidationResult;
            
            AddErro("Erro no salvamento");
            return ValidationResult;
        }
    }
}
