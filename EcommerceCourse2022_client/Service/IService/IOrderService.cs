using ECommerce_Models;

namespace EcommerceCourse2022_client.Service;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetAll(string? userId);
    Task<OrderDTO> Get(int orderId);
}
