using ClockERP.Util;
using ClockERP.Util.Actions;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClockERP
{

    public partial class Form1 : Form, IMessageFilter
    {
        [DllImport("user32.dll", EntryPoint = "mouse_event", SetLastError = true)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

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

        private Point p;     
        private int hour ;
        private int min ;
        private int second ;
        private DateTime startDateTime;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle=FormBorderStyle.FixedSingle;
            webBrowser1.ScriptErrorsSuppressed = true;
            //右键拦截器
            Application.AddMessageFilter(this);
            dateTimePicker1.Value = DateTime.Now;
           
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == MessageID.WM_RBUTTONDOWN)
            {
                //textBox1.Text =this.PointToClient(Control.MousePosition).X.ToString();
                //textBox2.Text =this.PointToClient(Control.MousePosition).Y.ToString();
                p = new Point(Control.MousePosition.X - this.Location.X, Control.MousePosition.Y - this.Location.Y);
                textBox1.Text = p.ToString();               
               
            }
            return false;
        }

    

        public void MouseMoveAndClick(Point thisFormLocation)
        {
            
            int x= (p.X + thisFormLocation.X)* 65536 /Screen.PrimaryScreen.Bounds.Width;
            int y= (p.Y + thisFormLocation.Y) * 65536 /Screen.PrimaryScreen.Bounds.Height;
            Util.Action action = new ActionMouseLeftClick(x,y);
            action.Run();
            //mouse_event(MouseCommand.MOUSEEVENTF_LEFTDOWN , x, y, 0, 0);//点击
            //mouse_event(MouseCommand.MOUSEEVENTF_LEFTUP , x, y, 0, 0);//抬起
        }
   

    
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan diff = DateTime.Now - startDateTime;

            if (diff.Seconds == 0&&
                diff.Minutes == 0&&
                diff.Hours == 0)
            {
                IntPtr myIntPtr = FindWindow(null, this.Text); //null为类名，可以用Spy++得到，也可以为空
                ShowWindow(FindWindow(null, this.Text), MouseCommand.SW_RESTORE); //将窗口还原
                SetForegroundWindow(myIntPtr); //如果没有ShowWindow，此方法不能设置最小化的窗口
                MouseMoveAndClick(this.Location);
            }

            DateTime dateTimeClickCore = DateTime.Now;
            diff = dateTimeClickCore - startDateTime;
            if (diff.Seconds == 10 &&
                diff.Minutes == 0 &&
                diff.Hours == 0)
            {
                int x = (277 + this.Location.X) * 65536 / Screen.PrimaryScreen.Bounds.Width;
                int y = (146 + this.Location.Y) * 65536 / Screen.PrimaryScreen.Bounds.Height;
                Util.Action action = new ActionMouseLeftClick(x, y);
                action.Run();
            }

            DateTime dateTimeExit = DateTime.Now;
            diff = dateTimeExit - dateTimeClickCore;
            if (diff.Seconds == 2 &&
                diff.Minutes == 0 &&
                diff.Hours == 0)
            {

                Application.Exit();
            }


        }

     

        private void button_start_Click(object sender, EventArgs e)
        {
            if (p.IsEmpty)
            {
                MessageBox.Show("请在屏幕上右键选择点击位置");
                return;
            }
            hour = dateTimePicker1.Value.Hour;
            min = dateTimePicker1.Value.Minute;
            second = dateTimePicker1.Value.Second;
            startDateTime = dateTimePicker1.Value;


            timer1.Enabled = true;
            timer1.Interval = 500;

            button_start.Enabled = false;
            button_end.Enabled = true;
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            button_end.Enabled = false;
            button_start.Enabled = true;
        }



        private void button_goto_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox_url.Text);
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBox_url.Text = webBrowser1.Url.ToString();
        }

        private void linkLabel_Instructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("右键在左侧浏览器上选择点击位置，然后在DatePicker上选择时间，点击开始，到点鼠标会点击右键记录的位置。窗体可以最小化，可以移动，但不能关闭。目前还不支持左侧浏览器，滚动条滑动距离还原，也就是右键选择位置后不要再动浏览器上的滑动条。");
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();        //可以要，也可以不要，取决于是否隐藏主窗体
                this.notifyIcon1.Visible = true;
            }

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.notifyIcon1.Visible = true;
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }
    }

   


    

}
