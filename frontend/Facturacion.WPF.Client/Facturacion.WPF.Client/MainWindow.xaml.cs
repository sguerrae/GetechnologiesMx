using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Facturacion.WPF.Client.ViewModels;
using Facturacion.WPF.Client.Services;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Facturacion.WPF.Client
{
    /// <summary>
    /// Ventana principal del cliente WPF.
    /// Contiene el formulario para crear una persona y la integración básica con la API.
    /// Mantener la lógica UI mínima aquí; la lógica de negocio/HTTP reside en ViewModel/Servicios.
    /// </summary>
    public partial class MainWindow : Window
    {
        // ViewModel que expone propiedades enlazables y validaciones reactivas.
        private readonly PersonaViewModel viewModel;
        // Cliente HTTP encapsulado para llamadas al backend.
        private readonly ApiClient apiClient;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new PersonaViewModel();
            DataContext = viewModel;

            // Instancia ApiClient apuntando al Swagger local mostrado en https://localhost:7202
            // ignoreSslErrorsForLocalhost=true solo para desarrollo con certificados auto-firmados.
            apiClient = new ApiClient("https://localhost:7202", ignoreSslErrorsForLocalhost: true);
        }

        /// <summary>
        /// Enviar_Click: ejecuta la llamada al endpoint de creación de persona.
        /// Validaciones básicas se realizan mediante IDataErrorInfo en el ViewModel.
        /// En un refactor posterior, mover el comando a PersonaViewModel como ICommand.
        /// </summary>
        private async void Enviar_Click(object sender, RoutedEventArgs e)
        {
            if (!viewModel.IsValid())
            {
                MessageBox.Show("Por favor completa los campos obligatorios.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var persona = new {
                nombre = viewModel.Nombre,
                apellidoPaterno = viewModel.ApellidoPaterno,
                apellidoMaterno = viewModel.ApellidoMaterno,
                identificacion = viewModel.Identificacion
            };

            try
            {
                var resp = await apiClient.CrearPersonaAsync(persona);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona creada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Limpiar formulario tras creación exitosa
                    viewModel.Nombre = viewModel.ApellidoPaterno = viewModel.ApellidoMaterno = viewModel.Identificacion = string.Empty;
                }
                else
                {
                    var content = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error del servidor: {resp.StatusCode}\n{content}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo conectar al servidor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Mostrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = await apiClient.GetPersonasAsync();
                PersonasList.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener personas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PersonasList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PersonasList.SelectedItem is Services.PersonaDto p)
            {
                viewModel.Id = p.Id;
                viewModel.Nombre = p.Nombre;
                viewModel.ApellidoPaterno = p.ApellidoPaterno;
                viewModel.ApellidoMaterno = p.ApellidoMaterno;
                viewModel.Identificacion = p.Identificacion;
            }
        }
    }
}
