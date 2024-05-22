using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Player
    {
        //constante
        private const char SeparatorFisier = ';';

        private const int NAME = 0;
        private const int STRENGHT = 1;
        private const int INTELLIGENCE = 2;
        private const int AGILITY = 3;
        private const int HEALTHPOINTS = 4;
        private const int ABILITYPOINTS = 5;
        private const int XP = 6;
        private const int LEVEL = 7;
        private const int SCOR = 8;
        private const int MERE = 9;
        private const int STAGE = 10;
        
        public string Name;
        public float HealthPoints { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int AbilityPoints { get; set; }
        public int xp { get; set; }
        public int Level { get; set; }
        public int scor {  get; set; }
        public int Mere { get; set; }
        public int stage { get; set; }

        public Player()
        {
            Name = string.Empty;
            Strength = 0;
            Intelligence = 0;
            Agility = 0;
            HealthPoints = 100;
            AbilityPoints = 5;
            xp = 0;
            Level = 1;
            scor = 0;
            Mere = 0;
            stage = 1;

        }

        public Player(string name, int strength, int intelligence, int agility, int level)
        {
            Name = name;
            Strength = strength;
            Intelligence = intelligence;
            Agility = agility;
            HealthPoints = 100;
            AbilityPoints = 5;
            xp = 0;
            Level = level;
            scor = 0;
            Mere = 0;
            stage =1;
        }

        public Player(string linieFisier)
        {
            //In ordinea lor din fisier
            string[] dateFisier = linieFisier.Split(SeparatorFisier);
            this.Name = dateFisier[NAME];
            this.Strength = Convert.ToInt32(dateFisier[STRENGHT]);
            this.Intelligence = Convert.ToInt32(dateFisier[INTELLIGENCE]);
            this.Agility = Convert.ToInt32(dateFisier[AGILITY]);
            this.HealthPoints = float.Parse(dateFisier[HEALTHPOINTS]);
            this.AbilityPoints = Convert.ToInt32(dateFisier[ABILITYPOINTS]);
            this.xp = Convert.ToInt32(dateFisier[XP]);
            this.Level = Convert.ToInt32(dateFisier[LEVEL]);
            this.scor = Convert.ToInt32(dateFisier[SCOR]);
            this.Mere = Convert.ToInt32(dateFisier[MERE]);
            this.stage = Convert.ToInt32(dateFisier[STAGE]);

        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectPlayerpentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}",
                SeparatorFisier,
                (Name ?? "Necunoscut"),
                Strength.ToString(),
                Intelligence.ToString(),
                Agility.ToString(),
                HealthPoints.ToString(),
                AbilityPoints.ToString(),
                xp.ToString(),
                Level.ToString(),
                scor.ToString(),
                Mere.ToString(),
                stage.ToString());
            return obiectPlayerpentruFisier;

        }
        public string Info()
        {
            string infoPlayer = string.Format(" Nume:{0} S:{1} I:{2} A:{3} HP:{4} AP:{5} XP:{6} Level:{7} Scor:{8}",
                this.Name ?? "NECUNOSCUT",
                this.Strength,
                this.Intelligence,
                this.Agility,
                this.HealthPoints,
                this.AbilityPoints,
                this.xp,
                this.Level,
                this.scor);
            return infoPlayer;
           
        }


    }
}
