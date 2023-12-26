using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FirewallManager;
using Open.Nat;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using OphiussaServerManager.Properties;
using Action = FirewallManager.Action;

namespace OphiussaServerManager.Forms {
    public partial class FrmPortForward : Form {
        public FrmPortForward() {
            InitializeComponent();
        }

        private void FrmPortForward_Load(object sender, EventArgs e) {
        }

        public async void LoadPortFoward(Dictionary<string, Profile> profiles) {
            try {
                var discoverer = new NatDiscoverer();
                var device     = await discoverer.DiscoverDeviceAsync();

                foreach (string key in profiles.Keys) {
                    var     profile            = profiles[key];
                    Mapping serverMapping      = null;
                    Mapping peerMapping        = null;
                    Mapping queryMapping       = null;
                    Mapping rconMapping        = null;
                    string  serverName         = string.Empty;
                    bool    firewallServerPort = false;
                    bool    firewallPeerPort   = false;
                    bool    firewallQueryPort  = false;
                    bool    firewallRconPort   = false;

                    bool useServerPort = false;
                    bool usePeerPort   = false;
                    bool useQueryPort  = false;
                    bool useRconPort   = false;

                    ushort vServerPort = 0;
                    ushort vPeerPort   = 0;
                    ushort vQueryPort  = 0;
                    ushort vRconPort   = 0;

                    switch (profile.Type.ServerType) {
                        case EnumServerType.ArkSurviveEvolved:
                        case EnumServerType.ArkSurviveAscended:
                            serverName = profile.ArkConfiguration.ServerName;

                            useServerPort = true;
                            usePeerPort   = true;
                            useQueryPort  = true;
                            useRconPort   = profile.ArkConfiguration.UseRcon;

                            serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ArkConfiguration.ServerPort.ToInt());
                            peerMapping   = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ArkConfiguration.PeerPort.ToInt());
                            queryMapping  = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ArkConfiguration.QueryPort.ToInt());
                            if (profile.ArkConfiguration.UseRcon) rconMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ArkConfiguration.RconPort.ToInt());

                            firewallServerPort = NetworkTools.IsPortOpen(profile.Name, profile.ArkConfiguration.ServerPort.ToInt());
                            firewallPeerPort   = NetworkTools.IsPortOpen(profile.Name, profile.ArkConfiguration.PeerPort.ToInt());
                            firewallQueryPort  = NetworkTools.IsPortOpen(profile.Name, profile.ArkConfiguration.QueryPort.ToInt());
                            if (profile.ArkConfiguration.UseRcon) firewallRconPort = NetworkTools.IsPortOpen(profile.Name, profile.ArkConfiguration.RconPort.ToInt());
                            vServerPort = profile.ArkConfiguration.ServerPort.ToUShort();
                            vPeerPort   = profile.ArkConfiguration.PeerPort.ToUShort();
                            vQueryPort  = profile.ArkConfiguration.QueryPort.ToUShort();
                            if (profile.ArkConfiguration.UseRcon) vRconPort = profile.ArkConfiguration.RconPort.ToUShort();
                            break;
                        case EnumServerType.Valheim:
                            serverName = profile.ValheimConfiguration.Administration.ServerName;

                            useServerPort = true;
                            usePeerPort   = true;
                            useQueryPort  = false;
                            useRconPort   = false;

                            serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ValheimConfiguration.Administration.ServerPort.ToInt());
                            peerMapping   = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ValheimConfiguration.Administration.PeerPort.ToInt());

                            firewallServerPort = NetworkTools.IsPortOpen(profile.Name, profile.ValheimConfiguration.Administration.ServerPort.ToInt());
                            firewallPeerPort   = NetworkTools.IsPortOpen(profile.Name, profile.ValheimConfiguration.Administration.PeerPort.ToInt());

                            vServerPort = profile.ValheimConfiguration.Administration.ServerPort.ToUShort();
                            vPeerPort   = profile.ValheimConfiguration.Administration.PeerPort.ToUShort();
                            break;
                    }

                    portForwardGridBindingSource.Add(new PortForwardGrid {
                                                                             Profile            = profile.Name,
                                                                             ServerName         = serverName,
                                                                             FirewallServerPort = firewallServerPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             FirewallPeerPort   = firewallPeerPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             FirewallQueryPort  = firewallQueryPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             FirewallRconPort   = firewallRconPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             RouterServerPort   = serverMapping != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             RouterPeerPort     = peerMapping   != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             RouterQueryPort    = queryMapping  != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             RouterRconPort     = rconMapping   != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon),
                                                                             ServerPort         = vServerPort,
                                                                             PeerPort           = vPeerPort,
                                                                             QueryPort          = vQueryPort,
                                                                             RconPort           = vRconPort,
                                                                             UseServerPort      = useServerPort,
                                                                             UsePeerPort        = usePeerPort,
                                                                             UseQueryPort       = useQueryPort,
                                                                             UseRconPort        = useRconPort,
                                                                             IsOk = (useServerPort ? firewallServerPort : true)
                                                                                 && (usePeerPort ? firewallPeerPort : true)
                                                                                 && (useQueryPort ? firewallQueryPort : true)
                                                                                 && (useRconPort ? firewallRconPort : true)
                                                                                 && (useServerPort ? serverMapping != null : true)
                                                                                 && (usePeerPort ? peerMapping     != null : true)
                                                                                 && (useQueryPort ? queryMapping   != null : true)
                                                                                 && (useRconPort ? rconMapping     != null : true)
                                                                         });
                }

                base.Show();
                //TODO:FIX THIS STUPID CODE, LAST LINE DONT REFRESH CORRECTLY, JUST A QUICK WORKAROUND
                int index = portForwardGridBindingSource.Count - 1;
                var obj   = (PortForwardGrid)portForwardGridBindingSource[index];
                portForwardGridBindingSource.RemoveAt(index);
                portForwardGridBindingSource.Insert(index, obj);
                dataGridView1.Refresh();
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            try {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clAddFW") {
                    int index = e.RowIndex;
                    var obj   = (PortForwardGrid)portForwardGridBindingSource[index];
                    OpenFireWallPort(obj);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDeleteFW") {
                    int index = e.RowIndex;
                    var obj   = (PortForwardGrid)portForwardGridBindingSource[index];
                    RemoveFirewallRules(obj);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clAddRouter") {
                    int index = e.RowIndex;
                    var obj   = (PortForwardGrid)portForwardGridBindingSource[index];
                    OpenRouterPort(obj);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDeleteRouter") {
                    int index = e.RowIndex;
                    var obj   = (PortForwardGrid)portForwardGridBindingSource[index];
                    RemoveRouterPort(obj);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clRefresh") {
                    int index = e.RowIndex;

                    var obj = (PortForwardGrid)portForwardGridBindingSource[index];

                    var discoverer = new NatDiscoverer();
                    var device     = await discoverer.DiscoverDeviceAsync();

                    Mapping serverMapping      = null;
                    Mapping peerMapping        = null;
                    Mapping queryMapping       = null;
                    Mapping rconMapping        = null;
                    string  serverName         = string.Empty;
                    bool    firewallServerPort = false;
                    bool    firewallPeerPort   = false;
                    bool    firewallQueryPort  = false;
                    bool    firewallRconPort   = false;

                    if (obj.UseServerPort) serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.ServerPort);
                    if (obj.UsePeerPort) peerMapping     = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.PeerPort);
                    if (obj.UseQueryPort) queryMapping   = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.QueryPort);
                    if (obj.UseRconPort) rconMapping     = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.RconPort);

                    if (obj.UseServerPort) firewallServerPort = NetworkTools.IsPortOpen(obj.Profile, obj.ServerPort);
                    if (obj.UsePeerPort) firewallPeerPort     = NetworkTools.IsPortOpen(obj.Profile, obj.PeerPort);
                    if (obj.UseQueryPort) firewallQueryPort   = NetworkTools.IsPortOpen(obj.Profile, obj.QueryPort);
                    if (obj.UseRconPort) firewallRconPort     = NetworkTools.IsPortOpen(obj.Profile, obj.RconPort);

                    obj.FirewallServerPort = firewallServerPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.FirewallPeerPort   = firewallPeerPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.FirewallQueryPort  = firewallQueryPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.FirewallRconPort   = firewallRconPort ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.RouterServerPort   = serverMapping != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.RouterPeerPort     = peerMapping   != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.RouterQueryPort    = queryMapping  != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.RouterRconPort     = rconMapping   != null ? new Bitmap(Resources.OkIcon) : new Bitmap(Resources.CloseIcon);
                    obj.IsOk = (obj.UseServerPort ? firewallServerPort : true)
                            && (obj.UsePeerPort ? firewallPeerPort : true)
                            && (obj.UseQueryPort ? firewallQueryPort : true)
                            && (obj.UseRconPort ? firewallRconPort : true)
                            && (obj.UseServerPort ? serverMapping != null : true)
                            && (obj.UsePeerPort ? peerMapping     != null : true)
                            && (obj.UseQueryPort ? queryMapping   != null : true)
                            && (obj.UseRconPort ? rconMapping     != null : true);

                    portForwardGridBindingSource.RemoveAt(index);
                    portForwardGridBindingSource.Insert(index, obj);
                }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private async void OpenRouterPort(PortForwardGrid obj) {
            try {
                var discoverer = new NatDiscoverer();
                var device     = await discoverer.DiscoverDeviceAsync();

                var serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.ServerPort);
                var peerMapping   = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.PeerPort);
                var queryMapping  = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.QueryPort);
                var rconMapping   = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.RconPort);

                if (obj.UseServerPort)
                    if (serverMapping == null)
                        await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.ServerPort, obj.ServerPort, obj.Profile));
                if (obj.UsePeerPort)
                    if (peerMapping == null)
                        await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.PeerPort, obj.PeerPort, obj.Profile));
                if (obj.UseQueryPort)
                    if (queryMapping == null)
                        await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.QueryPort, obj.QueryPort, obj.Profile));
                if (obj.UseRconPort)
                    if (rconMapping == null)
                        await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.RconPort, obj.RconPort, obj.Profile));
                MessageBox.Show("Mapping added/Updated");
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show("Error adding mapping :" + ex.Message);
            }
        }

        private async void RemoveRouterPort(PortForwardGrid obj) {
            try {
                var discoverer = new NatDiscoverer();
                var device     = await discoverer.DiscoverDeviceAsync();

                var serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.ServerPort);
                var peerMapping   = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.PeerPort);
                var queryMapping  = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.QueryPort);
                var rconMapping   = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.RconPort);


                if (obj.UseServerPort)
                    if (serverMapping != null)
                        await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.ServerPort, obj.ServerPort, obj.Profile));
                if (obj.UsePeerPort)
                    if (peerMapping != null)
                        await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.PeerPort, obj.PeerPort, obj.Profile));
                if (obj.UseQueryPort)
                    if (queryMapping != null)
                        await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.QueryPort, obj.QueryPort, obj.Profile));
                if (obj.UseRconPort)
                    if (rconMapping != null)
                        await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.RconPort, obj.RconPort, obj.Profile));
                MessageBox.Show("Mapping deleted");
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show("Error adding mapping :" + ex.Message);
            }
        }

        private void OpenFireWallPort(PortForwardGrid obj) {
            var fw = new FirewallCom();

            //TCP
            try {
                var rulesTcp = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " TCP");

                if (rulesTcp.Count > 0)
                    rulesTcp.ForEach(r => { fw.RemoveRule(r.Name); });

                string ports                 = "";
                if (obj.UseServerPort) ports =  $"{obj.ServerPort}";
                if (obj.UsePeerPort) ports   += (ports != "" ? "," : "") + $"{obj.PeerPort}";
                if (obj.UseQueryPort) ports  += (ports != "" ? "," : "") + $"{obj.QueryPort}";
                if (obj.UseRconPort) ports   += (ports != "" ? "," : "") + $"{obj.RconPort}";

                var rule = new Rule {
                                        Action          = Action.Allow,
                                        Description     = "Ophiussa Server Manager - " + obj.Profile,
                                        Direction       = Direction.In,
                                        Protocol        = ProtocolPort.Tcp,
                                        Name            = obj.Profile + " TCP",
                                        RemotePorts     = "*",
                                        InterfaceTypes  = "All",
                                        Profiles        = ProfileType.All,
                                        EdgeTraversal   = false,
                                        LocalAddresses  = "*",
                                        RemoteAddresses = "*",
                                        LocalPorts      = ports,
                                        Enabled         = true
                                    };

                NetworkTools.AddRule(rule);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show("Error adding TCP Rule :" + ex.Message);
            }

            //UDP
            try {
                var rulesTcp = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " UDP");

                if (rulesTcp.Count > 0)
                    rulesTcp.ForEach(r => { fw.RemoveRule(r.Name); });

                string ports                 = "";
                if (obj.UseServerPort) ports =  $"{obj.ServerPort}";
                if (obj.UsePeerPort) ports   += (ports != "" ? "," : "") + $"{obj.PeerPort}";
                if (obj.UseQueryPort) ports  += (ports != "" ? "," : "") + $"{obj.QueryPort}";
                if (obj.UseRconPort) ports   += (ports != "" ? "," : "") + $"{obj.RconPort}";

                var rule = new Rule {
                                        Action          = Action.Allow,
                                        Description     = "Ophiussa Server Manager - " + obj.Profile,
                                        Direction       = Direction.In,
                                        Protocol        = ProtocolPort.Udp,
                                        Name            = obj.Profile + " UDP",
                                        RemotePorts     = "*",
                                        InterfaceTypes  = "All",
                                        Profiles        = ProfileType.All,
                                        EdgeTraversal   = false,
                                        LocalAddresses  = "*",
                                        RemoteAddresses = "*",
                                        LocalPorts      = ports,
                                        Enabled         = true
                                    };

                NetworkTools.AddRule(rule);

                MessageBox.Show("Mapping added/Updated");
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show("Error adding UDP Rule :" + ex.Message);
            }
        }

        public static void RemoveFirewallRules(PortForwardGrid obj) {
            var fw = new FirewallCom();

            //TCP
            try {
                var rulesTcp = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " TCP");

                if (rulesTcp.Count > 0)
                    rulesTcp.ForEach(r => { fw.RemoveRule(r.Name); });
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show("Error delete TCP Rule :" + ex.Message);
            }

            //UDP
            try {
                var rulesTcp = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " UDP");

                if (rulesTcp.Count > 0)
                    rulesTcp.ForEach(r => { fw.RemoveRule(r.Name); });
                MessageBox.Show("Mapping deleted");
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show("Error delete UDP Rule :" + ex.Message);
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            var obj = (PortForwardGrid)portForwardGridBindingSource[e.RowIndex];

            foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
                if (obj.IsOk)
                    cell.Style.BackColor = Color.LightGreen;
                else
                    cell.Style.BackColor = Color.LightCoral;
        }
    }
}