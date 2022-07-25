using ECommerce_Models;

namespace EcommerceCourse2022_client.Service;

public interface IProductService
{
    public Task<IEnumerable<ProductDTO>> GetAll();
    public Task<ProductDTO> Get(int productId);
}
