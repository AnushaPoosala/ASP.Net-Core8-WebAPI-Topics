using AutoMapper;
using AutoMapperUsageInWebAPI.Data;
using AutoMapperUsageInWebAPI.Models;
using AutoMapperUsageInWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace AutoMapperUsageInWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithAutoMapperController : ControllerBase
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IMapper _mapper;
        public ProductWithAutoMapperController(ProductDbContext context, IMapper mapper)
        {
            _productDbContext = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable>> GetProductsAync()
        {
            var products =await _productDbContext.Products.AsNoTracking().ToListAsync();

            //AutoMapper automatically maps the Product table oblect products(source) to the destination(ProductDTO)
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
                
        }

       [HttpGet("GetProductByIdAsync/{id}")]
        public async Task<ActionResult<IEnumerable>> GetProductByIdAsync(int id)
        {
            var product = await _productDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            
            if(product==null)
                return NotFound();

            var productDTO = _mapper.Map<ProductDTO>(product);

            return Ok(productDTO);

        }

        [HttpPost("CreateProduct")]
        public   async Task<ActionResult<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(createProductDTO);   

            product.SerialNumber= "Test";
            product.IsActive = createProductDTO.StockQuantity > 0;
            product.CreatedDateTime= DateTime.UtcNow;


            _productDbContext.Products.Add(product);
           await _productDbContext.SaveChangesAsync();

            product.SerialNumber = CreateSerialNumber(product);

             await _productDbContext.SaveChangesAsync(true);

            var productDTO = _mapper.Map<ProductDTO>(product);

            return  productDTO;
        }

        private string CreateSerialNumber(Product product)
        {
            return $"{product.CategoryId}-{product.Brand}-{product.Name}-{DateTime.UtcNow.Year}-{product.Id}"; ;
        }
    }                                          
}
