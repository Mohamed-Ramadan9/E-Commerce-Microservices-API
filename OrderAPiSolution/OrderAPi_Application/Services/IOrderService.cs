using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderAPi_Application.DTOs;

namespace OrderAPi_Application.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrderByClientId(int clientId);

        Task<OrderDetailsDTO> GetOrderDetails(int orderId);
    }
}
