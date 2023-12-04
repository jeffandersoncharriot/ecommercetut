using AutoMapper;
using Ecommerce.Api.Orders.Db;

namespace Ecommerce.Api.Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, Models.Order>();
            CreateMap<OrderItem, Models.OrderItem>();
        }
    }
}