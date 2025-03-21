using AutoMapper;

namespace AutoMapperUsageInWebAPI.MappingProfiles
{
    public class ProductMappingProfile  : Profile
    {
        public ProductMappingProfile()
        {  

            //CreateMap<source,Destination>()

            //When we fetch Data from DB(Product) to ProdctDTO
            CreateMap<Models.Product, Models.DTOs.ProductDTO>()

                .ForMember(//Mapping logic: take name from Product, map it with ProductName in ProductDTO
                destinationMember=> destinationMember.ProductName,
                memberOptions=> memberOptions.MapFrom(sourceMember=>sourceMember.Name)


                ).ForMember(//Mapping logic: take Description from Product, map it with ProductDescription in ProductDTO
                destinationMember => destinationMember.ProductDescription,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Description)
                );

            
            //This mapping is used when a new product is being created from the data provided by admin or any api user
            //When we insert data from UI(CreateProductDTO) into DB(Product)
            CreateMap<Models.DTOs.CreateProductDTO, Models.Product>();
        }
    }
}
