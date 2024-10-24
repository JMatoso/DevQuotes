using DevQuotes.Application.UseCases.Languages.Add;
using DevQuotes.Application.UseCases.Languages.Delete;
using DevQuotes.Application.UseCases.Languages.Get;
using DevQuotes.Application.UseCases.Languages.Update;
using DevQuotes.Application.UseCases.Quotes.Add;
using DevQuotes.Application.UseCases.Quotes.Delete;
using DevQuotes.Application.UseCases.Quotes.Get;
using DevQuotes.Application.UseCases.Quotes.Update;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevQuotes.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IAddQuoteUseCase, AddQuoteUseCase>();
        services.AddScoped<IGetQuotesUseCase,  GetQuotesUseCase>();
        services.AddScoped<IDeleteQuoteUseCase, DeleteQuoteUseCase>();
        services.AddScoped<IUpdateQuoteUseCase, UpdateQuoteUseCase>();

        services.AddScoped<IAddLanguageUseCase, AddLanguageUseCase>();
        services.AddScoped<IGetLanguagesUseCase, GetLanguagesUseCase>();
        services.AddScoped<IDeleteLanguageUseCase, DeleteLanguageUseCase>();
        services.AddScoped<IUpdateLanguageUseCase, UpdateLanguageUseCase>();
    }
}
