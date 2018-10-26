using PluralsightWinFormsDemoApp.Commands;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Presenters
{
    class ToolbarPresenter
    {
        public ToolbarPresenter(IToolbarView toolbarView, IToolbarCommand[] commands)
        {
            toolbarView.SetCommands(commands);
        }
    }
}
