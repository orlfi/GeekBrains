using BankCards.ApiOrm.DTO.Cards;
using BankCards.Domain;
using FluentValidation;

namespace BankCards.ApiOrm.Validators;

public class BookUpdateRequestValidator : AbstractValidator<BookUpdateRequest>
{
    public BookUpdateRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Укажите ID")
            .Length(24).WithMessage("Длина должна быть от 24 символа");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Укажите имя книги");

        RuleFor(x => x.Categories)
            .NotNull().WithMessage("Укажите категории")
            .Length(1, 255).WithMessage("Длина должна быть от 1 до 255 символов");

        RuleFor(x => x.Authors)
            .NotEmpty().WithMessage("Авторы книги")
            .Length(1, 255).WithMessage("Длина должна быть от 1 до 255 символов");

        RuleFor(x => x.PageCount)
            .NotNull();
    }
}
