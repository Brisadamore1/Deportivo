using AppMovil.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace AppMovil.Pages
{
    [QueryProperty(nameof(ActividadId), "actividadId")]
    [QueryProperty(nameof(ActividadNombre), "actividadNombre")]
    public partial class ClasesPage : ContentPage
    {
        public ClasesPage()
        {
            InitializeComponent();
        }

        // Shell query parameters (actividadId, actividadNombre) serán seteados por la navegación
        private string actividadId = string.Empty;
        public string ActividadId
        {
            get => actividadId;
            set
            {
                actividadId = value;
                _ = ActividadIdChangedAsync(value);
            }
        }

        private string actividadNombre = string.Empty;
        public string ActividadNombre
        {
            get => actividadNombre;
            set
            {
                actividadNombre = value;
                _ = ActividadNombreChangedAsync(value);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // If ActividadId was set before appearing, load now
            if (int.TryParse(ActividadId, out var id) && BindingContext is ClasesViewModel vm)
            {
                await vm.LoadForActivity(id, ActividadNombre ?? string.Empty);
            }
        }

        private async Task ActividadIdChangedAsync(string value)
        {
            if (!int.TryParse(value, out var id)) return;
            if (BindingContext is ClasesViewModel vm)
            {
                // Always load data for new id using current nombre
                await vm.LoadForActivity(id, ActividadNombre ?? string.Empty);
            }
        }

        private Task ActividadNombreChangedAsync(string value)
        {
            // Only update the title immediately; do not attempt to reload data here.
            if (BindingContext is ClasesViewModel vm)
            {
                vm.Titulo = value ?? string.Empty;
            }
            return Task.CompletedTask;
        }

        private async void OnVolverClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
