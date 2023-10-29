using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Common;

public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}