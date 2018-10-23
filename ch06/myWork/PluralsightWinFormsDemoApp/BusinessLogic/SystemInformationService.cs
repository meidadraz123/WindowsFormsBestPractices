using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class SystemInformationService : ISystemInformationService
    {
        public bool IsHighContrastColourScheme
        {
            get { return SystemInformation.HighContrast; }
        }
    }
}
