using AppMovil.ViewModels;
using Service.Models;
using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace AppMovil
{
    public partial class AppShell : Shell
    {
        public AppShellViewModel ViewModel => (AppShellViewModel)BindingContext;

        public AppShell()
        {
            InitializeComponent();
            // Asegurar que el item 'Inicio' esté seleccionado por defecto
            SelectFlyoutItemForRoute("MainPage");

            // Actualizar selección cuando cambie la navegación
            this.Navigated += AppShell_Navigated;
        }

        // Método público para cambiar el estado de login desde otras páginas
        public void SetLoginState(bool isLoggedIn)
        {
            ViewModel.SetLoginState(isLoggedIn);

        }

        //Método para saber que usuario se ha logueado
        public void SetUserLogin(Usuario usuario)
        {
            ViewModel.SetUserLogin(usuario);
        }

        // Handlers para botones del flyout (si existen en XAML)
        private async void OnNavInicioClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void OnNavBuscarActividadesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//BuscarActividadesPage");
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void OnNavBuscarSociosClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//BuscarSociosPage");
            Shell.Current.FlyoutIsPresented = false;
        }

        private void AppShell_Navigated(object? sender, ShellNavigatedEventArgs e)
        {
            try
            {
                var location = e?.Current?.Location?.ToString() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(location))
                {
                    if (location.Contains("MainPage"))
                        SelectFlyoutItemForRoute("MainPage");
                    else if (location.Contains("BuscarActividadesPage"))
                        SelectFlyoutItemForRoute("BuscarActividadesPage");
                    else if (location.Contains("BuscarSociosPage"))
                        SelectFlyoutItemForRoute("BuscarSociosPage");
                    else
                        SelectFlyoutItemForRoute("MainPage");
                }
            }
            catch
            {
                // ignore
            }
        }

        private void SelectFlyoutItemForRoute(string route)
        {
            try
            {
                var flyoutItem = this.Items
                    .OfType<FlyoutItem>()
                    .FirstOrDefault(fi => fi.Items.OfType<ShellContent>().Any(sc => sc.Route == route));

                if (flyoutItem != null)
                {
                    this.CurrentItem = flyoutItem;
                }
            }
            catch
            {
                // ignore
            }
        }
    }
}
