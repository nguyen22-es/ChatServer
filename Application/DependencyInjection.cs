
using System.Reflection;
using ChatServer.Application.EventHandlers;
using Mapster;
using MapsterMapper;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton(GetConfiguredMappingConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddAutoMapper(typeof(DependencyInjection));
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddTransient<MessagesRoomCreatedEventHandler>();
        return services;
    }


    private static TypeAdapterConfig GetConfiguredMappingConfig()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        IList<IRegister> registers = config.Scan(Assembly.GetExecutingAssembly());

        config.Apply(registers);

        return config;
    }

}
