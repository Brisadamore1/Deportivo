using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.DTOs;
using Service.Interfaces;
using Service.Models;
using Service.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppMovil.ViewModels
{
    public partial class BuscarSociosViewModel : ObservableObject
    {
        SociosService _socioService = new();
        ActivityService _actividadService = new();
        GenericService<Localidad> _localidadService = new();

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<Socio> _socios = new();

        // Propiedades para los filtros
        [ObservableProperty]
        private bool filtrarPorNombre = true; // Por defecto filtrar por nombre activado

        [ObservableProperty]
        private bool filtrarPorDni = false;

        [ObservableProperty]
        private bool mostrarFiltros = false;

        [ObservableProperty]
        private bool filtrarPorLocalidad = false;

        [ObservableProperty]
        private bool filtrarPorActividad = false;
      
        

        private List<Socio> _todosLosSocios = new();
        
        public IRelayCommand BuscarCommand { get; }
        public IRelayCommand LimpiarCommand { get; }
        public IRelayCommand ToggleFiltrosCommand { get; }

        public BuscarSociosViewModel()
        {
            BuscarCommand = new RelayCommand(OnBuscar);
            LimpiarCommand = new RelayCommand(OnLimpiar);
            ToggleFiltrosCommand = new RelayCommand(OnToggleFiltros);
            _ = InicializarAsync();

            // no state filter handling
        }

        private async Task InicializarAsync()
        {
           OnBuscar();
        }

        partial void OnSearchTextChanged(string value)
        {
            // Ejecutar búsqueda automática si lo deseas

        }
        partial void OnFiltrarPorNombreChanged(bool value) => ActivarDesactivarFiltrosSegunNombre();

        private void ActivarDesactivarFiltrosSegunNombre()
        {
            if (FiltrarPorNombre)
            {
                // keep other filters unchanged
            }
        }
        private void ActivarDesactivarFiltrosSegunDni()
        {
            if (FiltrarPorDni)
            {
                // keep other filters unchanged
            }

        }
        //OnfiltrarActividad
        private void ActivarDesactivarFiltrosSegunActividad()
        {
            if (FiltrarPorActividad)
            {
                // keep other filters unchanged
            }
        }
        //OnFiltrarLocalidad
        private void ActivarDesactivarFiltrosSegunLocalidad()
        {
            if (FiltrarPorLocalidad)
            {
                // keep other filters unchanged
            }
        }
        partial void OnFiltrarPorDniChanged(bool value) => ActivarDesactivarFiltrosSegunDni();
        partial void OnFiltrarPorActividadChanged(bool value) => ActivarDesactivarFiltrosSegunActividad();
        partial void OnFiltrarPorLocalidadChanged(bool value) => ActivarDesactivarFiltrosSegunLocalidad();

        // Los cambios en el filtro disparan la búsqueda
        private async void OnBuscar()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;


                FilterSocioDTO filtro = new()
                {
                    SearchText = this.SearchText,
                    ForNombre = this.FiltrarPorNombre,
                    ForDni = this.FiltrarPorDni,
                    ForLocalidad = this.FiltrarPorLocalidad,
                    ForActividad = this.FiltrarPorActividad,
                    // no estado filter
                };
                var sociosFiltrados = await _socioService.GetWithFilterAsync(filtro);

                Socios = sociosFiltrados != null ?
                                        new ObservableCollection<Socio>(sociosFiltrados)
                                        : new ObservableCollection<Socio>();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnLimpiar()
        {
            SearchText = string.Empty;
            // Mantener los filtros pero ejecutar búsqueda limpia
            OnBuscar();
        }

        private void OnToggleFiltros()
        {
            MostrarFiltros = !MostrarFiltros;
        }
    }
}
