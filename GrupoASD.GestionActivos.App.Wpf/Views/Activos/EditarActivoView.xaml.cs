using GrupoASD.GestionActivos.App.Servicios.Entities;
using GrupoASD.GestionActivos.App.Servicios.Facade;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            int id = 0;
            try
            {
                id = Convert.ToInt32(txtIdActivo.Text);
                var resultado = await _asdGestionActivosApi.ActivosObtenerPorIdAsync(id);
                if (resultado.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    ActivosModel activo = JsonConvert.DeserializeObject<ActivosModel>(resultado.JsonResultado, new JsonSerializerSettings { Formatting = Formatting.None });

                    List<ActivosModel> listaActivos = new List<ActivosModel>();
                    listaActivos.Add(activo);
                    dgDatosActivo.ItemsSource = null;
                    dgDatosActivo.ItemsSource = listaActivos;
                }
                else
                {
                    JObject resultadoObjet = JObject.Parse(resultado.JsonResultado);
                    MessageBox.Show(resultadoObjet["mensaje"].ToString(), "Busqueda Activo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Busqueda Activo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnEditarActivo_Click(object sender, RoutedEventArgs e)
        {
            int idActivo = (int)((Button)sender).CommandParameter;
            txtIdActivoUpdate.Text = idActivo.ToString();
            txtSerial.Text = "";
            txtFechaBaja.Text = "";
            pnlEdicion.Visibility = Visibility.Visible;
        }

        private async void btnActualizar_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSerial.Text) && string.IsNullOrEmpty(txtFechaBaja.Text))
                {
                    throw new Exception("Ingrese almenos 1 valor");
                }

                ActivosUpdate activosUpdate = new ActivosUpdate
                {
                    IdActivo = Convert.ToInt32(txtIdActivoUpdate.Text),
                    Serial = txtSerial.Text,
                    FechaBaja = Convert.ToDateTime(txtFechaBaja.Text)
                };
                var resultado = await _asdGestionActivosApi.ActivosUpdate(activosUpdate);

                if (resultado.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Se actualizo correctamente", "Actualización Activo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    JObject resultadoObjet = JObject.Parse(resultado.JsonResultado);
                    MessageBox.Show(resultadoObjet["mensaje"].ToString(), "Actualización Activo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Actualización Activo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
    }
}
