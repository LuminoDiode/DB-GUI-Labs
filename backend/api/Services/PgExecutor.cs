using api.Const;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace api.Services;

public class PgExecutor
{
	public string ConnectionString { get; init; }
	public string MasterConnectionString { get; init; }
	// public string DatabaseName { get; init; }

	public PgExecutor(string connectionString, string masterConnectionString/*, string databaseName = "Airportlabs"*/)
	{
		this.ConnectionString = connectionString;
		this.MasterConnectionString = masterConnectionString;
		//this.DatabaseName = databaseName;
	}

	#region to delete
	//public async Task<bool> IsDatabaseExists()
	//{
	//	await using var conn = new NpgsqlConnection(this.MasterConnectionString);
	//	await using var result = await conn.ExecuteReaderAsync(RequestTemplates.SearchDatabase_1p, new { p1 = DatabaseName });

	//	return result.HasRows;
	//}

	//private async Task<int> CreateDatabase()
	//{
	//	await using var conn = new NpgsqlConnection(this.MasterConnectionString);

	//	var dynamicParameters = new DynamicParameters();
	//	dynamicParameters.Add("p1", DatabaseName, DbType.String, direction: ParameterDirection.Input);

	//	var result = await conn.ExecuteAsync(RequestTemplates.CreateDatabase_1p.Trim(), dynamicParameters);

	//	return result;
	//}
	//private async Task<int> DropDatabase()
	//{
	//	await using var conn = new NpgsqlConnection(this.MasterConnectionString);

	//	return await conn.ExecuteAsync(RequestTemplates.DropDatabase_1p, new { p1 = DatabaseName });
	//}

	//public async Task<int> EnsureDatabaseCreated()
	//{
	//	if(!(await IsDatabaseExists())) return await CreateDatabase();
	//	return 0;
	//}
	//public async Task<int> EnsureDatabaseDroped()
	//{
	//	if((await IsDatabaseExists())) return await DropDatabase();
	//	return 0;
	//}


	///// <summary>
	///// never expose this directly to the public API
	///// </summary>
	//public async Task CreateTable(string tableName, Dictionary<string, string> columnToType)
	//{
	//	await using var conn = new NpgsqlConnection(this.ConnectionString);
	//	await conn.ExecuteAsync(RequestTemplates.CreateTable(tableName, columnToType.AsEnumerable()));
	//}
	// 
	/// <summary>
	/// never expose this directly to the public API
	/// </summary>
	//public async Task DropTable(string tableName)
	//{
	//	await using var conn = new NpgsqlConnection(this.ConnectionString);
	//	await conn.ExecuteAsync(RequestTemplates.DropTable(tableName));
	//}
	#endregion

	/// <summary>
	/// never expose this directly to the public API
	/// </summary>
	public async Task<int> ExecuteAnyMaster(string sql)
	{
		await using var conn = new NpgsqlConnection(this.MasterConnectionString);
		return await conn.ExecuteAsync(sql);
	}

	public async Task<int> ExecuteAny(string sql, object param = null!)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.ExecuteAsync(sql, param);
	}

	public async Task<IEnumerable<T>> ExecuteAny<T>(string sql, object param = null!)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync<T>(sql, param);
	}

	public async Task<IEnumerable<dynamic>> L2_AvgFlyTime(string brand, string from, string to)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates2.AvgFlyTime_3p, new {p1 = brand, p2 = from, p3 = to});
	}
	public async Task<IEnumerable<dynamic>> L2_MostlySameRoute()
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates2.MostlySameRoute_0p);
	}
	public async Task<IEnumerable<dynamic>> L2_MostlyEmpty70()
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates2.MostlyEmpty70_0p);
	}
	public async Task<IEnumerable<dynamic>> L2_FreeSeats(int routeId, DateTime dateISO8601)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates2.FreeSeats_2p, new { p1 = routeId, p2 = dateISO8601 });
	}

	public async Task<IEnumerable<dynamic>> L4_DistanceOnRouteByPlain()
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates2.PlaneToDistanceOnRoute);
	}

	public async Task<IEnumerable<dynamic>> L4_CreateTimeTable(string from, string to)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates4.CreateTimeTable_2p, new { p1 = from, p2 = to });
	}

	public async Task<int> L4_ChangeCapacity(string brand, int delta)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.ExecuteAsync(Templates4.ChangeCapacity_2p, new { p1 = brand, p2 = delta });
	}

	public async Task<IEnumerable<dynamic>> L4_FlightsOnRoute()
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates4.FlightsOnRoute_0p);
	}

	public async Task<int> L4_ChangeSpeed(string brand, int percentChange)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.ExecuteAsync(Templates4.UpdateSpeed_2p, new { p2 = brand, p1 = (1 - percentChange/100f) });
	}


	public async Task<IEnumerable<dynamic>> L6_SelectByBrandAndCapacity(string[] brands, int minCapacity, int maxCapcity)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates6.ByBrandAndCapacity_3p, new { p1 = brands, p2 = minCapacity, p3 = maxCapcity });
	}


	public async Task<IEnumerable<dynamic>> L6_SelectByLastBoardDigit(char min, char max)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates6.ByLastBoardNumber_2p, new { p1 = min, p2 = max });
	}



	public async Task<IEnumerable<dynamic>> L6_DepartureAndFirstChar(char firstChar)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates6.WhereDepartureAndStartsWith_1p, new { p1 = firstChar });
	}


	public async Task<IEnumerable<dynamic>> L6_WhereSoldN(int quantity)
	{
		await using var conn = new NpgsqlConnection(this.ConnectionString);
		return await conn.QueryAsync(Templates6.WhereSoldN_1p, new { p1 = quantity });
	}
}
