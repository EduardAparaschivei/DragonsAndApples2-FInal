using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataStorageLevel
{
    
    
    public class DragonsDataStorage_FisierText
    {
        private const int NR_Max_Dragoni = 50;
        private string numeFisier;

        public DragonsDataStorage_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier,FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddDragon(Dragon dragon)
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier,true))
            {
                streamWriterFisierText.WriteLine(dragon.ConversieLaSir_PentruFisier());
            }
        }

        public Dragon[] GetDragons(out int nrDragons)
        {
            Dragon[] dragons = new Dragon[NR_Max_Dragoni];
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFiser;
                nrDragons = 0;

                while((linieFiser = streamReader.ReadLine()) != null) 
                {
                    dragons[nrDragons++] = new Dragon(linieFiser);
                }
            }
            Array.Resize(ref dragons, nrDragons);
            return dragons;

        }
    }
}
