using System;

namespace MakarovDev.ExpandCollapsePanel {
    /// <summary>
    ///     Информация о развёртывании/свёртывании контрола
    /// </summary>
    public class ExpandCollapseEventArgs : EventArgs {
        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="isExpanded">true - контрол развёрнут. false - контрол свёрнут</param>
        public ExpandCollapseEventArgs(bool isExpanded) {
            IsExpanded = isExpanded;
        }

        /// <summary>
        ///     true - контрол развёрнут. false - контрол свёрнут
        /// </summary>
        public bool IsExpanded { get; private set; }
    }
}