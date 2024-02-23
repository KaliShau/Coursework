using Npgsql;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;


        private void LoginButton_Click(object sender, EventArgs e)
        {
            string login = LoginField.Text;
            string Password = PasswordField.Text;

            if (LoginField.Text == "")
            {
                panel1.BackColor = Color.FromArgb(255, 155, 155);
            } else
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

            if (login == "" || Password == "")
            {
                return;
            }

            DB db = new DB();
            DataTable dt = new DataTable();

            dt = db.login(login, Password);

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

        private void label5_Click(object sender, EventArgs e)
        {
            Form registerForm = new RegisterForm();
            registerForm.Show();
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
            } else
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
    }
}
