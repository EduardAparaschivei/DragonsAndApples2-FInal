using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using DataStorageLevel;
using System.Configuration;
using System.Drawing;

namespace InterfataUtilizator_WindowsForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
            //Application.Run(new LoseScreen());
            //Application.Run(new WinScreen());
        }
    }
}
