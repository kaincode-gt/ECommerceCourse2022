using ECommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Business.Repository;

public interface IOrderRepository
{
    Task<OrderDTO> Get(int id);
    Task<IEnumerable<OrderDTO>> GetAll(string? userId= null, string? status=null);

    Task<OrderDTO> Create(OrderDTO orderDTO);
    Task<int> Delete(int id);

    Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO);
    Task<OrderHeaderDTO> MarkPaymentSuccessful(int id);
    Task<bool> UpdateOrderStatus(int orderId, string status);

}
