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

                    Mapping serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.ServerPort.ToInt());
                    Mapping peerMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.PeerPort.ToInt());
                    Mapping QueryMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.QueryPort.ToInt());
                    Mapping RconMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, profile.ARKConfiguration.Administration.RCONPort.ToInt());

                    bool FirewallServerPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.ServerPort.ToInt());
                    bool FirewallPeerPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.PeerPort.ToInt());
                    bool FirewallQueryPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.QueryPort.ToInt());
                    bool FirewallRconPort = NetworkTools.IsPortOpen(profile.Name, profile.ARKConfiguration.Administration.RCONPort.ToInt());

                    portForwardGridBindingSource.Add(new PortForwardGrid()
                    {
                        Profile = profile.Name,
                        ServerName = profile.ARKConfiguration.Administration.ServerName,
                        FirewallServerPort = FirewallServerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        FirewallPeerPort = FirewallPeerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        FirewallQueryPort = FirewallQueryPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        FirewallRconPort = FirewallRconPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterServerPort = serverMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterPeerPort = peerMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterQueryPort = QueryMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        RouterRconPort = RconMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon),
                        ServerPort = profile.ARKConfiguration.Administration.ServerPort.ToUShort(),
                        PeerPort = profile.ARKConfiguration.Administration.PeerPort.ToUShort(),
                        QueryPort = profile.ARKConfiguration.Administration.QueryPort.ToUShort(),
                        RconPort = profile.ARKConfiguration.Administration.RCONPort.ToUShort(),
                        isOK = FirewallServerPort && FirewallPeerPort && FirewallQueryPort && FirewallRconPort && serverMapping != null && peerMapping != null && QueryMapping != null && RconMapping != null
                    });
                }

                base.ShowDialog();
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
                    Mapping serverMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.ServerPort);
                    Mapping peerMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.PeerPort);
                    Mapping QueryMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.QueryPort);
                    Mapping RconMapping = await device.GetSpecificMappingAsync(Protocol.TcpUpd, obj.RconPort);

                    bool FirewallServerPort = NetworkTools.IsPortOpen(obj.Profile, obj.ServerPort);
                    bool FirewallPeerPort = NetworkTools.IsPortOpen(obj.Profile, obj.PeerPort);
                    bool FirewallQueryPort = NetworkTools.IsPortOpen(obj.Profile, obj.QueryPort);
                    bool FirewallRconPort = NetworkTools.IsPortOpen(obj.Profile, obj.RconPort);

                    obj.FirewallServerPort = FirewallServerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.FirewallPeerPort = FirewallPeerPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.FirewallQueryPort = FirewallQueryPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.FirewallRconPort = FirewallRconPort ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterServerPort = serverMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterPeerPort = peerMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterQueryPort = QueryMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.RouterRconPort = RconMapping != null ? new Bitmap(Properties.Resources.ok_icon_icon) : new Bitmap(Properties.Resources.Close_icon_icon);
                    obj.isOK = FirewallServerPort && FirewallPeerPort && FirewallQueryPort && FirewallRconPort && serverMapping != null && peerMapping != null && QueryMapping != null && RconMapping != null;

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

                if (serverMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.ServerPort, obj.ServerPort, obj.Profile));
                if (peerMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.PeerPort, obj.PeerPort, obj.Profile));
                if (QueryMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.QueryPort, obj.QueryPort, obj.Profile));
                if (RconMapping == null) await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, obj.RconPort, obj.RconPort, obj.Profile));
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


                if (serverMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.ServerPort, obj.ServerPort, obj.Profile));
                if (peerMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.PeerPort, obj.PeerPort, obj.Profile));
                if (QueryMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.QueryPort, obj.QueryPort, obj.Profile));
                if (RconMapping != null) await device.DeletePortMapAsync(new Mapping(Protocol.TcpUpd, obj.RconPort, obj.RconPort, obj.Profile));
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
                    LocalPorts = $"{obj.ServerPort},{obj.PeerPort},{obj.QueryPort},{obj.RconPort}",
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
                    LocalPorts = $"{obj.ServerPort},{obj.PeerPort},{obj.QueryPort},{obj.RconPort}",
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
