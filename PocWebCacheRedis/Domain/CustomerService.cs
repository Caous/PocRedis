using PocWebCacheRedis.Infrastructure.Publish;

namespace PocWebCacheRedis.Domain;
public class CustomerService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    private readonly ICachingService _cache;
    private readonly IConfiguration _configuration;
    private string _cacheKey = "customer_";

    public CustomerService(IRepository<Customer> customerRepository, IMapper mapper, ICachingService cache, IConfiguration configuration)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _cache = cache;
        _configuration = configuration;
    }

    public async Task<CustomerDto> RegisterCustomer(CustomerDto customer)
    {
        var customerSave = await _customerRepository.Save(_mapper.Map<Customer>(customer));
        await _cache.SetAsync(_cacheKey + customerSave, JsonSerializer.Serialize(customer));
        new PublishCustomer(_configuration).PublishChannel(_mapper.Map<Customer>(customer));
        return customer;
    }

    public async Task<CustomerDto?> GetCustomerDto(CustomerDto customer)
    {
        var customerCache = await _cache.GetAsync(_cacheKey + customer.Id);

        if (customerCache != null)
            return JsonSerializer.Deserialize<CustomerDto>(customerCache);
        else
        {
            var result = await _customerRepository.Get(customer.Id);
            if (result != null)
            {
                await _cache.SetAsync(_cacheKey + result.Id, JsonSerializer.Serialize(customer));
                return _mapper.Map<CustomerDto>(result);
            }
        }
        return null;
    }

    public async Task<List<CustomerDto>?> GetAllCustomers()
    {
        var customerCache = await _cache.GetAsync(_cacheKey + "all");

        if (customerCache != null)
            return JsonSerializer.Deserialize<List<CustomerDto>>(customerCache);

        var result = await _customerRepository.GetAll();

        if (result != null)
        {
            await _cache.SetAsync(_cacheKey + "all", JsonSerializer.Serialize(result));
            return _mapper.Map<List<CustomerDto>>(result.ToList());
        }

        return null;
    }

    public async Task EditCustomer(CustomerDto customer) => await _customerRepository.Edit(_mapper.Map<Customer>(customer));

    public async Task DeleteCustomer(CustomerDto customer) => await _customerRepository.Delete(customer.Id);
}
