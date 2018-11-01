using System.Windows.Forms;
using PluralsightWinFormsDemoApp.Commands;

namespace PluralsightWinFormsDemoApp.Views
{
    public partial class ToolBarView : UserControl, IToolbarView
    {
        public ToolBarView()
        {
            InitializeComponent();
        }

        public void SetCommands(IToolbarCommand[] commands)
        {
            toolStripMainForm.Items.Clear();
            foreach (var command in commands)
            {
                var button = new ToolStripButton(command.ToolTip, command.Icon, (o, s) => command.Execute())
                {
                    Enabled = command.IsEnabled,
                    ImageScaling = ToolStripItemImageScaling.None,
                    DisplayStyle = ToolStripItemDisplayStyle.Image,
                    Tag = command.ShortcutKey
                };
                var c = command; // create a closure around the command
                command.PropertyChanged += (o, s) =>
                {
                    button.Text = c.ToolTip;
                    button.Image = c.Icon;
                    button.Enabled = c.IsEnabled;
                };
                toolStripMainForm.Items.Add(button);
            }
        }
    }

    public interface IToolbarView
    {
        void SetCommands(IToolbarCommand[] commands);
    }
}
