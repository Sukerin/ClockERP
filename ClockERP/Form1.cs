using ClockERP.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClockERP
{
    
    public partial class Form1 : Form, IMessageFilter
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Application.AddMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == MessageID.WM_RBUTTONDOWN)
            {
                //textBox1.Text =this.PointToClient(Control.MousePosition).X.ToString();
                //textBox2.Text =this.PointToClient(Control.MousePosition).Y.ToString();
                Point p = new Point(Control.MousePosition.X - this.Location.X, Control.MousePosition.Y - this.Location.Y);
                textBox1.Text = p.X.ToString();
                textBox2.Text = p.Y.ToString();
            }
            return false;
        }

        private int relateX;
        private int relateY;


        [DllImport("user32.dll", EntryPoint = "mouse_event", SetLastError = true)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);


        public void MouseRightClick()
        {
            //点击鼠标右键
            mouse_event(MouseCommand.MOUSEEVENTF_RIGHTDOWN | MouseCommand.MOUSEEVENTF_RIGHTUP, 410, 0, 0, 0);
        }

        public void MouseLeftClick()
        {
            //点击鼠标左键
            mouse_event(MouseCommand.MOUSEEVENTF_LEFTDOWN | MouseCommand.MOUSEEVENTF_LEFTUP, 410, 0, 0, 0);
        }

        public void MouseMoveAndClick(Point thisFormLocation)
        {
            
            int x= (relateX + thisFormLocation.X)* 65536 /Screen.PrimaryScreen.Bounds.Width;
            int y= (relateY + thisFormLocation.Y) * 65536 /Screen.PrimaryScreen.Bounds.Height;
            //移动and点击鼠标左键
            //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
            //mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            //mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            mouse_event(MouseCommand.MOUSEEVENTF_MOVE | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);

            mouse_event(MouseCommand.MOUSEEVENTF_LEFTDOWN | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);//点击
            mouse_event(MouseCommand.MOUSEEVENTF_LEFTUP | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);//抬起

            //mouse_event(MouseCommand.MOUSEEVENTF_MOVE | MouseCommand.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);
        }
   

    
        private void timer1_Tick(object sender, EventArgs e)
        {
            MouseMoveAndClick(this.Location);
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            relateX = Convert.ToInt16(textBox1.Text);
            relateY = Convert.ToInt16(textBox2.Text);
            timer1.Enabled = true;
            timer1.Interval = 3000;
        }

        
    }

   


    

}
