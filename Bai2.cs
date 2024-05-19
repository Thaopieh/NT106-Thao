using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MailKit.Net.Imap;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace WindowsFormsApp1
{
    public partial class Bai2 : Form
    {
        public Bai2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listView1.Columns.Add("Email", 200);
            listView1.Columns.Add("From", 100);
            listView1.Columns.Add("Thời gian", 100);
            listView1.View = View.Details;


            var client = new ImapClient();
            client.Connect("127.0.0.1", 143, 0);


            try {
                client.Authenticate(textBox1.Text, textBox2.Text);

                var inbox = client.Inbox;
                inbox.Open(MailKit.FolderAccess.ReadOnly);
                for (int i = 0; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                ListViewItem name = new ListViewItem(message.Subject);
                ListViewItem.ListViewSubItem from = new
                ListViewItem.ListViewSubItem(name, message.From.ToString());
                name.SubItems.Add(from);
                ListViewItem.ListViewSubItem date = new
                ListViewItem.ListViewSubItem(name, message.Date.Date.ToString());
                name.SubItems.Add(date);
                listView1.Items.Add(name);
            }

            textBox3.Text = inbox.Count.ToString();
            textBox4.Text = inbox.Recent.ToString();

        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void Bai2_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;

        }
    }
}
