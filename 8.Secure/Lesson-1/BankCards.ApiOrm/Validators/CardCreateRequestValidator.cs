using BankCards.ApiOrm.DTO.Cards;
using BankCards.Domain;
using FluentValidation;

namespace BankCards.ApiOrm.Validators;

public class CardCreateRequestValidator: AbstractValidator<CardCreateRequest>
{
    public CardCreateRequestValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Укажите номер карты")
            .Length(16).WithMessage("Длина должна быть 16 символов");

        RuleFor(x => x.Type)
            .NotNull().WithMessage("Укажите тип карты");

        RuleFor(x => x.Created)
            .NotEmpty().WithMessage("Укажите дату создания карты");

        RuleFor(x => x.ValidThru)
            .NotEmpty().WithMessage("Укажите срок действия карты");

        RuleFor(x => x.Owner)
            .NotEmpty().WithMessage("Укажите владельца карты")
            .Length(5, 100).WithMessage("Длина должна быть от 5 до 100 символов");

        RuleFor(x => x.Cvc)
            .NotNull()
            .InclusiveBetween(100, 999).WithMessage("Код должен быть 3х значным");
    }
}
