using Dapper;
using System.Data;
using System.Data.SqlClient;
using TestBlazorApp.Models;

public class DataAccessLayer : IDataAccessLayer
{
    public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var rows = await connection.QueryAsync<T>(sql, parameters);

            

            return rows.ToList();
        }
    }

    public async Task SaveData<T>(string sql, T parameters, string connectionString)
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

    public async Task InsertPersonAsync(Person person, string connectionString)
    {
        const string sql = "INSERT INTO People (Name, Gender, Birthday) VALUES (@Name, @Gender, @Birthday)";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, new { person.Name, person.Gender, person.Birthday });
        }
    }

    public async Task<Person> GetPersonAsync(int id, string connectionString)
    {
        const string sql = "SELECT Id, Name, Gender FROM People WHERE Id = @Id";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Person>(sql, new { Id = id });
        }
    }
    public async Task UpdatePersonAsync(Person person, string connectionString)
    {
        const string sql = "UPDATE People SET Name = @Name, Gender = @Gender WHERE Id = @Id";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, person);
        }
    }

    public async Task DeletePersonAsync(int id, string connectionString)
    {
        const string sql = "DELETE FROM People WHERE Id = @Id";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }

}
