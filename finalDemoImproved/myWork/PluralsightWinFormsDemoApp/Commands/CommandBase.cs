using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.Commands
{
    abstract class CommandBase : IToolbarCommand
    {
        private bool isEnabled;
        private Image icon;
        private string toolTip;
        public event PropertyChangedEventHandler PropertyChanged;

        protected CommandBase()
        {
            isEnabled = true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void Execute();

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        public Image Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    icon = value;
                    OnPropertyChanged(nameof(Icon));
                }
            }
        }

        public string ToolTip
        {
            get { return toolTip; }
            set
            {
                if (toolTip != value)
                {
                    toolTip = value;
                    OnPropertyChanged(nameof(ToolTip));
                }
            }
        }

        public Keys ShortcutKey { get; set; }
    }
}
