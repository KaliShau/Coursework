﻿using System;
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
    public partial class RegisterForm : Form
    {
        public Form ReturnForm;
        public RegisterForm()
        {
            InitializeComponent();
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

            if (login == "" || pass == "" || name == "")
            {
                return;
            }

            DB db = new DB();
            DataTable dt = new DataTable();

            dt = db.register(login, pass, name);

            if (dt.Rows.Count > 0)
            {
                this.Hide();
                HomeForm homeForm = new HomeForm(dt);
                homeForm.Show();
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
            ReturnForm.Show();
            this.Close();
           
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

    }
}
