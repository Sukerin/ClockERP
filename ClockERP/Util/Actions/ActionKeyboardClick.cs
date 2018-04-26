using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockERP.Util.Actions
{
    class ActionKeyboardClick : Action
    {
        private int keyCode;
        public ActionKeyboardClick(int keyCode)
        {
            this.keyCode = keyCode;
        }
        protected override void Process()
        {
            throw new NotImplementedException();
        }
    }
}
