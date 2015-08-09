using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phopuissance4
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            JeuxPuissance4 fenetre = new JeuxPuissance4();
            Application.Run(fenetre);
        }
    }
}
