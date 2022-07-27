using EcommerceCourse2022_client.ViewModels;

namespace EcommerceCourse2022_client.Service;

public interface ICartService
{
    event Action OnChange;
    Task DecrementCart(ShoppingCart shoppingCart);
    Task IncrementCart(ShoppingCart shoppingCart);
}
