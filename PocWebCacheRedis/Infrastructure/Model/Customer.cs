namespace PocWebCacheRedis.Infrastructure.Model;

public class Customer
{
    public Customer(Guid id, string name, string lastName, string email)
    {
        if (id == null || id.Equals(Guid.Empty))
            this.Id = Guid.NewGuid();
        else
            this.Id = id;

        this.LastName = lastName;
        this.Email = email;
        this.Name = name;
        this.DateRegister = DateTime.Now;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime DateRegister { get; private set; }
}
