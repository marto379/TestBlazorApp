using Dapper;
using System.Data;
using System.Data.SqlClient;
using TestBlazorApp.Models;

public class DataAccessLayer : IDataAccessLayer
{
    public List<T> LoadData<T, U>(string sql, U parameters, string connectionString)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(sql, parameters).ToList();

            return rows;
        }
    }

    public void SaveData<T>(string sql, T parameters, string connectionString)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, parameters);
        }
    }

}
