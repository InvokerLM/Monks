using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Monks
{
    public partial class PassForm : Form
    {
        public PassForm()
        {
            InitializeComponent();
        }

        private string GetMD5(string pass)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(pass);
            byte[] tagData = md5.ComputeHash(fromData);

           return System.BitConverter.ToString(tagData);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string s=GetMD5(textBox1.Text);
            if (s == "35-64-BE-03-05-00-70-82-5D-B0-7A-E8-9D-07-83-7B")
            {
               this. DialogResult = DialogResult.OK;
            }
        }
    }


}
