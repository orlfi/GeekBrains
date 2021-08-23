using FluentValidation;
using Timesheets.Infrastructure.Validation;
using Timesheets.Services.Requests.Customers;

namespace Timesheets.Services.Validators
{
    public class AddCustomerCommandValidator: AbstractValidator<AddCustomerCommand>
    {
        private const string nameEmptyCode = "BRL-101.1";
        
        public AddCustomerCommandValidator(IErrorCodes errorCodes)
        {
            RuleFor(v => v.Name).
                NotNull().
                    WithErrorCode(nameEmptyCode).
                    WithMessage(errorCodes.GetMessage(nameEmptyCode)).
                NotEmpty().
                    WithErrorCode(nameEmptyCode).
                    WithMessage(errorCodes.GetMessage(nameEmptyCode));
        }
    }
            
    
}