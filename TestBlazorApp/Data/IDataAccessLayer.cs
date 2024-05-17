

using TestBlazorApp.Models;

public interface IDataAccessLayer
{
    Task DeletePersonAsync(int id);
    Task<Person> GetPersonAsync(int id);
    Task InsertPersonAsync(Person person);
    Task<List<T>> LoadData<T, U>(U parameters);
    Task SaveData<T>(string sql, T parameters);
    Task UpdatePersonAsync(Person person);
}