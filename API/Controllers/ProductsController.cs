using System;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//We will use our respository instead of store context here
//Use GenericRepository instead
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        //Add argus to constructor of ProductSpecification, which is designed for generic repository
        var spec = new ProductSpecification(brand, type);
        var products = await repo.ListAsync(spec);
        return Ok(products);
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        // var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if (await repo.SaveAllAsync())
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
        repo.Update(product);
        // Check if it is modified
        //context.Entry(product).State = EntityState.Modified;
        //await context.SaveChangesAsync();
        //return NoContent();
        if (await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        //var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        repo.Remove(product);
        //context.Products.Remove(product);
        //await context.SaveChangesAsync();
        //return NoContent();
        if (await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        //TODO: Implement method later as current generic repository does not support
        return Ok();
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        //TODO: Implement method later as current generic repository does not support
        return Ok();
    }

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
        //return context.Products.Any(x => x.Id == id);
    }



}
