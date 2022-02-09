using BankCards.ApiOrm.DTO.Cards;
using BankCards.Domain;
using FluentValidation;

namespace BankCards.ApiOrm.Validators;

public class BookCreateRequestValidator: AbstractValidator<BookCreateRequest>
{
    public BookCreateRequestValidator()
    {
        RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("Укажите имя книги");

        RuleFor(x => x.Category)
            .NotNull().WithMessage("Укажите категория")
            .Length(1, 255).WithMessage("Длина должна быть от 1 до 255 символов");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Автора книги")
            .Length(1, 255).WithMessage("Длина должна быть от 1 до 255 символов");

        RuleFor(x => x.Price)
            .NotNull();
    }
}
