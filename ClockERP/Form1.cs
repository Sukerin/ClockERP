using ClockERP.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClockERP
{
    
    public partial class Form1 : Form, IMessageFilter
    {
        [DllImport("user32.dll", EntryPoint = "mouse_event", SetLastError = true)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private Point p;     
        private int hour ;
        private int min ;
        private int second ;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle=FormBorderStyle.FixedSingle;
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
            mouse_event(MouseCommand.MOUSEEVENTF_MOVE | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);
            mouse_event(MouseCommand.MOUSEEVENTF_LEFTDOWN | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);//点击
            mouse_event(MouseCommand.MOUSEEVENTF_LEFTUP | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);//抬起

           
        }
   

    
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (DateTime.Now.Second == second&& 
                DateTime.Now.Minute == min&& 
                DateTime.Now.Hour == hour)
            {
                this.Activate();
                MouseMoveAndClick(this.Location);
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

            timer1.Enabled = true;
            timer1.Interval = 500;
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
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
    }

   


    

}
