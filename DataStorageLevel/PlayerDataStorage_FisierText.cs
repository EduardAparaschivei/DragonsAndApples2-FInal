using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.IO;

namespace DataStorageLevel
{
    public class PlayerDataStorage_FisierText
    {
        private const int NR_MAX_PLAYERS = 10;
        private string numeFisier;

        public PlayerDataStorage_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            //incercare deschidere fisier
            // se creaza un fisier daca nu exista deja
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddPlayer(Player player) 
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier,true)) 
            {
                streamWriterFisierText.WriteLine(player.ConversieLaSir_PentruFisier());
            }
        }

        public Player[] GetPlayers(out int nrPlayers)
        {
            Player[] players = new Player[NR_MAX_PLAYERS];
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrPlayers = 0;
                while((linieFisier = streamReader.ReadLine()) != null) 
                {
                    players[nrPlayers++] = new Player(linieFisier);
                }
            }
            Array.Resize(ref players, nrPlayers);

            return players;
        }

        public Player GetPlayer(string nume,int level)
        {
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                while((linieFisier = streamReader.ReadLine()) != null)
                        {
                         Player player = new Player( linieFisier);
                    if (player.Name.Equals(nume) && player.Level.Equals(level))
                    { return player; }
                }
            }
            return null;
        }

    }
}
