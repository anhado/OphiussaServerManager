using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using OphiussaFormBuilder.Models;

namespace OphiussaFormBuilder {
    public partial class FrmResult : Form {
        private List<ObjectDefinition> ObjectsList;
        public FrmResult(List<ObjectDefinition> objectsList) {
            InitializeComponent();
            ObjectsList = objectsList;

            GenerateCode();
        }

        private void GenerateCode() {

            int top = 23;

            StringBuilder appenderDesignHeader = new StringBuilder();
            StringBuilder appenderObjectDefinition = new StringBuilder();
            StringBuilder appenderVariablesDeclaration = new StringBuilder();
            StringBuilder appenderBindings = new StringBuilder();
            StringBuilder appenderAddDefinition = new StringBuilder();

            int          i         = 0;
            List<string> groupList = ObjectsList.Select(x => {
                                                            return x.Group;
                                                        }).Distinct().Select(x1 => {
                                                                                 i++;
                                                                                 return $"{i:00} - {x1}";
                                                                             }).OrderByDescending(x=> x).ToList();

            foreach (string grp in groupList) {

                List<ObjectDefinition> filtereDefinitions = ObjectsList.FindAll(x => x.Group == grp.Substring(5));

                string grpObjName = $"ecp{grp.Replace("-", "").Replace("_", "").Replace(" ", "")}";

                appenderVariablesDeclaration.Append($"\nprivate MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel  {grpObjName};");
                appenderDesignHeader.Append($"\nthis.{grpObjName} = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel(); ");

                appenderObjectDefinition.Append($"\nthis.{grpObjName}.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.ButtonSize  = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.Dock           = System.Windows.Forms.DockStyle.Top; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.ExpandedHeight = 511; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.IsExpanded     = true; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.Location       = new System.Drawing.Point(0, {top}); ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.Name           = \"{grpObjName}\"; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.Size           = new System.Drawing.Size(1279, 619); ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.TabIndex       = 4; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.Text           = \"{grp.Substring(5)}\"; ");
                appenderObjectDefinition.Append($"\nthis.{grpObjName}.UseAnimation   = false; ");

                appenderAddDefinition.Append($"\nthis.pContainer.Controls.Add(this.{grpObjName});");

                int topObj = 40;
                foreach (ObjectDefinition obj in filtereDefinitions) {

                    switch (obj.ObjectType) {
                        case ObjectType.TextBox:
                            appenderAddDefinition.Append($"\nthis.{grpObjName}.Controls.Add(this.lbl{obj.ObjectName.Substring(3)});");
                            appenderAddDefinition.Append($"\nthis.{grpObjName}.Controls.Add(this.{obj.ObjectName});");
                            appenderDesignHeader.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}          = new System.Windows.Forms.Label(); ");
                            appenderDesignHeader.Append($"\nthis.{obj.ObjectName} = new System.Windows.Forms.TextBox(); ");


                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.AutoSize = true;");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Location = new System.Drawing.Point(9, {topObj});");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Name     = \"lbl{obj.ObjectName.Substring(3)}\";");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Size     = new System.Drawing.Size(84, 15);");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.TabIndex = 4;");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Text     = \"{obj.Description}\"; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) ");
                            appenderObjectDefinition.Append($"\n                                                                  | System.Windows.Forms.AnchorStyles.Right)));");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Location = new System.Drawing.Point(192, {topObj});");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Name     = \"{obj.ObjectName}\";");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.ReadOnly = true;");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Size     = new System.Drawing.Size(952, 21);");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.TabIndex = 6;");


                            appenderVariablesDeclaration.Append($"\nprivate System.Windows.Forms.Label lbl{obj.ObjectName.Substring(3)};");
                            appenderVariablesDeclaration.Append($"\nprivate System.Windows.Forms.TextBox {obj.ObjectName};");

                            appenderBindings.Append($"\n{obj.ObjectName}.DataBindings.Add(\"Text\", _plugin.{obj.BindingLocation}, \"{obj.PropertyName}\", true, DataSourceUpdateMode.OnPropertyChanged);");
                            break;
                        case ObjectType.ExTrackBar:
                            appenderAddDefinition.Append($"\nthis.{grpObjName}.Controls.Add(this.{obj.ObjectName});");
                            appenderDesignHeader.Append($"\nthis.{obj.ObjectName} = new OphiussaFramework.Components.exTrackBar(); ");

                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) ");
                            appenderObjectDefinition.Append($"\n                                                              | System.Windows.Forms.AnchorStyles.Right))); ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.DisableTextBox  = false; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.DisableTrackBar = false; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Location        = new System.Drawing.Point(12,  {topObj}); ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Maximum         = {GetCurrectValue(obj.Maximum)}; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Minimum         = {GetCurrectValue(obj.Minimum)}; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Name            = \"{obj.ObjectName}\"; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Scale           = 1F; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Size            = new System.Drawing.Size(1248, 26); ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.TabIndex        = 20; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Text            = \"{obj.Description}\"; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.TickFrequency   = 1; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Units           = \"\"; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Value           = {GetCurrectValue(obj.Value)}; ");

                            appenderVariablesDeclaration.Append($"\nprivate OphiussaFramework.Components.exTrackBar {obj.ObjectName};");
                            appenderBindings.Append($"\n{obj.ObjectName}.DataBindings.Add(\"Value\", _plugin.{obj.BindingLocation}, \"{obj.PropertyName}\", true, DataSourceUpdateMode.OnPropertyChanged);");
                            break;
                        case ObjectType.CheckBox:

                            appenderAddDefinition.Append($"\nthis.{grpObjName}.Controls.Add(this.{obj.ObjectName});");
                            appenderDesignHeader.Append($"\nthis.{obj.ObjectName} = new System.Windows.Forms.CheckBox(); ");

                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.AutoSize                = true;");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Location                = new System.Drawing.Point(12, {topObj});");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Name                    = \"{obj.ObjectName}\";");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Size                    = new System.Drawing.Size(141, 19);");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.TabIndex                = 25;");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Text                    = \"{obj.Description}\";");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.UseVisualStyleBackColor = true;");

                            appenderVariablesDeclaration.Append($"\nprivate System.Windows.Forms.CheckBox {obj.ObjectName};");
                            appenderBindings.Append($"\n{obj.ObjectName}.DataBindings.Add(\"Checked\", _plugin.{obj.BindingLocation}, \"{obj.PropertyName}\", true, DataSourceUpdateMode.OnPropertyChanged);");
                            break;
                        case ObjectType.ComboBox:
                            appenderAddDefinition.Append($"\nthis.{grpObjName}.Controls.Add(this.lbl{obj.ObjectName.Substring(3)});");
                            appenderAddDefinition.Append($"\nthis.{grpObjName}.Controls.Add(this.{obj.ObjectName});");
                            appenderDesignHeader.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}          = new System.Windows.Forms.Label(); ");
                            appenderDesignHeader.Append($"\nthis.{obj.ObjectName} = new System.Windows.Forms.ComboBox(); ");

                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.AutoSize = true;");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Location = new System.Drawing.Point(9, {topObj});");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Name     = \"lbl{obj.ObjectName.Substring(3)}\";");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Size     = new System.Drawing.Size(84, 15);");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.TabIndex = 4;");
                            appenderObjectDefinition.Append($"\nthis.lbl{obj.ObjectName.Substring(3)}.Text     = \"{obj.Description}\"; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.FormattingEnabled = true; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Location          = new System.Drawing.Point(70, {topObj}); ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Name              = \"{obj.ObjectName}\"; ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.Size              = new System.Drawing.Size(132, 21); ");
                            appenderObjectDefinition.Append($"\nthis.{obj.ObjectName}.TabIndex          = 15; ");

                            appenderVariablesDeclaration.Append($"\nprivate System.Windows.Forms.Label lbl{obj.ObjectName.Substring(3)};");
                            appenderVariablesDeclaration.Append($"\nprivate System.Windows.Forms.ComboBox {obj.ObjectName};");
                            appenderBindings.Append($"\n{obj.ObjectName}.DataBindings.Add(\"SelectedValue\", _plugin.{obj.BindingLocation}, \"{obj.PropertyName}\", true, DataSourceUpdateMode.OnPropertyChanged);");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    topObj += 23;
                }
                top += 23;
            }

            txtDesignerHeader.Text       = appenderDesignHeader.ToString();
            txtObjectDefinition.Text     = appenderObjectDefinition.ToString();
            txtVariablesDeclaration.Text = appenderVariablesDeclaration.ToString();
            txtAddDefinition.Text        = appenderAddDefinition.ToString();
            txtBindings.Text             = appenderBindings.ToString();
        }

        private object GetCurrectValue(JToken value) {
            switch (value.Type) { 
                case JTokenType.Float:
                    return ((float)value).ToString("0.00");
                    break;
                default:
                    return value;
            }
        }
    }
}
