using Open.Nat;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using FirewallManager;
using System.Windows.Interop;
using NetFwTypeLib;
using System.Reflection;
using System.Windows.Markup.Localizer;

namespace OphiussaServerManager.Forms
{
    public partial class FrmPortForward : Form
    {
        public FrmPortForward()
        {
            InitializeComponent();
        }

        private void FrmPortForward_Load(object sender, EventArgs e)
        {

        }

        public async void LoadPortFoward(Dictionary<string, Profile> profiles)
        {

            try
            {
                var discoverer = new NatDiscoverer();
                var device = await discoverer.DiscoverDeviceAsync();

                foreach (string key in profiles.Keys)
                {
                    Profile profile = profiles[key];
                    Mapping serverMapping = null;
                    Mapping peerMapping = null;
                    Mapping QueryMapping = null;
                    Mapping RconMapping = null;
                    string serverName = string.Empty;
                    bool FirewallServerPort = false;
                    bool FirewallPeerPort = false;
                    bool FirewallQueryPort = false;
                    bool FirewallRconPort = false;

                    bool UseServerPort = false;
                    bool UsePeerPort = false;
                    bool UseQueryPort = false;
                    bool UseRconPort = false;

                    ushort vServerPort = 0;
                    ushort vPeerPort = 0;
                    ushort vQueryPort = 0;
                    ushort vRconPort = 0;

                    switch (profile.Type.ServerType)
                    {
                        case Common.Models.SupportedServers.EnumServerType.ArkSurviveEvolved:
                        case Common.Models.SupportedServers.EnumServerType.ArkSurviveAscended:
                            serverName = profile.ARKConfiguration.Administration.ServerName;

                            UseServerPort = true;
                            UsePeerPort = true;
                            UseQueryPort = true;
                            UseRconPort = profile.ARKConfiguration.Administration.UseRCON;

                            serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.ServerPort.ToInt());
                            peerMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.PeerPort.ToInt());
                            QueryMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.QueryPort.ToInt());
                            if (profile.ARKConfiguration.Administration.UseRCON) RconMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.RCONPort.ToInt());

                            FirewallServerPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.ServerPort.ToInt());
                            FirewallPeerPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.PeerPort.ToInt());
                            FirewallQueryPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.QueryPort.ToInt());
                            if (profile.ARKConfiguration.Administration.UseRCON) FirewallRconPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.RCONPort.ToInt());
                            vServerPort = profile.ARKConfiguration.Administration.ServerPort.ToUShort();
                            vPeerPort = profile.ARKConfiguration.Administration.PeerPort.ToUShort();
                            vQueryPort = profile.ARKConfiguration.Administration.QueryPort.ToUShort();
                            if (profile.ARKConfiguration.Administration.UseRCON) vRconPort = profile.ARKConfiguration.Administration.RCONPort.ToUShort();
                            break;
                        case Common.Models.SupportedServers.EnumServerType.Valheim:
                            serverName = profile.ValheimConfiguration.Administration.ServerName;

                            UseServerPort = true;
                            UsePeerPort = true;
                            UseQueryPort = false;
                            UseRconPort = false;

                            serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ValheimConfiguration.Administration.ServerPort.ToInt());
                            peerMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ValheimConfiguration.Administration.PeerPort.ToInt());

                            FirewallServerPort = NetworkTools.IsPortOpen(profile.Name, profile.ValheimConfiguration.Administration.ServerPort.ToInt());
                            FirewallPeerPort = NetworkTools.IsPortOpen(profile.Name, profile.ValheimConfiguration.Administration.PeerPort.ToInt());

                            vServerPort = profile.ValheimConfiguration.Administration.ServerPort.ToUShort();
                            vPeerPort = profile.ValheimConfiguration.Administration.PeerPort.ToUShort();
                            break;
                    }

                    portForwardGridBindingSource.Add(new PortForwardGrid()
                    {
                        Profile = profile.Name,
                        ServerName = serverName,
                        FirewallServerPort = FirewallServerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        FirewallPeerPort = FirewallPeerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        FirewallQueryPort = FirewallQueryPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        FirewallRconPort = FirewallRconPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterServerPort = serverMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterPeerPort = peerMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterQueryPort = QueryMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterRconPort = RconMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        ServerPort = vServerPort,
                        PeerPort = vPeerPort,
                        QueryPort = vQueryPort,
                        RconPort = vRconPort,
                        UseServerPort = UseServerPort,
                        UsePeerPort = UsePeerPort,
                        UseQueryPort = UseQueryPort,
                        UseRconPort = UseRconPort,
                        isOK = (UseServerPort ? FirewallServerPort : true)
                            && (UsePeerPort ? FirewallPeerPort : true)
                            && (UseQueryPort ? FirewallQueryPort : true)
                            && (UseRconPort ? FirewallRconPort : true)
                            && (UseServerPort ? serverMapping != null : true)
                            && (UsePeerPort ? peerMapping != null : true)
                            && (UseQueryPort ? QueryMapping != null : true)
                            && (UseRconPort ? RconMapping != null : true)

                    });

                }

                base.Show();
                //TODO:FIX THIS STUPID CODE, LAST LINE DONT REFRESH CORRECTLY, JUST A QUICK WORKAROUND
                int index = portForwardGridBindingSource.Count - 1;
                PortForwardGrid obj = (PortForwardGrid)this.portForwardGridBindingSource[index];
                this.portForwardGridBindingSource.RemoveAt(index);
                this.portForwardGridBindingSource.Insert(index, obj);
                dataGridView1.Refresh();

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show(ex.Message);
            }

        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                if (dataGridView1.Columns[e.ColumnIndex].Name == "clAddFW")
                {
                    int index = e.RowIndex;
                    PortForwardGrid obj = (PortForwardGrid)this.portForwardGridBindingSource[index];
                    OpenFireWallPort(obj);
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDeleteFW")
                {
                    int index = e.RowIndex;
                    PortForwardGrid obj = (PortForwardGrid)this.portForwardGridBindingSource[index];
                    RemoveFirewallRules(obj);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clAddRouter")
                {
                    int index = e.RowIndex;
                    PortForwardGrid obj = (PortForwardGrid)this.portForwardGridBindingSource[index];
                    OpenRouterPort(obj);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDeleteRouter")
                {
                    int index = e.RowIndex;
                    PortForwardGrid obj = (PortForwardGrid)this.portForwardGridBindingSource[index];
                    RemoveRouterPort(obj);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clRefresh")
                {
                    int index = e.RowIndex;

                    PortForwardGrid obj = (PortForwardGrid)this.portForwardGridBindingSource[index];

                    var discoverer = new NatDiscoverer();
                    var device = await discoverer.DiscoverDeviceAsync();

                    Mapping serverMapping = null;
                    Mapping peerMapping = null;
                    Mapping QueryMapping = null;
                    Mapping RconMapping = null;
                    string serverName = string.Empty;
                    bool FirewallServerPort = false;
                    bool FirewallPeerPort = false;
                    bool FirewallQueryPort = false;
                    bool FirewallRconPort = false;

                    if (obj.UseServerPort) serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.ServerPort);
                    if (obj.UsePeerPort) peerMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.PeerPort);
                    if (obj.UseQueryPort) QueryMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.QueryPort);
                    if (obj.UseRconPort) RconMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.RconPort);

                    if (obj.UseServerPort) FirewallServerPort = NetworkTools.IsPortOpen(obj.Profile, obj.ServerPort);
                    if (obj.UsePeerPort) FirewallPeerPort = NetworkTools.IsPortOpen(obj.Profile, obj.PeerPort);
                    if (obj.UseQueryPort) FirewallQueryPort = NetworkTools.IsPortOpen(obj.Profile, obj.QueryPort);
                    if (obj.UseRconPort) FirewallRconPort = NetworkTools.IsPortOpen(obj.Profile, obj.RconPort);

                    obj.FirewallServerPort = FirewallServerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.FirewallPeerPort = FirewallPeerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.FirewallQueryPort = FirewallQueryPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.FirewallRconPort = FirewallRconPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterServerPort = serverMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterPeerPort = peerMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterQueryPort = QueryMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterRconPort = RconMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.isOK = (obj.UseServerPort ? FirewallServerPort : true)
                            && (obj.UsePeerPort ? FirewallPeerPort : true)
                            && (obj.UseQueryPort ? FirewallQueryPort : true)
                            && (obj.UseRconPort ? FirewallRconPort : true)
                            && (obj.UseServerPort ? serverMapping != null : true)
                            && (obj.UsePeerPort ? peerMapping != null : true)
                            && (obj.UseQueryPort ? QueryMapping != null : true)
                            && (obj.UseRconPort ? RconMapping != null : true);

                    this.portForwardGridBindingSource.RemoveAt(index);
                    this.portForwardGridBindingSource.Insert(index, obj);

                }
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private async void OpenRouterPort(PortForwardGrid obj)
        {
            try
            {
                var discoverer = new NatDiscoverer();
                var device = await discoverer.DiscoverDeviceAsync();

                Mapping serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.ServerPort);
                Mapping peerMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.PeerPort);
                Mapping QueryMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.QueryPort);
                Mapping RconMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.RconPort);

                if (obj.UseServerPort) if (serverMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.ServerPort, obj.ServerPort, obj.Profile));
                if (obj.UsePeerPort) if (peerMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.PeerPort, obj.PeerPort, obj.Profile));
                if (obj.UseQueryPort) if (QueryMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.QueryPort, obj.QueryPort, obj.Profile));
                if (obj.UseRconPort) if (RconMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.RconPort, obj.RconPort, obj.Profile));
                MessageBox.Show("Mapping added/Updated");
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show("Error adding mapping :" + ex.Message);
            }
        }

        private async void RemoveRouterPort(PortForwardGrid obj)
        {
            try
            {
                var discoverer = new NatDiscoverer();
                var device = await discoverer.DiscoverDeviceAsync();

                Mapping serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.ServerPort);
                Mapping peerMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.PeerPort);
                Mapping QueryMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.QueryPort);
                Mapping RconMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.RconPort);


                if (obj.UseServerPort) if (serverMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.ServerPort, obj.ServerPort, obj.Profile));
                if (obj.UsePeerPort) if (peerMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.PeerPort, obj.PeerPort, obj.Profile));
                if (obj.UseQueryPort) if (QueryMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.QueryPort, obj.QueryPort, obj.Profile));
                if (obj.UseRconPort) if (RconMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.RconPort, obj.RconPort, obj.Profile));
                MessageBox.Show("Mapping deleted");
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show("Error adding mapping :" + ex.Message);
            }
        }

        private void OpenFireWallPort(PortForwardGrid obj)
        {
            FirewallCom fw = new FirewallCom();

            //TCP
            try
            {
                var rulesTCP = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " TCP");

                if (rulesTCP.Count > 0)
                {
                    rulesTCP.ForEach(r =>
                    {
                        fw.RemoveRule(r.Name);
                    });
                }

                string ports = "";
                if (obj.UseServerPort) ports = $"{obj.ServerPort}";
                if (obj.UsePeerPort) ports += (ports != "" ? "," : "") + $"{obj.PeerPort}";
                if (obj.UseQueryPort) ports += (ports != "" ? "," : "") + $"{obj.QueryPort}";
                if (obj.UseRconPort) ports += (ports != "" ? "," : "") + $"{obj.RconPort}";

                Rule rule = new Rule()
                {
                    Action = FirewallManager.Action.Allow,
                    Description = "Ophiussa Server Manager - " + obj.Profile,
                    Direction = Direction.In,
                    Protocol = ProtocolPort.Tcp,
                    Name = obj.Profile + " TCP",
                    RemotePorts = "*",
                    InterfaceTypes = "All",
                    Profiles = ProfileType.All,
                    EdgeTraversal = false,
                    LocalAddresses = "*",
                    RemoteAddresses = "*",
                    LocalPorts = ports,
                    Enabled = true
                };

                NetworkTools.AddRule(rule);

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show("Error adding TCP Rule :" + ex.Message);
            }

            //UDP
            try
            {
                var rulesTCP = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " UDP");

                if (rulesTCP.Count > 0)
                {
                    rulesTCP.ForEach(r =>
                    {
                        fw.RemoveRule(r.Name);
                    });
                }

                string ports = "";
                if (obj.UseServerPort) ports = $"{obj.ServerPort}";
                if (obj.UsePeerPort) ports += (ports != "" ? "," : "") + $"{obj.PeerPort}";
                if (obj.UseQueryPort) ports += (ports != "" ? "," : "") + $"{obj.QueryPort}";
                if (obj.UseRconPort) ports += (ports != "" ? "," : "") + $"{obj.RconPort}";

                Rule rule = new Rule()
                {
                    Action = FirewallManager.Action.Allow,
                    Description = "Ophiussa Server Manager - " + obj.Profile,
                    Direction = Direction.In,
                    Protocol = ProtocolPort.Udp,
                    Name = obj.Profile + " UDP",
                    RemotePorts = "*",
                    InterfaceTypes = "All",
                    Profiles = ProfileType.All,
                    EdgeTraversal = false,
                    LocalAddresses = "*",
                    RemoteAddresses = "*",
                    LocalPorts = ports,
                    Enabled = true
                };

                NetworkTools.AddRule(rule);

                MessageBox.Show("Mapping added/Updated");
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show("Error adding UDP Rule :" + ex.Message);
            }
        }

        public static void RemoveFirewallRules(PortForwardGrid obj)
        {
            FirewallCom fw = new FirewallCom();

            //TCP
            try
            {
                var rulesTCP = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " TCP");

                if (rulesTCP.Count > 0)
                {
                    rulesTCP.ForEach(r =>
                    {
                        fw.RemoveRule(r.Name);
                    });
                }

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show("Error delete TCP Rule :" + ex.Message);
            }

            //UDP
            try
            {
                var rulesTCP = fw.GetRules().ToList().FindAll(r => r.Name == obj.Profile + " UDP");

                if (rulesTCP.Count > 0)
                {
                    rulesTCP.ForEach(r =>
                    {
                        fw.RemoveRule(r.Name);
                    });
                }
                MessageBox.Show("Mapping deleted");
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show("Error delete UDP Rule :" + ex.Message);
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            PortForwardGrid obj = (PortForwardGrid)this.portForwardGridBindingSource[e.RowIndex];

            foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
            {
                if (obj.isOK)
                {
                    cell.Style.BackColor = Color.LightGreen;
                }
                else
                {
                    cell.Style.BackColor = Color.LightCoral;
                }
            }
        }
    }
}
