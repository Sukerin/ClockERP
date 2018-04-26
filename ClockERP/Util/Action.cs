using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClockERP.Util
{
    abstract class Action
    {

        abstract protected int IntervalTime();//动作结束后间隔下一个动作的时长
        abstract protected void Process();//动作处理

        private String name;

        private void Log()
        {
            Console.Out.WriteLine(this.name);

        }

        public void Run()
        {
            Log();
            Process();
            Thread.Sleep(IntervalTime());
        }
    }
}
