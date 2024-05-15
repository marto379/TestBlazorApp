namespace TestBlazorApp
{
    using Dapper;
    using System.Data.SqlClient;
    using TestBlazorApp.Components;
    using TestBlazorApp.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped<IDataAccessLayer,DataAccessLayer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            //app.MapGet("People",async (IConfiguration configuration) =>
            //{
            //    string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            //    using var connection = new SqlConnection(connectionString);

            //    const string sql = "SELECT * FROM People";

            //    var people = await connection.QueryAsync<Person>(sql);
            //    return Results.Ok(people);
            //});

            app.Run();
        }
    }
}
