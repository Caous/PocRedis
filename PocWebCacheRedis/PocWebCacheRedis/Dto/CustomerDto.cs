namespace PocWebCacheRedis.Dto;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public override string ToString()
    {
        return $"Name of customer: {this.Name + this.LastName} and e-mail: {this.Email} ";
    }
}
