using Nucs.JsonSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG3ModdingUtil
{
    class ConfigSettings : JsonSettings
    {
        public override string FileName { get; set; } = "Bg3ModdingUtil.config.json";

        public string GameDataFolder { get; set; }
        public string LoadOrderFolder { get; set; }
        public string ModsFolder { get; set; }
        public string BG3SteamFolder { get; set; }
    }
}
