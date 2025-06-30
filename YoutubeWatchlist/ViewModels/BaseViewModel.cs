namespace YoutubeWatchlist.ViewModels;

public abstract class BaseViewModel
{
	public bool IsLoading { get; set; }
	public string ErrorMessages { get; set; } = string.Empty;

	public abstract Task InitAsync();
}