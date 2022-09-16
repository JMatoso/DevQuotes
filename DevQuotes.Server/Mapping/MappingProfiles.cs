using AutoMapper;
using DevQuotes.Models.Entities;
using DevQuotes.Models.Models;
using DevQuotes.Models.Requests;

namespace DevQuotes.Server.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Quote, QuoteVM>().ReverseMap();
            CreateMap<Quote, QuoteRequest>().ReverseMap();
        }
    }
}
