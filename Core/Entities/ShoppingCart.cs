using System;

namespace Core.Entities;
// It is not Entity Framework, it is for Redis
public class ShoppingCart
{
    public required string Id { get; set; }
    public List<CartItem> Items { get; set; } = [];

}
