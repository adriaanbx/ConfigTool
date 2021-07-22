using ConfigTool.DataAccess;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModels;
using ConfigTool.UI.Views.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                 .ConfigureLogging(loggingBuilder =>
                  {
                      loggingBuilder.ClearProviders();
                      loggingBuilder
                         .AddDebug()
                         .AddFile("Logs/mylog-{Date}.txt");

                  })
                 .Build();
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IMessageDialogService, MessageDialogService>();

            services.AddTransient<IModelRepository, ModelRepository>();
            services.AddTransient<IPlctagRepository, PlctagRepository>();
            services.AddTransient<IDatablockRepository, DatablockRepository>();
            services.AddTransient<IValueTypeRepository, ValueTypeRepository>();
            services.AddTransient<IUnitCategoryRepository, UnitCategoryRepository>();
            services.AddTransient<ITextLanguageRepository, TextLanguageRepository>();
            services.AddTransient<IPressParameterRepository, PressParameterRepository>();
            services.AddTransient<IPressParameterTypeRepository, PressParameterTypeRepository>();
            services.AddTransient<ILayerSideRepository, LayerSideRepository>();
            services.AddTransient<IEngineeringRepository, EngineeringRepository>();
            services.AddTransient<IEquipmentRepository, EquipmentRepository>();
            services.AddTransient<IReadWriteTypeRepository, ReadWriteTypeRepository>();
            services.AddTransient<IEcmParameterRepository, EcmParameterRepository>();

            services.AddTransient<INavigationViewModel, NavigationViewModel>();

            services.AddTransient<IDatablockDetailViewModel, DatablockDetailViewModel>();
            services.AddTransient<IValueTypeDetailViewModel, ValueTypeDetailViewModel>();
            services.AddTransient<IUnitCategoryDetailViewModel, UnitCategoryDetailViewModel>();
            services.AddTransient<ITextLanguageDetailViewModel, TextLanguageDetailViewModel>();
            services.AddTransient<IPlctagDetailViewModel, PlctagDetailViewModel>();
            services.AddTransient<IPressParameterTypeDetailViewModel, PressParameterTypeDetailViewModel>();
            services.AddTransient<ILayerSideDetailViewModel, LayerSideDetailViewModel>();
            services.AddTransient<IReadWriteTypeDetailViewModel, ReadWriteTypeDetailViewModel>();

            services.AddTransient<IPlctagTableViewModel, PlctagTableViewModel>();
            services.AddTransient<IDatablockTableViewModel, DatablockTableViewModel>();
            services.AddTransient<IPressParameterTableViewModel, PressParameterTableViewModel>();
            services.AddTransient<IPressParameterTypeTableViewModel, PressParameterTypeTableViewModel>();
            services.AddTransient<ILayerSideTableViewModel, LayerSideTableViewModel>();
            services.AddTransient<IEngineeringTableViewModel, EngineeringTableViewModel>();
            services.AddTransient<IEquipmentTableViewModel, EquipmentTableViewModel>();
            services.AddTransient<IReadWriteTypeTableViewModel, ReadWriteTypeTableViewModel>();
            services.AddTransient<IEcmParameterTableViewModel, EcmParameterTableViewModel>();

            services.AddTransient<Func<IDatablockDetailViewModel>>(sp => () => sp.GetService<IDatablockDetailViewModel>());
            services.AddTransient<Func<IValueTypeDetailViewModel>>(sp => () => sp.GetService<IValueTypeDetailViewModel>());
            services.AddTransient<Func<IUnitCategoryDetailViewModel>>(sp => () => sp.GetService<IUnitCategoryDetailViewModel>());
            services.AddTransient<Func<ITextLanguageDetailViewModel>>(sp => () => sp.GetService<ITextLanguageDetailViewModel>());
            services.AddTransient<Func<IPressParameterTypeDetailViewModel>>(sp => () => sp.GetService<IPressParameterTypeDetailViewModel>());
            services.AddTransient<Func<ILayerSideDetailViewModel>>(sp => () => sp.GetService<ILayerSideDetailViewModel>());
            services.AddTransient<Func<IPlctagDetailViewModel>>(sp => () => sp.GetService<IPlctagDetailViewModel>());
            services.AddTransient<Func<IReadWriteTypeDetailViewModel>>(sp => () => sp.GetService<IReadWriteTypeDetailViewModel>());
            services.AddTransient<Func<IPlctagTableViewModel>>(sp => () => sp.GetService<IPlctagTableViewModel>());
            services.AddTransient<Func<IDatablockTableViewModel>>(sp => () => sp.GetService<IDatablockTableViewModel>());
            services.AddTransient<Func<IPressParameterTableViewModel>>(sp => () => sp.GetService<IPressParameterTableViewModel>());
            services.AddTransient<Func<IPressParameterTypeTableViewModel>>(sp => () => sp.GetService<IPressParameterTypeTableViewModel>());
            services.AddTransient<Func<ILayerSideTableViewModel>>(sp => () => sp.GetService<ILayerSideTableViewModel>());
            services.AddTransient<Func<IEngineeringTableViewModel>>(sp => () => sp.GetService<IEngineeringTableViewModel>());
            services.AddTransient<Func<IEquipmentTableViewModel>>(sp => () => sp.GetService<IEquipmentTableViewModel>());
            services.AddTransient<Func<IReadWriteTypeTableViewModel>>(sp => () => sp.GetService<IReadWriteTypeTableViewModel>());
            services.AddTransient<Func<IEcmParameterTableViewModel>>(sp => () => sp.GetService<IEcmParameterTableViewModel>());

            services.AddDbContext<ModelContext>(options =>
                     options.UseFirebird(configuration.GetConnectionString("ConfigToolDatabase"))
                            .EnableSensitiveDataLogging()
                            , ServiceLifetime.Transient, ServiceLifetime.Transient);
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
