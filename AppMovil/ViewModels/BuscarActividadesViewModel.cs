using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.DTOs;
using Service.Models;
using Service.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace AppMovil.ViewModels
{
    public partial class BuscarActividadesViewModel : ObservableObject
    {
        ActivityService _activityService = new ();
         
        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<Actividad> actividades = new();

        // Propiedades para los filtros
        [ObservableProperty]
        private bool filtrarPorNombre = true;

        [ObservableProperty]
        private bool filtrarPorProfesor = false;

        [ObservableProperty]
        private bool filtrarPorNivel = false;

        [ObservableProperty]
        private bool mostrarFiltros = false;

        private List<Actividad> _todasLasActividades = new();

        public IRelayCommand BuscarCommand { get; }
        public IRelayCommand LimpiarCommand { get; }
        public IRelayCommand ToggleFiltrosCommand { get; }
        public IRelayCommand VolverCommand { get; }

        public BuscarActividadesViewModel()
        {
            BuscarCommand = new RelayCommand(OnBuscar);
            LimpiarCommand = new RelayCommand(OnLimpiar);
            ToggleFiltrosCommand = new RelayCommand(OnToggleFiltros);
            VerActividadCommand = new RelayCommand<int?>(OnVerActividad);
            VolverCommand = new AsyncRelayCommand(OnVolver);
            _ = InicializarAsync();
        }

        public IRelayCommand<int?> VerActividadCommand { get; }

        private async void OnVerActividad(int? actividadId)
        {
            if (actividadId == null) return;
            var actividad = Actividades.FirstOrDefault(a => a.Id == actividadId.Value);
            var nombre = actividad?.Nombre ?? string.Empty;
            await Shell.Current.GoToAsync($"//ClasesPage?actividadId={actividadId}&actividadNombre={Uri.EscapeDataString(nombre)}");
        }

        private async Task InicializarAsync()
        {
            OnBuscar();
        }

        partial void OnSearchTextChanged(string value)
        {
            //if (string.IsNullOrEmpty(value)) OnBuscar();
        }

        // Los cambios en filtros también disparan nueva búsqueda
        partial void OnFiltrarPorNombreChanged(bool value) => ActivarDesactivarFiltrosSegunNombre();

        private void ActivarDesactivarFiltrosSegunNombre()
        {
            if (FiltrarPorNombre)
            {
                FiltrarPorNivel = false;
            }
        }
        private void ActivarDesactivarFiltrosSegunProfesor()
        {
            if (FiltrarPorProfesor)
            {
                FiltrarPorNivel = false;
            }
        }

        partial void OnFiltrarPorProfesorChanged(bool value) => ActivarDesactivarFiltrosSegunProfesor();
        partial void OnFiltrarPorNivelChanged(bool value) => ActivarDesactivarFiltrarSegunNivel();

        private void ActivarDesactivarFiltrarSegunNivel()
        {
            if (FiltrarPorNivel)
            {
                FiltrarPorNombre = false;
                FiltrarPorProfesor = false;
            }
            
        }

        private async void OnBuscar()
        {
            if (IsBusy) return; //Si se disparan varias búsquedas, solo la primera se ejecuta, las demás se ignoran hasta que termine la primera

            try
            {
                IsBusy = true;

                FilterActivityDTO filtro = new()
                {
                    SearchText = this.SearchText,
                    ForNombre = this.FiltrarPorNombre,
                    ForProfesor = this.FiltrarPorProfesor,
                    ForNivel = this.FiltrarPorNivel

                };
                // Obtener todos los libros si no los tenemos
                 var actividadesFiltradas= await _activityService.GetWithFilterAsync(filtro);

                Actividades = actividadesFiltradas != null ? 
                        new ObservableCollection<Actividad>(actividadesFiltradas)
                        : new ObservableCollection<Actividad>();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnVolver()
        {
            await Shell.Current.GoToAsync("//MainPage");
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
