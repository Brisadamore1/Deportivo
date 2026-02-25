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

        public IAsyncRelayCommand RegistrarseCommand { get; }
        public IAsyncRelayCommand VolverCommand { get; }


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
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private bool isBusy;

        public RegistrarseViewModel()
        {
            RegistrarseCommand = new AsyncRelayCommand(RegistrarseAsync, CanRegistrarse);
            VolverCommand = new AsyncRelayCommand(OnVolverAsync);
        }

        private bool CanRegistrarse()
        {
            if (!IsBusy)
            {
                // Validar email con expresión regular simple
                var emailPattern = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
                var isEmailValid = !string.IsNullOrEmpty(Mail) && Regex.IsMatch(Mail, emailPattern, RegexOptions.IgnoreCase);

                return !string.IsNullOrEmpty(Nombre) &&
                       isEmailValid &&
                       !string.IsNullOrEmpty(Password) &&
                       !string.IsNullOrEmpty(VerifyPassword) &&
                       Password.Length >= 6 &&
                       Password == VerifyPassword &&
                       !string.IsNullOrEmpty(Dni) &&
                       !string.IsNullOrEmpty(Telefono);
            }
            return false;
        }

        private async Task OnVolverAsync()
        {
            ResetForm();
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void ResetForm()
        {
            Nombre = string.Empty;
            Mail = string.Empty;
            Dni = string.Empty;
            Telefono = string.Empty;
            Password = string.Empty;
            VerifyPassword = string.Empty;
            TipoRol = TipoRolEnum.Administrativo;

            // Notificar cambios para actualizar el estado del botón
            RegistrarseCommand?.NotifyCanExecuteChanged();
        }

        private async Task RegistrarseAsync()
        {
            if (IsBusy) return;
            if (!CanRegistrarse()) return;

            try
            {
                IsBusy = true;
                RegistrarseCommand?.NotifyCanExecuteChanged();

                // create auth user via backend
                var created = await _authService.CreateUserWithEmailAndPasswordAsync(Mail, Password, Nombre);
                if (!created)
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
                    Password = Password.GetHashSha256(),
                    Telefono = Telefono,
                };

                await _usuarioService.AddAsync(nuevoUsuario);

                await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada. Debe verificar su correo electrónico.", "Ok");

                ResetForm();
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (FirebaseAuthException error)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Problema: " + error.Reason, "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Error: " + ex.Message, "Ok");
                await _authService.DeleteUser(new LoginDTO { Password = Password, Username = Mail }); 
            }
            finally
            {
                IsBusy = false;
                RegistrarseCommand?.NotifyCanExecuteChanged();
            }
        }
    }
}
