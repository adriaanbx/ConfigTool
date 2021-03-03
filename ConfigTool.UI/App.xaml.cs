using Autofac;
using ConfigTool.UI.Startup;
using System.Windows;

namespace ConfigTool.UI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Define Dependency Container
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            // Create new mainwindow object with dependency injection
            var mainWindow = container.Resolve<MainWindow>();

            // Show mainwindow
            mainWindow.Show();
        }
    }
}
