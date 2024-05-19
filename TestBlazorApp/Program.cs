using ItemsBlazorApp.Components;
using ItemsBlazorApp.Models;
using ItemsBlazorApp.Services;

namespace ItemsBlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddServerSideBlazor()
                    .AddCircuitOptions(options => { options.DetailedErrors = true; });

            var settings = builder.Configuration.Get<AppSettings>();
            builder.Services.AddSingleton<AppSettings>(settings!);
            

            builder.Services.AddScoped<ICommunicationService, CommunicationService>();
            builder.Services.AddHttpClient<ICommunicationService, CommunicationService>();

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

            app.Run();
        }
    }
}
