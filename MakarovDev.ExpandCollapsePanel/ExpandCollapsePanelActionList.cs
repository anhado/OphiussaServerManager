using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MakarovDev.ExpandCollapsePanel {
    /// <summary>
    ///     DesignerActionList-derived class defines smart tag entries and resultant actions.
    ///     <remarks>http://msdn.microsoft.com/en-us/library/ms171829.aspx</remarks>
    /// </summary>
    public class ExpandCollapsePanelActionList : DesignerActionList {
        private readonly ExpandCollapsePanel     _panel;
        private          DesignerActionUIService _designerActionUiSvc;

        //The constructor associates the control  
        //with the smart tag list. 
        public ExpandCollapsePanelActionList(IComponent component)
            : base(component) {
            _panel = component as ExpandCollapsePanel;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            _designerActionUiSvc =
                GetService(typeof(DesignerActionUIService))
                    as DesignerActionUIService;
        }

        // Properties that are targets of DesignerActionPropertyItem entries. 
        public bool IsExpanded {
            get => _panel.IsExpanded;
            set => GetPropertyByName("IsExpanded").SetValue(_panel, value);
            // Refresh the list. 
            //this.designerActionUISvc.Refresh(this.Component);
        }

        public ExpandCollapseButton.ExpandButtonStyle ButtonStyle {
            get => _panel.ButtonStyle;
            set => GetPropertyByName("ButtonStyle").SetValue(_panel, value);
            // Refresh the list. 
            //this.designerActionUISvc.Refresh(this.Component);
        }

        public ExpandCollapseButton.ExpandButtonSize ButtonSize {
            get => _panel.ButtonSize;
            set => GetPropertyByName("ButtonSize").SetValue(_panel, value);
            // Refresh the list. 
            //this.designerActionUISvc.Refresh(this.Component);
        }

        // Helper method to retrieve control properties. Use of  
        // GetProperties enables undo and menu updates to work properly. 
        private PropertyDescriptor GetPropertyByName(string propName) {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(_panel)[propName];
            if (null == prop)
                throw new ArgumentException(
                                            "Matching ExpandCollapsePanel property not found!",
                                            propName);
            return prop;
        }


        // Implementation of this abstract method creates smart tag   
        // items, associates their targets, and collects into list. 
        public override DesignerActionItemCollection GetSortedActionItems() {
            var items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));
            //items.Add(new DesignerActionHeaderItem("Information"));

            //Boolean property for locking color selections.
            items.Add(new DesignerActionPropertyItem("IsExpanded",
                                                     "IsExpanded", "Appearance",
                                                     "Expand/collapse the panel."));
            items.Add(new DesignerActionPropertyItem("ButtonStyle",
                                                     "ButtonStyle", "Appearance",
                                                     "Visual style of the expand-collapse button."));
            items.Add(new DesignerActionPropertyItem("ButtonSize",
                                                     "ButtonSize", "Appearance",
                                                     "Size preset of the expand-collapse button."));

            return items;
        }
    }
}