using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace phopuissance4
{
    public class MyPlateau
    {
        public static int nbLignes = 6;
        public static int nbColonnes = 7;
        Case[,] lesCases;
        //int nbLignes;
        //int nbColonnes;
        private Joueur joueur1;
        private Joueur joueur2;
        private Joueur joueurGagnant;
        enum Etat : int { NonCommence, EnCours, Finie };
        Etat etatPlateau;
        bool matchNul;      // True en cas de match nul

        // Construit le plateau
        public MyPlateau(Joueur joueur1, Joueur joueur2)
        {
            //nbLignes = 6;
            //nbColonnes = 7;
            // Crée un tableau de 6 lignes, 7 colonnes
            lesCases = new Case[nbLignes, nbColonnes];

            // Alloue le tableau
            for (int i = 0; i < nbLignes; i++)
                for (int j = 0; j < nbColonnes; j++)
                    lesCases[i, j] = new Case();

            // Initialise les joueurs
            this.joueur1 = joueur1;
            this.joueur2 = joueur2;

            etatPlateau = Etat.NonCommence;

            matchNul = false;
        }

        // Vide le plateau
        public void vider()
        {

        }

        public Point ajouterPion(Joueur unJoueur, int numeroColonne)
        {
            int i;
            Point positionPion;

            // Si l'état n'est pas en cours
            if (etatPlateau == Etat.NonCommence)
                // Met en état en cours
                etatPlateau = Etat.EnCours;

            // Vérifie si la colonne n'est pas pleine
            if (lesCases[nbLignes - 1, numeroColonne - 1].isOccupe)
            {
                // La colonne est pleine, on ne fait rien
                // Retourne null
                positionPion = new Point(0, 0);
                return positionPion;
            }
            else
            {
                // la colonne n'est pas pleine
                for (i = 0; lesCases[i, numeroColonne - 1].isOccupe; i++)
                {
                }
                // On regarde jusqu'où la colonne est remplie
                Console.WriteLine("Numero de ligne :{0} ", i);
                // On ajoute le pion
                lesCases[i, numeroColonne - 1].occuperCase(unJoueur);
            }
            // Retourne la position du pion
            positionPion = new Point(i + 1, numeroColonne);
            return positionPion;
        }

        private bool cherche4alignes(int oCol, int oLigne, int dCol, int dLigne)
        {
            //Joueur joueurActuel = null;
            Joueur ancienJoueur = null;
            Joueur joueurOccupant = null;
            int compteur = 0;

            int curCol = oCol;
            int curRow = oLigne;

            while ((curCol >= 0) && (curCol < nbColonnes) && (curRow >= 0) && (curRow < nbLignes))
            {
                // Récupère le joueur actuel
                joueurOccupant = lesCases[curRow, curCol].getJoueurOccupant();

                // Si le joueur actuel est "null"
                if (joueurOccupant == null)
                {
                    // On réinitialise le compteur
                    compteur = 1;
                    // On met à jour l'ancien joueur
                    ancienJoueur = null;
                }
                // Si le joueur occupant n'est pas nul
                else
                {
                    // Si l'ancien joueur est différent du joueur occupant
                    if (joueurOccupant != ancienJoueur)
                    {
                        // On réinitialise le compteur
                        compteur = 1;
                        // On met à jour l'ancien joueur
                        ancienJoueur = joueurOccupant;
                    }
                    // Sinon 
                    else
                    {
                        // on incrémente
                        compteur++;
                    }
                }

                // On sort lorsque le compteur atteint 4
                if ((joueurOccupant != null) && (compteur == 4))
                {
                    // Le joueur occupant devient le joueur gagnant
                    joueurGagnant = joueurOccupant;
                    return true;
                }

                // On passe à l'itération suivante
                curCol += dCol;
                curRow += dLigne;
            }

            // Aucun alignement n'a été trouvé
            return false;
        }

        // Vérifie si le plateau de jeu est plein
        public bool verifierPlateauPlein()
        {
            // On part du principe que le plateau est plein
            bool plateauPlein = true;

            // Pour chaque colonne
            for (int i = 0; i < nbColonnes; i++)
            {
                // Si le haut de la colonne n'est pas occupé
                if (!(lesCases[nbLignes - 1, i].isOccupe))
                {
                    // On met plateau plein à false
                    plateauPlein = false;
                }
            }

            // True si le plateau est plein, false sinon
            return plateauPlein;
        }


        // Vérifie si un joueur a gagné
        public bool verifierFinPartie()
        {
            // Vérifie les horizontales ( - )
            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                if (cherche4alignes(0, ligne, 1, 0))
                {
                    return true;
                }
            }

            // Vérifie les verticales ( ¦ )
            for (int col = 0; col < nbColonnes; col++)
            {
                if (cherche4alignes(col, 0, 0, 1))
                {
                    return true;
                }
            }

            // Diagonales (cherche depuis la ligne du bas)
            for (int col = 0; col < nbColonnes; col++)
            {
                // Première diagonale ( / )
                if (cherche4alignes(col, 0, 1, 1))
                {
                    return true;
                }
                // Deuxième diagonale ( \ )
                if (cherche4alignes(col, 0, -1, 1))
                {
                    return true;
                }
            }

            // Diagonales (cherche depuis les colonnes gauches et droites)
            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                // Première diagonale ( / )
                if (cherche4alignes(0, ligne, 1, 1))
                {
                    return true;
                }
                // Deuxième diagonale ( \ )
                if (cherche4alignes(nbLignes - 1, ligne, -1, 1))
                {
                    return true;
                }
            }

            // Vérifie si le puissance 4 est plein
            if (verifierPlateauPlein())
            {
                // Le plateau est plein
                // On met le plateau en mode match nul
                matchNul = true;

                // La partie est finie
                return true;
            }

            // On n'a rien trouvé
            return false;


        }

        // renvoie le joueur gagnant
        public Joueur getJoueurGagnant()
        {
            return joueurGagnant;
        }

        // Renvoie true si match nul
        public bool getMatchNul()
        {
            return matchNul;
        }

        // Affiche le plateau
        public void afficher()
        {
            // Reviens à la ligne
            Console.Write("\n");
            // affiche toutes les lignes
            for (int i = nbLignes - 1; i >= 0; i--)
            {
                // Reviens à la ligne
                Console.Write("\n");
                // Affiche toutes les colonnes
                for (int j = 0; j < nbColonnes; j++)
                {
                    Char pion = new Char();
                    pion = ' ';

                    // Si la case est occupée
                    if (lesCases[i, j].isOccupe)
                    {
                        // Si c'est le joueur1 qui occupe la case
                        Joueur joueurOccupant = lesCases[i, j].getJoueurOccupant();
                        if (joueurOccupant.Equals(joueur1))
                        {
                            // On met un pion du joueur1
                            pion = 'X';
                        }
                        else if (joueurOccupant.Equals(joueur2))
                        {
                            // On met un pion du joueur2
                            pion = 'O';
                        }
                    }
                    // Sinon
                    else
                    {
                        // On met un blanc
                        pion = '%';
                    }
                    Console.Write("{0}", pion);
                }

            }
            Console.WriteLine();
            // Affiche les numéros sous les colonnes
            for (int i = 0; i < nbColonnes; i++)
            {
                Console.Write(i + 1);
            }

            Console.WriteLine("\n\n");
        }

        public Joueur Joueur1
        {
            get
            {
                return joueur1;
            }
        }

        public Joueur Joueur2
        {
            get
            {
                return joueur2;
            }
        }
    }
}
