using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

//We will use our respository instead of store context here
//Use GenericRepository instead

public class ProductsController(IUnitOfWork unit) : BaseApiController
{
    //Need to specify FromQuery as default is FromBody
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecsParams specsParams)
    {
        //Add argus to constructor of ProductSpecification, which is designed for generic repository
        var spec = new ProductSpecification(specsParams);
       

        return await CreatePagedResult(unit.Repository<Product>(), spec, specsParams.PageIndex, specsParams.PageSize);
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);
        // var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        unit.Repository<Product>().Add(product);
        if (await unit.Complete())
        {
            //Call GetProduct(int id) method, need to provide id
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        //context.Products.Add(product);
        //await context.SaveChangesAsync();
        //return product;
        return BadRequest("Problem creating product");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");
        unit.Repository<Product>().Update(product);
        // Check if it is modified
        //context.Entry(product).State = EntityState.Modified;
        //await context.SaveChangesAsync();
        //return NoContent();
        if (await unit.Complete())
        {
            return NoContent();
        }
        return BadRequest("Problem updating the product");
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);
        //var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        unit.Repository<Product>().Remove(product);
        //context.Products.Remove(product);
        //await context.SaveChangesAsync();
        //return NoContent();
        if (await unit.Complete())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification(); 
        return Ok(await unit.Repository<Product>().ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification(); 
        return Ok(await unit.Repository<Product>().ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return unit.Repository<Product>().Exists(id);
        //return context.Products.Any(x => x.Id == id);
    }



}
