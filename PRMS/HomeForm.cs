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
    public partial class HomeForm : Form
    {

        DataTable user;
        public HomeForm(DataTable dt)
        {
            InitializeComponent();
            user = dt;

            openForm(new AllRecordsForm(user));

            if (user.Rows.Count == 0)
            {
                panel6.Visible = false;
                panel8.Visible = false;
                panel15.Visible = false;
                ExitButton.Visible = false;
            } else
            {
                button1.Visible = false;
                button2.Visible = false;
                bool isAdmin = Convert.ToBoolean(user.Rows[0]["isAdmin"]);

                if (isAdmin == true)
                {
                    panel13.Visible = true;
                }
            }

        }

        
        private Form activeForm = null;

        private void openForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close(); 
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void ExitButton_Click_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            Form homeForm = new HomeForm(dt);
            homeForm.Show();
            this.Hide();
        }

        //LeftBar

        Color activeColor = Color.FromArgb(155, 166, 196);
        Color defaultColor = Color.Transparent;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            panel7.Visible = false; 
            panel9.Visible = false;
            panel14.Visible = false;
            panel16.Visible = false;

            guna2Button1.BackColor = activeColor;
            guna2Button2.BackColor = defaultColor;
            guna2Button3.BackColor = defaultColor;
            guna2Button4.BackColor = defaultColor;
            guna2Button5.BackColor = defaultColor;


            openForm(new AllRecordsForm(user));
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel7.Visible = true;
            panel9.Visible = false;
            panel14.Visible = false;
            panel16.Visible = false;

            guna2Button1.BackColor = defaultColor;
            guna2Button2.BackColor = activeColor;
            guna2Button3.BackColor = defaultColor;
            guna2Button4.BackColor = defaultColor;
            guna2Button5.BackColor = defaultColor;


            openForm(new MyRecordsForm(user));

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel7.Visible = false;
            panel9.Visible = true;
            panel14.Visible = false;
            panel16.Visible = false;

            guna2Button1.BackColor = defaultColor;
            guna2Button2.BackColor = defaultColor;
            guna2Button3.BackColor = activeColor;
            guna2Button4.BackColor = defaultColor;
            guna2Button5.BackColor = defaultColor;

            openForm(new AddRecordForm(user));

        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel14.Visible = true;
            panel16.Visible = false;

            guna2Button1.BackColor = defaultColor;
            guna2Button2.BackColor = defaultColor;
            guna2Button3.BackColor = defaultColor;
            guna2Button4.BackColor = activeColor;
            guna2Button5.BackColor = defaultColor;

            openForm(new AdminForm(user));
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel14.Visible = false;
            panel16.Visible = true;

            guna2Button1.BackColor = defaultColor;
            guna2Button2.BackColor = defaultColor;
            guna2Button3.BackColor = defaultColor;
            guna2Button4.BackColor = defaultColor;
            guna2Button5.BackColor = activeColor;

            openForm(new LKForm(user));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form fomrLogin = new LoginForm();
            fomrLogin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

    }
}
