using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockERP.Util.Actions
{
    class ActionBrowserNavigate : Action
    {
        private String url;
        public ActionBrowserNavigate(String url)
        {
            this.url = url;
        }

        protected override void Process()
        {
            throw new NotImplementedException();
        }

        protected override int IntervalTime()
        {
            throw new NotImplementedException();
        }
    }
}
