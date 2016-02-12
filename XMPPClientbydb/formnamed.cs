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
    public partial class formnamed : Form
    {
        public formnamed()
        {
            InitializeComponent();
        }

        private void formnamed_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Form formmain = new Form1();
            formmain.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
