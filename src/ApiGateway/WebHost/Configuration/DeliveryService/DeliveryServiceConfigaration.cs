// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WebHost.Configuration.DeliveryService
{
    public static class DeliveryServiceConfigaration
    {
        public static IServiceCollection ConfigureOrderService(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            services.AddHttpClient("DeliveryService", client =>
            {
                client.BaseAddress = new Uri(configuration["DELIVERY_SERVICE_URI"] ??
                       throw new Exception("DELIVERY_SERVICE_URI is not specified in ENV"));
            });

            return services;
        }
    }
}
