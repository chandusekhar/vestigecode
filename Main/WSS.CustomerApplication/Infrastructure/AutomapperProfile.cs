using AutoMapper;
using UtilityBilling.Data;
using WSS.CustomerApplication.Models;
using WSS.Data;

namespace WSS.CustomerApplication.Infrastructure
{
    public class AutomapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<WssAccount, AccountViewModel>();

            CreateMap<DocumentHeader, DocumentListViewModel>()
                .ForMember(dest => dest.DocumentDate, opt => opt.MapFrom(src => src.DocumentIssueDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.DocumentStatusCode))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentTypeCode)) 
                .ForMember(dest => dest.AmountDue, opt => opt.MapFrom(src => src.BillAmmountDue));
        }
    }
}