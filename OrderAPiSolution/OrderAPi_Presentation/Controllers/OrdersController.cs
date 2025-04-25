using AutoMapper;
using E_Commerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPi_Application.DTOs;
using OrderAPi_Application.Interfaces;
using OrderAPi_Application.Services;
using OrderAPi_Domain.Entites;

namespace OrderAPi_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrder order_Repo , IOrderService orderService , IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await order_Repo.GetAllAsync();
            if(!orders.Any())
            {
                return NotFound("No Order was found");
            }
          var OrdersDTO = mapper.Map< List <OrderDTO>>(orders);
            
            return Ok(OrdersDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await order_Repo.FindByIdAsync(id);
            if (order == null)
                return NotFound("order not found");
            var orderDTO = mapper.Map<OrderDTO>(order);
            return Ok(orderDTO);
        }
        [HttpGet("client/{clientId:int}")]
        public async Task<ActionResult <OrderDTO>> GetClientOrders(int clientId)
        {
            if (clientId <= 0) return NotFound("Invalid data provided");
            var orders = await orderService.GetOrderByClientId(clientId);
            return !orders.Any()? NotFound() : Ok(mapper.Map<List<OrderDTO>>(orders));
        }

        [HttpGet("details/{orderId:int}")]
        public async Task<ActionResult<OrderDetailsDTO>>GetOrderDetails(int orderId)
        {
            if(orderId <= 0) return NotFound("Invalid data provided");
            var orderDetails = await orderService.GetOrderDetails(orderId);
            return orderDetails.OrderId > 0 ? Ok(orderDetails) : NotFound("the Order not found");


        }

        [HttpPost]
        public async Task <ActionResult<Response>> CreateOrder(OrderDTO orderDTO)
        {
             
            if(!ModelState.IsValid)
            {
                return BadRequest("Incomplete data submitted");
            }

            // convert to entity
            var entity = mapper.Map<Order>(orderDTO);
            var response = await order_Repo.CreateAsync(entity);
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateOrder(OrderDTO orderDTO)
        {
            // convert from dto to entity
            var order = mapper.Map<Order>(orderDTO);
            var response = await order_Repo.UpdateAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task <ActionResult<Response>> DeleteOrder(OrderDTO orderDTO)
        {
            var order = mapper.Map<Order>(orderDTO);
            var response = await order_Repo.DeleteAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);

        }
        
    }
}
