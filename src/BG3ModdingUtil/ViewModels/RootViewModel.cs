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

        public RootViewModel()
        {
            ConfigSettings settings = JsonSettings.Load<ConfigSettings>();
            if(!Path.Exists(settings.BG3SteamFolder)&& !Path.Exists(settings.GameDataFolder)&&
                !Path.Exists(settings.LoadOrderFolder)&& !Path.Exists(settings.ModsFolder))
            {
                CurrentUserControl = new SettingsView();
            }
            else
            {
                CurrentUserControl = new MainView();
            }

        }

        [RelayCommand]
        public void ShowSettingsView()
        {
            CurrentUserControl = new SettingsView();
        }

        [RelayCommand]
        public void ShowMainView()
        {
            CurrentUserControl = new MainView();
        }

        [RelayCommand]
        public void ToggleView()
        {
            if(CurrentUserControl.GetType() == typeof(MainView))
            {
                CurrentUserControl = new SettingsView();
            }
            else
            {
                CurrentUserControl = new MainView();
            }
        }
    }
}
