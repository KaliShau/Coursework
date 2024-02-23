using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PRMS
{
    public partial class MyRecordsForm : Form
    {
        int userId;
        int patientId;
        public MyRecordsForm(DataTable user)
        {
            InitializeComponent();
            this.userId = Convert.ToInt16(user.Rows[0]["id"]);

            DB db = new DB();
            DataTable dt = new DataTable();
            dt = db.getPatientId(userId);
            guna2DataGridView1.DataSource = dt;
        }

        private void guna2DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel1.Visible = true;
            button2.Visible = true;
            label7.Visible = false;

            if (e.RowIndex == -1) return;

            DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
            try
            {
            patientId = Convert.ToInt32(row.Cells[0].Value);
            guna2TextBox1.Text = row.Cells[2].Value.ToString();
            guna2TextBox2.Text = row.Cells[3].Value.ToString();
            guna2TextBox3.Text = row.Cells[4].Value.ToString();
            guna2TextBox4.Text = row.Cells[5].Value.ToString();

            } catch
            {
                panel1.Visible = false;
                button2.Visible = false;
                label7.Visible = true;
            }
        }

         private void button2_Click(object sender, EventArgs e)
        {
            int id = patientId;
            string fio = guna2TextBox1.Text;
            string record = guna2TextBox2.Text;
            string address = guna2TextBox3.Text;
            string content = guna2TextBox4.Text;

            

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
                guna2TextBox4.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (fio == "" || record == "" || address == "" || content == "")
            {
                return;
            }

            DB db = new DB();
            DataTable dt = new DataTable();
             db.updataPatient(id, fio, record, address, content);

            dt = db.getPatientId(userId);
            guna2DataGridView1.DataSource = dt;
        }

        

       

        private void button1_Click(object sender, EventArgs e)
        {
            int id = patientId;

            DB db = new DB();
            DataTable dt = new DataTable();
            db.deletePatient(id);

            dt = db.getPatientId(userId);
            guna2DataGridView1.DataSource = dt;

            panel1.Visible = false;
            button2.Visible = false;
            label7.Visible = true;
        }

        
    }
}
