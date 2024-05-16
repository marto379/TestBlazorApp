

using TestBlazorApp.Models;

public interface IDataAccessLayer
{
    Task DeletePersonAsync(int id, string connectionString);
    Task InsertPersonAsync(Person person, string connectionString);
    Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
    Task SaveData<T>(string sql, T parameters, string connectionString);
}