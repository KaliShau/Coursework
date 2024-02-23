using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PRMS
{
    public partial class AdminForm : Form
    {

        int userId;
        public AdminForm(DataTable dt)
        {
            InitializeComponent();

            DB db = new DB();
            DataTable dtUsers;
            dtUsers = db.getData("SELECT id as ID, created_at as \"Дата создания\", \"FIO\" as \"ФИО\", email as \"Логин\", password as \"Пароль\" FROM \"User\" ");
            guna2DataGridView1.DataSource = dtUsers;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = userId;
            string fio = guna2TextBox1.Text;
            string login = guna2TextBox2.Text;
            string password = guna2TextBox3.Text;



            if (fio == "")
            {
                guna2TextBox1.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (login == "")
            {
                guna2TextBox2.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (password == "")
            {
                guna2TextBox3.BorderColor = Color.FromArgb(255, 155, 155);
            }

           

            if (fio == "" || login == "" || password == "")
            {
                return;
            }

            DB db = new DB();
            DataTable dt = new DataTable();
            db.updataUser(id, fio, login, password);

            dt = db.getData("SELECT id as ID, created_at as \"Дата создания\", \"FIO\" as \"ФИО\", email as \"Логин\", password as \"Пароль\" FROM \"User\" ");
            guna2DataGridView1.DataSource = dt;
        }

        private void guna2DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;
            label5.Visible = false;

            if (e.RowIndex == -1) return;

            DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
            try
            {
                userId = Convert.ToInt32(row.Cells[0].Value);
                guna2TextBox1.Text = row.Cells[2].Value.ToString();
                guna2TextBox2.Text = row.Cells[3].Value.ToString();
                guna2TextBox3.Text = row.Cells[4].Value.ToString();

            }
            catch
            {
                panel1.Visible = false;
                panel2.Visible = false;
                label5.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable dt = new DataTable();
            db.deleteUser(userId);

            dt = db.getData("SELECT id as ID, created_at as \"Дата создания\", \"FIO\" as \"ФИО\", email as \"Логин\", password as \"Пароль\" FROM \"User\" ");
            guna2DataGridView1.DataSource = dt;
        }
    }
}
