using FluentValidation;
using Timesheets.Infrastructure.Validation;
using Timesheets.Services.Requests.Contracts;

namespace Timesheets.Services.Validators
{
    public class AddContractCommandValidator: AbstractValidator<AddContractCommand>
    {
        private const string nameEmptyCode = "BRL-102.1";
        
        private const string numberEmptyCode = "BRL-102.2";
        private const string hourCostErrorCode = "BRL-102.3";

        public AddContractCommandValidator(IErrorCodes errorCodes)
        {
            RuleFor(v => v.Name).
                NotNull().
                    WithErrorCode(nameEmptyCode).
                    WithMessage(errorCodes.GetMessage(nameEmptyCode)).
                NotEmpty().
                    WithErrorCode(nameEmptyCode).
                    WithMessage(errorCodes.GetMessage(nameEmptyCode));

            RuleFor(v => v.Number).
                NotNull().
                    WithErrorCode(numberEmptyCode).
                    WithMessage(errorCodes.GetMessage(numberEmptyCode)).
                NotEmpty().
                    WithErrorCode(numberEmptyCode).
                    WithMessage(errorCodes.GetMessage(numberEmptyCode));

            RuleFor(v => v.HourCost).
                GreaterThan(0).
                    WithErrorCode(hourCostErrorCode).
                    WithMessage(errorCodes.GetMessage(hourCostErrorCode));

        }
    }
            
    
}