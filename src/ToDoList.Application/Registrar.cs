using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Applications.Handlers.Notes.Commands.CompleteNote;
using ToDoList.Application.Applications.Services;
using ToDoList.Services.Abstract;

namespace ToDoList.Application;

public static class Registrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<INoteService, NoteService>();
        
        return services;
    }
}