namespace OphiussaServerManager.Common.Models {
    public interface IDefaultUserControl {
        void LoadData(ref ArkProfile profile);
        void GetData(ref  ArkProfile profile);
    }
}