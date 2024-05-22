using Entities;
using DataStorageLevel;
using System;
using System.Configuration;
using System.IO;

namespace DragonsAndApples2
{
    class Program
    {
        static void Main()
        {
            

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier2"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            DragonsDataStorage_FisierText adminDragons = new DragonsDataStorage_FisierText(caleCompletaFisier);
            Dragon dragon = new Dragon();
            int nrDragons =0;

            adminDragons.GetDragons(out nrDragons);
            bool verificare;
            int dificultate, loot;
            string name;
            char choice;
            do
            {
                Console.WriteLine("1.Create Player");
                Console.WriteLine("2.Show Created Player");
                Console.WriteLine("3.Salvare in fisier.");
                Console.WriteLine("4.Afisare din fisier.");
                Console.WriteLine("0.Exit");
                Console.Write("Enter your choice: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        name = GetPlayerName();
                        do
                        {
                            Console.WriteLine("Dificultate: ");
                            verificare = Int32.TryParse(Console.ReadLine(), out  dificultate);
                        } while (!verificare);
                        verificare = false;
                        do
                        {
                            Console.WriteLine("Loot: ");
                            verificare = Int32.TryParse(Console.ReadLine(), out loot);
                        } while (!verificare);
                        dragon = new Dragon(name,dificultate,loot);
                        break;
                    case '2':
                        if (dragon != null)
                        {
                            Console.WriteLine(dragon.InfoDragon());
                        }
                        else 
                        {
                            Console.WriteLine("No dragon created.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                    case '3':
                        int idDragon = ++nrDragons;
                        dragon.IdDragon = idDragon;
                        adminDragons.AddDragon(dragon);
                        break;
                    case '4':
                        Dragon[] dragons = adminDragons.GetDragons(out nrDragons);
                        ShowDragons(dragons);

                        break;
                }
            } while (choice != '0');

        } 

        static string GetPlayerName()
        {
            Console.Write("Enter your name:");
            return Console.ReadLine();
        }

        public static void AfisarePlayer(Player player)
        {
            string infoPlayer = string.Format(" Nume:{0} S:{1} I:{2} A:{3} Money:{4} HP:{5} AP:{6} XP:{7} Level:{8}",
                player.Name ?? "NECUNOSCUT",
                player.Strength,
                player.Intelligence,
                player.Agility,
                player.Money,
                player.HealthPoints,
                player.AbilityPoints,
                player.xp,
                player.Level);
            Console.WriteLine(infoPlayer);
        }

        public static void AfisarePlayers(Player[] players)
        {
            Console.WriteLine("Players:");
            foreach (Player player in players) 
            {
                string infoStudent = player.Info();
                Console.WriteLine(infoStudent);
            }
        }

        public static void ShowItems(Item[] items,int nritems)
        {
            Console.WriteLine("The items are:");
            for(int i = 0;i<nritems;i++) 
            {
                string infoitem = items[i].Info();
                Console.WriteLine(infoitem);
            }
        }
        public static void ShowDragons(Dragon[] dragons)
        {
            Console.WriteLine("The dragons are:");
            foreach(Dragon dragon in dragons)
            {
                string infoDragon = dragon.InfoDragon();
                Console.WriteLine(infoDragon);
            }
           
        }

    }
}
