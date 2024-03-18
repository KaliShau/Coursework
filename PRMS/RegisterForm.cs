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
using System.Xml.Linq;

namespace PRMS
{
    public partial class RegisterForm : Form
    {

        DataTable departament;

        public RegisterForm()
        {
            InitializeComponent();

            DB db = new DB();
            DataTable dt = new DataTable();

            List<string> departmentList = new List<string>();

            dt = db.getData("SELECT * FROM \"Department\"");
            departament = dt;

            foreach(DataRow row in dt.Rows)
{
                departmentList.Add(row["name"].ToString());
            }

            foreach (string department in departmentList)
            {
                guna2ComboBox1.Items.Add(department);
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {

            string login = LoginField.Text;
            string pass = PasswordField.Text;
            string name = NameField.Text;

            

            if (LoginField.Text == "")
            {
                panel1.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel1.BackColor = Color.White;

            }

            if (PasswordField.Text == "")
            {
                panel2.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel2.BackColor = Color.White;

            }

            if (NameField.Text == "")
            {
                panel3.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel3.BackColor = Color.White;

            }

            if (guna2ComboBox1.Text == "")
            {
                panel4.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel4.BackColor = Color.White;

            }

            if (login == "" || pass == "" || name == "" || guna2ComboBox1.Text == "")
            {
                return;
            }

            string nameToFind = guna2ComboBox1.Text;
            DataRow[] foundRows = departament.Select("name = '" + nameToFind + "'");
            int departamentId = Convert.ToInt32(foundRows[0]["id"]);

            DB db = new DB();
            DataTable dt = new DataTable();

            dt = db.register(login, pass, name, departamentId);

            if (dt.Rows.Count > 0)
            {
                HomeForm homeForm = new HomeForm(dt);
                homeForm.Show();
                this.Close();
            } else
            {
                errorMessage.Visible = true;
            }
        }

        private void LoginField_TextChanged(object sender, EventArgs e)
        {
            if (LoginField.Text == "")
            {
                panel1.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel1.BackColor = Color.White;

            }
        }

        private void PasswordField_TextChanged(object sender, EventArgs e)
        {
            if (PasswordField.Text == "")
            {
                panel2.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel2.BackColor = Color.White;

            }
        }

        private void NameField_TextChanged(object sender, EventArgs e)
        {
            if (NameField.Text == "")
            {
                panel3.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel3.BackColor = Color.White;

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form fomrLogin = new LoginForm();
            fomrLogin.Show();
            this.Hide();
           
        }

        //Top bar

        bool dragging = false;
        Point dragCursotPoint;
        Point dragFromPoint;

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void TopBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void TopBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursotPoint = Cursor.Position;
            dragFromPoint = this.Location;
        }

        private void TopBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursotPoint));
                this.Location = Point.Add(dragFromPoint, new Size(dif));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            Form homeForm = new HomeForm(dataTable);
            homeForm.Show();
            this.Hide();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.Text == "")
            {
                panel4.BackColor = Color.FromArgb(255, 155, 155);
            }
            else
            {
                panel4.BackColor = Color.White;

            }
        }
    }
}
