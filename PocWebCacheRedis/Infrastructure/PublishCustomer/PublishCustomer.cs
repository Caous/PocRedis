using StackExchange.Redis;

namespace PocWebCacheRedis.Infrastructure.Publish;
public class PublishCustomer
{
    private readonly IConfiguration _configuration;
    private static ConnectionMultiplexer _connection;
    public PublishCustomer(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"] ?? string.Empty);
    }

    public async void PublishChannel(Customer customer) =>  await _connection.GetSubscriber().PublishAsync(_configuration["Redis:Chanel"] ?? string.Empty, $"Cliente cadastro com sucesso {customer.Name}", CommandFlags.HighPriority);

}
