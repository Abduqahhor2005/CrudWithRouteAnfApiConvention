using CrudWithRouteApiConvention.Model;
using CrudWithRouteApiConvention.Service;
using Microsoft.AspNetCore.Mvc;

namespace CrudWithRouteApiConvention.Controller;
[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> GetCategories(ICustomerService customerService)
    {
        return Results.Ok(await customerService.GetAll());
    }
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> GetCategoryById(ICustomerService customerService,[FromRoute] int id)
    {
        Customer? customer = await customerService.GetById(id);
        if(customer==null) return Results.NotFound(new { message = "Customer not found" });
        return Results.Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> CreateCustomer(ICustomerService customerService, [FromBody] Customer? customer)
    {
        if(customer==null) return Results.NotFound(new { message = "Customer not created" });
        return await customerService.Create(customer);
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> UpdateCustomer(ICustomerService customerService, [FromBody] Customer? customer)
    {
        if(customer==null) return Results.NotFound(new { message = "Customer not updated" });
        return await customerService.Update(customer);
    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> DeleteCategory(ICustomerService customerService,[FromRoute] int id)
    {
        if(id<0) return Results.NotFound(new { message = "Customer not deleted" });
        return await customerService.Delete(id);
    }
}