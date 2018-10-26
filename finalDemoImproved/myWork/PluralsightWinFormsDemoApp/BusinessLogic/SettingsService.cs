using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluralsightWinFormsDemoApp.Properties;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class SettingsService : ISettingsService
    {
        public bool FirstRun
        {
            get { return Settings.Default.FirstRun; }
            set { Settings.Default.FirstRun = value; }
        }

        public void Save()
        {
            Settings.Default.Save();
        }
    }
}
