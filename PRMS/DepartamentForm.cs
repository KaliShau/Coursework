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
using System.Xml.Linq;

namespace PRMS
{
    public partial class DepartamentForm : Form
    {
        DataTable User;
        int departamentId;
        public DepartamentForm(DataTable user)
        {
            InitializeComponent();
            this.User = user;

            DB db = new DB();
            DataTable departament;
            departament = db.getData("SELECT id as ID, created_at as \"Дата создания\", name as Наименование FROM \"Department\"");
            guna2DataGridView1.DataSource = departament;
        }

        private void guna2DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel2.Visible = true;
            if (e.RowIndex == -1) return;

            DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
            try
            {
                departamentId = Convert.ToInt32(row.Cells[0].Value);
                guna2TextBox1.Text = row.Cells[2].Value.ToString();

            }
            catch
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox2.Text;
            if (name == "")
            {
                guna2TextBox2.BorderColor = Color.Red;
                return;
            } else
            {
                guna2TextBox2.BorderColor = Color.FromArgb(213, 218, 223);

            }

            DB db = new DB();
            DataTable departament = new DataTable();
            db.createDepartament(name);

            departament = db.getData("SELECT id as ID, created_at as \"Дата создания\", name as Наименование FROM \"Department\"");
            guna2DataGridView1.DataSource = departament;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text;
            if (name == "")
            {
                guna2TextBox1.BorderColor = Color.Red;
                return;
            }
            else
            {
                guna2TextBox1.BorderColor = Color.FromArgb(213, 218, 223);

            }

            DB db = new DB();
            DataTable departament = new DataTable();
            db.updateDepartament(name, departamentId);

            departament = db.getData("SELECT id as ID, created_at as \"Дата создания\", name as Наименование FROM \"Department\"");
            guna2DataGridView1.DataSource = departament;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable departament = new DataTable();
            db.deleteDepartament(departamentId);

            departament = db.getData("SELECT id as ID, created_at as \"Дата создания\", name as Наименование FROM \"Department\"");
            guna2DataGridView1.DataSource = departament;
        }
    }
}
