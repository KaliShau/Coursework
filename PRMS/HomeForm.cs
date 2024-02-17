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
            string name = user.Rows[0]["FIO"].ToString();
            sayHiLabel.Text = "Добро пожаловать " + name;

            openForm(new AllRecordsForm(user));
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
            Application.Exit();
        }

        //LeftBar

        Color activeColor = Color.FromArgb(155, 166, 196);
        Color defaultColor = Color.Transparent;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            panel7.Visible = false; 
            panel9.Visible = false;

            guna2Button1.BackColor = activeColor;
            guna2Button2.BackColor = defaultColor;
            guna2Button3.BackColor = defaultColor;


            openForm(new AllRecordsForm(user));
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel7.Visible = true;
            panel9.Visible = false;

            guna2Button1.BackColor = defaultColor;
            guna2Button2.BackColor = activeColor;
            guna2Button3.BackColor = defaultColor;


            openForm(new MyRecordsForm(user));

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel7.Visible = false;
            panel9.Visible = true;

            guna2Button1.BackColor = defaultColor;
            guna2Button2.BackColor = defaultColor;
            guna2Button3.BackColor = activeColor;

            openForm(new AddRecordForm(user));

        }

        
    }
}
