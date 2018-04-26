using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockERP.Util
{
    abstract class Action
    {

        abstract protected void Process();

        private String name;

        private void Log()
        {
            Console.Out.WriteLine(this.name);

        }

        public void Run()
        {
            Log();
            Process();
        }
    }
}
