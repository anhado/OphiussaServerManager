﻿using System;
using OphiussaFramework.Models;

namespace OphiussaFramework.Extensions {
    public static class PluginExtensions {
        public static PluginController Clone(this PluginController          ctrl,
                                             EventHandler<InstallEventArgs> installServerClick = null,
                                             EventHandler<InstallEventArgs> backupServerClick  = null,
                                             EventHandler<InstallEventArgs> StopServerClick    = null,
                                             EventHandler<InstallEventArgs> startServerClick   = null) {
            return new PluginController(ctrl.PluginLocation(), installServerClick, backupServerClick, StopServerClick, startServerClick);
        }
    }
}