using AutoMapper;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;

namespace DevQuotes.Application.Mapping;

public sealed class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<QuoteRequest, Quote>();
        CreateMap<Quote, QuoteResponse>();
        CreateMap<QuoteRequest, QuoteResponse>().ReverseMap();

        CreateMap<LanguageRequest, Language>();
        CreateMap<Language, LanguageResponse>();
    }
}
