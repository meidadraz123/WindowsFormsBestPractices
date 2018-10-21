using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Commands
{
    class SettingsCommand : CommandBase
    {
        public SettingsCommand()
        {
            Icon = IconResources.settings_icon_32;
            ToolTip = "Settings";
        }
        public override void Execute() { }
    }
}
