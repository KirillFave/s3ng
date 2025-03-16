using CartService.Dto;
using CartService.Services;
using Microsoft.AspNetCore.SignalR;
using ILogger = Serilog.ILogger;

public class CartHub : Hub
{
    private readonly ICartCacheService _cartCacheService;
    private readonly ILogger _logger;

    public CartHub(ICartCacheService cartService, ILogger logger)
    {
        _cartCacheService = cartService;
        _logger = logger.ForContext<CartHub>();
    }

    public async Task AddToCart(string userId, CartItemDto item, CancellationToken ct = default)
    {
        _logger.Information("Adding item {ProductId} to cart for user {UserId}", item.ProductId, userId);

        await _cartCacheService.AddToCartAsync(userId, item, ct);
        var cart = await _cartCacheService.GetCartAsync(userId, ct);

        _logger.Information("Cart updated for user {UserId}", userId);
        await Clients.All.SendAsync("ReceiveCartUpdate", userId, cart);
    }

    public async Task RemoveFromCart(string userId, string productId, CancellationToken ct = default)
    {
        _logger.Information("Removing item {ProductId} from cart for user {UserId}", productId, userId);

        await _cartCacheService.RemoveFromCartAsync(userId, productId, ct);
        var cart = await _cartCacheService.GetCartAsync(userId, ct);

        _logger.Information("Cart updated for user {UserId}", userId);
        await Clients.All.SendAsync("ReceiveCartUpdate", userId, cart);
    }

    public async Task GetCart(string userId, CancellationToken ct = default)
    {
        _logger.Information("Fetching cart for user {UserId}", userId);

        var cart = await _cartCacheService.GetCartAsync(userId, ct);
        _logger.Information("Cart fetched successfully for user {UserId}", userId);
        await Clients.Caller.SendAsync("ReceiveCartUpdate", userId, cart);
    }
}
