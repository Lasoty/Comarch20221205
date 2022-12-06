namespace Restauracja.Common.Model.Repositories;

using Microsoft.Extensions.DependencyInjection;
public static class RegisterRepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));
        return services;
    }
}
