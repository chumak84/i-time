using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TimeToTomato.Infrastructure;

namespace TimeToTomato
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            TimeToTomato.Model.Infrastructure.InfrastructureFactory.ProvideTimer<Timer>();
        }
    }
}
