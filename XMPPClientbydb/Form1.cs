using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPPClientbydb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                Form form2 = new formnimbuzz();
                form2.Show();
                
            }

            if (radioButton2.Checked)
            {
                Form form3 = new formgmail();
                form3.Show();
            }

            if (radioButton3.Checked)
            {
                Form form4 = new formnamed();
                form4.Show();
            }

            if (radioButton4.Checked)
            {
                Form form5 = new formfb();
                form5.Show();
            }

            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click()
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            
           
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
