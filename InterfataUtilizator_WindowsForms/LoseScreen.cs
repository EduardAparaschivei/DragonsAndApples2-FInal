using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace InterfataUtilizator_WindowsForms
{
    public partial class LoseScreen : Form
    {
        private PictureBox pctLose;

        private Button btnExit;
        private Button btnMainMenu;

        private const int pasx = 100;
        private const int pasy = 800;
        private const int latime = 200;
        private const int inaltime = 80;
        public LoseScreen()
        {
            InitializeComponent();
            this.Size = new Size(700, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Location = new Point(0,0);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.Gray;
            this.Text = "Dragons and Apples 2";

            
            pctLose = new PictureBox();
            pctLose.Location = new Point(0, 0);
            pctLose.Image = Properties.Resources.game_over;
            pctLose.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(pctLose);
            

            btnExit = new Button();
            btnExit.Location = new Point(this.Width-(this.Width-latime)-pasx, pasy);
            btnExit.Text = "Exit";
            btnExit.ForeColor = Color.White;
            btnExit.BackColor = Color.Black;
            btnExit.Font = new Font("Arial",20, FontStyle.Bold);
            btnExit.Width = latime;
            btnExit.Height = inaltime;
            btnExit.Click += ExitGame;
            this.Controls.Add(btnExit);

            btnMainMenu = new Button();
            btnMainMenu.Location = new Point(this.Width-pasx-latime, pasy);
            btnMainMenu.Text = "Meniu Principal";
            btnMainMenu.ForeColor = Color.White;
            btnMainMenu.BackColor = Color.Black;
            btnMainMenu.Font = new Font("Arial", 20, FontStyle.Bold);
            btnMainMenu.Width = latime;
            btnMainMenu.Height = inaltime;
            btnMainMenu.Click += MainMenu;
            this.Controls.Add(btnMainMenu);
        }

        private void LoseScreen_Load(object sender, EventArgs e)
        {

        }

        private void ExitGame(object sender, EventArgs e) 
        {
            Application.Exit();
        }

        private void MainMenu(object sender, EventArgs e)
        {
            var MenuForm = new MainMenu();
            MenuForm.Show();
            this.Hide();
        }
    }
}
