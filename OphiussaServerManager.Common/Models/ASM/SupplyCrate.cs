using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
    public class SupplyCrate {
        public string ClassName { get; set; } = "";

        public string Mod { get; set; } = "";

        public bool KnownSupplyCrate { get; set; } = false;

        public string DisplayName => GameData.FriendlySupplyCrateNameForClass(ClassName);

        public string DisplayMod => GameData.FriendlyNameForClass($"Mod_{Mod}", true) ?? Mod;

        public SupplyCrate Duplicate() {
            var properties = GetType().GetProperties();

            var result = new SupplyCrate();
            foreach (var prop in properties)
                if (prop.CanWrite)
                    prop.SetValue(result, prop.GetValue(this));

            return result;
        }
    }
}