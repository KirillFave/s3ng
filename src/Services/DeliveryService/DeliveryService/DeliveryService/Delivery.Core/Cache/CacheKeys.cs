namespace DeliveryService.Delivery.Core.Cache
{
    public static class CacheKeys
    {
        public static string DeliveryKey(Guid _deliveryId) => $"Delivery_{_deliveryId}";
    }
}
