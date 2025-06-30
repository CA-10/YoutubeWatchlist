using YoutubeWatchlist.Library.Services;
using YoutubeWatchlist.ViewModels;

namespace YoutubeWatchlist.Blazor;

public static class DependencyContainer
{
	public static IServiceCollection AddDependencies(this IServiceCollection services)
	{
		//Services
		services.AddSingleton<IDatabaseService, SQLiteService>();

		//View Models
		services.AddScoped<HomeViewModel>();

		return services;
	}
}