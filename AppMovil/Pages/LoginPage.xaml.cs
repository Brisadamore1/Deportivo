using Microsoft.Maui.Controls;
using AppMovil.ViewModels;

namespace AppMovil.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Al mostrar la página de login, limpiar la contraseña para mayor seguridad.
            if (BindingContext is LoginViewModel vm)
            {
                vm.Password = string.Empty;
            }
        }
    }
}
