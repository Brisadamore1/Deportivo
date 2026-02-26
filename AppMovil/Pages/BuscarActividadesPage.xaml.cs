using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;

namespace AppMovil.Pages
{
    public partial class BuscarActividadesPage : ContentPage
    {
        public BuscarActividadesPage()
        {
            InitializeComponent();
            // Ejecutar búsqueda al presionar Enter en el entry
            searchEntry.Completed += SearchEntry_Completed;
        }

        private void SearchEntry_Completed(object? sender, System.EventArgs e)
        {
            if (BindingContext is AppMovil.ViewModels.BuscarActividadesViewModel vm)
            {
                if (vm.BuscarCommand?.CanExecute(null) ?? false)
                    vm.BuscarCommand.Execute(null);
            }
        }

        // Helper para exponer el comando desde el BindingContext de forma conveniente en XAML
        //public object BindingContextOrSelf => this.BindingContext ?? this;
    }
}