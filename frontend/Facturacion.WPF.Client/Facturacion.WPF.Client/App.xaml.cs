using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Facturacion.WPF.Client
{
    /// <summary>
    /// Punto de entrada de la aplicación WPF.
    /// Contiene manejo global de excepciones para registrar fallos inesperados
    /// durante el arranque o en el hilo de la interfaz de usuario.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// OnStartup se usa para inicializar handlers globales antes de mostrar la UI.
        /// Mantener aquí inicializaciones ligeras; las inicializaciones pesadas deben moverse
        /// a servicios o a la ventana principal para facilitar pruebas.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Registrar manejadores globales para depuración en entorno de desarrollo.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        /// <summary>
        /// Captura excepciones no controladas que ocurren en el hilo de interfaz (Dispatcher).
        /// En entornos de producción considerar un logger (Serilog/ILogger) en lugar de Console.
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Console.Error.WriteLine("Excepción no manejada en UI: " + e.Exception);
            // No marcar como handled para que el comportamiento por defecto sea preservado
            // durante la depuración. Cambiar a 'true' si se desea mantener la app viva.
            e.Handled = false;
        }

        /// <summary>
        /// Captura excepciones no manejadas a nivel de dominio de la aplicación.
        /// Útil para registrar errores que no pasan por el Dispatcher.
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.Error.WriteLine("Excepción no manejada de dominio: " + e.ExceptionObject?.ToString());
        }
    }
}
