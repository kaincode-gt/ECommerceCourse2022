using Blazored.LocalStorage;
using ECommerce_Common;
using EcommerceCourse2022_client.ViewModels;
namespace EcommerceCourse2022_client.Service;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;

    public CartService (ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public async Task DecrementCart(ShoppingCart cartToDelete)
    {
        var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);
        bool itemInCart = false;

        for(int i = 0; i < cart.Count; i++)
        {
            if (cart[i].ProductId == cartToDelete.ProductId && cart[i].ProductPriceId == cartToDelete.ProductPriceId)
            {
                if (cart[i].Count ==0 || cart[i].Count ==1)
                {
                    cart.Remove(cart[i]);
                }
                else
                {
                    cart[i].Count -= cartToDelete.Count;
                }
            }
        }
        await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
    }

    public async Task IncrementCart(ShoppingCart cartToAdd)
    {
        var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);
        bool itemInCart = false;

        if(cart==null)
        {
            cart= new List<ShoppingCart>();
        }
        foreach (var obj in cart)
        {
            if (obj.ProductId==cartToAdd.ProductId && obj.ProductPriceId==cartToAdd.ProductPriceId)
            {
                itemInCart = true;
                obj.Count += cartToAdd.Count;
            }
        }
        if (!itemInCart)
        {
            cart.Add(new ShoppingCart()
            {
                ProductId= cartToAdd.ProductId,
                ProductPriceId= cartToAdd.ProductPriceId,
                Count= cartToAdd.Count
            });
        }
        await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
    }
}
