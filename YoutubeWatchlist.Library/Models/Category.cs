namespace YoutubeWatchlist.Library.Models;

public class Category
{
	public string CategoryID { get; set; } = Guid.NewGuid().ToString();
	public string Name { get; set; } = string.Empty;
}