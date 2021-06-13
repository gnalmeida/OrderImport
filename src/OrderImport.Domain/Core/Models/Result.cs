using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace OrderImport.Domain.Core.Models
{
    public class Result<T>
    {
        private ValidationResult _validationResult;

        public Result(T command)
        {
            Command = command;
            _validationResult = new ValidationResult();
        }

        public Result(T command, ValidationResult validationResult)
        {
            Command = command;
            _validationResult = validationResult;
        }

        public T Command { get; }
        public bool IsValid => this._validationResult.IsValid;
        public IEnumerable<string> ErrorMessages => this._validationResult.Errors.Select(e => e.ErrorMessage);

        public void AddErrorMessage(string message)
        {
            this._validationResult.Errors.Add(new ValidationFailure("", message));
        }

        public void AddErrorMessage(IEnumerable<ValidationFailure> validationFailures)
        {
            foreach (var item in validationFailures)
            {
                this._validationResult.Errors.Add(item);
            }
        }
    }
}
