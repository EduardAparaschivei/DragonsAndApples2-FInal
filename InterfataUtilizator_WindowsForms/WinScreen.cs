using DataStorageLevel;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace InterfataUtilizator_WindowsForms
{
    public partial class WinScreen : Form
    {
        PlayerDataStorage_FisierText adminPlayers;
        static string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
        static string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;
        public Player player;
        string linieFisier;

        private PictureBox pctWin;

        private Button btnExit;
        private Button btnMainMenu;

        private Label lblScor;

        private const int pasx = 100;
        private const int pasy = 800;
        private const int latime = 200;
        private const int inaltime = 80;

        public WinScreen()
        {
            using(StreamReader sr = new StreamReader(caleCompletaFisier))
            {
                linieFisier = sr.ReadLine();
            }
            player = new Player(linieFisier);
            InitializeComponent();
            this.Size = new Size(700, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Location = new Point(0,0);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.Gray;
            this.Text = "Dragons and Apples 2";

            pctWin = new PictureBox();
            pctWin.Location = new Point(0, 0);
            pctWin.Image = Properties.Resources.victorie;
            pctWin.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(pctWin);

            btnExit = new Button();
            btnExit.Location = new Point(this.Width - (this.Width - latime) - pasx-pasx/2, pasy);
            btnExit.Text = "Exit";
            btnExit.ForeColor = Color.White;
            btnExit.BackColor = Color.Black;
            btnExit.Font = new Font("Arial", 20, FontStyle.Bold);
            btnExit.Width = latime;
            btnExit.Height = inaltime;
            btnExit.Click += ExitGame;
            this.Controls.Add(btnExit);

            lblScor = new Label();
            lblScor.Left = pasx * 3;
            lblScor.Top = pasy;
            lblScor.Text = "Scor:" + player.scor;
            lblScor.ForeColor = Color.Black;
            lblScor.Width = latime+latime/3;
            lblScor.Height = inaltime;
            lblScor.Font = new Font("Arial", 30,FontStyle.Bold);
            this.Controls.Add(lblScor);

            btnMainMenu = new Button();
            btnMainMenu.Location = new Point(this.Width - pasx - latime+pasx/2, pasy);
            btnMainMenu.Text = "Meniu Principal";
            btnMainMenu.ForeColor = Color.White;
            btnMainMenu.BackColor = Color.Black;
            btnMainMenu.Font = new Font("Arial", 20, FontStyle.Bold);
            btnMainMenu.Width = latime;
            btnMainMenu.Height = inaltime;
            btnMainMenu.Click += MainMenu;
            this.Controls.Add(btnMainMenu);


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
