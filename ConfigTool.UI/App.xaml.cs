using ConfigTool.DataAccess;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;

namespace ConfigTool.UI
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            // Define configuration files
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables();

            // Build application with defined configuration files
            Configuration = builder.Build();

            //Build IoC containter
            var serviceCollection = new ServiceCollection();

            //Add services to IoC container
            ConfigureServices(serviceCollection);

            //Build Serviceprovider
            ServiceProvider = serviceCollection.BuildServiceProvider();

            //Build mainWindow
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IPlcTagRepository, PlctagRepository>();
            services.AddDbContext<ModelContext>(options =>
                options.UseFirebird(Configuration.GetConnectionString("ConfigToolDatabase")));
        }
    }
}
