using Autofac;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModel;

namespace ConfigTool.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            // Create container
            var builder = new ContainerBuilder();

            // Register types
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<PlctagRepositoryFake>().As<IPlcTagRepository>();

            // Create container
            return builder.Build();
        }
    }
}
