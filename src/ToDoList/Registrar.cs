using Microsoft.EntityFrameworkCore;
using ToDoList.Application;
using ToDoList.Infrastructure;

namespace ToDoList;

public static class Registrar
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddDatabase(configuration);
        
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>(sp =>
            new ApplicationDbContext(configuration.GetConnectionString("Default")!));
        
        return services;
    }
    
    public static async Task AddMigrations(this IApplicationBuilder app, CancellationToken cancellationToken = default)
    {
        var scope = app.ApplicationServices.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync(cancellationToken);
    }
}