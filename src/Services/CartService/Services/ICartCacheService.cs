using CartService.Dto;

namespace CartService.Services;

public interface ICartCacheService
{
    Task AddToCartAsync(string userId, CartItemDto item, CancellationToken ct);
    Task RemoveFromCartAsync(string userId, string productId, CancellationToken ct);
    Task<List<CartItemDto>> GetCartAsync(string userId, CancellationToken ct);
}
