using Microsoft.Extensions.DependencyInjection;

namespace ORM.Contract.Interfaces;

public interface IModuleInitializer
{
    void Initialize(IServiceCollection services);
}