using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
using System.Threading;
using agsXMPP.protocol.iq.avatar;
using agsXMPP.protocol.iq.vcard;

namespace XMPPClientbydb
{
    public partial class loggednimbuzz : Form
    {
        private ListBox.ObjectCollection objectCollection;
        private agsXMPP.XmppClientConnection dbcon;
//        private agsXMPP.XmppClientConnection dbco2;

       

        public loggednimbuzz(ListBox.ObjectCollection objectCollection, agsXMPP.XmppClientConnection dbcon)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.objectCollection = objectCollection;
            this.dbcon = dbcon;
      //      dbcon.OnRosterItem += new XmppClientConnection.RosterHandler(dbcon_OnRosterItem);
            dbcon.OnMessage += new agsXMPP.protocol.client.MessageHandler(dcon_onmsg);
            
            dbcon.OnPresence += new agsXMPP.protocol.client.PresenceHandler(dbcon_onpresence);
            
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


            if (msg.Type == MessageType.groupchat)
            {
                if (msg.From.Resource == "admin")
                {
                    pictureBox1.Load(msg.Body.Replace("Enter the right answer to start chatting.", ""));
                    // textBox5.Text = msg.Body.Replace("Enter the right answer to start chatting.", "");
                }

                if (msg.From.User != string.Empty)
                {


                    if (msg.From.User + "@conference.nimbuzz.com" == Roomjid.ToString())
                    {
                        var user = msg.From.Resource;

                        richTextBox1.SelectionColor = Color.Blue;

                        richTextBox1.AppendText(user + " ");

                        richTextBox1.SelectionColor = Color.Red;
                        richTextBox1.AppendText("[" + DateTime.Now.ToShortTimeString() + "]" + ": ");

                        richTextBox1.SelectionColor = richTextBox1.ForeColor;
                        richTextBox1.AppendText(msg.Body);
                        richTextBox1.AppendText("\r\n");

                    }

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

   //         if (pres.From.User != null && pres.Type == PresenceType.available)
   //         {
   //             listBox1.Items.Add(pres.From.User + "@" + pres.From.Server + "/" + pres.From.Resource);
  //             
   //         }


            if (pres.Type == PresenceType.available && pres.From.User != (sender as XmppClientConnection).Username && pres.From.Server.ToLower() == "nimbuzz.com")
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString().StartsWith(pres.From.Bare))
                    {
                        listBox1.Items.RemoveAt(i);
                    }
                }
                listBox1.Items.Add(pres.From.User + "@" + pres.From.Server + "/" + pres.From.Resource);
               
           }
            else if (pres.Type == PresenceType.unavailable)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString().StartsWith(pres.From.User))
                    {
                        listBox1.Items.RemoveAt(i);
                    }
                }
            }


        }



        public static string sendtextusr = "";
        private void loggednimbuzz_Load(object sender, EventArgs e)
        {
            label2.Text = formnimbuzz.sendtext;
            //
            sendtextusr = label2.Text;

            GetMyVcard();
        }

        public void GetMyVcard()
        {
            //  throw new NotImplementedException();

            VcardIq viq = new VcardIq(IqType.get);
            dbcon.IqGrabber.SendIq(viq, new IqCB(VcardResult), null);


        }

        private void VcardResult(object sender, IQ iq, object data)
        {
            //  throw new NotImplementedException();

            if (iq.Type == IqType.result) 
            {
                Vcard vcard = iq.Vcard;
                if (vcard != null)
                {
                    vcard.Photo = new Photo(pictureBox2.Image, System.Drawing.Imaging.ImageFormat.Png);
                    
               }
            }
        }

       

        private Jid Roomjid;
        agsXMPP.protocol.x.muc.MucManager manager;
        private void button1_Click_1(object sender, EventArgs e)
        {
            manager = new agsXMPP.protocol.x.muc.MucManager(dbcon);
            Jid room = new Jid(textBox1.Text + "@conference.nimbuzz.com");
            //manager.JoinRoom(room, Username);
            //manager.AcceptDefaultConfiguration(room);
            Roomjid = room;

            //you can also use xml to join room
            try
            {
                dbcon.Send("<presence to='" + textBox1.Text + "@conference.nimbuzz.com/" + textBox3.Text + "' xml:lang='en'><x xmlns='http://jabber.org/protocol/muc' /></presence>");
            }

            catch { }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            manager.LeaveRoom(Roomjid, textBox3.Text);
            richTextBox1.AppendText("You have left this conference!");
            richTextBox1.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
            msg.Type = MessageType.groupchat;
            msg.To = Roomjid;
            msg.Body = textBox2.Text;
            dbcon.Send(msg);
            textBox2.Clear();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (richTextBox2.Text != string.Empty)
            {
                agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                msg.Type = MessageType.groupchat;
                msg.To = Roomjid;
                msg.Body = richTextBox2.Text;
                dbcon.Send(msg);
                richTextBox2.Clear();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dbcon.SocketDisconnect();
            this.Hide();
            Form frmbck = new formnimbuzz();
            frmbck.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // Form profileform = new profilenimbuzz(dbcon);
           // profileform.ShowDialog();

            var profileform = new profilenimbuzz(dbcon);
            profileform.ShowDialog();

            
            //var loggednimbuzz = new profilenimbuzz(dbcon);
             //   loggednimbuzz.ShowDialog();
            //pictureBox2.Load("http://avatar.nimbuzz.com/getAvatar?jid=" + label2.Text + "%40nimbuzz.com");

        }

        
        public static string chatsendtext = "";
        
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chatsendtext = listBox1.SelectedItem.ToString();
            if(chatsendtext != null)
            {
                var buddyform = new chatnimbuddy(dbcon);
                buddyform.ShowDialog();
                
            }

            
        }

        string itemtip;
        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
           // ListBox listBox1 = sender as ListBox;
            itemtip = "double click on me and start chatting";
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != -1 && toolTip1.GetToolTip(listBox1) != itemtip)
            {
                toolTip1.SetToolTip(listBox1, itemtip);
            }
            
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
           
             
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click_1(sender, e);
                richTextBox2.Clear();
                this.richTextBox2.Focus();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //  DB~@NC BLOG
            Process.Start("http://dbh4ck.blogspot.com");
        }




        
    }



}
