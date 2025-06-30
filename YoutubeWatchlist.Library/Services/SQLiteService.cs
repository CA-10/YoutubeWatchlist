using Microsoft.Data.Sqlite;
using YoutubeWatchlist.Library.Models;

namespace YoutubeWatchlist.Library.Services;

public class SQLiteService : IDatabaseService
{
	private const string connectionName = "Data Source=database.db3";

	private string createStatements = """
		CREATE TABLE IF NOT EXISTS videos (
			NAME VARCHAR(255),
			CATEGORY_ID VARCHAR(255),
			TIMESTAMP_SECONDS INT
		);

		CREATE TABLE IF NOT EXISTS categories (
			CATEGORY_ID VARCHAR(255),
			CATEGORY_NAME VARCHAR(255)
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
							int timestampSeconds = reader.GetInt32(reader.GetOrdinal("TIMESTAMP_SECONDS"));

							string categoryID = reader.GetString(reader.GetOrdinal("CATEGORY_ID"));
							string categoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME"));

							Category category = new Category()
							{
								Name = categoryName,
								CategoryID = categoryID,
							};

							Video video = new Video()
							{
								Name = name,
								Category = category,
								TimestampSeconds = timestampSeconds
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