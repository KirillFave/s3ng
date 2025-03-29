using CartService.Dto;

namespace CartService.Services;

public interface ICartCacheService
{
    Task AddToCartAsync(string userId, CartItemDto item, CancellationToken ct);
    Task RemoveFromCartAsync(string userId, string productId, CancellationToken ct);
    Task<IReadOnlyList<CartItemDto>> GetCartAsync(string userId, CancellationToken ct);
    Task ClearCartAsync(string userId, CancellationToken ct);
}
