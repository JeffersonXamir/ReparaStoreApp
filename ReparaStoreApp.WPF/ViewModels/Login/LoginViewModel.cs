using Caliburn.Micro;
using ReparaStoreApp.Security.Security;
using ReparaStoreApp.WPF.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Login
{
    public class LoginViewModel : Screen
    {
        //private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IWindowManager _windowManager;
        private string _username;
        private string _password;
        private bool _isLoading;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
                //NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                //NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; NotifyOfPropertyChange(() => Password); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; NotifyOfPropertyChange(() => ErrorMessage); }
        }

        //public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);


        public LoginViewModel(IAuthService authService, IWindowManager windowManager)
        {
            _authService = authService;
            _windowManager = windowManager;
        }

        public async Task Login()
        {
            try
            {
                var result = await _authService.AuthenticateAsync(Username, Password);

                if (result.Success)
                {
                    //// Guardar token en configuración de aplicación
                    //Properties.Settings.Default.AuthToken = result.Token;
                    //Properties.Settings.Default.UserId = result.UserId;
                    //Properties.Settings.Default.Save();
                    // Guardar token en configuración (forma correcta para WPF)
                    var settings = new Settings();
                    settings.AuthToken = result.Token;
                    settings.UserId = result.UserId;
                    settings.Save();


                    var shell = (ShellViewModel)Parent;
                    shell.OnLoginSuccess();
                    await TryCloseAsync();
                }
                else
                {
                    ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error de conexión con el servidor";
                // Log.Error(ex, "Error en Login");
            }

            //if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) { return; }

            //var user = await _userService.Authenticate(Username, Password);

            //if (user == null)
            //{
            //    ErrorMessage = "Usuario o contraseña incorrectos";
            //    return;
            //}

            //// Aquí iría la lógica de autenticación real
            //if (Username == "admin" && Password == "admin")
            //{

            //    IsLoading = true;
            //    ErrorMessage = string.Empty;

            //    var shell = (ShellViewModel)Parent;
            //    shell.OnLoginSuccess();
            //    await TryCloseAsync();
            //}
            //else
            //{
            //    var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            //    {
            //        Title = "Error",
            //        Content = "Credenciales incorrectas",
            //        CloseButtonText = "Aceptar",
            //    };

            //    _ = await uiMessageBox.ShowDialogAsync();
            //    //MessageBox.Show("Credenciales incorrectas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
    }
}