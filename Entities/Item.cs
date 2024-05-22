using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Item
    {
        private static readonly Random random = new Random();
        private const char SeparatorFisier = ';';
       public enum ItemName
        {
            Sabie,
            Topor,
            Ciocan,
            Arc,
            Cutit
        }

        private const int IDUL = 0;
        private const int NAME = 1;
        private const int DAMAGE = 2;
        private const int CONDITION = 3;

        public int ID = 0;
        public ItemName Name;
        public int Damage {  get; set; }
        public int Condition { get; set; }

        public Item(int id) 
        {
            ID = id;
            Name = (ItemName)random.Next(Enum.GetValues(typeof(ItemName)).Length);
            Damage = random.Next(5, 41);
            Condition = random.Next(1, 21);
        }

        public Item()
        {
            ID = 0;
            Name = ItemName.Sabie;
            Damage = 0;
            Condition = 0;
        }

        public Item(ItemName name, int damage, int condition)
        {
            Name = name;
            Damage = damage;
            Condition = condition;
        }

        public Item(string linieFisier) 
        {
            string[] dateFisier = linieFisier.Split(SeparatorFisier);
            this.ID =Convert.ToInt32(dateFisier[IDUL]);
            if(Enum.TryParse(dateFisier[NAME],out ItemName name) && Enum.IsDefined(typeof(ItemName),name))
            {
                this.Name = name;
            }
            else
            {
                throw new ArgumentException("Nume necorespunzator in fisier!");
            }
            this.Damage = Convert.ToInt32(dateFisier[DAMAGE]);
            this.Condition = Convert.ToInt32(dateFisier[CONDITION]);

        }

        public string ConversieLaSir_PentruFisier()
        {

            string obiectItempentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}",
                SeparatorFisier,
                ID.ToString(),
                Name.ToString(),
                Damage.ToString(),
                Condition.ToString());
            return obiectItempentruFisier;
        }

        public string Info()
        {
            return $"{Name} D: {Damage} C: {Condition}";
        }

    }
}
