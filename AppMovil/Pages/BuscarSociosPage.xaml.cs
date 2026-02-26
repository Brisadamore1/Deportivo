using AppMovil.ViewModels;
using Microsoft.Maui.Controls;

namespace AppMovil.Pages
{
    public partial class BuscarSociosPage : ContentPage
    {
        public BuscarSociosPage()
        {
            InitializeComponent();
            // Suscribir el evento Completed para Enter
            searchEntry.Completed += SearchEntry_Completed;
        }

        private void SearchEntry_Completed(object? sender, System.EventArgs e)
        {
            // Quitar foco para cerrar teclado y evitar comportamientos inconsistentes
            try { searchEntry?.Unfocus(); } catch { }

            if (BindingContext is BuscarSociosViewModel vm)
            {
                if (vm.BuscarCommand?.CanExecute(null) ?? false)
                    vm.BuscarCommand.Execute(null);
            }
        }
    }
}
