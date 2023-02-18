using GrupoASD.GestionActivos.App.Servicios.Facade;
using GrupoASD.GestionActivos.App.Wpf.Views;
using GrupoASD.GestionActivos.App.Wpf.Views.Activos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrupoASD.GestionActivos.App.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly ILogger _logger;
        public readonly IAsdGestionActivosApi _asdGestionActivosApi;
        public static MainWindow AppWindow;
        public static Frame StaticMainFrame;
        public MainWindow(ILogger<MainWindow> logger, IAsdGestionActivosApi asdGestionActivosApi)
        {
            AppWindow = this;
            _logger = logger;
            _asdGestionActivosApi = asdGestionActivosApi;
            InitializeComponent();
            StaticMainFrame = MainFrame;
            this.Title = "Gestión Activos v." + getRunningVersion();
        }

        /// <summary>
        /// Devuelve la version del ensamblado
        /// </summary>
        /// <returns></returns>
        private Version getRunningVersion()
        {
            try
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region Menu Aplicacón
        private void MenuInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StaticMainFrame.Content = new InicioView();
        }
        private void menuEditarActivo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StaticMainFrame.Content = new EditarActivoView();
        }

        #endregion
    }
}
