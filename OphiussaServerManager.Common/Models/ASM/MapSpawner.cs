using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
    public class MapSpawner {
        public string ClassName { get; set; } = "";

        public string Mod { get; set; } = "";

        public bool KnownSpawner { get; set; } = false;

        public string DisplayName => GameData.FriendlyMapSpawnerNameForClass(ClassName);

        public string DisplayMod => GameData.FriendlyNameForClass($"Mod_{Mod}", true) ?? Mod;

        public MapSpawner Duplicate() {
            var properties = GetType().GetProperties();

            var result = new MapSpawner();
            foreach (var prop in properties)
                if (prop.CanWrite)
                    prop.SetValue(result, prop.GetValue(this));

            return result;
        }
    }
}