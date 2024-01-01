using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
    public class PrimalItem {
        public string ClassName { get; set; } = "";

        public string Mod { get; set; } = "";

        public bool KnownItem { get; set; } = false;

        public string Category { get; set; } = "";

        public string DisplayName => GameData.FriendlyItemNameForClass(ClassName);

        public string DisplayMod => GameData.FriendlyNameForClass($"Mod_{Mod}", true) ?? Mod;

        public PrimalItem Duplicate() {
            var properties = GetType().GetProperties();

            var result = new PrimalItem();
            foreach (var prop in properties)
                if (prop.CanWrite)
                    prop.SetValue(result, prop.GetValue(this));

            return result;
        }
    }
}