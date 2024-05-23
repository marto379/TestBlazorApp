using Dapper;
using System.Data.SqlClient;
using ItemsBlazorApp.Data;
using ItemsAPI.Models;

public class DataAccessLayer : IDataAccessLayer
{
    private readonly string connectionString;

    public DataAccessLayer(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<List<Item>> GetAllItemsAsync()
    {
        using var  connection = new SqlConnection(connectionString);
        var rows = await connection.QueryAsync<Item>(Queries.GetAllItemsQuery);

        return rows.ToList();
    }

    public async Task InsertItemAsync(Item item)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(Queries.InsertItemQuery, new { item.Name, item.Price, item.DateAdded });
    }

    public async Task<Item?> GetItemAsync(long id)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstOrDefaultAsync<Item>(Queries.GetItemQuery, new { Id = id });
        return result;
    }

    public async Task UpdateItemAsync(Item item)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.OpenAsync();
        await connection.ExecuteAsync(Queries.UpdateItemQuery, item);
    }

    public async Task DeleteItemAsync(long id)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(Queries.DeleteItemQuery, new { Id = id });
    }

    public static async Task EnsureDatabaseCreated(string connectionString)
    {
        var sql = await File.ReadAllTextAsync("Data\\DataBase.sql");
        // Split with go because create database statement must be in
        // separate execute statement
        var statements = sql.Split("GO");

        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        foreach (var statement in statements)
        {
            await connection.ExecuteAsync(statement);
        }
    }
}
