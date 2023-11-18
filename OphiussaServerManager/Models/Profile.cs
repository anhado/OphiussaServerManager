using Newtonsoft.Json;
using OphiussaServerManager.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Models.Profiles
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
        public ArkProfile ARKConfiguration { get; set; }

        public Profile() { }

        public Profile(string key, string name, SupportedServersType type)
        {
            this.Key = key;
            this.Name = name;
            this.Type = type;
            switch (type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                    this.ARKConfiguration = new ArkProfile();
                    this.ARKConfiguration.LoadNewArkProfile(key);
                    break;
                case EnumServerType.ArkSurviveAscended:
                    this.ARKConfiguration = new ArkProfile();
                    this.ARKConfiguration.LoadNewArkProfile(key);
                    break;
            }
            LoadProfile();
        }

        public Profile(string key, string name, SupportedServersType type, dynamic configuration)
        {
            this.Key = key;
            this.Name = name;
            this.Type = type;
            this.ARKConfiguration = configuration;
            LoadProfile(false);
        }

        public void SaveProfile()
        {
            string fileName = "config.json";
            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
            string dir = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(dir + this.Key + ".json", jsonString);
        }

        public void LoadProfile(bool readFileDisk = true)
        {
            string fileName = "config.json";
            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
            string dir = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(dir + this.Key + ".json", jsonString);
            }
            if (File.Exists(dir + this.Key + ".json") && readFileDisk)
            {
                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(dir + this.Key + ".json"));

                this.ARKConfiguration = p.ARKConfiguration;
            }
        }

        public string GetProfileSaveGamesPath(Profile profile) => profile.GetProfileSaveGamesPath(profile?.InstallLocation);

        public string GetProfileSaveGamesPath(string installDirectory) => Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, this.Type.SaveGamesRelativePath);

        public string GetProfileSavePath(Profile profile) => profile.GetProfileSavePath(profile?.InstallLocation, profile?.ARKConfiguration.Administration.AlternateSaveDirectoryName);

        public string GetProfileSavePath(
          string installDirectory,
          string altSaveDirectoryName)
        {
            if (!string.IsNullOrWhiteSpace(altSaveDirectoryName))
            {
                return Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, altSaveDirectoryName);
            }
            return Path.Combine(installDirectory ?? string.Empty, this.Type.SavedFilesRelativePath);
        }
    }

}
