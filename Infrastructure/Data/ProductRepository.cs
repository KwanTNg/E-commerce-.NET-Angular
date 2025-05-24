using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        //project a list of brand
        return await context.Products.Select(x => x.Brand)
        .Distinct()
        .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    //Filter by brand or type
    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(x => x.Brand == brand);
        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(x => x.Type == type);
        //adds an ORDER BY clause to the same query. Again, nothing is executed yet.
        query = sort switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            //Default
            _ => query.OrderBy(x => x.Name)
        };
        return await query.ToListAsync();
        //return await context.Products.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x => x.Type)
        .Distinct()
        .ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        //Check if it is return greater than 0
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
