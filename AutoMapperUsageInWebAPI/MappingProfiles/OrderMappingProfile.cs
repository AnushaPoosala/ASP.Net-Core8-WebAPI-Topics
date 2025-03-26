using AutoMapper;
using AutoMapperUsageInWebAPI.Models;
using AutoMapperUsageInWebAPI.Models.DTOs;

namespace AutoMapperUsageInWebAPI.MappingProfiles
{
    public class OrderMappingProfile  :Profile
    {
        public OrderMappingProfile() 
        {
            //Order.cs is source, OrderDTO(HTTP get call) is Destination
            CreateMap<Order, OrderDTO>()
                .ForMember(dest=>dest.CustomerName, opt=>opt.MapFrom(src=>src.Customer.FirstName +" "+src.Customer.LastName))
                .ForMember(dest=>dest.CustomerEmail, opt=>opt.MapFrom(src=>src.Customer.Email))
                .ForMember(dest=>dest.CustomerPhoneNumber,opt=>opt.MapFrom(src=>src.Customer.PhoneNumber))
                .ForMember(dest=>dest.Address, opt=>opt.MapFrom(src=>src.Customer.Address))
                .ForMember(dest=>dest.OrderItems, opt=>opt.MapFrom(src=>src.OrderItems));


            CreateMap<OrderItem, OrderItemDTO>()
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.ProductPrice));

            CreateMap<Address, AddressDTO>();

            CreateMap<CreateOrderItemDTO, OrderItem>();

            CreateMap<CreateOrderDTO, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));


        }


    }
}
                                                                                                       