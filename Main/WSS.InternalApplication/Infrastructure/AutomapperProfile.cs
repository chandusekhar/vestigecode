using AutoMapper;
using UtilityBilling.Data;
using WSS.Data;
using WSS.InternalApplication.Models;

namespace WSS.InternalApplication.Infrastructure
{
    public class AutomapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<WssAccount, AccountViewModel>();
            // .ForMember(dest => dest.PrimaryAccountHolderName, opt => opt.MapFrom(src => src.AccountHolderName));

            CreateMap<DocumentHeader, DocumentListViewModel>()
                .ForMember(dest => dest.DocumentDate, opt => opt.MapFrom(src => src.DocumentIssueDate))
                .ForMember(dest => dest.DocumnetStatuscode, opt => opt.MapFrom(src => src.DocumentStatusCode))
                //.ForMember(dest => dest.DocumentLink, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentTypeCode))
                .ForMember(dest => dest.AmountDue, opt => opt.MapFrom(src => src.BillAmmountDue));
        }
    }
}