using Caliburn.Micro;
using ReparaStoreApp.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using Wpf.Ui.Appearance;

namespace ReparaStoreApp.WPF.ViewModels.Configuracion
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IThemeService _themeService;
        private readonly IWindowManager _windowManager;

        public SettingsViewModel(IThemeService themeService, IWindowManager windowManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _themeService = themeService;
            _windowManager = windowManager;

            ThemeOptions = new BindableCollection<ThemeOption>
            {
                new ThemeOption { DisplayName = "Claro", Value = ApplicationTheme.Light },
                new ThemeOption { DisplayName = "Oscuro", Value = ApplicationTheme.Dark },
                new ThemeOption { DisplayName = "Sistema", Value = ApplicationTheme.HighContrast }
            };

            SelectedThemeOption = ThemeOptions.FirstOrDefault(x => x.Value == _themeService.GetTheme());
        }

        public BindableCollection<ThemeOption> ThemeOptions { get; }

        private ThemeOption _selectedThemeOption;
        public ThemeOption SelectedThemeOption
        {
            get => _selectedThemeOption;
            set
            {
                if (_selectedThemeOption != value)
                {
                    _selectedThemeOption = value;
                    NotifyOfPropertyChange(() => SelectedThemeOption);

                    if (_selectedThemeOption != null)
                    {
                        //_themeService.SetTheme(_selectedThemeOption.Value);
                        //ApplicationThemeManager.Apply(_selectedThemeOption.Value);
                        _themeService.SetTheme(_selectedThemeOption.Value);
                    }
                }
            }
        }

        public override Task<ValidateForm> ValidateForm()
        {
            throw new NotImplementedException();
        }
    }
}
