using System;
using System.ComponentModel;

namespace Facturacion.WPF.Client.ViewModels
{
    public class PersonaViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;

        /// <summary>
        /// Identificador de la persona (BD).
        /// </summary>
        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string nombre;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private string identificacion;

        public string Nombre
        {
            get => nombre;
            set { nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        public string ApellidoPaterno
        {
            get => apellidoPaterno;
            set { apellidoPaterno = value; OnPropertyChanged(nameof(ApellidoPaterno)); }
        }

        public string ApellidoMaterno
        {
            get => apellidoMaterno;
            set { apellidoMaterno = value; OnPropertyChanged(nameof(ApellidoMaterno)); }
        }

        public string Identificacion
        {
            get => identificacion;
            set { identificacion = value; OnPropertyChanged(nameof(Identificacion)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    nameof(Nombre) => string.IsNullOrWhiteSpace(Nombre) ? "El nombre requerido" : null,
                    nameof(ApellidoPaterno) => string.IsNullOrWhiteSpace(ApellidoPaterno) ? "El apellido paterno requerido" : null,
                    nameof(Identificacion) => string.IsNullOrWhiteSpace(Identificacion) ? "La identificación requerida" : null,
                    _ => null,
                };
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(this[nameof(Nombre)])
                && string.IsNullOrWhiteSpace(this[nameof(ApellidoPaterno)])
                && string.IsNullOrWhiteSpace(this[nameof(Identificacion)]);
        }
    }
}
