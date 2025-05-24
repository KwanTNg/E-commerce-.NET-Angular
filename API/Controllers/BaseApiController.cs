using System;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//Move Pagination functionality to here so that it is generic
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
    ISpecification<T> spec, int PageIndex, int PageSize) where T : BaseEntity
    {
        //Get list of products
        var items = await repo.ListAsync(spec);
        //Get number of product
        var count = await repo.CountAsync(spec);
        var pagination = new Pagination<T>(PageIndex, PageSize, count, items);
        return Ok(pagination);
    }
}
