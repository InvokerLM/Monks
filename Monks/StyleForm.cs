using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Monks
{
    public partial class StyleForm : Form
    {
        public StyleForm()
        {
            InitializeComponent();
            UpStyle();
        }
        private void UpStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            List<PointF> list = new List<PointF>();
            int width = this.Width;
            int height = this.Height;

            //左上
            list.Add(new Point(0, 3));
            list.Add(new Point(1, 1));
            list.Add(new Point(3, 0));

            //右上
            list.Add(new Point(width - 3, 0));
            list.Add(new Point(width - 1, 1));
            list.Add(new Point(width - 0, 3));
            //右下
            list.Add(new Point(width - 0, height - 3));
            list.Add(new Point(width - 1, height - 1)); ;
            list.Add(new Point(width - 3, height - 0));
            //左下
            list.Add(new Point(3, height - 0));
            list.Add(new Point(1, height - 1));
            list.Add(new Point(0, height - 3));

            PointF[] points = list.ToArray();

            GraphicsPath shape = new GraphicsPath();
            shape.AddPolygon(points);

            //将窗体的显示区域设为GraphicsPath的实例 
            this.Region = new System.Drawing.Region(shape);
            e.Graphics.DrawPath(new Pen(Color.LightGray, 2), shape);

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //移动
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < this.Width)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            base.OnMouseMove(e);
        }
    }
}
