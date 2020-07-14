using AutoMapper;
using Omf.OrderManagementService.DomainModel;
using Omf.OrderManagementService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Order, OrderModel>()
            //    .ForMember(dest => dest.OrderId, o => o.MapFrom(src => src.OrderId))
            //    .ForMember(dest => dest.OrderTime, o => o.MapFrom(src => src.OrderTime))
            //    .ForMember(dest => dest.RestaurantId, o => o.MapFrom(src => src.RestaurantId))
            //    .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status))
            //    .ForMember(dest => dest.TotalAmount, o => o.MapFrom(src => src.TotalAmount))
            //    .ForMember(dest => dest.UserId, o => o.MapFrom(src => src.UserId))
            //    .ForMember(dest => dest.OrderItems, o => o.MapFrom(src => src.OrderItems));

            //CreateMap<Menu, OrderItems>()
            //    .ForMember(dest => dest.Item, o => o.MapFrom(src => src.Item))
            //    .ForMember(dest => dest.MenuId, o => o.MapFrom(src => src.MenuId))
            //    .ForMember(dest => dest.Price, o => o.MapFrom(src => src.Price))
            //    .ForMember(dest => dest.Quantity, o => o.MapFrom(src => src.Quantity));

            CreateMap<Order, OrderModel>()
            .ForMember(d => d.OrderItems, opt => opt.Ignore());
            CreateMap<OrderModel, Order>()
            .ForMember(d => d.OrderItems, opt => opt.Ignore());
            //.AfterMap(AddOrUpdateOrderItems);
        }
    }
}
