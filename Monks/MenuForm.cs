using System;
using System.Windows.Forms;

namespace Monks
{
    public partial class MenuForm : StyleForm
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        public static void Show(string msg)
        {
            MenuForm m = new MenuForm();
            m.label1.Text = msg;
            m.ShowDialog();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.pichomeSel;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.pichomeDown;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.pichome;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.pichomeDown;
            DialogResult = DialogResult.OK;
        }
    }
}
