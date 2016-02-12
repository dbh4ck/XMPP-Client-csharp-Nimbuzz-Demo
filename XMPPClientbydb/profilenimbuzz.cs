using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using agsXMPP;
using System.Diagnostics;
using agsXMPP.Exceptions;
using agsXMPP.Collections;
using agsXMPP.Util;
using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;

namespace XMPPClientbydb
{
    public partial class profilenimbuzz : Form
    {
        private agsXMPP.XmppClientConnection dbcon;

       
        public profilenimbuzz(agsXMPP.XmppClientConnection dbcon)
        {
                 // TODO: Complete member initialization
            InitializeComponent();
            this.dbcon = dbcon;
        //    dbcon.OnIq += new agsXMPP.protocol.client.IqHandler(dbcon_iq);
        //    dbcon.OnReadXml += new XmlHandler(dbcon_xmlread);
        //    dbcon.OnWriteXml += new XmlHandler(dbcon_xmlwrite);
         }

       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dbcon.Send("<iq from='" + dbcon.Username.Replace("&", "&amp;").Replace(">", "&gt;").Replace("'", "&apos;") + "@nimbuzz.com' type='set' to='uprofile.nimbuzz.com' id='dbhere'><profile xmlns='http://jabber.org/protocol/profile'><x xmlns='jabber:x:data' type='submit'><field type='hidden' var='FORM_TYPE'><value>http://jabber.org/protocol/profile</value></field><field var='nickname'><value>" + textBox1.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + " </value></field><field var='status'><value>" + textBox2.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</value></field><field var='gender'><value>"+ gender +"</value></field><field var='birth_dayofmonth'><value>" + numericUpDown1.Value + "</value></field><field var='birth_month'><value>" + numericUpDown2.Value + "</value></field><field var='birth_year'><value>" + numericUpDown3.Value + "</value></field><field var='country'><value>" + textBox3.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</value></field><field var='locality'><value>" + textBox4.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</value></field><field var='region'><value>" + textBox5.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</value></field><field var='street'><value>" + textBox6.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</value></field></x></profile></iq> ");
                MessageBox.Show("Your Nimbuzz Profile has been updated!");
                this.Hide();
                

            }
            catch
            { 
            
            }

        }

        string gender;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                
                    gender = "male";
                
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
               
                    gender = "female";
                
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void profilenimbuzz_Load(object sender, EventArgs e)
        {
            
           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        
    }
}
