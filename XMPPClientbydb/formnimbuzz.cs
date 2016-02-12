using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using agsXMPP;
using System.Diagnostics;
using agsXMPP.Exceptions;
using agsXMPP.Collections;
using agsXMPP.Util;
using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.iq.roster;
using agsXMPP.protocol.iq.vcard;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.Threading;
using MehdiComponent;
using MehdiComponent.MehdiXmpp.Create;
using MehdiComponent.Xml;


namespace XMPPClientbydb
{
    

    public partial class formnimbuzz : Form

    {
        
        
        XmppClientConnection dbcon = new XmppClientConnection();
    //    private Roster _roster;
        private Random random = new Random();

        public formnimbuzz()
        {
            InitializeComponent();

            dbcon.OnLogin += new ObjectHandler(dbcon_onlogin);
            dbcon.OnAuthError += new XmppElementHandler(dbcon_onerror);
            dbcon.OnPresence += new agsXMPP.protocol.client.PresenceHandler(dbcon_onpresence);
            dbcon.OnMessage += new agsXMPP.protocol.client.MessageHandler(dcon_onmsg);
            dbcon.OnReadXml += new XmlHandler(dbcon_OnReadXml);
            // dbcon.OnWriteXml += new XmlHandler(XmppCon_OnWriteXml);
         //   dbcon.OnRosterItem += new XmppClientConnection.RosterHandler(dbcon_OnRosterItem);
        //    dbcon.OnRosterStart += new ObjectHandler(dbcon_OnRosterStart);
        //    dbcon.OnRosterEnd += new ObjectHandler(dbcon_OnRosterEnd);
            dbcon.OnIq += new IqHandler(dbcon_oniq);

            {
                idMaker.Created += idMaker_Created;
                idMaker.ErrorCreate += idMaker_ErrorCreate;
                idMaker.Available += idMaker_Available;
                idMaker.WrongCaptcha += idMaker_WrongCaptcha;
                idMaker.InvalidPassword += idMaker_InvalidPassword;
                idMaker.LoadChaptcha(pictureBox1);
                

            }
         
        
        }


        //------id maker----

        private IdMaker idMaker = new IdMaker();

        //------id maker------


//         private void dbcon_OnRosterEnd(object sender)
        //       {
  //           if (InvokeRequired)
  //           {
   //              BeginInvoke(new ObjectHandler(dbcon_OnRosterEnd), new object[] { sender });
//                 return;
  //           }
  //           dbcon.SendMyPresence();
  //       }

    //     private void dbcon_OnRosterStart(object sender)
    //     {
       //      if (InvokeRequired)
          //   {
         //        BeginInvoke(new ObjectHandler(dbcon_OnRosterStart), new object[] { sender });
        //         return;
        //     }
        // }

   //      private void dbcon_OnRosterItem(object sender, agsXMPP.protocol.iq.roster.RosterItem item)
      //   {
         //    if (InvokeRequired)
            // {
               //  BeginInvoke(new XmppClientConnection.RosterHandler(dbcon_OnRosterItem), new object[] { sender, item });
                // return;
         //    }
            //listBox1.Items.Add(String.Format("{0}", item.Jid.User));
            //listBox1.SelectedIndex = listBox1.Items.Count - 1;
           // _roster.AddRosterItem(item);


        //  }

        

        private void dbcon_oniq(object sender, IQ iq)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new IqHandler(dbcon_oniq), new object[] { sender, iq });
                return;
            }

            if (iq.Query != null)
            {
                if (iq.Query.GetType() == typeof(agsXMPP.protocol.iq.version.Version))
                {
                    agsXMPP.protocol.iq.version.Version vers = (agsXMPP.protocol.iq.version.Version)iq.Query;
                    if (iq.Type == agsXMPP.protocol.client.IqType.get)
                    {
                        iq.SwitchDirection();
                        iq.Type = agsXMPP.protocol.client.IqType.result;
                        vers.Name = "XMPPClient by db~@NC";
                        vers.Ver = "1.0.0";
                        vers.Os = "Coded By db~@NC\nFor More Visit: http://dbh4ck.blogspot.in";
                        ((XmppClientConnection)sender).Send(iq);
                    }
                }
            }

        }

        private void dbcon_OnReadXml(object sender, string xml)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new agsXMPP.XmlHandler(dbcon_OnReadXml), new object[] { sender, xml });
                return;
            }
        }

        private void dcon_onmsg(object sender, agsXMPP.protocol.client.Message msg)
        {
            if (msg.Type == MessageType.error || msg.Body == null)
                return;
            if (InvokeRequired)
            {
                BeginInvoke(new agsXMPP.protocol.client.MessageHandler(dcon_onmsg), new object[] { sender, msg });
                return;

            }



            //if message is a group chat message
            if (msg.From.User != string.Empty)
            {
                //if (msg.From.User + "@conference.nimbuzz.com" == Roomjid.ToString())
               // {
                //    richTextBox1.SelectionColor = Color.Green;
               //     richTextBox1.AppendText(msg.From.Resource + ": ");
               //     richTextBox1.AppendText(msg.Body);
               //     richTextBox1.AppendText("\r\n");

               // }

            }


            if (msg.Type == MessageType.groupchat)
            {
                if (msg.From.Resource == "admin")
                {
                  //  pictureBox2.Load(msg.Body.Replace("Enter the right answer to start chatting.", ""));
                    // textBox5.Text = msg.Body.Replace("Enter the right answer to start chatting.", "");
                }
            }
        }

        private void dbcon_onpresence(object sender, Presence pres)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new agsXMPP.protocol.client.PresenceHandler(dbcon_onpresence), new object[] { sender, pres });
                return;
            }

           
        }

        private void dbcon_onerror(object sender, Element e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XmppElementHandler(dbcon_onerror), new object[] { sender, e });
                return;
            }

            MessageBox.Show("Yu have entered a wrong username or password", "Attention!");

            listBox1.Items.Add("Wrong username or password");
            textBox1.BackColor = Color.Red;
            textBox2.BackColor = Color.Red;
        }

        private void dbcon_onlogin(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(dbcon_onlogin), new object[] { sender });
                return;
            }
            listBox1.Items.Remove("Please wait...");
         //   listBox1.Items.Add("Yu are logged in successfully");
            textBox1.BackColor = Color.Green;
            textBox2.BackColor = Color.Green;

            this.Hide();
            var loggednimbuzz  = new loggednimbuzz(listBox1.Items,dbcon);
            loggednimbuzz.ShowDialog();

            
        }

        private void formnimbuzz_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Form formmain = new Form1();
            formmain.ShowDialog();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public static string sendtext = "";
    //    public static string passtext = "";
   //     private ListBox.ObjectCollection objectCollection;
        private void button1_Click(object sender, EventArgs e)
        {
            
            listBox1.Items.Add("Please wait...");
            dbcon.Server = textBox3.Text;
            dbcon.ConnectServer = textBox9.Text;
            dbcon.Open(textBox1.Text, textBox2.Text, "XMPPClientbydb" + (object)this.random.Next(100000, 9999999));
            dbcon.Username = textBox1.Text;
            dbcon.Password = textBox2.Text;
         //   dbcon.Resource = textBox5.Text;
            dbcon.Priority = 10;
            dbcon.Status = "Online via DBuzz :)";
            dbcon.Port = 5222;
            dbcon.AutoAgents = false;
            dbcon.AutoResolveConnectServer = true;
            dbcon.UseCompression = false;

            sendtext = textBox1.Text;

           // passtext = textBox5.Text;
           
        }

       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }


        private void idMaker_Created(IdMaker idMaker, string msg, string data)
        {
            listBox2.Items.Add("New Account Created!");
            MessageBox.Show("Id Created Sucessfully!", "Attention!");
            pictureBox1.Hide();
            return;
        }

        private void idMaker_ErrorCreate(IdMaker idMaker)
        {
            
            MessageBox.Show("Error in Creating Id", "Attention!");
            return;
        }

        private void idMaker_Available(IdMaker idMaker, bool available)
        {
            Invoke(new Action(delegate
            {
                if (available == false)
                {
                    var msgBox = new MehdiMessageBox("\nThis Id is Already Created!", "Attention",
                    MehdiMessageBox.Type.Error);
                    //msgBox.MessageColor = Color.Red;
                    //msgBox.TitleColor = Color.Red;
                    msgBox.ShowDialog();
                }
            }));
        }


        private void idMaker_InvalidPassword(IdMaker idMaker, string msg)
        {
            MessageBox.Show("Invalid Password", "Error");
            return;
        }

        private void idMaker_WrongCaptcha(IdMaker idMaker, string msg)
        {
            MessageBox.Show("Wrong Captcha", "Error");
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Please Enter Your Account Name");
                return;
            }
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("Please Enter Your Password");
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please Enter Captcha");
                return;
            }
            idMaker.Create(textBox7.Text, textBox8.Text, textBox6.Text);
            textBox9.Clear();
            listBox2.Items.Add("Creating ID....");
            
            //pictureBox1.Hide();
           // idMaker.LoadChaptcha(pictureBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            idMaker.LoadChaptcha(pictureBox1);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void formnimbuzz_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            idMaker.LoadChaptcha(pictureBox1);
        }

       

    }
}
