using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phopuissance4
{
    public class Partie
    {
        // Tableau contenant les deux joueurs
        Joueur[] joueur;
        MyPlateau lePlateau;
        // Numero du joeur qui est en train de jouer (0 ou 1)
        int numeroJoueurEnCours;
        // Dans le cas d'un match nul, passe à true
        bool matchNul;
        // Initialise la partie

        public Partie(Joueur joueur1, Joueur joueur2)
        {
            // Initialise les joueurs
            joueur = new Joueur[2];
            joueur[0] = joueur1;
            joueur[1] = joueur2;

            // Pas encore de match nul
            matchNul = false;

            // Initialise le plateau
            lePlateau = new MyPlateau(joueur1, joueur2);
        }

        public Joueur tirerAuSortJoueur()
        {
            // Tire au sort le joueur en cours
            Random theRandom = new Random();
            int number = theRandom.Next();
            numeroJoueurEnCours = number % 2;

            return getJoueurEnCours();
        }

        public Joueur getJoueurEnCours()
        {
            return joueur[numeroJoueurEnCours];
        }

        //Retoure la liste des oueru
        public Joueur getJoueur(int i)
        {
            return joueur[i];
        }

        // Passe au joueur suivant
        public void PasserJoueurSuivant()
        {
            numeroJoueurEnCours = (++numeroJoueurEnCours) % 2;
        }

        // Revoie le plateau
        public MyPlateau getPlateau()
        {
            return lePlateau;
        }

        public int NumeroJoueurEnCours
        {
            get
            {
                return numeroJoueurEnCours;
            }
        }

        // Renvoie le match nul
        public bool getMatchNul()
        {
            return matchNul;
        }
    }
}
