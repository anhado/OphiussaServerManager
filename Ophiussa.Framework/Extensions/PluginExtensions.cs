using System;
using OphiussaFramework.Models;

namespace OphiussaFramework.Extensions {
    public static class PluginExtensions {
        public static PluginController Clone(this PluginController           ctrl,
                                             EventHandler<OphiussaEventArgs> installServerClick = null,
                                             EventHandler<OphiussaEventArgs> backupServerClick  = null,
                                             EventHandler<OphiussaEventArgs> StopServerClick    = null,
                                             EventHandler<OphiussaEventArgs> startServerClick   = null,
                                             EventHandler<OphiussaEventArgs> saveClick          = null,
                                             EventHandler<OphiussaEventArgs> reloadClick        = null,
                                             EventHandler<OphiussaEventArgs> syncClick          = null,
                                             EventHandler<OphiussaEventArgs> openRCONClick      = null,
                                             EventHandler<OphiussaEventArgs> chooseFolderClick  = null,
                                             EventHandler<OphiussaEventArgs> TabHeadChangeEvent = null) {
            return new PluginController(ctrl.PluginLocation(), installServerClick, backupServerClick, StopServerClick, startServerClick, saveClick, reloadClick, syncClick, openRCONClick, chooseFolderClick, TabHeadChangeEvent);
        }
    }
}