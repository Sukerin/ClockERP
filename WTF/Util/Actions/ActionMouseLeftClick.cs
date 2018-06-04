using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockERP.Util.Actions
{
    class ActionMouseLeftClick : Action
    {
        private int x;
        private int y;
        public ActionMouseLeftClick(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        protected override void Process()
        {
            BaseActions.Mouse_event(MouseCommand.MOUSEEVENTF_MOVE | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);
            BaseActions.Mouse_event(MouseCommand.MOUSEEVENTF_LEFTDOWN | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);//点击
            BaseActions.Mouse_event(MouseCommand.MOUSEEVENTF_LEFTUP | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);//抬起

        }

        protected override int IntervalTime()
        {
            return 0;
        }
    }
}
