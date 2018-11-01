using PluralsightWinFormsDemoApp.Commands;
using PluralsightWinFormsDemoApp.Views;

namespace PluralsightWinFormsDemoApp.Presenters
{
    internal class ToolbarPresenter
    {
        public ToolbarPresenter(IToolbarView toolbarView, IToolbarCommand[] commands)
        {
            toolbarView.SetCommands(commands);
        }
    }
}
