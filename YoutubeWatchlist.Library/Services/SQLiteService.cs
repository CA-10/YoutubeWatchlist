using Microsoft.Data.Sqlite;
using YoutubeWatchlist.Library.Models;

namespace YoutubeWatchlist.Library.Services;

public class SQLiteService : IDatabaseService
{
	private string createStatements = """
			CREATE TABLE IF NOT EXISTS videos (
			NAME VARCHAR(255),
			CATEGORY VARCHAR(255),
			TIMESTAMP_SECONDS INT
		);
	""";

	public SQLiteService()
	{

	}

	private bool CreateTablesIfNotExist()
	{
		bool success = true;

		try
		{
			using (var connection = new SqliteConnection("Data Source=database.db3"))
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

	public async Task<(bool success, List<Video> videos)> GetVideosAsync()
	{
		bool success = true;
		List<Video> videos = new();

		string query = """
			SELECT * FROM videos;
		""";

		try
		{
			using (var connection = new SqliteConnection("Data Source=database.db"))
			{
				using var command = new SqliteCommand(query, connection);

				using (var reader = await command.ExecuteReaderAsync())
				{
					if (reader.HasRows)
					{
						while (await reader.ReadAsync())
						{
							string name = reader.GetString(reader.GetOrdinal("NAME"));
							string categoryID = reader.GetString(reader.GetOrdinal("CATEGORY_ID"));
							int timestampSeconds = reader.GetInt32(reader.GetOrdinal("TIMESTAMP_SECONDS"));

							Video video = new Video()
							{
								Name = name,
								CategoryID = categoryID,
								TimestampSeconds = timestampSeconds
							};

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
		}

		return (success, videos);
	}
}