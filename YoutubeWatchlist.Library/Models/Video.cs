namespace YoutubeWatchlist.Library.Models;

public class Video
{
	public string Name { get; set; } = string.Empty;
	public string CategoryID { get; set; } = string.Empty;
	public int TimestampSeconds { get; set; } = 0;
}