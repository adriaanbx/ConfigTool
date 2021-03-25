using ConfigTool.DataAccess;
using ConfigTool.UI.Lookups;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModel;
using ConfigTool.UI.Views.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using System;
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
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IMessageDialogService, MessageDialogService>();
            services.AddTransient<IPlctagRepository, PlctagRepository>();
            services.AddTransient<IPlctagLookupDataRepository, LookupDataRepository>();
            services.AddTransient<INavigationViewModel, NavigationViewModel>();
            services.AddTransient<IPlctagDetailViewModel, PlctagDetailViewModel>();
            services.AddTransient<Func<IPlctagDetailViewModel>>(sp => () => sp.GetService<IPlctagDetailViewModel>());
            services.AddDbContext<ModelContext>(options =>
                options.UseFirebird(configuration.GetConnectionString("ConfigToolDatabase")),ServiceLifetime.Transient, ServiceLifetime.Transient);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetService<MainWindow>();
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
