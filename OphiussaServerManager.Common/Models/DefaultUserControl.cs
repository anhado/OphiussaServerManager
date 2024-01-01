using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Common.Models {
    public interface IDefaultUserControl  {
        void LoadData(ref ArkProfile profile);
        void GetData(ref  ArkProfile profile);
    }
}
