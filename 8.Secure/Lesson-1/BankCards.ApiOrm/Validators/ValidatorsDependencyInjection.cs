using AutoMapper;
using BankCards.ApiOrm.DTO.Cards;
using BankCards.ApiOrm.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BankCards.ApiOrm.Mappings
{
    public static class ValidatorsDependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {

            services.AddFluentValidation();
            services.AddScoped<IValidator<CardCreateRequest>, CardCreateRequestValidator>();
            services.AddScoped<IValidator<CardUpdateRequest>, CardUpdateRequestValidator>();
            return services;
        }
    }
}
