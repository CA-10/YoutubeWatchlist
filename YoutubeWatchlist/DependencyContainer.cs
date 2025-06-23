using YoutubeWatchlist.Library.Services;

namespace YoutubeWatchlist.Blazor;

public static class DependencyContainer
{
	public static IServiceCollection AddDependencies(this IServiceCollection services)
	{
		services.AddSingleton<IDatabaseService, SQLiteService>();

		return services;
	}
}