using ItemsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemsAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDataAccessLayer, DataAccessLayer>();

            var app = builder.Build();

            var connectionString = builder.Configuration.GetConnectionString("InitConnectionString");
            await DataAccessLayer.EnsureDatabaseCreated(connectionString!);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("item/{id}", async ([FromRoute] long id, IDataAccessLayer accessLayer) =>
            {
                var result = await accessLayer.GetItemAsync(id);
                if (result is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(result);
                }
            });

            app.MapGet("items", async (IDataAccessLayer accessLayer) =>
            {
                var result = await accessLayer.GetAllItemsAsync();
                return Results.Ok(result);
            });

            ///
            app.MapPost("item", async ([FromBody]Item item ,IDataAccessLayer accesslayer) =>
            {
                //add validation
                await accesslayer.InsertItemAsync(item);
                return Results.Ok();
            });

            ///
            app.MapPut("item", async ([FromBody] Item item, IDataAccessLayer accessLayer) =>
            {
                //add validation
                //if (item.Name.Length < 0)
                //{

                //}
                await accessLayer.UpdateItemAsync(item);
                return Results.Ok();
            });

            ///
            app.MapDelete("item/{id}", async ([FromRoute]long id, IDataAccessLayer accessLayer) =>
            {
                await accessLayer.DeleteItemAsync(id);
            });


            app.Run();
        }
    }
}
