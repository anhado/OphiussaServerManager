using System.ComponentModel;

namespace OphiussaServerManager.Common.Models {
    [DefaultValue(False)]
    public enum DinoBreedingable
    {
        False,
        True,
    }
    
    [DefaultValue(False)]
    public enum DinoTamable
    {
        False,
        True,
        ByBreeding,
        ByCrafting,
    }
}