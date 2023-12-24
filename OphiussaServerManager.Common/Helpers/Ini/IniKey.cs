namespace OphiussaServerManager.Common.Ini {
    public class IniKey {
        public string KeyName;
        public string KeyValue;

        public IniKey() {
            KeyName  = string.Empty;
            KeyValue = string.Empty;
        }

        public override string ToString() {
            return KeyName + "=" + KeyValue;
        }
    }
}