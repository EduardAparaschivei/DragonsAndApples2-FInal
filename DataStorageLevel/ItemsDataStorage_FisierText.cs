using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.IO;

namespace DataStorageLevel
{
    public class ItemsDataStorage_FisierText
    {
        private const int NR_MAX_ITEMS = 10;
        private string numeFisier;

        public ItemsDataStorage_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddItem(Item item) 
        {
            using(StreamWriter streamWriterFisierText = new StreamWriter(numeFisier,true))
            {
                streamWriterFisierText.WriteLine(item.ConversieLaSir_PentruFisier());
            }
        }

        public void Resalvare(int id)
        {
            List<Item> items = GetItems();
          
                using(StreamWriter streamWriter = new StreamWriter(numeFisier,false))
                {
                    foreach(Item item in items)
                    {
                        if(item.ID != id)
                        {
                            streamWriter.WriteLine(item.ConversieLaSir_PentruFisier());
                        }
                    }
                }

        }

        public void Resalvare(Item itemModificat)
        {
            List<Item> items = GetItems();
            using (StreamWriter streamWriter = new StreamWriter(numeFisier, false))
            {
                foreach (Item item in items)
                {
                    if (item.ID == itemModificat.ID)
                    {
                        streamWriter.WriteLine(itemModificat.ConversieLaSir_PentruFisier());
                    }
                    else
                    {
                        streamWriter.WriteLine(item.ConversieLaSir_PentruFisier());
                    }
                }
            }

        }

        public List<Item> GetItems()
        {
           List<Item> iteme = new List<Item>();
            using(StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while((linieFisier = streamReader.ReadLine()) != null) 
                {
                    iteme.Add(new Item(linieFisier));
                }
            }
            return iteme;
            
        }

        public Item GetItem(string nume,int ID)
        {
            using(StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while((linieFisier = streamReader.ReadLine()) != null)
                {
                    Item item = new Item(linieFisier);
                    if(item.Name.Equals(nume)&& item.ID.Equals(ID))
                        {
                        return item;
                    }
                }
            }
            return null;
        }

        public Item[] SearchItemByName(string search)
        {
            List<Item> FoundItems = new List<Item>();
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                
                while((linieFisier = streamReader.ReadLine()) != null)
                {
                    Item item = new Item( linieFisier);
                    if(item.Name.ToString().ToLower() == search.ToLower())
                    {
                        FoundItems.Add(item);
                    }
                    
                }
            }
                return FoundItems.ToArray();
        }

        public Item[] SearchItemByDamage(int damage)
        {
            List<Item> FoundItems = new List<Item>();
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Item item = new Item(linieFisier);
                    if (item.Damage == damage)
                    {
                        FoundItems.Add(item);
                    }

                }
            }
            return FoundItems.ToArray();
        }
    }
}
