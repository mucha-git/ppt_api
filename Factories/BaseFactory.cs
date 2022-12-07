using AutoMapper;

namespace WebApi.Factories;

public abstract class BaseFactory
{
    public readonly IMapper _mapper;

    public BaseFactory(
        IMapper mapper
    )
    {
        _mapper = mapper;
    }
}