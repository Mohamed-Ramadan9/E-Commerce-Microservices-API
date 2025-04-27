using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OrderAPi_Application.DTOs;
using OrderAPi_Application.Interfaces;
using Polly;
using Polly.Registry;

namespace OrderAPi_Application.Services
{
    public class OrderService(IOrder orderInterface,HttpClient http ,IMapper mapper , ResiliencePipelineProvider<string> resilience) : IOrderService
    {
        public async Task<ProductDTO> GetProduct(int productid)
        {
            // Call Product Api using HttpClient
            // Redirect this call to the Api Gateway since product Api is not response to outsiders
            var getProduct = await http.GetAsync($"/api/products/{productid}");
            if (!getProduct.IsSuccessStatusCode)
                return null;
            var  product = await getProduct.Content.ReadFromJsonAsync<ProductDTO>();
            return product!;


        }

        // GET USER
        public async Task<AppUserDTO>GetUser(int userId)
        {
            // Call Product Api using HttpClient
            // Redirect this call to the Api Gateway since product Api is not response to outsiders
            var getUser = await http.GetAsync($"/api/authentication/{userId}");
            if (!getUser.IsSuccessStatusCode)
                return null;
            var user = await getUser.Content.ReadFromJsonAsync<AppUserDTO>();
            return user!;

        }
        //GET ORDERS BY CLIENT ID
        public async Task<IEnumerable<OrderDTO>> GetOrderByClientId(int clientId)
        {
            // Get all Client 's orders
            var orders = await orderInterface.GetOrdersAsync(o => o.ClientId == clientId);
           if (!orders.Any())
                return null!;

            // Convert from entity to DTO
            var orderDTO = mapper.Map<List<OrderDTO>>(orders);
            return orderDTO;

        }
        // GET ORDER DETAILS BY ID
        public async Task<OrderDetailsDTO> GetOrderDetails(int orderId)
        {
            //Prepare Order
            var order = await orderInterface.FindByIdAsync(orderId);
            if (order == null || order!.Id <= 0) return null;

            //Get Retry Pipline
            var retryPipline = resilience.GetPipeline("my-retry-pipeline");

            // Prepare Product
            var productDTO = await retryPipline.ExecuteAsync(async token => await GetProduct(order.ProductId));

            // Prepare Client
            var appUserDTO = await retryPipline.ExecuteAsync(async token => await GetUser(order.ClientId));

            //Populate order Details
            return new OrderDetailsDTO(orderId, productDTO.Id, appUserDTO.Id , appUserDTO.Name , appUserDTO.Email , appUserDTO.Address , appUserDTO.TelephoneNumber , productDTO.Name! , order.PurchaseQuantity , productDTO.Price , productDTO.Quantity * order.PurchaseQuantity , order.OrderedDate ) ;
        }
    }
}
