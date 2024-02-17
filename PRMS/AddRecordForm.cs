using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRMS
{
    public partial class AddRecordForm : Form
    {
        int userId;
        public AddRecordForm(DataTable user)
        {
            InitializeComponent();
            this.userId = Convert.ToInt16(user.Rows[0]["id"]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fio = guna2TextBox1.Text;
            string record = guna2TextBox2.Text;
            string address = guna2TextBox3.Text;
            string content = richTextBox1.Text;

            if (fio == "")
            {
                guna2TextBox1.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (record == "")
            {
                guna2TextBox2.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (address == "")
            {
                guna2TextBox3.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (content == "")
            {
                panel3.BackColor = Color.FromArgb(255, 155, 155);

            }

            if (fio == "" || record == "" || address == "" || content == "")
            {
                    return;
            }

            DB db = new DB();
            db.createPatient(userId, fio, record, address, content);

            


           panel1.Visible = false;
           panel2.Visible = false;
           label5.Visible = true;
        }

    }
}
