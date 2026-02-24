    using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
    using System.Text.RegularExpressions;
using Service.DTOs;
using Service.Services;

namespace AppMovil.ViewModels
{
    public partial class RecuperarPasswordViewModel : ObservableObject
    {
        AuthService authService = new AuthService();
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnviarCommand))] //Cuando cambia el valor de Mail, se notifica que el comando EnviarCommand debe reevaluar si puede ejecutarse o no.
        private string mail = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnviarCommand))]
        private bool isBusy = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasSuccess))]
        private string successMessage = string.Empty;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
        public bool HasSuccess => !string.IsNullOrWhiteSpace(SuccessMessage);

        public IRelayCommand EnviarCommand { get; }
        public IRelayCommand VolverCommand { get; }

        public RecuperarPasswordViewModel()
        {
            EnviarCommand = new AsyncRelayCommand(OnEnviar, CanEnviar);
            VolverCommand = new AsyncRelayCommand(OnVolver);
        }

        private bool CanEnviar()
        {
            return !string.IsNullOrWhiteSpace(Mail) && IsValidEmail(Mail) && !IsBusy;
        }

        private bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // Simple, robust email regex
            var pattern = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public bool CanEnviarBindable => CanEnviar();

        partial void OnMailChanged(string value)
        {
            OnPropertyChanged(nameof(CanEnviarBindable));
            EnviarCommand.NotifyCanExecuteChanged();
        }

        partial void OnIsBusyChanged(bool value)
        {
            // Notify CanEnviarBindable when IsBusy changes
            OnPropertyChanged(nameof(CanEnviarBindable));
            EnviarCommand.NotifyCanExecuteChanged();
        }

        private async Task OnEnviar()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                SuccessMessage = string.Empty;

                // Validación básica de email
                if (!Mail.Contains("@") || !Mail.Contains("."))
                {
                    ErrorMessage = "Por favor, ingrese un correo electrónico válido";
                    return;
                }
                LoginDTO loginReset= new LoginDTO
                {
                    Username = Mail,
                    Password = "" // Placeholder, el backend debe manejar esto adecuadamente
                };

                var result = await authService.ResetPassword(loginReset);

                if (result)
                {
                    SuccessMessage = "Se enviaron las instrucciones a tu correo.";
                    // Notify computed property change
                    OnPropertyChanged(nameof(HasSuccess));
                    // Do not navigate away immediately so user can see the message
                }
                else
                {
                    ErrorMessage = "No se pudieron enviar las instrucciones. Intente nuevamente más tarde.";
                    OnPropertyChanged(nameof(HasError));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al enviar las instrucciones: {ex.Message}";
                OnPropertyChanged(nameof(HasError));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnVolver()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }


    }
}