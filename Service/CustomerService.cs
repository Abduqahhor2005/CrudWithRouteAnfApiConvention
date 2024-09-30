using CrudWithRouteApiConvention.Model;
using Dapper;
using Npgsql;

namespace CrudWithRouteApiConvention.Service;

public class CustomerService : ICustomerService
{
    public async Task<IEnumerable<Customer>> GetAll()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<Customer>(SqlCommands.GetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Customer?> GetById(int id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Customer>(SqlCommands.GetById,new{Id=id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IResult> Create(Customer customer)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Create,customer)>0? 
                    Results.Ok(new {message="created"}):Results.NotFound(new {message="not created"});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IResult> Update(Customer customer)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Update,customer)>0? 
                    Results.Ok(new {message="updated"}):Results.NotFound(new {message="not updated"});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IResult> Delete(int id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Delete,new{Id=id})>0? 
                    Results.Ok(new {message="deleted"}):Results.NotFound(new {message="not deleted"});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}


file class SqlCommands
{
    public const string ConnectionString = @"Host=localhost;Database=test;User Id=postgres;Port=4321;Password=salom;";
    public const string GetAll = @"select * from customers";
    public const string GetById = @"select * from customers where id=@id";
    public const string Create = @"insert into customers(Id,FullName,Email,Phone) values(@Id,@FullName,@Email,@Phone)";
    public const string Update = @"update customers set FullName = @FullName, Email = @Email, Phone = @Phone where id=@id";
    public const string Delete = @"delete from customers where id=@id";
}