using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockERP.Util.Actions
{
    class ActionFindAndShowWindow : Action
    {
        private String windowName;
        public ActionFindAndShowWindow(String windowName)
        {
            this.windowName = windowName;
        }
        protected override void Process()
        {
            IntPtr myIntPtr = BaseActions.FindWindow(null, windowName); //null为类名，可以用Spy++得到，也可以为空
            BaseActions.ShowWindow(myIntPtr, MouseCommand.SW_RESTORE); //将窗口还原
            BaseActions.SetForegroundWindow(myIntPtr); //如果没有ShowWindow，此方法不能设置最小化的窗口

        }

        protected override int IntervalTime()
        {
            throw new NotImplementedException();
        }
    }
}
