using Dapper;
using System.Data;
using System.Data.SqlClient;
using TestBlazorApp.Models;


public class DataAccessLayer : IDataAccessLayer
{
    private readonly string connectionString;

    public DataAccessLayer(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }



    public async Task<List<T>> LoadData<T, U>(U parameters)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            string sql = "select * from people";
            var rows = await connection.QueryAsync<T>(sql, parameters);

            return rows.ToList();
        }
    }

    public async Task SaveData<T>(string sql, T parameters)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                
                await connection.ExecuteAsync(sql, parameters);
            }
        }
        catch (Exception ex)
        {
            // Log the exception and handle it accordingly
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public async Task InsertPersonAsync(Person person)
    {
        const string sql = "INSERT INTO People (Name, Gender, Birthday) VALUES (@Name, @Gender, @Birthday)";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, new { person.Name, person.Gender, person.Birthday });
        }
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        const string sql = "SELECT Id, Name, Gender FROM People WHERE Id = @Id";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Person>(sql, new { Id = id });
        }
    }
    public async Task UpdatePersonAsync(Person person)
    {
        const string sql = "UPDATE People SET Name = @Name, Gender = @Gender WHERE Id = @Id";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, person);
        }
    }

    public async Task DeletePersonAsync(int id)
    {
        const string sql = "DELETE FROM People WHERE Id = @Id";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }

}
