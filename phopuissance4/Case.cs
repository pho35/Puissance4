using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phopuissance4
{
    public class Case
    {
        // Si la case est occupé
        public bool isOccupe;
        // Joueur qui occupe la case
        Joueur leJoueur;
        // Construit une case non occupé
        public Case()
        {
            isOccupe = false;
            leJoueur = null;
        }

        // Rend une case occupé par un joueur
        // Renvoie true si la case a pu être occupé
        // Sinon false
        public bool occuperCase(Joueur unJoueur)
        {
            // Si la case n'est pas occupée
            if (!isOccupe)
            {
                // On assigne la case au joueur
                leJoueur = unJoueur;
                isOccupe = true;
                return true;
            }
            // Si la case est déjà occupée
            else
            {
                // Renvoie false
                return false;
            }
        }

        // Obtient le joueur qui occupe la case
        public Joueur getJoueurOccupant()
        {
            return leJoueur;
        }
    }
}
