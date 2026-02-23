using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.DTOs;
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
        SociosService _socioservice = new();

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool filtrarPorDni = false;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<Socio> _socios = new();
        
        public IRelayCommand BuscarCommand { get; }
        public IRelayCommand LimpiarCommand { get; }

        public BuscarSociosViewModel()
        {
            BuscarCommand = new RelayCommand(OnBuscar);
            LimpiarCommand = new RelayCommand(OnLimpiar);
            _ = InicializarAsync();
        }

        private async Task InicializarAsync()
        {
            OnBuscar();
        }

        partial void OnSearchTextChanged(string value)
        {
            // Ejecutar búsqueda automática si lo deseas

        }
        // Los cambios en el filtro disparan la búsqueda
        private async void OnBuscar()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var filtro = new FilterSocioDTO {
                    SearchText = this.SearchText,
                    ForDni = this.FiltrarPorDni
                };
                var resultado = await _socioservice.GetWithFilterAsync(filtro);

                Socios = resultado != null ? new ObservableCollection<Socio>(resultado) : new ObservableCollection<Socio>();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnLimpiar()
        {
            SearchText = string.Empty;
            OnBuscar();
        }
    }
}
