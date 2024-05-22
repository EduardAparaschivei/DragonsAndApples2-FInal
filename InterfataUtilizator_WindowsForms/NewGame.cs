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

namespace InterfataUtilizator_WindowsForms
{
    public partial class NewGame : Form
    {
        PlayerDataStorage_FisierText adminPlayers;

        static string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
        static string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

        static string numeFisierItems = ConfigurationManager.AppSettings["NumeFisier3"];
        string caleCompletaFisieritems = locatieFisierSolutie + "\\" + numeFisierItems;

        public Player player;
        public Dragon dragon;
        public int pass;

        private PictureBox imgCavaler;

        private Label lblNumeWarning;
        private Label lblNumeWarning2;
        private Label lblNume;

        private Label lblStrenght;
        private Label lblIntelicenge;
        private Label lblAgility;

        private Label lblAbilityPoints;
        private Label lblPointsWarning;

        private TextBox txtNume;

        private Button bntStartGame;
        private Button btnMainMenu;

        private Button PlusStr;
        private Button PlusInte;
        private Button PlusAgility;
        private Button MinusStr;
        private Button MinusInte;
        private Button MinusAgility;

        private const int latime_control = 100;
        private const int dimensiune_pas_x = 120;
        private const int dimensiune_pas_y = 30;
        
        public NewGame()
        {
            InitializeComponent();
            adminPlayers = new PlayerDataStorage_FisierText(caleCompletaFisier);
            int nrPlayers = 0;
            Player[] players = adminPlayers.GetPlayers(out nrPlayers);
            player = new Player();


            //setare proprietati
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Arial",9,FontStyle.Bold);
            this.ForeColor = Color.LimeGreen;
            this.Text = "Dragons and Apples 2";
            
            imgCavaler = new PictureBox();
            imgCavaler.Location = new Point(260, 130);
            imgCavaler.Image = Properties.Resources.Cavaler;
            imgCavaler.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(imgCavaler);

            bntStartGame = new Button();
            bntStartGame.Width = 200;
            bntStartGame.Height = 100;
            bntStartGame.Text = "Start Joc";
            bntStartGame.Location = new System.Drawing.Point(dimensiune_pas_x-20,0);
            bntStartGame.Font = new Font("Arial", 15, FontStyle.Bold);
            bntStartGame.ForeColor = Color.Blue;
            bntStartGame.Click += StartJoc;
            this.Controls.Add(bntStartGame);

            btnMainMenu = new Button();
            btnMainMenu.Width = 200;
            btnMainMenu.Height = 100;
            btnMainMenu.Text = "Meniu Principal";
            btnMainMenu.Location = new System.Drawing.Point(dimensiune_pas_x *3, 0);
            btnMainMenu.Font = new Font("Arial", 15, FontStyle.Bold);
            btnMainMenu.ForeColor = Color.Blue;
            btnMainMenu.Click += MeniuPrincipal;
            this.Controls.Add(btnMainMenu);

            //Nume lbl+txtbox+warning;
            lblNume = new Label();
            lblNume.Width = latime_control-50;
            lblNume.Text = "Nume:";
            lblNume.Top = dimensiune_pas_y*6;
            lblNume.ForeColor = Color.Black;
            this.Controls.Add(lblNume);

            txtNume = new TextBox();
            txtNume.Location = new Point(dimensiune_pas_x-70, dimensiune_pas_y * 6);
            txtNume.ForeColor = Color.Black;
            txtNume.GotFocus += FocusPeTextBox;
            txtNume.LostFocus += FocusPierdut;
            this.Controls.Add(txtNume);

            lblNumeWarning = new Label();
            lblNumeWarning.Width = latime_control;
            lblNumeWarning.Text = "Nume prea lung!";
            lblNumeWarning.ForeColor = Color.Red;
            lblNumeWarning.Left = dimensiune_pas_x+40;
            lblNumeWarning.Top = dimensiune_pas_y * 6;
            lblNumeWarning.Visible = false;
            this.Controls.Add(lblNumeWarning);

            lblNumeWarning2 = new Label();
            lblNumeWarning2.Width = latime_control;
            lblNumeWarning2.Text = "Caseta goala!";
            lblNumeWarning2.ForeColor = Color.Red;
            lblNumeWarning2.Left = dimensiune_pas_x + 40;
            lblNumeWarning2.Top = dimensiune_pas_y * 7;
            lblNumeWarning2.Visible = false;
            this.Controls.Add(lblNumeWarning2);

            //Label Puncte si Warning Puncte
            lblAbilityPoints = new Label();
            lblAbilityPoints.Width = latime_control+10;
            lblAbilityPoints.Top = dimensiune_pas_y * 10;
            lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            lblAbilityPoints.ForeColor = Color.Black;
            this.Controls.Add(lblAbilityPoints);

            lblPointsWarning = new Label();
            lblPointsWarning.Width = latime_control+10;
            lblPointsWarning.Top = dimensiune_pas_y * 10;
            lblPointsWarning.Left = dimensiune_pas_x+30;
            lblPointsWarning.Text = "Puncte Nealocate!";
            lblPointsWarning.ForeColor = Color.Red;
            lblPointsWarning.Visible = false;
            this.Controls.Add(lblPointsWarning);

            //labeluri abilitati
            lblStrenght = new Label();
            lblStrenght.Width = latime_control;
            lblStrenght.Text = "Putere: " + player.Strength;
            lblStrenght.Top = dimensiune_pas_y * 12;
            lblStrenght.ForeColor = Color.Black;
            this.Controls.Add(lblStrenght);

            lblIntelicenge = new Label();
            lblIntelicenge.Width = latime_control;
            lblIntelicenge.Text = "Inteligenta: " + player.Intelligence;
            lblIntelicenge.Top = dimensiune_pas_y * 14;
            lblIntelicenge.ForeColor = Color.Black;
            this.Controls.Add(lblIntelicenge);

            lblAgility = new Label();
            lblAgility.Width = latime_control;
            lblAgility.Text = "Agilitate: " + player.Strength;
            lblAgility.Top = dimensiune_pas_y * 16;
            lblAgility.ForeColor = Color.Black;
            this.Controls.Add(lblAgility);

            //Butoane abilitati
            PlusStr = new Button();
            PlusStr.Width = 30;
            PlusStr.Height = 30;
            PlusStr.Text = "+";
            PlusStr.Location = new System.Drawing.Point(dimensiune_pas_x, dimensiune_pas_y * 12);
            PlusStr.Font = new Font("Arial",15,FontStyle.Bold);
            PlusStr.ForeColor = Color.Blue;
            PlusStr.Click += PlusStrenght;
            this.Controls.Add(PlusStr);

            MinusStr = new Button();
            MinusStr.Width = 30;
            MinusStr.Height = 30;
            MinusStr.Text = "-";
            MinusStr.Location = new System.Drawing.Point(dimensiune_pas_x+50, dimensiune_pas_y * 12);
            MinusStr.Font = new Font("Arial", 15, FontStyle.Bold);
            MinusStr.ForeColor = Color.Red;
            MinusStr.Click += MinusStrenght;
            this.Controls.Add(MinusStr);

            PlusInte = new Button();
            PlusInte.Width = 30;
            PlusInte.Height = 30;
            PlusInte.Text = "+";
            PlusInte.Location = new System.Drawing.Point(dimensiune_pas_x, dimensiune_pas_y * 14);
            PlusInte.Font = new Font("Arial", 15, FontStyle.Bold);
            PlusInte.ForeColor = Color.Blue;
            PlusInte.Click += PlusInteligence;
            this.Controls.Add(PlusInte);

            MinusInte = new Button();
            MinusInte.Width = 30;
            MinusInte.Height = 30;
            MinusInte.Text = "-";
            MinusInte.Location = new System.Drawing.Point(dimensiune_pas_x + 50, dimensiune_pas_y * 14);
            MinusInte.Font = new Font("Arial", 15, FontStyle.Bold);
            MinusInte.ForeColor = Color.Red;
            MinusInte.Click += MinusInteligence;
            this.Controls.Add(MinusInte);

            PlusAgility = new Button();
            PlusAgility.Width = 30;
            PlusAgility.Height = 30;
            PlusAgility.Text = "+";
            PlusAgility.Location = new System.Drawing.Point(dimensiune_pas_x, dimensiune_pas_y * 16);
            PlusAgility.Font = new Font("Arial", 15, FontStyle.Bold);
            PlusAgility.ForeColor = Color.Blue;
            PlusAgility.Click += PlusAgilitate;
            this.Controls.Add(PlusAgility);

            MinusAgility = new Button();
            MinusAgility.Width = 30;
            MinusAgility.Height = 30;
            MinusAgility.Text = "-";
            MinusAgility.Location = new System.Drawing.Point(dimensiune_pas_x + 50, dimensiune_pas_y * 16);
            MinusAgility.Font = new Font("Arial", 15, FontStyle.Bold);
            MinusAgility.ForeColor = Color.Red;
            MinusAgility.Click += MinusAgilitate;
            this.Controls.Add(MinusAgility);


        }

        private void New_game(object sender, EventArgs e)
        {
            
        }

        private void PlusStrenght(object sender, EventArgs e) 
        {
            if (player.AbilityPoints > 0)
            {
                player.Strength++;
                player.AbilityPoints--;
                lblStrenght.Text = "Putere: " + player.Strength;
                lblStrenght.ForeColor = Color.Black;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            }
            else
            {
                lblPointsWarning.Text = "Nu mai ai puncte";
                lblPointsWarning.Visible = true;
            }

        }

        private void MinusStrenght( object sender, EventArgs e)
        {
            if(player.AbilityPoints < 5 && player.Strength > 0)
            {
                player.Strength--;
                player.AbilityPoints++;
                lblStrenght.Text = "Putere: " + player.Strength;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
                lblPointsWarning.Visible = false;
            }
            else
            {
                lblStrenght.ForeColor = Color.Red;
            }
            
        }

        private void PlusInteligence(object sender, EventArgs e)
        {
            if (player.AbilityPoints > 0)
            {
                player.Intelligence++;
                player.AbilityPoints--;
                lblIntelicenge.Text = "Inteligenta: " + player.Intelligence;
                lblIntelicenge.ForeColor = Color.Black;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            }
            else
            {
                lblPointsWarning.Text = "Nu mai ai puncte";
                lblPointsWarning.Visible = true;
            }

        }

        private void MinusInteligence(object sender, EventArgs e)
        {
            if (player.AbilityPoints < 5 && player.Intelligence > 0)
            {
                player.Intelligence--;
                player.AbilityPoints++;
                lblIntelicenge.Text = "Inteligenta: " + player.Intelligence;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
                lblPointsWarning.Visible = false;
            }
            else
            {
                lblIntelicenge.ForeColor = Color.Red;
            }

        }

        private void PlusAgilitate(object sender, EventArgs e)
        {
            if (player.AbilityPoints > 0)
            {
                player.Agility++;
                player.AbilityPoints--;
                lblAgility.Text = "Agilitate: " + player.Agility;
                lblAgility.ForeColor = Color.Black;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            }
            else
            {
                lblPointsWarning.Text = "Nu mai ai puncte";
                lblPointsWarning.Visible = true;
            }

        }

        private void MinusAgilitate(object sender, EventArgs e)
        {
            if (player.AbilityPoints < 5 && player.Agility > 0)
            {
                player.Agility--;
                player.AbilityPoints++;
                lblAgility.Text = "Agilitate: " + player.Agility;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
                lblPointsWarning.Visible = false;
            }
            else
            {
                lblAgility.ForeColor = Color.Red;
            }

        }

        private void StartJoc(object sender, EventArgs e)
        {
            pass = 0 ;

            if (txtNume.Text.Length > 15)
            {
                lblNumeWarning.Visible = true;
                lblNumeWarning2.Visible = false;
            }
            if(txtNume.Text.Length == 0)
            {
                lblNumeWarning2.Visible = true;
                lblNumeWarning.Visible = false;
            }
            if(txtNume.Text.Length > 0 && txtNume.Text.Length < 15)
            {
                lblNumeWarning.Visible = false;
                lblNumeWarning2.Visible = false;
                pass++;
            }
             
            if (player.AbilityPoints > 0)
            {
                lblPointsWarning.Text = "Puncte nealocate";
                lblPointsWarning.Visible = true;
            }
            else
            {
                pass++;
                lblPointsWarning.Visible = false;
            }
            if(pass ==  2)
            {
                File.WriteAllText(caleCompletaFisier, string.Empty);
                File.WriteAllText(caleCompletaFisieritems, string.Empty);
                PlusStr.Visible = false;
                PlusInte.Visible = false;
                PlusAgility.Visible = false;
                lblPointsWarning.Visible = false;

                player.Name = txtNume.Text;
                adminPlayers.AddPlayer(player);



                var MainGame = new MainGame();
                MainGame.Show();
                this.Hide();
            }
        }

        private void MeniuPrincipal(object sender, EventArgs e)
        {
            var MenuForm = new MainMenu();
            MenuForm.Show();
            this.Hide();
        }

        private void FocusPeTextBox(object sender, EventArgs e) 
        {
            txtNume.BackColor = Color.Gray;

        }
        private void FocusPierdut(object sender, EventArgs e) 
        {
            txtNume.BackColor = Color.White;
        }


    } 

    
}
