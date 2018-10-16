using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class NewSubscriptionService : INewSubscriptionService
    {
        public string GetSubscriptionUrl()
        {
            var form = new NewPodcastForm();
            var result = form.ShowDialog();
            return result == DialogResult.OK ? form.PodcastUrl : null;
        }
    }
}
