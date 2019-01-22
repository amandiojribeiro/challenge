using Application.Dto;
using AutoMapper;
using Domain.Model;

namespace Application.Services.TypeAdatpters
{
    public class DtoAdapterProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoAdapterProfile"; }
        }

        public DtoAdapterProfile()
        {
            CreateMap<CommentDto, Comment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(e => e.Message))
            .ForMember(dest => dest.State, opt => opt.MapFrom(e => e.State))
            .ReverseMap();
        }
    }
}
