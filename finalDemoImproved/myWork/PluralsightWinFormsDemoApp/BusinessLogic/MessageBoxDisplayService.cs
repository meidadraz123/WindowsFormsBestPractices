using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class MessageBoxDisplayService : IMessageBoxDisplayService
    {
        public void Show( string text)
        {
            MessageBox.Show(text);
        }
    }
}
