using Microsoft.Data.Sqlite;
using YoutubeWatchlist.Library.Models;

namespace YoutubeWatchlist.Library.Services;

public class SQLiteService : IDatabaseService
{
	private const string connectionName = "Data Source=database.db3";

	private string createStatements = """
		CREATE TABLE IF NOT EXISTS videos (
			NAME TEXT,
			NOTES TEXT,
			LINK TEXT,
			CATEGORY_ID TEXT,
			TIMESTAMP_SECONDS INT,
			IMAGE_BASE64 TEXT
		);

		CREATE TABLE IF NOT EXISTS categories (
			CATEGORY_ID TEXT,
			CATEGORY_NAME TEXT
		);
	""";

	public SQLiteService()
	{
		CreateTablesIfNotExist();
	}

	private bool CreateTablesIfNotExist()
	{
		bool success = true;

		try
		{
			using (var connection = new SqliteConnection(connectionName))
			{
				connection.Open();

				using (var command = new SqliteCommand(createStatements, connection))
				{
					command.ExecuteNonQuery();
				}
			}
		}
		catch (Exception ex)
		{
			success = false;
		}

		return success;
	}

	public async Task<(bool success, List<Video> videos, List<Category> categories)> GetDataAsync()
	{
		bool success = true;

		List<Video> videos = new();
		List<Category> categories = new();

		string query = """
			SELECT * FROM videos
			JOIN categories
			ON videos.CATEGORY_ID = categories.CATEGORY_ID;
		""";

		try
		{
			using (var connection = new SqliteConnection(connectionName))
			{
				connection.Open();

				using var command = new SqliteCommand(query, connection);

				using (var reader = await command.ExecuteReaderAsync())
				{
					if (reader.HasRows)
					{
						while (await reader.ReadAsync())
						{
							string name = reader.GetString(reader.GetOrdinal("NAME"));
							string notes = reader.GetString(reader.GetOrdinal("NOTES"));
							int timestampSeconds = reader.GetInt32(reader.GetOrdinal("TIMESTAMP_SECONDS"));
							string link = reader.GetString(reader.GetOrdinal("LINK"));

							string categoryID = reader.GetString(reader.GetOrdinal("CATEGORY_ID"));
							string categoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME"));
							string image = reader.GetString(reader.GetOrdinal("IMAGE_BASE64"));

							Category category = new Category()
							{
								Name = categoryName,
								CategoryID = categoryID,
							};

							Video video = new Video()
							{
								Name = name,
								Notes = notes,
								Link = link,
								Category = category,
								TimestampSeconds = timestampSeconds,
								Base64Image = image,
							};

							categories.Add(category);
							videos.Add(video);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			success = false;
			videos = new();
			categories = new();
		}

		return (success, videos, categories);
	}
}