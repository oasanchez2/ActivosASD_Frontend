using GrupoASD.GestionActivos.App.Servicios.Entities;
using GrupoASD.GestionActivos.App.Servicios.Facade;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Configuration;
using System.Windows;

namespace GrupoASD.GestionActivos.App.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;
        public App()
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            _host = new HostBuilder()
          .ConfigureServices((hostContext, services) =>
          {              
              //Lets Create single tone instance of Master windows
              services.AddSingleton<MainWindow>();
              services.AddHttpClient<IAsdGestionActivosApi, AsdGestionActivosApi>(c =>
              {
                  c.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlApiGestionActivos"], System.UriKind.RelativeOrAbsolute);
                  c.Timeout = TimeSpan.FromMinutes(2);
              });

          }).ConfigureLogging(logBuilder =>
          {
              logBuilder.ClearProviders();
              logBuilder.SetMinimumLevel(LogLevel.Information);
              logBuilder.AddEventLog(eventLogSettings =>
              {
                  eventLogSettings.SourceName = "LogsGestionActivosAppWpf";
              });

          }).UseNLog().Build();

            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var masterWindows = services.GetRequiredService<MainWindow>();
                    masterWindows.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error a ocurrido " + ex.Message);
                    logger.Error(ex, "Expcion");
                }
            }
        }
    }
}
