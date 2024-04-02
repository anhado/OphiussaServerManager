using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaFramework.Forms;

namespace OphiussaFramework.Models {
    public class CommandBuilder {
        public CommandBuilder(List<CommandDefinition> commands = null) {
            var _tmpCommands = commands ?? new List<CommandDefinition>();

            ComandList = JsonConvert.DeserializeObject<List<CommandDefinition>>(JsonConvert.SerializeObject(_tmpCommands, Formatting.Indented));
        }

        public List<CommandDefinition> ComandList { get; private set; }

        public void AddCommand(int order, bool addSpaceInPrefix, string namePrefix, string name, string valuePrefix, string value, bool enabled) {
            ComandList.Add(new CommandDefinition {
                                                     Order            = order,
                                                     AddSpaceInPrefix = addSpaceInPrefix,
                                                     NamePrefix       = namePrefix,
                                                     Name             = name,
                                                     Value            = value,
                                                     Enabled          = enabled,
                                                     ValuePrefix      = valuePrefix
                                                 });
        }

        public override string ToString() {
            var cmd = ComandList.OrderBy(cm => cm.Order).ToList();

            var cmdFinal = new StringBuilder();

            cmd.ForEach(c => {
                            if (!c.Enabled) return;
                            if (c.AddSpaceInPrefix) cmdFinal.Append(" ");
                            cmdFinal.Append(c.NamePrefix);
                            cmdFinal.Append(c.Name);
                            if (string.IsNullOrEmpty(c.Value)) return;
                            cmdFinal.Append(c.ValuePrefix);
                            cmdFinal.Append(c.Value);
                        });

            return cmdFinal.ToString();
        }

        public void OpenCommandEditor(Action<CommandBuilder> buildCommand) {
            var commandEditor = new FrmCommandEditor(ComandList);
            commandEditor.BuildCommand = lst => { ComandList = lst; };
            if (commandEditor.ShowDialog() == DialogResult.OK) buildCommand.Invoke(this);
        }
    }
}