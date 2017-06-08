using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockERP
{
    
    public partial class Form1 : Form
    {
        
        int relateX ;
        int relateY ;
        Boolean flag;
        const int MOUSEEVENTF_MOVE = 0x0001;    //  移动鼠标
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;// 模拟鼠标左键按下
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下
        const int MOUSEEVENTF_RIGHTUP = 0x0010;// 模拟鼠标右键抬起
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标

        [DllImport("user32.dll", EntryPoint = "mouse_event", SetLastError = true)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);


        public static void MouseRightClick()
        {
            //点击鼠标右键
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 410, 0, 0, 0);
        }

        public static void MouseLeftClick()
        {
            //点击鼠标右键
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 410, 0, 0, 0);
        }
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolTip1.Show(String.Format("相对于主窗体位置{0},{1}", Control.MousePosition.X - this.Location.X, Control.MousePosition.Y - this.Location.Y), this.webBrowser1);
            if (flag)
            {

            }
           
            //tetoolTip1.SetToolTip(this.webBrowser1, String.Format("{0},{1}", Control.MousePosition.X, Control.MousePosition.Y));xtBox2.Text = String.Format("{0},{1}", Control.MousePosition.X, Control.MousePosition.Y);
            //DateTime now=DateTime.Now;
            //DateTime dt = DateTime.Parse("2017-05-22 17:43:20");
            //if (now > dt)
            //{
            //    Console.WriteLine("dr");
            //    timer1.Stop();
            //}
            //MouseLeftClick();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            //textBox1.Text = String.Format("{0},{1}", this.Location.X, this.Location.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            relateX = Convert.ToInt16(textBox1.Text);
            relateY = Convert.ToInt16(textBox2.Text);
            flag = true;
        }
    }

   


    

}
