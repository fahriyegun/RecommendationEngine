using AutoMapper;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Models.Entities;

namespace Recommendation.API.Mappings
{
    public class ObjectMapperProfile : Profile
    {

        public ObjectMapperProfile()
        {
            CreateMap<CreateHistoryModel, History>()
                .ForMember(dest => dest.Messageid, opt => opt.MapFrom(src => src.messageid))
                .ForMember(dest => dest.ViewedDate, opt => opt.MapFrom(src => src.ViewedDate))
                .ForMember(dest => dest.Userid, opt => opt.MapFrom(src => src.userid))
                .ForMember(dest => dest.Productid, opt => opt.MapFrom(src => src.properties.productid));

            CreateMap<OrderModel, Orders>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<OrderItemModel, OrderItems>();

            CreateMap<ProductModel, Products>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.productId))
                 .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.categoryId));



        }

    }
}
