﻿namespace YoutubeWatchlist.Library.Models;

public class Video
{
	public string Name { get; set; } = string.Empty;
	public string Notes { get; set; } = string.Empty;
	public string Link { get; set; } = string.Empty;

	public Category Category { get; set; } = new();
	public int TimestampSeconds { get; set; } = 0;
	public string Base64Image { get; set; } = string.Empty;
}