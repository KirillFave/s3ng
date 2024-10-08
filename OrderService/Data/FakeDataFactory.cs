using OrderService.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Data;

public static class FakeDataFactory
{
    public static List<Order> Orders =>
    [
        new Order()
        {
            // TODO
        },
        new Order()
        {
            // TODO
        },
    ];

    public static List<OrderItem> OrderItems => [];
}