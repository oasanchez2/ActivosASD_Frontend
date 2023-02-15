using GrupoASD.GestionActivos.App.Servicios.Facade;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

namespace GrupoASD.GestionActivos.App.Wpf.Views.Activos
{
    /// <summary>
    /// Lógica de interacción para EditarActivoView.xaml
    /// </summary>
    public partial class EditarActivoView : Page
    {
        private readonly ILogger _logger;
        private readonly IAsdGestionActivosApi _asdGestionActivosApi;
        public EditarActivoView()
        {
            if (MainWindow.AppWindow?._logger != null)
                _logger = MainWindow.AppWindow._logger;

            if (MainWindow.AppWindow?._asdGestionActivosApi != null)
                _asdGestionActivosApi = MainWindow.AppWindow._asdGestionActivosApi;

            InitializeComponent();
        }

        private async void btnBuscar_ClickAsync(object sender, RoutedEventArgs e)
        {
            var resultado = await _asdGestionActivosApi.ActivosObtenerAsync();
        }

    }
}
