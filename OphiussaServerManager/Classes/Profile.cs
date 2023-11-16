using Newtonsoft.Json;
using OphiussaServerManager.SupportedServers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Profiles
{
    public class Profile
    {
        [JsonProperty("Key")]
        public string Key { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Version")]
        public string Version { get; set; }
        [JsonProperty("InstallLocation")]
        public string InstallLocation { get; set; }
        [JsonProperty("Type")]
        public SupportedServersType Type { get; set; }
        [JsonProperty("Configuration")]
        public ArkProfile Configuration { get; set; }

        public Profile() { }

        public Profile(string key, string name, SupportedServersType type)
        {
            this.Key = key;
            this.Name = name;
            this.Type = type;
            switch (type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                    this.Configuration = new ArkProfile();
                    this.Configuration.LoadNewArkProfile(key);
                    break;
                case EnumServerType.ArkSurviveAscended:
                    this.Configuration = new ArkProfile();
                    this.Configuration.LoadNewArkProfile(key);
                    break;
            }
            LoadProfile();
        }

        public Profile(string key, string name, SupportedServersType type, dynamic configuration)
        {
            this.Key = key;
            this.Name = name;
            this.Type = type;
            this.Configuration = configuration;
            LoadProfile(false);
        }

        public void SaveProfile()
        {
            string fileName = "config.json";
            Classes.Settings settings = JsonConvert.DeserializeObject<Classes.Settings>(File.ReadAllText(fileName));
            string dir = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string jsonString = JsonConvert.SerializeObject(this);
            File.WriteAllText(dir + this.Key + ".json", jsonString);
        }

        public void LoadProfile(bool readFileDisk = true)
        {
            string fileName = "config.json";
            Classes.Settings settings = JsonConvert.DeserializeObject<Classes.Settings>(File.ReadAllText(fileName));
            string dir = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                string jsonString = JsonConvert.SerializeObject(this);
                File.WriteAllText(dir + this.Key + ".json", jsonString);
            }
            if (File.Exists(dir + this.Key + ".json") && readFileDisk)
            {
                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(dir + this.Key + ".json"));

                this.Configuration = p.Configuration;
            }
        }
    }
}
