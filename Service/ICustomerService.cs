using CrudWithRouteApiConvention.Model;

namespace CrudWithRouteApiConvention.Service;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> GetById(int id);
    Task<IResult> Create(Customer customer);
    Task<IResult> Update(Customer customer);
    Task<IResult> Delete(int id);
}