using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockERP.Util.Actions
{
    class ActionKeyboardClick : Action
    {
        private int keyCode;
        private String keyValue;

        public ActionKeyboardClick(int keyCode)
        {
            this.keyCode = keyCode;
        }
        public ActionKeyboardClick(String keyValue)
        {
            this.keyValue = keyValue;
        }
        protected override void Process()
        {
            SendKeys.SendWait(keyValue);
        }

        protected override int IntervalTime()
        {
            return 0;
        }
    }
}
