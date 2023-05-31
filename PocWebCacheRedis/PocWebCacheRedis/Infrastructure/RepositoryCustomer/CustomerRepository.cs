namespace PocWebCacheRedis.Infrastructure.RepositoryCustomer;

public class CustomerRepository : IRepository<Customer>
{

    private static Dictionary<Guid, Customer> InstanceCustomerClient()
    {
        var clientOne = new Customer(Guid.NewGuid(), "Admin", "Sys", "admin@email.com.br");
        var clientTwo = new Customer(Guid.NewGuid(), "Suport", "Sys", "suport@email.com.br");
        var clientThree = new Customer(Guid.NewGuid(), "Sale", "Sys", "sale@email.com.br");

        return new Dictionary<Guid, Customer>() { { clientOne.Id, clientOne }, { clientTwo.Id, clientTwo }, { clientThree.Id, clientThree } };
    }
    private static Dictionary<Guid, Customer> Customers = InstanceCustomerClient();

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await Task.Run(() => Customers.Values.ToList());
    }

    public async Task<Customer?> Get(Guid id)
    {
        return await Task.Run(() => Customers.GetValueOrDefault(id));
    }

    public async Task<Guid> Save(Customer customer)
    {
        await Task.Run(() => Customers.Add(customer.Id, customer));
        return customer.Id;
    }

    public async Task Edit(Customer customer)
    {
        await Task.Run(() =>
        {
            Customers.Remove(customer.Id);
            Customers.Add(customer.Id, customer);
        });
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() => Customers.Remove(id));
    }
}
