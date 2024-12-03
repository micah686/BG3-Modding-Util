using BG3ModdingUtil.Views;
using BG3ModdingUtil.Views.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nucs.JsonSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BG3ModdingUtil.ViewModels
{
    public partial class RootViewModel: ObservableObject
    {
        [ObservableProperty]
        private UserControl _currentUserControl;

        private UserControl _settingsView = new SettingsView();
        private UserControl _mainView = new MainView();

        public RootViewModel()
        {
            ConfigSettings settings = JsonSettings.Load<ConfigSettings>();
            if(!Path.Exists(settings.BG3SteamFolder)&& !Path.Exists(settings.ModsFolder))
            {
                CurrentUserControl = _settingsView;
            }
            else
            {
                CurrentUserControl = _mainView;
            }

        }

        [RelayCommand]
        public void ShowSettingsView()
        {
            CurrentUserControl = _settingsView;
        }

        [RelayCommand]
        public void ShowMainView()
        {
            CurrentUserControl = _mainView;
        }

        [RelayCommand]
        public void ToggleView()
        {
            if(CurrentUserControl.GetType() == typeof(MainView))
            {
                CurrentUserControl = _settingsView;
            }
            else
            {
                CurrentUserControl = _mainView;
            }
        }
    }
}
