using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    public partial class chatnimbuddy : Form
    {
        private agsXMPP.XmppClientConnection dbcon;
       
        
        public chatnimbuddy(agsXMPP.XmppClientConnection dbcon)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.dbcon = dbcon;

            agsXMPP.Jid JID = new Jid(label2.Text);
            dbcon.MessageGrabber.Add(JID, new agsXMPP.Collections.BareJidComparer(), new MessageCB(MessageCallBack), null);
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
            msg.Type = agsXMPP.protocol.client.MessageType.chat;
            msg.To = JID;
            msg.Body = richTextBox2.Text;
            dbcon.OnMessage += new agsXMPP.protocol.client.MessageHandler(dbcon_onmsg);
            dbcon.OnPresence += new agsXMPP.protocol.client.PresenceHandler(dbcon_onpresence);
            label2.Text = loggednimbuzz.chatsendtext;
        }

        private void MessageCallBack(object sender, agsXMPP.protocol.client.Message msg, object data)
        {
            if (msg.Body != null)
            {
                try
                {
                    string jid;
                    jid = msg.From.User;
                    richTextBox1.SelectionColor = Color.Purple;
                    richTextBox1.AppendText(jid + " ");

                    richTextBox1.SelectionColor = Color.Red;
                    richTextBox1.AppendText("[" + DateTime.Now.ToShortTimeString() + "]" + ": ");

                    richTextBox1.SelectionColor = richTextBox1.ForeColor;
                    richTextBox1.AppendText(msg.Body);
                    richTextBox1.AppendText("\r\n");

                }

                catch { }
            }
        }


        

        
        private void dbcon_onmsg(object sender, agsXMPP.protocol.client.Message msg)
        {
            if (msg.Type == MessageType.error || msg.Body == null)
                return;
            if (InvokeRequired)
            {
                BeginInvoke(new agsXMPP.protocol.client.MessageHandler(dbcon_onmsg), new object[] { sender, msg });
                return;
            }


            if (msg.Type == MessageType.chat)
            {
                if (msg.From == label2.Text)

                //  START auto-reply db~

                //   string[] chatMessage;
                //   chatMessage = msg.From.ToString().Split('/');
                //   agsXMPP.Jid jid = new agsXMPP.Jid(chatMessage[0]);        //////////////  this code is to setup an auto-reply functionality  ///////////////
                // jid = new agsXMPP.Jid(chatMessage[0]);
                //   agsXMPP.protocol.client.Message reply = new agsXMPP.protocol.client.Message(jid, agsXMPP.protocol.client.MessageType.chat, richTextBox2.Text);
                //  reply = new agsXMPP.protocol.client.Message(jid, agsXMPP.protocol.client.MessageType.chat, richTextBox2.Text);
                //    dbcon.Send(reply);
                //  var user = msg.From.Resource; 
                //  END auto-reply db~
                {

                    try
                    {
                        string jid;
                        jid = msg.From.User;
                        richTextBox1.SelectionColor = Color.Purple;
                        richTextBox1.AppendText(jid + " ");

                        richTextBox1.SelectionColor = Color.Red;
                        richTextBox1.AppendText("[" + DateTime.Now.ToShortTimeString() + "]" + ": ");

                        richTextBox1.SelectionColor = richTextBox1.ForeColor;
                        richTextBox1.AppendText(msg.Body);
                        richTextBox1.AppendText("\r\n");

                    }

                    catch { }
                }

            }

        }

        private void dbcon_onpresence(object sender, agsXMPP.protocol.client.Presence pres)
        {
            
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dbcon.Send("<message xmlns='jabber:client' to='" +label2.Text+ "' type='chat'><body>" + richTextBox2.Text + "</body><active xmlns='http://jabber.org/protocol/chatstates' /><x xmlns='jabber:x:event'><composing /></x></message>");

                richTextBox1.SelectionColor = Color.Blue;
                richTextBox1.AppendText(getuser + " ");

                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.AppendText("[" + DateTime.Now.ToShortTimeString() + "]" + ": ");


                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.AppendText(richTextBox2.Text);
                richTextBox1.AppendText("\r\n");

                richTextBox2.Clear();
            }
            catch { }
            
        }


        string getuser;
        private void chatnimbuddy_Load(object sender, EventArgs e)
        {
            getuser = loggednimbuzz.sendtextusr;
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
                richTextBox2.Clear();
                this.richTextBox2.Focus();                
            }
        }

        private void button1_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
