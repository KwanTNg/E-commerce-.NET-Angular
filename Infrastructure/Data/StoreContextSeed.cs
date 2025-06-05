using System;
using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    //Can use this method without creating new instance
    public static async Task SeedAsync(StoreContext context, UserManager<AppUser> userManager)
    {
        //check if that email/username was already in use
        if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
        {
            var user = new AppUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
            //assing to admin role
            await userManager.AddToRoleAsync(user, "Admin");
        }

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Products.Any())
        {
            var productsData = await File
            .ReadAllTextAsync(path + @"/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products == null) return;
            context.Products.AddRange(products);
            await context.SaveChangesAsync();

        }

        if (!context.DeliveryMethods.Any())
        {
            var dmData = await File.ReadAllTextAsync(path + @"/Data/SeedData/delivery.json");
            var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
            if (methods == null) return;
            context.DeliveryMethods.AddRange(methods);
            await context.SaveChangesAsync();

        }

    }

}
