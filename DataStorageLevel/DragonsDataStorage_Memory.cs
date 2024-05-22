using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorageLevel
{
    public class DragonsDataStorage_Memory
    {
        private const int MAX_Dragons_NR = 50;

        private Dragon[] dragons;
        private int nrDragons;


        public DragonsDataStorage_Memory()
        {
            dragons = new Dragon[MAX_Dragons_NR];
            nrDragons = 0;
        }

        public void AddDragon(Dragon dragon)
        {
            dragons[nrDragons] = dragon;
            nrDragons++;
        }

        public Dragon[] GetDragons(out int nrDragons)
        {
            nrDragons = this.nrDragons;
            return dragons;
        }
        public Dragon[] SearchDragonByName(string search)
        {
            List<Dragon> FoundDragons = new List<Dragon>();
            foreach (Dragon dragon in dragons)
            {
                if (dragon != null && dragon.Name.ToLower() == search.ToLower())
                {
                    FoundDragons.Add(dragon);
                }
            }
            return FoundDragons.ToArray();
        }
        public Dragon[] SearchDragonByDifficulty(int difficulty)
        {
            List<Dragon> FoundDragons = new List<Dragon>();
            foreach (Dragon dragon in dragons)
            {
                if (dragon != null && dragon.Difficulty == difficulty)
                {
                    FoundDragons.Add(dragon);
                }
            }
            return FoundDragons.ToArray();
        }


    }
}
