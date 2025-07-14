using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels
{
    public class ControlMessage
    {
        public bool Enable { get; set; }
        public bool IsLocked { get; set; }
        public List<ToolbarButtonsAction> Actions { get; set; } = new List<ToolbarButtonsAction>();
    }
}
