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
    public partial class MyDepartamentRecordForm : Form
    {
        DataTable User;
        public MyDepartamentRecordForm(DataTable user)
        {
            InitializeComponent();

            User = user;

            DB db = new DB();
            DataTable dataTable = new DataTable();
            dataTable = db.getDepartamenthPatient(Convert.ToInt16(user.Rows[0]["departmentId"]));
            guna2DataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = searchField.Text;
            DB db = new DB();
            DataTable dt = new DataTable();

            if (keyword == "")
            {
                dt = db.getData("SELECT id as ID, created_at as \"Дата записи\", \"FIO\" as ФИО, record_type as \"Тип записи\", address as Адрес, content as Содержимое, \"userId\" as \"ID Врача\" FROM \"Patient\"");
                guna2DataGridView1.DataSource = dt;
                return;
            }

            dt = db.getSearchPatient(keyword);
            guna2DataGridView1.DataSource = dt;
        }
    }
}
