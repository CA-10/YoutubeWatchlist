using YoutubeWatchlist.Library.Models;

namespace YoutubeWatchlist.Library.Services;

public interface IDatabaseService
{
	public Task<(bool success, List<Video> videos)> GetVideosAsync();
}