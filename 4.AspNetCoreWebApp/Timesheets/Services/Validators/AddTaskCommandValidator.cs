using FluentValidation;
using Timesheets.Infrastructure.Validation;
using Timesheets.Services.Requests.Tasks;

namespace Timesheets.Services.Validators
{
    public class AddTaskCommandValidator: AbstractValidator<AddTaskCommand>
    {
        private const string nameEmptyCode = "BRL-101.1";
        private const string amountErrorCode = "BRL-101.2";
         
        public AddTaskCommandValidator(IErrorCodes errorCodes)
        {
            RuleFor(v => v.Name).
                NotNull().
                    WithErrorCode(nameEmptyCode).
                    WithMessage(errorCodes.GetMessage(nameEmptyCode)).
                NotEmpty().
                    WithErrorCode(nameEmptyCode).
                    WithMessage(errorCodes.GetMessage(nameEmptyCode));
                    
            RuleFor(v => v.Amount).                
                GreaterThan(0).
                    WithErrorCode(amountErrorCode).
                    WithMessage(errorCodes.GetMessage(amountErrorCode));
        }
    }
            
    
}