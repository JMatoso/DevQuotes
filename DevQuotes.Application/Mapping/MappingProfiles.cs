using AutoMapper;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;

namespace DevQuotes.Application.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<QuoteJsonRequest, Quote>();
        CreateMap<Quote, QuoteJsonResponse>();
        CreateMap<QuoteJsonRequest, QuoteJsonResponse>().ReverseMap();
    }
}
