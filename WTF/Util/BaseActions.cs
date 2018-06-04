using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClockERP.Util
{
    class BaseActions
    {

        [DllImport("user32.dll", EntryPoint = "mouse_event", SetLastError = true)]
        public static extern int Mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        /// The FindWindow API
        /// </summary>
        /// <param name="lpClassName">the class name for the window to search for</param>
        /// <param name="lpWindowName">指向一个指定了窗口名（窗口标题）的空结束字符串。
        /// 如果该参数为空，则为所有窗口全匹配。</param>
        /// <returns>如果函数成功，返回值为具有指定类名和窗口名的窗口句柄；如果函数失败，返回值为NULL</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        /// <summary>
        /// 该函数设置指定窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="nCmdShow">指定窗口如何显示</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);


        /// <summary>
        /// 函数功能：该函数将创建指定窗口的线程设置到前台，
        /// 并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        /// 系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns> 
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

    }
}
