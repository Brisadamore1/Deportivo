using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Service.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppMovil.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoggedIn;

        public Usuario? Usuario { get; private set; }


        public IRelayCommand LogoutCommand { get; }

        public AppShellViewModel()
        {
            LogoutCommand = new RelayCommand(OnLogout);
            SetLoginState(false); // Inicialmente no está logueado
        }

        public void SetLoginState(bool isLoggedIn)
        {
            if (Application.Current?.MainPage is AppShell shell)
            {
                if (isLoggedIn)
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout; //Menu izquierdo habilitado cuando el usuario se ha logueado
                else
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled; //Menu izquierdo deshabilitado cuando el usuario no se ha logueado

                IsLoggedIn = isLoggedIn; // Actualiza la propiedad IsLoggedIn para reflejar el estado de inicio de sesión y activar/desactivar el menú lateral
                if (isLoggedIn)
                    Shell.Current.GoToAsync("//MainPage");  // Cambio a MainPage (pantalla de inicio)
                else
                    Shell.Current.GoToAsync("//LoginPage");
            }
                
        }

        //Método para saber que usuario se ha logueado
        public void SetUserLogin(Usuario usuario)
        {
            Usuario = usuario; //Se escribe en la pantalla quién es el usuario logueado.
        }

        private void OnLogout()
        {
            SetLoginState(false);
        }

    }
}