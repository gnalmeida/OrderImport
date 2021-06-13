using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderImport.Domain.Core.Models
{
    public class DomainException : Exception
    {
        public IEnumerable<ValidationFailure> ValidationFailures { get; private set; }

        public DomainException(IEnumerable<ValidationFailure> validationFailures)
            : base(string.Join(Environment.NewLine, validationFailures.Select(e => e.ErrorMessage)))
        {
            ValidationFailures = validationFailures;
        }
    }
}
