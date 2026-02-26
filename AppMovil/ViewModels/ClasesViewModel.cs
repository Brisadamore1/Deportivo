using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.Models;
using Service.Services;
using System.Collections.ObjectModel;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace AppMovil.ViewModels
{
    public partial class ClasesViewModel : ObservableObject
    {
        private readonly GenericService<Clase> _claseService = new GenericService<Clase>();
        private int _currentLoadId = 0;

        public IAsyncRelayCommand VolverCommand { get; }

        [ObservableProperty]
        private bool isBusy;

        // Exponer la colección preparada para la UI como diccionarios (sin crear nuevas clases)
        [ObservableProperty]
        private ObservableCollection<System.Collections.Generic.Dictionary<string, object>> clases = new();

        // Mostrar mensaje vacío solo cuando no está cargando y no hay clases
        public bool ShowEmpty => !IsBusy && (Clases == null || Clases.Count == 0);

        [ObservableProperty]
        private string titulo = string.Empty;

        public ClasesViewModel()
        {
            // Esperar a que la página le pase la ActividadId en OnAppearing
            VolverCommand = new AsyncRelayCommand(OnVolverAsync);
        }

        private async Task OnVolverAsync()
        {
            await Shell.Current.GoToAsync("//BuscarActividadesPage");
        }

        public async Task LoadForActivity(int actividadId, string actividadNombre)
        {
            // Start a new load token. This lets us discard results from earlier requests.
            var token = Interlocked.Increment(ref _currentLoadId);

            // Show the new title immediately only if provided (avoid overwriting a previous title with empty)
            if (!string.IsNullOrWhiteSpace(actividadNombre))
            {
                Titulo = actividadNombre;
            }
            // Clear previous items to avoid showing stale data
            Clases = new ObservableCollection<System.Collections.Generic.Dictionary<string, object>>();

            // Mark busy for current load
            IsBusy = true;

            try
            {
                var all = await _claseService.GetAllAsync();

                // If another load started meanwhile, discard these results
                if (token != _currentLoadId) return;

                // Order days so that Monday appears first (Mon, Tue, ... Sun) and then by start time
                var filtered = all?.Where(c => c.ActividadId == actividadId)
                                   .OrderBy(c => (((int)c.DiaSemana + 6) % 7)) // maps Monday=0, ..., Sunday=6
                                   .ThenBy(c => c.HoraInicio)
                                   .ToList() ?? new System.Collections.Generic.List<Clase>();

                // Single entry per Clase: day, start, end, cupo and activa
                var display = filtered.Select(c => new System.Collections.Generic.Dictionary<string, object>
                {
                    ["DiaSemanaEsp"] = DiaSemanaEnEsp(c.DiaSemana),
                    ["HoraInicio"] = c.HoraInicio,
                    ["HoraFin"] = c.HoraFin,
                    ["CupoMaximo"] = c.CupoMaximo,
                    ["Activa"] = c.Activa
                }).ToList();

                Clases = new ObservableCollection<System.Collections.Generic.Dictionary<string, object>>(display);
                // Only set title if this load is the latest and a non-empty name was provided
                if (!string.IsNullOrWhiteSpace(actividadNombre))
                {
                    Titulo = actividadNombre;
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                // Only clear IsBusy if this is the last load
                if (token == _currentLoadId) IsBusy = false;
            }
        }

        private string DiaSemanaEnEsp(System.DayOfWeek d)
        {
            return d switch
            {
                System.DayOfWeek.Sunday => "Domingo",
                System.DayOfWeek.Monday => "Lunes",
                System.DayOfWeek.Tuesday => "Martes",
                System.DayOfWeek.Wednesday => "Miércoles",
                System.DayOfWeek.Thursday => "Jueves",
                System.DayOfWeek.Friday => "Viernes",
                System.DayOfWeek.Saturday => "Sábado",
                _ => d.ToString()
            };
        }

        // Notificar ShowEmpty cuando cambian IsBusy o Clases
        partial void OnIsBusyChanged(bool value)
        {
            OnPropertyChanged(nameof(ShowEmpty));
        }

        partial void OnClasesChanged(ObservableCollection<System.Collections.Generic.Dictionary<string, object>> value)
        {
            OnPropertyChanged(nameof(ShowEmpty));
        }
    }
}
