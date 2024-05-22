using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataStorageLevel
{
    public class PlayerDataStorage_Memory
    {
        private const int NR_MAX_Players = 10;

        private Player[] players;
        private int nrPlayers;

        public PlayerDataStorage_Memory()
        {
            players = new Player[NR_MAX_Players];
            nrPlayers = 0;
        }

        public void AddPlayer(Player player)
        {
            players[nrPlayers] = player;
            nrPlayers++;
        }

        public Player[] GetPlayers(out int nrPlayers)
        {
            nrPlayers = this.nrPlayers;
            return players;
        }
    }
}
