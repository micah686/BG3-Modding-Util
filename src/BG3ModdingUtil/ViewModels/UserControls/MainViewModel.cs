using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nucs.JsonSettings;
using SSA.VirtualFileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BG3ModdingUtil.ViewModels.UserControls
{
    public partial class MainViewModel: ObservableObject
    {
        #region Properties
        private const string VANILLA_PROFILE = "Vanilla";
        private const string BG3_DX11 = "bg3_dx11.exe";
        private const string BG3_VULKAN = "bg3.exe";
        private const string RESHADE_CONFIG = "ReShade.ini";
        private const string MOD_PAKS = "Mods";
        private const string GAME_DATA = "GameData";
        private const string BIN = "Bin";

        private readonly VFileSystem _vfs = new();


        [ObservableProperty]
        private List<string> _modProfiles;
        [ObservableProperty]
        private int _modSelectedIndex =1;

        #endregion

        public MainViewModel()
        {
            InitPaths();
            GetModProfileNames();
        }

        private void InitPaths()
        {
            ConfigSettings settings = JsonSettings.Load<ConfigSettings>();

            Globals.UtilModProfilesFolder = settings.ModsFolder;
            Globals.SteamFolder = settings.BG3SteamFolder;
            Globals.SteamBINFolder = Path.Combine(settings.BG3SteamFolder, "bin");
            Globals.SteamDataFolder = Path.Combine(settings.BG3SteamFolder, "Data");

            Directory.CreateDirectory(Globals.UtilModProfilesFolder);
            Directory.CreateDirectory(Globals.UtilVanillaProfileFolder);

        }

        private void GetModProfileNames()
        {
            var modProfiles = GetModProfiles();
            List<string> names = new();
            foreach ( var modProfile in modProfiles )
            {
                names.Add(Path.GetFileNameWithoutExtension(modProfile));
            }
            ModProfiles = names;
        }

        private IEnumerable<string> GetModProfiles()
        {
            List<string> modProfiles = [VANILLA_PROFILE];
            var subfolders = Directory.GetDirectories(Globals.UtilModProfilesFolder);
            foreach (var subfolder in subfolders)
            {
                var lsxConfig = Directory.GetFiles(subfolder, "*.lsx", SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (lsxConfig != null)
                {
                    modProfiles.Add(lsxConfig);
                }
            }
            return modProfiles;
        }


        [RelayCommand]
        private void UseVanillaProfile()
        {
            RemoveInstall();
            var vanillaLsx = Path.Combine(Globals.UtilVanillaProfileFolder, $"{VANILLA_PROFILE}.lsx");
            _vfs.MakeSymbolicLink(vanillaLsx, Globals.BG3ModSettingsLsx);

            //TODO: include section for IncludeRoot and IncludeReshade
            ConfigSettings settings = JsonSettings.Load<ConfigSettings>();
            if (settings.UseVanillaReshade)
            {
                IncludeReshadeVanilla();
            }
            if(settings.UseVanillaPartyCam)
            {
                IncludePartyCamVanilla();
            }
        }

        [RelayCommand]
        private void UseModdedProfile()
        {
            var modLsx = GetModProfiles().ToArray()[ModSelectedIndex];
            var modDir = Path.GetDirectoryName(modLsx);
            if(string.IsNullOrEmpty(modDir))return;
            var modPaks = Directory.GetFiles(Path.Combine(modDir, MOD_PAKS));
            var profileBinFolders = Directory.GetDirectories(Path.Combine(modDir, BIN));
            var profileBinFiles = Directory.GetFiles(Path.Combine(modDir, BIN));
            var profileDataFolders = Directory.GetDirectories(Path.Combine(modDir, GAME_DATA));
            var profileDataFiles = Directory.GetFiles(Path.Combine(modDir, GAME_DATA));
            RemoveInstall();

            foreach (string file in modPaks)
            {
                FileInfo fileInfo = new(file);
                _vfs.MakeSymbolicLink(fileInfo.FullName, Globals.BG3ModsFolder);
            }
            foreach (string dir in profileBinFolders)
            {
                _vfs.MakeJunction(dir, Globals.SteamBINFolder);
            }
            foreach (string file in profileBinFiles)
            {
                FileInfo fileInfo = new(file);
                if (fileInfo.Name == BG3_VULKAN || fileInfo.Name == BG3_DX11)
                {
                    FileAttributes attr = fileInfo.Attributes;
                    if (!attr.HasFlag(FileAttributes.ReparsePoint))
                    {
                        _vfs.MakeSymbolicLink(file, Globals.SteamBINFolder);
                    }
                }
                else
                {
                    _vfs.MakeSymbolicLink(file, Globals.SteamBINFolder);
                }
            }
            foreach (string dir in profileDataFolders)
            {
                _vfs.MakeJunction(dir, Globals.SteamDataFolder);
            }
            foreach (string file in profileDataFiles)
            {
                _vfs.MakeSymbolicLink(file, Globals.SteamDataFolder);
            }
            if (File.Exists(Globals.BG3ModSettingsLsx))
            {
                File.Delete(Globals.BG3ModSettingsLsx);
            }
            _vfs.MakeSymbolicLink(modLsx, Globals.BG3ModSettingsLsx);

        }

        private void IncludeReshadeVanilla()
        {
            var profileBin = Path.Combine(Globals.UtilVanillaProfileFolder, BIN);
            var reshadeIni = Path.Combine(profileBin, RESHADE_CONFIG);
            if (!File.Exists(reshadeIni))
            {
                string reshadeshaders = Path.Combine(profileBin, "reshade-shaders");
                string reshadepresets = Path.Combine(profileBin, "reshade-presets");
                string reshadelog = Path.Combine(profileBin, "Reshade.log");
                _vfs.MakeJunction(reshadeshaders, Globals.SteamBINFolder);
                _vfs.MakeJunction(reshadepresets, Globals.SteamBINFolder);
                _vfs.MakeSymbolicLink(reshadeIni, Globals.SteamBINFolder);
                _vfs.MakeSymbolicLink(reshadelog, Globals.SteamBINFolder);
            }
        }

        private void IncludePartyCamVanilla()
        {            
            var vanillaBinFolders = Directory.GetDirectories(Path.Combine(Globals.UtilVanillaProfileFolder, BIN)).ToList();
            var vanillaDataFiles = Directory.GetFiles(Path.Combine(Globals.UtilVanillaProfileFolder, GAME_DATA)).ToList();

            if (vanillaBinFolders.Count != 0)
            {
                foreach (string dir in vanillaBinFolders)
                {
                    _vfs.MakeJunction(dir, Globals.SteamBINFolder);
                }
            }
            if (vanillaBinFolders.Count != 0)
            {
                foreach (string file in vanillaBinFolders)
                {
                    FileInfo fileInfo = new(file);
                    if (fileInfo.Name != "DWrite.dll")
                    {
                        _vfs.MakeSymbolicLink(file, Globals.SteamBINFolder);
                    }
                }
            }

            if (vanillaDataFiles.Count != 0)
            {
                foreach (string dir in vanillaDataFiles)
                {
                    DirectoryInfo directoryInfo = new(dir);
                    if (directoryInfo.Name == "Mods")
                    {
                        _vfs.MakeJunction(dir, Globals.SteamDataFolder);
                    }
                    else if (directoryInfo.Name == "PatchFiles")
                    {
                        _vfs.MakeJunction(dir, Globals.SteamDataFolder);
                    }
                }
            }
            if (vanillaDataFiles.Count != 0)
            {
                foreach (string file in vanillaDataFiles)
                {
                    FileInfo fileInfo = new(file);
                    if (fileInfo.Name == "PartyLimitBegonePatcher.bat")
                    {
                        _vfs.MakeSymbolicLink(file, Globals.SteamDataFolder);
                    }
                }
            }
        }

        private void RemoveInstall()
        {
            List<string> steambinfolders = Directory.GetDirectories(Globals.SteamBINFolder).ToList();
            List<string> steambinfiles = Directory.GetFiles(Globals.SteamBINFolder).ToList();
            List<string> steamdatafolders = Directory.GetDirectories(Globals.SteamDataFolder).ToList();
            List<string> steamdatafiles = Directory.GetFiles(Globals.SteamDataFolder).ToList();
            List<string> modfiles = Directory.GetFiles(Globals.BG3ModsFolder).ToList();

            foreach (string file in steambinfiles)
            {
                FileInfo fileInfo = new(file);
                FileAttributes attr = fileInfo.Attributes;
                if (attr.HasFlag(FileAttributes.ReparsePoint))
                {
                    _vfs.RemoveSymbolicLink(file);
                }
            }
            foreach (string dir in steambinfolders)
            {
                DirectoryInfo dirInfo = new(dir);
                FileAttributes attr = dirInfo.Attributes;
                if (attr.HasFlag(FileAttributes.ReparsePoint))
                {
                    _vfs.RemoveJunction(dir);
                }
            }
            foreach (string file in steamdatafiles)
            {
                FileInfo fileInfo = new(file);
                FileAttributes attr = fileInfo.Attributes;
                if (attr.HasFlag(FileAttributes.ReparsePoint))
                {
                    _vfs.RemoveSymbolicLink(file);
                }
            }
            foreach (string dir in steamdatafolders)
            {
                DirectoryInfo dirInfo = new(dir);
                FileAttributes attr = dirInfo.Attributes;
                if (attr.HasFlag(FileAttributes.ReparsePoint))
                {
                    _vfs.RemoveJunction(dir);
                }
            }
            foreach (string file in modfiles)
            {
                FileInfo fileInfo = new(file);
                FileAttributes attr = fileInfo.Attributes;
                if (attr.HasFlag(FileAttributes.ReparsePoint))
                {
                    _vfs.RemoveSymbolicLink(file);
                }
            }

            if (File.Exists(Globals.BG3ModSettingsLsx))
            {
                File.Delete(Globals.BG3ModSettingsLsx);
            }
        }


    }
}
