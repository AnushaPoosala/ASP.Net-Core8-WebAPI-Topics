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
    public class ProductWithOutAutoMapperController : ControllerBase
    {
        private readonly ProductDbContext _productDbContext;
        public ProductWithOutAutoMapperController(ProductDbContext context)
        {
            _productDbContext = context;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable>> GetProductsAync()
        {
            var productDTOs =await _productDbContext.Products.AsNoTracking().Select(p => new ProductDTO
            {
                Id= p.Id,
                ProductName = p.Name,
                ProductPrice = p.Price,
                SerialNumber = p.SerialNumber,
                IsActive = p.IsActive,
                ProductDescription = p.Description,
                CategoryId = p.CategoryId,
                Brand=p.Brand,
                CreatedDateTime = p.CreatedDateTime,

            }).ToListAsync();

            return Ok(productDTOs);
                
        }

       [HttpGet("GetProductByIdAsync/{id}")]
        public async Task<ActionResult<IEnumerable>> GetProductByIdAsync(int id)
        {
            var product = await _productDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            
            if(product==null)
                return NotFound();

            var productDTO = new ProductDTO {
                Id = product.Id,
                ProductName = product.Name,
                ProductPrice = product.Price,
                SerialNumber = product.SerialNumber,
                IsActive = product.IsActive,
                ProductDescription = product.Description,
                CategoryId = product.CategoryId,
                Brand = product.Brand,
                CreatedDateTime = product.CreatedDateTime,
            };

            return Ok(productDTO);

        }

        [HttpPost("CreateProduct")]
        public   async Task<ActionResult<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product {
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                Description = createProductDTO.Description,
                CategoryId = createProductDTO.CategoryId,
                Brand = createProductDTO.Brand,
                CreatedDateTime = DateTime.UtcNow,
                IsActive = createProductDTO.StockQuantity> 0,
                StockQuantity = createProductDTO.StockQuantity,
                SupplierName    = createProductDTO.SupplierName,
                SupplierPrice = createProductDTO.SupplierPrice,
                SerialNumber= "test"

            };

            _productDbContext.Products.Add(product);
           await _productDbContext.SaveChangesAsync();

            product.SerialNumber = CreateSerialNumber(product);

             await _productDbContext.SaveChangesAsync(true);

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductPrice = product.Price,
                Brand= product.Brand,
                SerialNumber = product.SerialNumber,
                CategoryId= product.CategoryId,
                CreatedDateTime= DateTime.UtcNow,
                IsActive = product.IsActive,


            };

            return  productDTO;
        }

        private string CreateSerialNumber(Product product)
        {
            return $"{product.CategoryId}-{product.Brand}-{product.Name}-{DateTime.UtcNow.Year}-{product.Id}"; ;
        }
    }                                          
}
