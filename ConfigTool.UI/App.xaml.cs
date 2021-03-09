using ConfigTool.DataAccess;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows;

namespace ConfigTool.UI
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                   .ConfigureServices((context, services) =>
                   {
                       ConfigureServices(context.Configuration, services);
                   })
                   .Build();
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<MainWindow>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<IPlcTagRepository, PlctagRepository>();
            services.AddDbContext<ModelContext>(options =>
                options.UseFirebird(configuration.GetConnectionString("ConfigToolDatabase")));
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.CreateScope().ServiceProvider.GetRequiredService<MainWindow>();
             mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
