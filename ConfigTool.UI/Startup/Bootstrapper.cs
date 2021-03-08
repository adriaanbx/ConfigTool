using Autofac;
using ConfigTool.DataAccess;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ConfigTool.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            // Create container
            var builder = new ContainerBuilder();

            // Register types
            //builder.RegisterType<ModelContext>();


            builder.Register(c =>
            {
                string path = "C:\\Users\\gea.halle.cni\\Source\\Repos\\ConfigTool\\Config\\MC5_CONFIG.FDB";

                var optionsBuilder = new DbContextOptionsBuilder<ModelContext>();
                optionsBuilder.UseFirebird("Server = localhost; Database =" + path + " ; user id = SYSDBA; password = SYSDBA; character set = UTF8;MaxPoolSize=300");

                return new ModelContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<PlctagRepository>().As<IPlcTagRepository>();

            // Create container
            return builder.Build();
        }
    }
}
