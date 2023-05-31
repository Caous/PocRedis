namespace PocWebCacheRedis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private CustomerService _customerService;

    public CustomerController(IRepository<Customer> repositoryCustomer, IMapper mapper, ICachingService cache, IConfiguration configuration)
    {
        _customerService = new CustomerService(repositoryCustomer, mapper, cache, configuration);        
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _customerService.GetAllCustomers());
    }

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _customerService.GetCustomerDto(new CustomerDto() { Id = id }));
    }

    // POST api/<ValuesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerDto customer)
    {
        return Ok(await _customerService.RegisterCustomer(customer));
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] CustomerDto value)
    {
        await _customerService.EditCustomer(value);
        return Ok("Edited");
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _customerService.DeleteCustomer(new CustomerDto() { Id = id });
        return Ok("Deleted");
    }
}
