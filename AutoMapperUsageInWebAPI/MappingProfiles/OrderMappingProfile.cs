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
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer.Address))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));


            CreateMap<OrderItem, OrderItemDTO>()
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.ProductPrice));

            CreateMap<Address, AddressDTO>();



            CreateMap<CreateOrderDTO, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));


            //Complex--->Primitive(Address(src)-->PrimitiveAddressDTO(dest))
            //Map the address to seperate primitive props in the AddressDTO
            ////Mapping the Address(Address containd Navigation Property for Customer, so we
            /////are mapping customer class also) to PrimitiveAddressDTO(with Primitive types)
            CreateMap<Models.Address, Models.DTOs.PrimitiveAddressDTO>()
               .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.CustomerId))
                //Check condition before reading the source value
                .ForMember(dest => dest.PhoneNumber, opt =>
                {
                    opt.PreCondition(src => src.Customer != null);
                    opt.MapFrom(src => src.Customer.PhoneNumber);
                })
              // .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName));



            //Primitive-->complex

            CreateMap<PrimitiveAddressDTO, Address>()
    .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new Customer
    {
        CustomerId = src.CustomerId,
        PhoneNumber = src.PhoneNumber,
        Email = src.Email,
        LastName = src.LastName,
        FirstName = src.FirstName
    }));


            //REverse Mapping  in Auto Mapper
            CreateMap<CreateOrderItemDTO, OrderItem>().ReverseMap();


        }
    }
}
                                                                                                       