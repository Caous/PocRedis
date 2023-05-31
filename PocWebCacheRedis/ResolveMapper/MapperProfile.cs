namespace PocWebCacheRedis.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
    }
}
