using YoutubeWatchlist.Library.Models;
using YoutubeWatchlist.Library.Services;

namespace YoutubeWatchlist.ViewModels;

public class HomeViewModel : BaseViewModel
{
	private readonly IDatabaseService _databaseService;

	public List<Video> Videos { get; set; } = new();

	public HomeViewModel(IDatabaseService databaseService)
	{
		_databaseService = databaseService;
	}

	public override async Task InitAsync()
	{
		var dataResponse = await _databaseService.GetDataAsync();

		if (dataResponse.success)
		{
			Videos = dataResponse.videos;
		}
		else
		{
			ErrorMessages += $"Failed to fetch data from database";
		}
	}
}