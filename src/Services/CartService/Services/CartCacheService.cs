using CartService.Dto;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;

namespace CartService.Services;

public class CartCacheService : ICartCacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger _logger;

    public CartCacheService(IDistributedCache cache, ILogger logger)
    {
        _cache = cache;
        _logger = logger.ForContext<CartCacheService>();
    }

    public async Task AddToCartAsync(string userId, CartItemDto item, CancellationToken ct)
    {
        try
        {
            _logger.Information("Adding item {ProductId} to cart for user {UserId}", item.ProductId, userId);

            var existingItems = (List<CartItemDto>)await GetCartAsync(userId, ct);
            var existingItem = existingItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                existingItems.Add(item);
            }

            var json = JsonConvert.SerializeObject(existingItems);
            await _cache.SetStringAsync(userId, json, ct);

            _logger.Information("Cart updated successfully for user {UserId}", userId);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error adding item to cart for user {UserId}", userId);
            throw;
        }
    }

    public async Task RemoveFromCartAsync(string userId, string productId, CancellationToken ct)
    {
        try
        {
            _logger.Information("Removing item {ProductId} from cart for user {UserId}", productId, userId);

            var existingItems = (List<CartItemDto>) await GetCartAsync(userId, ct);
            var itemToRemove = existingItems.FirstOrDefault(i => i.ProductId == productId);

            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    existingItems.Remove(itemToRemove);
                }

                var json = JsonConvert.SerializeObject(existingItems);
                await _cache.SetStringAsync(userId, json, ct);

                _logger.Information("Cart updated successfully for user {UserId}", userId);
            }
            else
            {
                _logger.Warning("Item {ProductId} not found in cart for user {UserId}", productId, userId);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error removing item from cart for user {UserId}", userId);
            throw;
        }
    }

    public async Task<IReadOnlyList<CartItemDto>> GetCartAsync(string userId, CancellationToken ct)
    {
        try
        {
            _logger.Information("Fetching cart for user {UserId}", userId);
            var cart = await _cache.GetStringAsync(userId, ct);
            if (string.IsNullOrEmpty(cart))
            {
                _logger.Information("Cart is empty for user {UserId}", userId);
                return new List<CartItemDto>();
            }

            var cartItems = JsonConvert.DeserializeObject<IReadOnlyList<CartItemDto>>(cart);
            _logger.Information("Cart fetched successfully for user {UserId}, items: {Count}", userId, cartItems.Count);
            return cartItems;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error fetching cart for user {UserId}", userId);
            return new List<CartItemDto>();
        }
    }
}
