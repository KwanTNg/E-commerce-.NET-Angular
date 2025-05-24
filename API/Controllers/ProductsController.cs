using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//We will use our respository instead of store context here
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        // becasue it will return a list, need to use Ok()
        return Ok(await repo.GetProductsAsync(brand, type, sort));
        // return await context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        // var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);
        if (await repo.SaveChangesAsync())
        {
            //Call GetProduct(int id) method, need to provide id
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        //context.Products.Add(product);
        //await context.SaveChangesAsync();
        //return product;
        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");
        repo.UpdateProduct(product);
        // Check if it is modified
        //context.Entry(product).State = EntityState.Modified;
        //await context.SaveChangesAsync();
        //return NoContent();
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        //var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        repo.DeleteProduct(product);
        //context.Products.Remove(product);
        //await context.SaveChangesAsync();
        //return NoContent();
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypesAsync());
    }

    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
        //return context.Products.Any(x => x.Id == id);
    }



}
