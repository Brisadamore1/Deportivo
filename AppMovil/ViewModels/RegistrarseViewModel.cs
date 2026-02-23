using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Service.Enums;
using Service.ExtentionMethods;
using Service.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Linq;
using Service.Models;
using Service.DTOs;

namespace AppMovil.ViewModels
{
    public partial class RegistrarseViewModel : ObservableObject
    {
        private readonly AuthService _authService = new();
        private readonly UsuarioService _usuarioService = new();

        public IRelayCommand RegistrarseCommand { get; }
        public IRelayCommand VolverCommand { get; }


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string nombre = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string mail = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string dni = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string domicilio = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string telefono = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private TipoRolEnum tipoRol = TipoRolEnum.Administrativo;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string password = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string verifyPassword = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        public RegistrarseViewModel()
        {
            RegistrarseCommand = new RelayCommand(Registrarse, CanRegistrarse);
            VolverCommand = new AsyncRelayCommand(OnVolver);
        }

        private bool CanRegistrarse()
        {
            if (!IsBusy)
            {
                return !string.IsNullOrEmpty(Nombre) &&
                       !string.IsNullOrEmpty(Mail) &&
                       !string.IsNullOrEmpty(Password) &&
                       !string.IsNullOrEmpty(VerifyPassword) &&
                       Password.Length >= 6 &&
                       !string.IsNullOrEmpty(Dni) &&
                       !string.IsNullOrEmpty(Domicilio) &&
                       !string.IsNullOrEmpty(Telefono);
            }
            return false;
        }

        private async Task OnVolver()
        {
            if (Application.Current?.MainPage is AppShell shell)
            {
                await shell.GoToAsync("//LoginPage");
            }
        }

        private async void Registrarse()
        {
            if (IsBusy) return; //Al hacer doble click rápido, se evita que se ejecute el proceso varias veces
            IsBusy = true;
            if (Password != VerifyPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Las contraseñas ingresadas no coinciden", "Ok");
                IsBusy = false;
                return;
            }

            try
            {
                var userOk = await _authService.CreateUserWithEmailAndPasswordAsync(Mail, Password, Nombre);
                if (!userOk)
                {
                    await Application.Current.MainPage.DisplayAlert("Registrarse", "No se pudo crear el usuario", "Ok");
                    return;
                }

                var nuevoUsuario = new Usuario
                {
                    Nombre = Nombre,
                    Email = Mail,
                    TipoRol = TipoRol,
                    Dni = Dni,
                    Password = Password.GetHashSha256(), //Contraseña hasheada para mayor seguridad
                    Domicilio = Domicilio,
                    Telefono = Telefono,
                };

                await _usuarioService.AddAsync(nuevoUsuario);
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada!", "Ok");
                if (Application.Current?.MainPage is AppShell shell)
                {
                    await shell.GoToAsync("//LoginPage");
                }
            }
            catch (FirebaseAuthException error)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Problema: " + error.Reason, "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Error: " + ex.Message, "Ok");
                await _authService.DeleteUser(new LoginDTO() { Password=Password, Username=Mail});
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
