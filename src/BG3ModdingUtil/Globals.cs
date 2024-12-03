using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG3ModdingUtil
{
    //public static class Globals
    //{
    //    public static string AppLocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    //    public static string BG3Folder = System.IO.Path.Combine(AppLocalFolder, @"Larian Studios\Baldur's Gate 3");
    //    public static string BG3ModsFolder = System.IO.Path.Combine(BG3Folder, "Mods");
    //    public static string ModSettingsFolder = System.IO.Path.Combine(BG3Folder, @"PlayerProfiles\Public");
    //    public static string ModDirectory = Environment.CurrentDirectory;
    //    public static string BG3LoadOrders = System.IO.Path.Combine(ModDirectory, "Load Orders");
    //    public static string AppLog = System.IO.Path.Combine(ModDirectory, "BGModUtil.log");
    //    public static string ModsFolder = System.IO.Path.Combine(ModDirectory, "Mods");
    //    public static string GameDataFolder = System.IO.Path.Combine(ModDirectory, "GameData");
    //    public static string GameDataBin = System.IO.Path.Combine(GameDataFolder, "bin");
    //    public static string GameDataData = System.IO.Path.Combine(GameDataFolder, "Data");
    //    public static string SteamFolder = File.ReadAllText(System.IO.Path.Combine(ModDirectory, "BG3Config.cfg"));
    //    public static string SteamBINFolder = System.IO.Path.Combine(SteamFolder, "bin");
    //    public static string SteamDataFolder = System.IO.Path.Combine(SteamFolder, "Data");

    //    public static void MakeExceptionReport(StringBuilder stringBuilder)
    //    {
    //        string logFolder = Path.Combine(ModDirectory, "ErrorLogs");
    //        Directory.CreateDirectory(logFolder);

    //        string fileName = $"BGMU_Exception_Report_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
    //        string fullPath = Path.Combine(logFolder, fileName);
    //        using (FileStream fileStream = new(fullPath, FileMode.CreateNew))
    //        {
    //            using (StreamWriter streamWriter = new(fileStream))
    //            {
    //                streamWriter.Write(stringBuilder.ToString());
    //                streamWriter.Flush();
    //                streamWriter.Close();
    //            }
    //            fileStream.Close();
    //        }
    //    }
    //}

    public static class Globals
    {
        //AppData BG3 Folders
        public static string AppLocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string BG3Folder = System.IO.Path.Combine(AppLocalFolder, @"Larian Studios\Baldur's Gate 3");
        public static string BG3ModsFolder = System.IO.Path.Combine(BG3Folder, "Mods");
        public static string BG3ModSettingsLsx = System.IO.Path.Combine(BG3Folder, @"PlayerProfiles\Public\modsettings.lsx");

        //Folders from this Tool

        //ModProfiles/X/Mods
        //ModProfiles/X/GameData
        //ModProfiles/X/bin
        //ModProfiles/X/X.lsx

        //Vanilla/X/Mods
        //Vanilla/X/GameData
        //Vanilla/X/bin
        //Vanilla/X/X.lsx


        public static string UtilDirectory = Environment.CurrentDirectory;
        public static string UtilLogsDirectory = System.IO.Path.Combine(UtilDirectory, "Logs");
        public static string UtilAppLog = System.IO.Path.Combine(UtilLogsDirectory, "BGModUtil.log");
        public static string UtilModProfilesFolder = System.IO.Path.Combine(UtilDirectory, "ModProfiles");
        public static string UtilVanillaProfileFolder = System.IO.Path.Combine(UtilDirectory, "Vanilla");



        //public static string UtilLoadOrders = System.IO.Path.Combine(UtilDirectory, "Load Orders");
        
        //public static string UtilModProfilesFolder = System.IO.Path.Combine(UtilDirectory, "ModProfiles");
        //public static string UtilGameDataFolder = System.IO.Path.Combine(UtilDirectory, "GameData");
        //public static string UtilGameDataBin = System.IO.Path.Combine(UtilGameDataFolder, "bin");
        //public static string UtilGameDataData = System.IO.Path.Combine(UtilGameDataFolder, "Data");

        //Folder containg BG3 Install Game files/.exe
        public static string SteamFolder = @"C:\Program Files (x86)\Steam\steamapps\common\Baldurs Gate 3";
        public static string SteamBINFolder = System.IO.Path.Combine(SteamFolder, "bin");
        public static string SteamDataFolder = System.IO.Path.Combine(SteamFolder, "Data");

        
    }
}
