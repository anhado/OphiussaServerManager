using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace MakarovDev.ExpandCollapsePanel {
    /// <summary>
    ///     Designer for the ExpandCollapsePanel control with support for a smart tag panel.
    ///     <remarks>http://msdn.microsoft.com/en-us/library/ms171829.aspx</remarks>
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class ExpandCollapsePanelDesigner : ScrollableControlDesigner {
        private DesignerActionListCollection _actionLists;

        // Use pull model to populate smart tag menu. 
        public override DesignerActionListCollection ActionLists {
            get {
                if (null == _actionLists) {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.Add(
                                     new ExpandCollapsePanelActionList(Component));
                }

                return _actionLists;
            }
        }
    }
}