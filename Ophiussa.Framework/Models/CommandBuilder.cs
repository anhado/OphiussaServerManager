using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Newtonsoft.Json;
using OphiussaFramework.Forms;

namespace OphiussaFramework.Models {
    public class CommandBuilder {
        private List<CommandDefinition> _commands;

        public List<CommandDefinition> ComandList {get { return _commands; } }

        public CommandBuilder(List<CommandDefinition> commands = null) { 

            var _tmpCommands = commands ?? new List<CommandDefinition>();

            _commands = JsonConvert.DeserializeObject<List<CommandDefinition>>(JsonConvert.SerializeObject(_tmpCommands, Formatting.Indented));
        }

        public void AddCommand(int order, bool addSpaceInPrefix, string namePrefix, string name, string valuePrefix, string value, bool enabled) {
            ComandList.Add(new CommandDefinition() {
                                                    Order=order,
                                                    AddSpaceInPrefix=addSpaceInPrefix,
                                                    NamePrefix=namePrefix,
                                                    Name=name,
                                                    Value=value,
                                                    Enabled=enabled,
                                                    ValuePrefix = valuePrefix
                                                   });
        }

        public override string  ToString() {
            var cmd = _commands.OrderBy(cm => cm.Order).ToList();

            StringBuilder cmdFinal = new StringBuilder();

            cmd.ForEach(c => {
                            if(!c.Enabled) return;
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
            
            FrmCommandEditor commandEditor = new FrmCommandEditor(_commands);
            commandEditor.BuildCommand = lst => {
                                             _commands = lst;
                                         };
            if (commandEditor.ShowDialog() == DialogResult.OK) {
                buildCommand.Invoke(this);
            } 
        }
    }
}
