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
    public partial class LKForm : Form
    {
        DataTable user;
        public LKForm(DataTable dt)
        {
            InitializeComponent();
            user = dt;

            guna2TextBox1.Text = dt.Rows[0]["FIO"].ToString();
            guna2TextBox2.Text = dt.Rows[0]["email"].ToString();
            guna2TextBox3.Text = dt.Rows[0]["password"].ToString();
            int userId = Convert.ToInt32(dt.Rows[0]["id"]);

            DB db = new DB();
            DataTable dtPaient = new DataTable();
            dtPaient = db.getPatientId(userId);

            label4.Text = "Количество моих записей: " + dtPaient.Rows.Count;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fio = guna2TextBox1.Text;
            string email = guna2TextBox2.Text;
            string password = guna2TextBox3.Text;

            if (fio == "")
            {
                guna2TextBox1.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (email == "")
            {
                guna2TextBox2.BorderColor = Color.FromArgb(255, 155, 155);
            }

            if (password == "")
            {
                guna2TextBox3.BorderColor = Color.FromArgb(255, 155, 155);
            }



            if (fio == "" || email == "" || password == "")
            {
                return;
            }

            DB db = new DB();
            DataTable updateUser;
            int userId = Convert.ToInt32(user.Rows[0]["id"]);

            db.updataUser(userId, fio, email, password);

            updateUser = db.login(email, password);

            guna2TextBox1.Text = updateUser.Rows[0]["FIO"].ToString();
            guna2TextBox2.Text = updateUser.Rows[0]["email"].ToString();
            guna2TextBox3.Text = updateUser.Rows[0]["password"].ToString();
        }
    }
}
