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

namespace InterfataUtilizator_WindowsForms
{
    public partial class MainMenu : Form
    {
        static string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
        static string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

        public int pasx = 190;
        public int pasy = 30;
        public int latime = 300;
        public int inaltime = 100;
        public int dimensiunefont = 20;

        private Button btnNewGame;
        private Button btnLoadGame;
        private Button btnHelp;
        private Button btnExit;
        public MainMenu()
        {
            InitializeComponent();
            //setare proprietati
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Location = new Point(0,0);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.Gray;
            this.Text = "Dragons and Apples 2";


            btnNewGame = new Button();
            btnNewGame.Width = latime;
            btnNewGame.Height = inaltime;
            btnNewGame.Location = new System.Drawing.Point(pasx, pasy);
            btnNewGame.Text = "Joc Nou";
            btnNewGame.Font = new Font("Arial",12, FontStyle.Bold);
            btnNewGame.Click += NewGame;
            this.Controls.Add(btnNewGame);

            btnLoadGame = new Button();
            btnLoadGame.Width = latime;
            btnLoadGame.Height = inaltime;
            btnLoadGame.Location = new System.Drawing.Point(pasx, pasy*6);
            btnLoadGame.Text = "Reia Joc";
            btnLoadGame.Font = new Font("Arial", 12, FontStyle.Bold);
            btnLoadGame.Click += LoadGame;
            this.Controls.Add(btnLoadGame);

            btnHelp = new Button();
            btnHelp.Width = latime;
            btnHelp.Height = inaltime;
            btnHelp.Location = new System.Drawing.Point(pasx, pasy * 11);
            btnHelp.Text = "Instructiuni";
            btnHelp.Font = new Font("Arial", 12, FontStyle.Bold);

            this.Controls.Add(btnHelp);

            btnExit = new Button();
            btnExit.Width = latime;
            btnExit.Height = inaltime;
            btnExit.Location = new System.Drawing.Point(pasx, pasy * 16);
            btnExit.Text = "Iesire";
            btnExit.Font = new Font("Arial", 12, FontStyle.Bold);
            btnExit.Click += Exitgame;
            this.Controls.Add(btnExit);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void NewGame(object sender, EventArgs e) 
        {
            var newGameForm = new NewGame();
            newGameForm.Show();
            this.Hide();
        }

        private void LoadGame(object sender, EventArgs e) 
        {
            if(File.Exists(caleCompletaFisier))
            {
                if(new FileInfo(caleCompletaFisier).Length == 0)
                {
                    btnLoadGame.Text = "Fisier Gol!";
                    btnLoadGame.ForeColor = Color.Red;
                }
                else
                {
                    var loadGame = new MainGame();
                    loadGame.Show();
                    this.Hide();
                }
            }else
            {
                btnLoadGame.Text = "Fisier Lipsa!";
                btnLoadGame.ForeColor = Color.Red;
            }
            
            
        }

        private void Exitgame(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
