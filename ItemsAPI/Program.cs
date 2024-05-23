using ItemsAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            builder.Services.AddSingleton<ItemValidator>();

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
                try
                {
                    var result = await accessLayer.GetAllItemsAsync();
                    return Results.Ok(result);
                }
                catch (Exception)
                {
                    return Results.NotFound("Something went wrong!");
                }
            });

            app.MapPost("item", async ([FromBody]Item item ,IDataAccessLayer accesslayer, ItemValidator validator) =>
            {
                if (!validator.IsValidItem(item, out var validationErrors))
                {
                    return Results.BadRequest(string.Join(", ", validationErrors));
                }
                await accesslayer.InsertItemAsync(item);
                return Results.Ok();
            });

            app.MapPut("item", async ([FromBody] Item item, IDataAccessLayer accessLayer, ItemValidator validator) =>
            {
                if (!validator.IsValidItem(item, out var validationErrors))
                {
                    return Results.BadRequest(string.Join(", ", validationErrors));
                }
                await accessLayer.UpdateItemAsync(item);
                return Results.Ok();
            });

            app.MapDelete("item/{id}", async ([FromRoute]long id, IDataAccessLayer accessLayer) =>
            {
                try
                {
                    await accessLayer.DeleteItemAsync(id);
                    return Results.Ok($"Successfully deleted item with id: {id}");
                }
                catch (Exception)
                {
                    return Results.Problem();
                }
            });

            app.Run();
        }
    }
}
