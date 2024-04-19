using DevQuotes.Application.UseCases.Quotes.Add;
using DevQuotes.Application.UseCases.Quotes.Delete;
using DevQuotes.Application.UseCases.Quotes.GetAll;
using DevQuotes.Application.UseCases.Quotes.GetById;
using DevQuotes.Application.UseCases.Quotes.GetLanguages;
using DevQuotes.Application.UseCases.Quotes.GetRandom;
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
        services.AddScoped<IGetRandomQuoteUseCase, GetRandomQuoteUseCase>();
        services.AddScoped<IDeleteQuoteUseCase, DeleteQuoteUseCase>();
        services.AddScoped<IGetAllQuotesUseCase, GetAllQuotesUseCase>();
        services.AddScoped<IUpdateQuoteUseCase, UpdateQuoteUseCase>();
        services.AddScoped<IGetQuoteByIdUseCase,  GetQuoteByIdUseCase>();
        services.AddScoped<IGetLanguagesUseCase, GetLanguagesUseCase>();
    }
}
