using AutoMapper;
using AutoMapperUsageInWebAPI.Data;
using AutoMapperUsageInWebAPI.Models;
using AutoMapperUsageInWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperUsageInWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EComOrderController : ControllerBase
    {
        private readonly EComDBContext _eComDBContext;
        private readonly IMapper _mapper;

        public EComOrderController(EComDBContext eComDBContext, IMapper mapper)
        {
            _eComDBContext = eComDBContext;
            _mapper = mapper;
        }

        [HttpGet("{OrderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int OrderId)
        {
            var order =await _eComDBContext.Orders
            .Include(o => o.Customer)
            .ThenInclude(o => o.Address)
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Product)
            .FirstOrDefaultAsync(o => o.OrderId==OrderId);

            if(order == null) 
            {
                string message = $"The given OrderId{OrderId} is not found";
                return NotFound(message);
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);

            return Ok(orderDTO);
        }


        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            if (createOrderDTO == null)
            {
                return BadRequest("Order data is required");
            }

            try
            {
                //Check if the Customer exists
                var IsCustomerExists = await _eComDBContext.MyCustomers.AnyAsync(c => c.CustomerId == createOrderDTO.CustomerId);

                //If the Customer does not exist, return a 404 Not Found response
                if (!IsCustomerExists)
                {
                    return NotFound($"Customer with Id {createOrderDTO.CustomerId} not found");
                }

                //Check if the Products exist
                var productIds = createOrderDTO.OrderItems.Select(oi => oi.ProductId).ToList();

                //Get the Products from the database
                var products = await _eComDBContext.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

                //If the Products do not exist, return a 404 Not Found response
                if (products.Count != productIds.Count)
                {
                    var missingProductIds = productIds.Except(products.Select(p => p.Id)).ToList();
                    return NotFound($"Products with Ids {string.Join(",", missingProductIds)} not found");
                }

                // Map the CreateOrderItemDTO to Order entity
                var order = _mapper.Map<Order>(createOrderDTO);

                //Calculate the Order Amount
                decimal orderAmount = 0;
                foreach (var orderItem in order.OrderItems)
                {
                    var product = products.FirstOrDefault(p => p.Id == orderItem.ProductId);
                    orderItem.ProductPrice = product.Price;
                    orderAmount += orderItem.Quantity * product.Price;
                }
                order.Amount = orderAmount;//Set the Order Amount after calculating the total amount

                //Add the Order to the Orders DbSet
                _eComDBContext.Orders.Add(order);
                await _eComDBContext.SaveChangesAsync();


                //Fetch the created Order from the database
                var createdOrder = await _eComDBContext.Orders
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Address)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

                if (createdOrder == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while creating the Order");
                }

                //Mapping the Order entity to the OrderDTO for Data Transfer purposes
                var orderDTO = _mapper.Map<OrderDTO>(createdOrder);

                return Ok(orderDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //HomeWork:cutomer id input, order output

        //Maps from Primitive to Complex  (PrimitiveAddressDTO->Address)

        [HttpPost("CreateAddress")]
        public async Task<ActionResult<AddressDTO>> CreateAddress([FromBody] PrimitiveAddressDTO primitiveAddressDTO)
        {
            if (primitiveAddressDTO == null)
            {
                return BadRequest("Address data is required");
            }

            try
            {
                //Map the PrimitiveAddressDTO to Address entity
                var address = _mapper.Map<Address>(primitiveAddressDTO);

                //Add the Address to the Addresses DbSet
                _eComDBContext.Address.Add(address);
                await _eComDBContext.SaveChangesAsync();

                //Mapping the Address entity to the AddressDTO for Data Transfer purposes
                var addressDTO = _mapper.Map<AddressDTO>(address);

                return Ok(addressDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
