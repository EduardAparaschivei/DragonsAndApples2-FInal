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
    public partial class MainGame : Form
    {
        PlayerDataStorage_FisierText adminPlayers;
        DragonsDataStorage_FisierText adminDragoni;
        ItemsDataStorage_FisierText adminItems;

        int selecteditem = -1;
        int IdGlobal = 1;
        Random random = new Random();

        static string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
        static string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

        string caleCompletaFisier2 = Path.Combine(Application.StartupPath, "Dragoni.txt");

        static string numeFisier3 = ConfigurationManager.AppSettings["NumeFisier3"];
        string caleCompletaFisier3 = locatieFisierSolutie + "\\" + numeFisier3;

        public Player player;
        public string linieFisier;
        public Dragon DragonGlobal;
        public Item ItemGlobal = new Item();
        public int pass;

        #region Panel,Labels,TextBoxes,Buttons
        private Panel LinieDesign;

        private Label lblNume;
        private Label lblNumeAfisare;

        private Label lblStrenght;
        private Button PlusStr;
        private Label lblIntelicenge;
        private Button PlusInte;
        private Label lblAgility;
        private Button PlusAgility;

        private Label lblAbilityPoints;
        private Label NoPoints;

        private Label lblHP;
        private Label lblScor;
        private Label lblXP;
        private Label lblLevel;
        private Label lblMere;
        private Label lblStagiu;

        private Button MainMenu;
        private Button SaveGame;

        private Button btnExplore;
        private Button btnManacaMar;
        private Button btnFight;
        private Button btnAttack;
        private Button btnFlee;
        private Button btnEchipeazaItem;
        private Button btnDezechipeazaItem;
        private Button btnCautaItem;
       

        private Label lblDetaliiDragon;

        private Label Informatii;
        private Label lblLevelUp;

        private Label[] lblsItem;
        private Label[] lblsItemDamage;
        private Label[] lblsItemCondition;

        private GroupBox gpbInventar;

        private GroupBox gpbCautareItem;
        private Button gpbBtnCautaItem;
        private Button gpbBtnInchide;
        private TextBox gpbTxtBoxCautaItemNume;
        private TextBox gpbTxtBoxCautaItemDamage;
        private Label gpbLblCautaItemNumeWarning;
        private Label gpbLblCautaItemDamageWarning;
        private Label gpbLblCautaItemWarningGeneral;
        private ComboBox CmbBoxItemeGasite;

        private Button btnSelect;
        private RadioButton[] rdbIteme;

        private RadioButton rdbitem;


        private Label lblInfoItemEchipat;
        private Label lblItemEchipatNume;
        private Label lblItemCondDmg;
        private PictureBox pctItem;
        private PictureBox pctFundal;
        #endregion

        private const int latime_control = 100;
       
        private const int inaltime_control = 50;
        private const int dimensiune_pas_x = 140;
        private const int dimensiune_pas_y = 30;

        private const int gpb_latime = 60;
        private const int gpb_pas_y = 30;
        private const int gpb_pas_x = 60;
        public MainGame()
        {
           
            InitializeComponent();
            adminItems = new ItemsDataStorage_FisierText(caleCompletaFisier3);
            adminPlayers = new PlayerDataStorage_FisierText(caleCompletaFisier);
            int nrPlayers = 0;
            Player[] players = adminPlayers.GetPlayers(out nrPlayers);
            using (StreamReader sr = new StreamReader(caleCompletaFisier))
            {
                linieFisier = sr.ReadLine();
            }
            player = new Player(linieFisier);

            adminDragoni = new DragonsDataStorage_FisierText(caleCompletaFisier2);
            int nrDragoni = 0;
            Dragon[] dragoni = adminDragoni.GetDragons(out nrDragoni);
            DragonGlobal = new Dragon();

            //setare proprietati
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Location = new Point(0,0);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.LimeGreen;
            this.Text = "Dragons and Apples 2";

            #region linii design
            LinieDesign = new Panel();
            LinieDesign.BackColor = Color.Red;
            LinieDesign.Size = new Size(5, this.ClientSize.Height);
            LinieDesign.Location = new Point(100, 0);
            this.Controls.Add(LinieDesign);

            LinieDesign = new Panel();
            LinieDesign.BackColor = Color.Red;
            LinieDesign.Size = new Size(this.ClientSize.Width, 5);
            LinieDesign.Location = new Point(0, 85);
            this.Controls.Add(LinieDesign);

            LinieDesign = new Panel();
            LinieDesign.BackColor = Color.Red;
            LinieDesign.Size = new Size(this.ClientSize.Width, 5);
            LinieDesign.Location = new Point(0, this.ClientSize.Height - 100);
            this.Controls.Add(LinieDesign);


            #endregion

            #region Inventar

            gpbInventar = new GroupBox();
            gpbInventar.Margin = new System.Windows.Forms.Padding(4);
            gpbInventar.Name = "gpbInventar";
            gpbInventar.Padding = new System.Windows.Forms.Padding(4);
            gpbInventar.Size = new System.Drawing.Size(600, 200);
            gpbInventar.Location = new System.Drawing.Point((this.ClientSize.Width - gpbInventar.Width) / 2, (this.ClientSize.Height - gpbInventar.Height) / 2);
            gpbInventar.TabStop = false;
            gpbInventar.Visible = false;
            this.Controls.Add(gpbInventar);

            btnSelect = new Button();
            btnSelect.Text = "Foloseste";
            btnSelect.Size = new System.Drawing.Size(70, 40);
            btnSelect.Location = new System.Drawing.Point(this.ClientSize.Width - dimensiune_pas_x, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += Echipare;
            btnSelect.Visible = false;
            this.Controls.Add(btnSelect);

            #endregion

            #region Cautare Item
            gpbCautareItem = new GroupBox();
            gpbCautareItem.Margin = new System.Windows.Forms.Padding(4);
            gpbCautareItem.Name = "gpbCautareitem";
            gpbCautareItem.Padding = new System.Windows.Forms.Padding(4);
            gpbCautareItem.Size = new System.Drawing.Size(400, 300);
            gpbCautareItem.Location = new System.Drawing.Point((this.ClientSize.Width - gpbCautareItem.Width) / 2, (this.ClientSize.Height - gpbCautareItem.Height) / 2);
            gpbCautareItem.TabStop = false;
            gpbCautareItem.Visible = false;
            gpbCautareItem.BackColor = Color.DarkBlue;
            this.Controls.Add(gpbCautareItem);

            gpbTxtBoxCautaItemNume = new TextBox();
            gpbTxtBoxCautaItemNume.Width = gpb_latime*2;
            gpbTxtBoxCautaItemNume.Top = gpb_pas_y;
            gpbTxtBoxCautaItemNume.Left = gpb_pas_x/10;
            this.gpbCautareItem.Controls.Add(gpbTxtBoxCautaItemNume);

            gpbLblCautaItemNumeWarning = new Label();
            gpbLblCautaItemNumeWarning.Text = "Test Test Test!";
            gpbLblCautaItemNumeWarning.Width = gpb_latime*2;
            gpbLblCautaItemNumeWarning.Top = gpb_pas_y * 2;
            gpbLblCautaItemNumeWarning.Left = gpb_pas_x / 10;
            gpbLblCautaItemNumeWarning.ForeColor = Color.Red;
            gpbLblCautaItemNumeWarning.Visible = false;
            this.gpbCautareItem.Controls.Add(gpbLblCautaItemNumeWarning);

            gpbBtnCautaItem = new Button();
            gpbBtnCautaItem.Width = gpb_latime;
            gpbBtnCautaItem.Text = "Cauta";
            gpbBtnCautaItem.Top = gpb_pas_y;
            gpbBtnCautaItem.Left = gpbCautareItem.Width/2-gpbBtnCautaItem.Width/2;
            gpbBtnCautaItem.Click += CautareObiect;
            this.gpbCautareItem.Controls.Add(gpbBtnCautaItem);

            gpbTxtBoxCautaItemDamage = new TextBox();
            gpbTxtBoxCautaItemDamage.Width = gpb_latime*2;
            gpbTxtBoxCautaItemDamage.Top = gpb_pas_y;
            gpbTxtBoxCautaItemDamage.Left = gpbCautareItem.Width - gpb_pas_x-gpbTxtBoxCautaItemDamage.Width/2;
            this.gpbCautareItem.Controls.Add(gpbTxtBoxCautaItemDamage);

            gpbLblCautaItemDamageWarning = new Label();
            gpbLblCautaItemDamageWarning.Text = "Test Test Test!";
            gpbLblCautaItemDamageWarning.Width = gpb_latime * 2;
            gpbLblCautaItemDamageWarning.Top = gpb_pas_y * 2;
            gpbLblCautaItemDamageWarning.Left = gpbCautareItem.Width - gpb_pas_x - gpbTxtBoxCautaItemDamage.Width / 2+5;
            gpbLblCautaItemDamageWarning.ForeColor = Color.Red;
            gpbLblCautaItemDamageWarning.Visible = false;
            this.gpbCautareItem.Controls.Add(gpbLblCautaItemDamageWarning);

            gpbLblCautaItemWarningGeneral = new Label();
            gpbLblCautaItemWarningGeneral.Text = "Centu centru centru";
            gpbLblCautaItemWarningGeneral.Width = gpb_latime * 2 + gpb_latime/2;
            gpbLblCautaItemWarningGeneral.Top = gpb_pas_y * 2;
            gpbLblCautaItemWarningGeneral.Left = gpbCautareItem.Width/2 - gpbLblCautaItemWarningGeneral.Width/2 + 12;
            gpbLblCautaItemWarningGeneral.ForeColor = Color.Red;
            gpbLblCautaItemWarningGeneral.Visible = false;
            this.gpbCautareItem.Controls.Add(gpbLblCautaItemWarningGeneral);

            CmbBoxItemeGasite = new ComboBox();
            CmbBoxItemeGasite.FormattingEnabled = true;
            CmbBoxItemeGasite.Name = "CmbBoxItemeGasite";
            CmbBoxItemeGasite.Width = gpb_latime*2;
            CmbBoxItemeGasite.Left = 1;
            CmbBoxItemeGasite.Top = gpb_pas_y * 3;
            this.gpbCautareItem.Controls.Add(CmbBoxItemeGasite);

            gpbBtnInchide = new Button();
            gpbBtnInchide.Width = gpb_latime + gpb_latime/2;
            gpbBtnInchide.Text = "Inchide";
            gpbBtnInchide.Top = gpb_pas_y * 3;
            gpbBtnInchide.Left = gpbCautareItem.Width / 2 + gpbBtnInchide.Width / 2;
            gpbBtnInchide.Click += Inchide;
            this.gpbCautareItem.Controls.Add(gpbBtnInchide);

            #endregion

            #region BLoc Nume + HP
            lblNume = new Label();
            lblNume.Width = latime_control;
            lblNume.Text = "Nume";
            lblNume.Left = dimensiune_pas_x;
            lblNume.ForeColor = Color.Black;
            lblNume.BackColor = Color.White;
            this.Controls.Add(lblNume);

            lblNumeAfisare = new Label();
            lblNumeAfisare.Width = latime_control;
            lblNumeAfisare.Text = player.Name;
            lblNumeAfisare.Left = dimensiune_pas_x;
            lblNumeAfisare.Top = dimensiune_pas_y;
            lblNumeAfisare.ForeColor = Color.Black;
            this.Controls.Add(lblNumeAfisare);

            lblHP = new Label();
            lblHP.Width = latime_control;
            lblHP.Text = "HP: " + player.HealthPoints;
            lblHP.Left = dimensiune_pas_x;
            lblHP.Top = 2 * dimensiune_pas_y;
            lblHP.ForeColor = Color.Black;
            this.Controls.Add(lblHP);

            #endregion

            #region Bloc Puncte
            lblAbilityPoints = new Label();
            lblAbilityPoints.Width = latime_control + 20;
            lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            lblAbilityPoints.Left = 2 * dimensiune_pas_x + 30;
            lblAbilityPoints.ForeColor = Color.Black;
            this.Controls.Add(lblAbilityPoints);

            #endregion

            #region Bloc Abilitati + Nr mere
            //Label Putere+Buton
            lblStrenght = new Label();
            lblStrenght.Width = latime_control - 30;
            lblStrenght.Text = "Putere: " + player.Strength;
            lblStrenght.Left = 2 * dimensiune_pas_x + 30;
            lblStrenght.Top = dimensiune_pas_y;
            lblStrenght.ForeColor = Color.Black;
            this.Controls.Add(lblStrenght);

            lblMere = new Label();
            lblMere.Width = latime_control;
            lblMere.Text = "Mere: " + player.Mere;
            lblMere.Left = 2 * dimensiune_pas_x + 30;
            lblMere.Top = dimensiune_pas_y * 2;
            lblMere.ForeColor = Color.Black;
            this.Controls.Add(lblMere);

            PlusStr = new Button();
            PlusStr.Width = 20;
            PlusStr.Location = new System.Drawing.Point(3 * dimensiune_pas_x - 20, dimensiune_pas_y);
            PlusStr.Text = "+";
            PlusStr.Click += PlusStrenght;
            PlusStr.Visible = false;
            this.Controls.Add(PlusStr);

            //Label Inteligenta+Buton
            lblIntelicenge = new Label();
            lblIntelicenge.Width = latime_control - 10;
            lblIntelicenge.Text = "Inteligenta: " + player.Intelligence;
            lblIntelicenge.Left = 3 * dimensiune_pas_x;
            lblIntelicenge.Top = dimensiune_pas_y;
            lblIntelicenge.ForeColor = Color.Black;
            this.Controls.Add(lblIntelicenge);

            PlusInte = new Button();
            PlusInte.Width = 20;
            PlusInte.Location = new System.Drawing.Point(4 * dimensiune_pas_x - 30, dimensiune_pas_y);
            PlusInte.Text = "+";
            PlusInte.Click += PlusInteligence;
            PlusInte.Visible = false;
            this.Controls.Add(PlusInte);

            //Label Agilitate+Buton
            lblAgility = new Label();
            lblAgility.Width = latime_control - 20;
            lblAgility.Text = "Agilitate: " + player.Agility;
            lblAgility.Left = 4 * dimensiune_pas_x;
            lblAgility.Top = dimensiune_pas_y;
            lblAgility.ForeColor = Color.Black;
            this.Controls.Add(lblAgility);

            PlusAgility = new Button();
            PlusAgility.Width = 20;
            PlusAgility.Location = new System.Drawing.Point(5 * dimensiune_pas_x - 30, dimensiune_pas_y);
            PlusAgility.Text = "+";
            PlusAgility.Click += PlusAgilitate;
            PlusAgility.Visible = false;
            this.Controls.Add(PlusAgility);
            #endregion

            #region Label XP si level
            lblXP = new Label();
            lblXP.Width = latime_control;
            lblXP.Text = "XP: " + player.xp;
            lblXP.Left = 5 * dimensiune_pas_x;
            lblXP.ForeColor = Color.Black;
            this.Controls.Add(lblXP);

            lblLevel = new Label();
            lblLevel.Width = latime_control;
            lblLevel.Text = "Level: " + player.Level;
            lblLevel.Left = 5 * dimensiune_pas_x;
            lblLevel.Top = dimensiune_pas_y;
            lblLevel.ForeColor = Color.Black;
            this.Controls.Add(lblLevel);
            #endregion

            #region Label Scor
          
            lblScor = new Label();
            lblScor.Width = latime_control;
            lblScor.Text = "Scor: " + player.scor;
            lblScor.Left = 6 * dimensiune_pas_x;
            lblScor.ForeColor = Color.Black;
            this.Controls.Add(lblScor);
            #endregion

            #region Label Stagiu Joc
            lblStagiu = new Label();
            lblStagiu.Width = latime_control;
            lblStagiu.Text = "Stagiu Joc: " + player.stage;
            lblStagiu.Left = 7 * dimensiune_pas_x;
            lblStagiu.ForeColor = Color.Black;
            this.Controls.Add(lblStagiu);
            #endregion

            #region Dragon
            lblDetaliiDragon = new Label();
            lblDetaliiDragon.Width = latime_control*3;
            lblDetaliiDragon.Text = DragonGlobal.Name +" HP:"+DragonGlobal.HealthPoints+ " D:" + DragonGlobal.Difficulty;
            lblDetaliiDragon.Top = 2 * dimensiune_pas_y;
            lblDetaliiDragon.Left = 3 * dimensiune_pas_x;
            lblDetaliiDragon.ForeColor = Color.Black;
            lblDetaliiDragon.Visible = false;
            this.Controls.Add(lblDetaliiDragon);

            #endregion

            #region Informatii
            Informatii = new Label();
            Informatii.Left = dimensiune_pas_x * 6;
            Informatii.Top = dimensiune_pas_y;
            Informatii.ForeColor = Color.Purple;
            Informatii.Text = "Informatii";
            Informatii.Width = latime_control * 3;
            this.Controls.Add(Informatii);

            lblLevelUp = new Label();
            lblLevelUp.Left = dimensiune_pas_x * 6;
            lblLevelUp.Top = dimensiune_pas_y *2;
            lblLevelUp.ForeColor = Color.Purple;
            lblLevelUp.Text = "Level Up!";
            lblLevelUp.Width = latime_control * 3;
            lblLevelUp.Visible = false;
            this.Controls.Add(lblLevelUp);
            #endregion

            #region Butoane Interactiuni Tip Explorare
            btnManacaMar = new Button();
            btnManacaMar.Width = latime_control;
            btnManacaMar.Height = latime_control / 2;
            btnManacaMar.Text = "Mănâncă Măr";
            btnManacaMar.ForeColor = Color.OrangeRed;
            btnManacaMar.Location = new System.Drawing.Point(0, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnManacaMar.Visible = false;
            btnManacaMar.Click += ManacaMar;
            this.Controls.Add(btnManacaMar);

            btnExplore = new Button();
            btnExplore.Width = latime_control;
            btnExplore.Height = latime_control/2;
            btnExplore.Text = "Explorează";
            btnExplore.ForeColor = Color.OrangeRed;
            btnExplore.Location = new System.Drawing.Point(dimensiune_pas_x, this.ClientSize.Height - 3*dimensiune_pas_y);
            btnExplore.Click += Explorare;
            this.Controls.Add(btnExplore);
           
            btnFight = new Button();
            btnFight.Width = latime_control;
            btnFight.Height = latime_control / 2;
            btnFight.Text = "Luptă!";
            btnFight.ForeColor = Color.OrangeRed;
            btnFight.Location = new System.Drawing.Point(dimensiune_pas_x *2, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnFight.Visible = false;
            btnFight.Click += Lupta;
            this.Controls.Add(btnFight);
            #endregion

            #region Butoane Inventar
            btnEchipeazaItem = new Button();
            btnEchipeazaItem.Width = latime_control;
            btnEchipeazaItem.Height = latime_control / 2;
            btnEchipeazaItem.Text = "Echipeaza Item";
            btnEchipeazaItem.ForeColor = Color.OrangeRed;
            btnEchipeazaItem.Location = new System.Drawing.Point(this.ClientSize.Width-dimensiune_pas_x, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnEchipeazaItem.Click += Echipeaza;
            this.Controls.Add(btnEchipeazaItem);

            btnDezechipeazaItem = new Button();
            btnDezechipeazaItem.Width = latime_control;
            btnDezechipeazaItem.Height = latime_control / 2;
            btnDezechipeazaItem.Text = "Dezchipeaza Item";
            btnDezechipeazaItem.ForeColor = Color.OrangeRed;
            btnDezechipeazaItem.Location = new System.Drawing.Point(this.ClientSize.Width - dimensiune_pas_x*2, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnDezechipeazaItem.Click += Dezechipare;
            this.Controls.Add(btnDezechipeazaItem);

            btnCautaItem = new Button();
            btnCautaItem.Width = latime_control;
            btnCautaItem.Height = latime_control / 2;
            btnCautaItem.Text = "Cauta Item";
            btnCautaItem.ForeColor = Color.OrangeRed;
            btnCautaItem.Location = new System.Drawing.Point(this.ClientSize.Width - dimensiune_pas_x * 3, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnCautaItem.Click += Cauta;
            this.Controls.Add(btnCautaItem);
            #endregion

            #region Butoane Tip Lupta
            btnAttack = new Button();
            btnAttack.Width = latime_control;
            btnAttack.Height = latime_control / 2;
            btnAttack.Text = "Ataca!";
            btnAttack.ForeColor = Color.OrangeRed;
            btnAttack.Location = new System.Drawing.Point(dimensiune_pas_x, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnAttack.Visible = false;
            btnAttack.Click += Ataca;
            this.Controls.Add(btnAttack);

            btnFlee = new Button();
            btnFlee.Width = latime_control;
            btnFlee.Height = latime_control / 2;
            btnFlee.Text = "FUGI!";
            btnFlee.ForeColor = Color.OrangeRed;
            btnFlee.Location = new System.Drawing.Point(dimensiune_pas_x * 2, this.ClientSize.Height - 3 * dimensiune_pas_y);
            btnFlee.Visible = false;
            btnFlee.Click += Fugi;
            this.Controls.Add(btnFlee);
            #endregion

            #region Butoane Menu si Savegame
            //MainMenu
            MainMenu = new Button();
            MainMenu.Width = latime_control;
            MainMenu.Location = new System.Drawing.Point(0, dimensiune_pas_y);
            MainMenu.Text = "Meniu Principal";
            MainMenu.Click += MeniuPrincipal;
            this.Controls.Add(MainMenu);
            //SaveGame
            SaveGame = new Button();
            SaveGame.Width = latime_control;
            SaveGame.Location = new System.Drawing.Point(0, 0);
            SaveGame.Text = "Salvare Joc";
            SaveGame.Click += SalvareJoc;
            this.Controls.Add(SaveGame);
            #endregion

            #region Fundaluri

            LinieDesign = new Panel();
            LinieDesign.BackColor = Color.IndianRed;
            LinieDesign.Size = new Size(this.Width, inaltime_control*2-15);
            this.Controls.Add(LinieDesign);

            LinieDesign = new Panel();
            LinieDesign.BackColor = Color.IndianRed;
            LinieDesign.Size = new Size(this.Width,inaltime_control*2);
            LinieDesign.Top = this.Height-(inaltime_control*2+40);
            this.Controls.Add(LinieDesign);

           


            pctFundal = new PictureBox();
            pctFundal.Location = new Point(dimensiune_pas_x+dimensiune_pas_x/3, dimensiune_pas_y * 3);
            pctFundal.Image = Properties.Resources.imaginedefault;
            pctFundal.SizeMode = PictureBoxSizeMode.AutoSize;
            //pctFundal.Visible = false;
            this.Controls.Add(pctFundal);

            LinieDesign = new Panel();
            LinieDesign.BackColor = Color.Black;
            LinieDesign.Size = new Size(this.Width - latime_control, inaltime_control * 10);
            LinieDesign.Location = new Point(dimensiune_pas_x - 30, dimensiune_pas_y * 3 );
            this.Controls.Add(LinieDesign);

            #endregion

            #region Detalii Item 
            lblInfoItemEchipat = new Label();
            lblInfoItemEchipat.Width = latime_control;
            lblInfoItemEchipat.Text = "Item echipat";
            lblInfoItemEchipat.ForeColor = Color.Black;
            lblInfoItemEchipat.Font = new Font("Arial", 11, FontStyle.Italic);
            lblInfoItemEchipat.Top = dimensiune_pas_y * 5;
            this.Controls.Add(lblInfoItemEchipat);

            lblItemEchipatNume = new Label();
            lblItemEchipatNume.Width = latime_control;
            lblItemEchipatNume.Text = "Niciun Item";
            lblItemEchipatNume.ForeColor = Color.Black;
            lblItemEchipatNume.Top = dimensiune_pas_y * 6;
            this.Controls.Add(lblItemEchipatNume);

            lblItemCondDmg = new Label();
            lblItemCondDmg.Width = latime_control;
            lblItemCondDmg.ForeColor = Color.Black;
            lblItemCondDmg.Top = dimensiune_pas_y * 7;
            lblItemCondDmg.Visible = false;
            this.Controls.Add(lblItemCondDmg);

            pctItem = new PictureBox();
            pctItem.Location = new Point(0, dimensiune_pas_y * 8);
            pctItem.Image = Properties.Resources.cutit;
            pctItem.SizeMode = PictureBoxSizeMode.AutoSize;
            pctItem.Visible = false;
            this.Controls.Add(pctItem);


            #endregion
        }

        #region Plus Abilitati
        private void PlusStrenght(object sender, EventArgs e)
        {
            if (player.AbilityPoints > 0)
            {
                player.Strength++;
                player.AbilityPoints--;
                lblStrenght.Text = "Putere: " + player.Strength;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            }
           
            if(player.AbilityPoints == 0)
            {
                PlusStr.Visible = false;
                PlusInte.Visible = false;
                PlusAgility.Visible = false;
            }

        }

        private void PlusInteligence(object sender, EventArgs e)
        {
            if (player.AbilityPoints > 0)
            {
                player.Intelligence++;
                player.AbilityPoints--;
                lblIntelicenge.Text = "Inteligenta: " + player.Intelligence;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            }
            if (player.AbilityPoints == 0)
            {
                PlusStr.Visible = false;
                PlusInte.Visible = false;
                PlusAgility.Visible = false;
            }

        }

        private void PlusAgilitate(object sender, EventArgs e)
        {
            if (player.AbilityPoints > 0)
            {
                player.Agility++;
                player.AbilityPoints--;
                lblAgility.Text = "Agilitate: " + player.Agility;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            }
            if (player.AbilityPoints == 0)
            {
                PlusStr.Visible = false;
                PlusInte.Visible = false;
                PlusAgility.Visible = false;
            }

        }

        #endregion

        #region Metode pentru functionalitate
        private void MeniuPrincipal(object sender, EventArgs e)
        {
           
            var MenuForm = new MainMenu();
            MenuForm.Show();
            this.Hide();
        }

        private void SalvareJoc(object sender, EventArgs e)
        {
            File.WriteAllText(caleCompletaFisier, string.Empty);
            adminPlayers.AddPlayer(player);
            Informatii.Text = "Proges Salvat!";
        }
        private void Actualizare()
        {
            lblHP.Text = "HP: " + player.HealthPoints;
            lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
            lblStrenght.Text = "Putere: " + player.Strength;
            lblIntelicenge.Text = "Inteligenta: " + player.Intelligence;
            lblAgility.Text = "Agilitate: " + player.Agility;
            lblXP.Text = "XP: " + player.xp;
            lblLevel.Text = "Level: " + player.Level;
            lblScor.Text = "Scor: " + player.scor;
            lblMere.Text = "Mere: " + player.Mere;
            lblStagiu.Text = "Stagiu Joc: " + player.stage;
            if (ItemGlobal.Condition > 0)
            {
                lblItemEchipatNume.Text = ItemGlobal.Name.ToString();
                lblItemCondDmg.Visible = true;
                lblItemCondDmg.Text = "DMG: " + ItemGlobal.Damage + " C: " + ItemGlobal.Condition;
                pctItem.Visible = true;
                AlegereImagine();
            }
            else
            {
                lblItemEchipatNume.Text = "Niciun Item!";
                lblItemCondDmg.Visible = false;
                pctItem.Visible = false;
            }
            if (player.Mere > 0)
            {
                btnManacaMar.Visible = true;
            }
            else
            {
                btnManacaMar.Visible = false;
            }
            ActualizareInventar();
            VerificareAvansareLevelAsync();
        }

        private void AlegereImagine()
        {
            string imagine = ItemGlobal.Name.ToString().ToLower();
            switch (imagine)
            {
                case "sabie":
                    pctItem.Image = Properties.Resources.sabie;
                    break;
                case "topor":
                    pctItem.Image = Properties.Resources.topor;
                    break;
                case "ciocan":
                    pctItem.Image = Properties.Resources.hammer;
                    break;
                case "arc":
                    pctItem.Image = Properties.Resources.bow;
                    break;
                case "cutit":
                    pctItem.Image = Properties.Resources.cutit;
                    break;
            }
        }

        private void ActualizareInventar()
        {
            gpbInventar.Controls.Clear();
            List<Item> iteme = adminItems.GetItems();
            if(iteme.Count == 0)
            {
                return;
            }
            int nrIteme = iteme.Count;
            lblsItem = new Label[nrIteme];
            lblsItemDamage = new Label[nrIteme];
            lblsItemCondition = new Label[nrIteme];
            int i = 0;
            int j = 0;
            foreach (Item item in iteme)
            {
                lblsItem[i] = new Label();
                lblsItem[i].Width = gpb_latime;
                lblsItem[i].Text = item.Name.ToString();
                lblsItem[i].Top = (i + 1) * gpb_pas_y;
                lblsItem[i].Left = (j) * gpb_pas_x + 2;
                this.gpbInventar.Controls.Add(lblsItem[i]);


                lblsItemDamage[i] = new Label();
                lblsItemDamage[i].Width = gpb_latime - 15;
                lblsItemDamage[i].Text = "D:" + item.Damage.ToString();
                lblsItemDamage[i].Top = (i + 1) * gpb_pas_y;
                lblsItemDamage[i].Left = (j + 1) * gpb_pas_x;
                this.gpbInventar.Controls.Add(lblsItemDamage[i]);


                lblsItemCondition[i] = new Label();
                lblsItemCondition[i].Width = gpb_latime - 15;
                lblsItemCondition[i].Text = "C:" + item.Condition.ToString();
                lblsItemCondition[i].Top = (i + 1) * gpb_pas_y;
                lblsItemCondition[i].Left = (j + 2) * gpb_pas_x;
                this.gpbInventar.Controls.Add(lblsItemCondition[i]);

                rdbitem = new RadioButton();
                rdbitem.Name = item.ID.ToString();
                rdbitem.Text = item.ID.ToString();
                rdbitem.Location = new System.Drawing.Point((j + 3) * gpb_pas_x, (i + 1) * gpb_pas_y - 5);
                rdbitem.CheckedChanged += SelectareItem;
                this.gpbInventar.Controls.Add(rdbitem);
                i++;
                if (i == 5)
                {
                    j = 6;
                    i = 0;
                }
            }
        }

        private  void VerificareAvansareLevelAsync()
        {
            if (player.xp >= player.Level * 100)
            {
                pctFundal.Image = Properties.Resources.lvlup__1_;
                player.xp = 0;
                player.Level=player.Level+1;
                player.AbilityPoints = player.AbilityPoints + 5;
                lblLevelUp.Visible = true;
                lblLevelUp.Text = "Ai avansat un nivel!(Nu uita sa salvezi)";
                PlusStr.Visible = true;
                PlusAgility.Visible = true;
                PlusInte.Visible = true;
                lblAbilityPoints.Text = "Puncte Abilitati: " + player.AbilityPoints;
                lblLevel.Text = "Level: " + player.Level;
            }

        }

        private async Task VerificareMoarteAsync()
        {
            if (player.HealthPoints <= 0)
            {
                await Task.Delay(1000);
                var LoseGame = new LoseScreen();
                LoseGame.Show();
                this.Hide();
            }
        }

        private void VerificareVictorie()
        {
            if(player.stage > 10)
            {
                File.WriteAllText(caleCompletaFisier, string.Empty);
                adminPlayers.AddPlayer(player);
                var WinScreen = new WinScreen();
                WinScreen.Show();
                this.Hide();
            }
        }
        #endregion

        #region Metode de tip Explorare
        private void Explorare(object sender, EventArgs e)
        {
            btnFight.Visible = false;
            lblLevelUp.Visible = false;
            Actualizare();
            int randomNumber = random.Next(1, 101);
            if(randomNumber <= 25)
            {
                Informatii.Text = "Padure gasita!";
                GasirePadure();
            }else if(randomNumber > 25 && randomNumber <= 45)
            {
                Informatii.Text = "Ai fost atacat de banditi!";
                GasireBanditi();
            }else if(randomNumber > 45 && randomNumber <=65)
            {
                Informatii.Text = "Ai gasit un dragon!";
                pctFundal.Image = Properties.Resources.gasiredragon__1_;
                btnFight.Visible = true;
            }else if(randomNumber > 65 && randomNumber <=95)
            {
                Informatii.Text = "Ai gasit un peisaj!";
                GasirePeisaj();
            }else
            {
                Informatii.Text = "Ai gasit un cufar!";
                GasireCufar();
               
            }
        }

        private void GasirePadure()
        {
            pctFundal.Image = Properties.Resources.gasire_mar;
            Informatii.Text = "Ai gasit 1 mar in padure!";
            player.Mere++;
            player.scor += 10;
            player.xp += 1;
            Actualizare();
        }

        private void GasireBanditi()
        {
            pctFundal.Image = Properties.Resources.banditi__1_;
            Informatii.Text = "Ai fost atacat de banditi!";
            player.HealthPoints -= 5;
            player.xp += 5;
            player.scor += 10;
            VerificareMoarteAsync();
            Actualizare();
            
        }
        
        private void GasireCufar()
        {
            pctFundal.Image = Properties.Resources.gasirecufar__1_;
            Informatii.Text = "Ai gasit o arma in cufar!";
            Item arma = new Item(IdGlobal);
            IdGlobal++;
            adminItems.AddItem(arma);
            player.scor += 50;
            player.xp += 30;
            VerificareAvansareLevelAsync();
            Actualizare();
        }

        private void GasirePeisaj()
        {
            int randomNumber = random.Next(1, 5);
            if(randomNumber == 1)
            {
                pctFundal.Image = Properties.Resources.cascada__1_;
                Informatii.Text = "Ai gasit o cascada!";
            } else if(randomNumber == 2)
            {
                pctFundal.Image = Properties.Resources.deers__1_;
                Informatii.Text = "Ai gasit caprioare!";
            }
            else if(randomNumber == 3)
            {
                pctFundal.Image = Properties.Resources.statuie1;
                Informatii.Text = "Ai gasit o statuie!";
            }
            else if(randomNumber == 4)
            {
                pctFundal.Image = Properties.Resources.statuie2;
                Informatii.Text = "Ai gasit o statuie!";
            }
        }
        private void ManacaMar(object sender,EventArgs e)
        {
            pctFundal.Image = Properties.Resources.manacmar__1_;
            Informatii.Text = "Ai mâncat un măr!";
            player.HealthPoints += player.Intelligence + 10;
            player.Mere--;
            Actualizare();
        }
        private void Lupta(object sender, EventArgs e)
        {
            btnFight.Visible = false;
            btnExplore.Visible = false;

            btnAttack.Visible = true;
            btnFlee.Visible = true;

            SaveGame.Visible = false;

            lblDetaliiDragon.Visible = true;

            PickDragon(player.stage);
        }
        
        private void Fugi(object sender, EventArgs e)
        {
            pctFundal.Image = Properties.Resources.ai_fugit__1_;
            lblDetaliiDragon.Visible = false;
            btnFlee.Visible = false;
            btnAttack.Visible = false;
            btnExplore.Visible = true;
            Actualizare();
        }
        private void PickDragon(int stagiu)
        {
            Dragon dragon = new Dragon();
            using (StreamReader reader = new StreamReader(caleCompletaFisier2))
            {
                string linie;
                do
                {
                    linie = reader.ReadLine();
                    dragon = new Dragon(linie);

                } while (dragon.IdDragon != stagiu);

            }
            DragonGlobal = dragon;
            lblDetaliiDragon.Text = DragonGlobal.Name + " HP:" + DragonGlobal.HealthPoints + " D:" + DragonGlobal.Difficulty;
        }
        #endregion

        #region Metode tip lupta
        private async void Ataca(object sender, EventArgs e) 
        {
            Informatii.Text = "Ai atacat!";
            pctFundal.Image = Properties.Resources.atac_dragon__1_;
            int randomnumber = random.Next(1, 101);
            DragonGlobal.HealthPoints -= player.Strength+10+ItemGlobal.Damage;
            ItemGlobal.Condition -= 1;
            if(ItemGlobal.Condition == 0)
            {
                adminItems.Resalvare(selecteditem);
                ItemGlobal = new Item();
                selecteditem = -1;
                Actualizare();
            }
            if (randomnumber > player.Agility)
            {
                btnAttack.Visible = false;
                btnFlee.Visible = false;
                await Task.Delay(1000);
                Informatii.Text = "Dragonul a atacat!";
                pctFundal.Image = Properties.Resources.dragonulataca__1_;
                player.HealthPoints -= DragonGlobal.Difficulty * 2;
                btnAttack.Visible = true;
                btnFlee.Visible = true;
            }
            else
            {
                Informatii.Text =Informatii.Text+"Si ai evitat atacul dragonului!";
            }
            lblHP.Text = "HP: " + player.HealthPoints;
            lblDetaliiDragon.Text = DragonGlobal.Name + " HP:" + DragonGlobal.HealthPoints + " D:" + DragonGlobal.Difficulty;

            VerificareMoarteAsync();
            
            if(DragonGlobal.HealthPoints <= 0) 
            {
                Informatii.Text = "Dragon Ucis!(Poti salva progresul)";
                pctFundal.Image = Properties.Resources.dragonmort__1_;
                await Task.Delay(1000);
                SaveGame.Visible = true;

                lblDetaliiDragon.Visible = false;

                btnAttack.Visible = false;
                btnFlee.Visible = false;

                btnExplore.Visible = true;

                player.scor += 100 * DragonGlobal.Difficulty;
                player.xp += 100*DragonGlobal.Difficulty;
                player.stage++;
                VerificareVictorie();

                Item arma = new Item(IdGlobal);
                IdGlobal++;
                adminItems.AddItem(arma);

                VerificareAvansareLevelAsync();
                Actualizare();

            }
            Actualizare();
        }

        #endregion

        #region Metode Inventar
        private void SelectareItem(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                int itemID;
                if (int.TryParse(radioButton.Name, out itemID))
                {
                    selecteditem = itemID;
                }
            }

        }

        private void Echipare(object sender, EventArgs e)
        { 
            if (selecteditem != -1 && ItemGlobal.ID != selecteditem)
            {
                List<Item> iteme = adminItems.GetItems();
                foreach (Item item in iteme)
                {
                    if (item.ID == selecteditem)
                    {
                        ItemGlobal = item;
                    }
                }
                
                
            }
            gpbInventar.Visible = false;
            btnSelect.Visible = false;
            btnEchipeazaItem.Visible = true;
            btnDezechipeazaItem.Visible = true;
            btnCautaItem.Visible = true;
            Actualizare();
        }

        private void Dezechipare(object sender, EventArgs e)
        {
           
            if(selecteditem != -1)
            {
                adminItems.Resalvare(ItemGlobal);
                ItemGlobal = new Item();
                selecteditem = -1;
                Actualizare();
            }
        }
        private void Dezechipare()
        {
            if (selecteditem != -1)
            {
                adminItems.Resalvare(ItemGlobal);
                ItemGlobal = new Item();
                selecteditem = -1;
                Actualizare();
            }
        }

        private void Echipeaza(object sender, EventArgs e)
        {
            adminItems.Resalvare(ItemGlobal);
            ActualizareInventar();
            btnCautaItem.Visible = false;
            gpbInventar.Visible = true;
            btnEchipeazaItem.Visible = false;
            btnDezechipeazaItem.Visible = false;
            btnSelect.Visible = true;
            

        }

        private void Cauta(object sender, EventArgs e)
        {
            gpbInventar.Visible = false;
            gpbCautareItem.Visible = true;
            btnEchipeazaItem.Visible=false;
            btnDezechipeazaItem.Visible = false;
            btnCautaItem.Visible = false;
        }

        private void Inchide(object sender, EventArgs e)
        {
            gpbCautareItem.Visible = false;
            btnEchipeazaItem.Visible = true;
            btnDezechipeazaItem.Visible = true;
            btnCautaItem.Visible = true;
        }
        private void CautareObiect(object sender, EventArgs e) 
        {
            int NrDeCautat;
            if(gpbTxtBoxCautaItemNume.Text.Length==0 && gpbTxtBoxCautaItemDamage.Text.Length==0)
            {
                gpbLblCautaItemWarningGeneral.Visible = true;
                gpbLblCautaItemWarningGeneral.ForeColor = Color.Red;
                gpbLblCautaItemWarningGeneral.Text = "Casute goale!";
                return;
            }

            if(!(Int32.TryParse(gpbTxtBoxCautaItemDamage.Text,out NrDeCautat)) && gpbTxtBoxCautaItemDamage.Text.Length > 0)
            {
                gpbLblCautaItemDamageWarning.Visible = true;
                gpbLblCautaItemDamageWarning.Text = "Introdu un numar!";
                gpbLblCautaItemDamageWarning.ForeColor = Color.Red;
                return;
            }


            gpbLblCautaItemDamageWarning.Visible = false;
            gpbLblCautaItemWarningGeneral.Visible = false;
            CmbBoxItemeGasite.Items.Clear();
            Item[] ItemeGasiteNume = new Item[10];
            Item[] ItemeGasiteDamage = new Item[10];
            ItemeGasiteNume = adminItems.SearchItemByName(gpbTxtBoxCautaItemNume.Text);
            ItemeGasiteDamage = adminItems.SearchItemByDamage(NrDeCautat);
            
            if(ItemeGasiteDamage.Length > 0)
            {
                gpbLblCautaItemDamageWarning.Visible = true;
                gpbLblCautaItemDamageWarning.Text = "Damage Gasit!";
                gpbLblCautaItemDamageWarning.ForeColor = Color.Green;
            }
            else
            {
                gpbLblCautaItemDamageWarning.Visible = true;
                gpbLblCautaItemDamageWarning.Text = "Damage Negasit!";
                gpbLblCautaItemDamageWarning.ForeColor = Color.Red;
            }
            if (ItemeGasiteNume.Length > 0)
            {
                gpbLblCautaItemNumeWarning.Visible = true;
                gpbLblCautaItemNumeWarning.ForeColor = Color.Green;
                gpbLblCautaItemNumeWarning.Text = "Nume Gasit!";
            }
            else
            {
                gpbLblCautaItemNumeWarning.Visible = true;
                gpbLblCautaItemNumeWarning.ForeColor = Color.Red;
                gpbLblCautaItemNumeWarning.Text = "Nume Negasit!";
            }
            if(ItemeGasiteDamage.Length == 0 && ItemeGasiteNume.Length == 0)
            {
                gpbLblCautaItemWarningGeneral.Visible = true;
                gpbLblCautaItemWarningGeneral.Text = "Niciun Item Gasit";
                gpbLblCautaItemWarningGeneral.ForeColor = Color.Red;
            }
            else
            {
                gpbLblCautaItemWarningGeneral.Visible = true;
                gpbLblCautaItemWarningGeneral.Text = "Item Gasit";
                gpbLblCautaItemWarningGeneral.ForeColor = Color.Green;
            }

            if (CautareInMatrici(ItemeGasiteNume,ItemeGasiteDamage).Length == 0 && ItemeGasiteNume.Length > 0 && ItemeGasiteDamage.Length>0)
            {
                gpbLblCautaItemWarningGeneral.Visible = true;
                gpbLblCautaItemWarningGeneral.Text = "Combinatie Negasita!";
                gpbLblCautaItemWarningGeneral.ForeColor = Color.Red;
            }
           
            foreach (Item item in CautareInMatrici(ItemeGasiteNume, ItemeGasiteDamage))
            {
                CmbBoxItemeGasite.Items.Add(item.Info());
            }



        }
       
        private Item[] CautareInMatrici(Item[] items1, Item[] items2)
        {
            if(items2.Length == 0)
            {
                return items1;
            }
            if(items1.Length == 0)
            {
                return items2;
            }
           List<Item> itemeAsemenea = new List<Item>();
            for(int i=0; i < items1.Length; i++) 
            {
                for(int j =0; j < items2.Length; j++)
                {
                    if (items1[i].Name == items2[j].Name && items1[i].Damage == items2[j].Damage)
                    {
                        itemeAsemenea.Add(items1[i]);
                    }
                }
            }
            return itemeAsemenea.ToArray();
        }
        #endregion


    }

}
