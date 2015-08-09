using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phopuissance4
{
    public class Joueur
    {
        string nom;
        // Score du joueur
        private int score;
        public bool cd = false;
        private int numero;
        private int special;
        private Color couleur;        

        // Construcuteur
        public Joueur(string unNom, Color laCouleur, int number)
        {
            nom = unNom;
            // On initialise le score à 0
            score = 0;
            numero = number;
            special = 1;
            // On initlaise la couleur du joueur
            couleur = laCouleur;

        }

        // TODO : renvoyer la position du pion joué
        public Point jouer(MyPlateau lePlateau, int numeroColonne)
        {
            Point positionPion;
            Console.WriteLine("la colonne {0} a été jouée", numeroColonne);

            // Le joueur joue le pion
            positionPion = lePlateau.ajouterPion(this, numeroColonne);

            // Renvoie la position du pion
            return positionPion;
            
        }

        public void jouer(MyPlateau lePlateau)
        {
            bool isEntreeOk = false;
            

            do
            {
                int choix = 0;
                // Demande au joueur de choisir une colonne
                Console.WriteLine("Cher {0}, dans quelle colonne souhaitez vous mettre votre jeton (1,2,3,4,5,6,7)?", nom);

                string lechoix = Console.ReadLine();
                Console.WriteLine("choix : " + lechoix);

                // Transorme la chaine de caractère en int
                try
                {

                    choix = Int32.Parse(lechoix);
                }
                // Problème lors de l'entrée
                catch (FormatException)
                {
                    // Signalement à l'utilisateur
                    Console.WriteLine("Erreur lors de la saisie");
                }

                // Verifie si le choix est valide
                if (choix > 0 && choix < 8)
                {
                    // La choix est valide
                    isEntreeOk = true;
                    // Met le pion dans cette colonne
                    lePlateau.ajouterPion(this, choix);
                }
                else
                {
                    isEntreeOk = false;
                }
                
            }
            while (!isEntreeOk);
            
            
        }

        // Retourne le score
        public int getScore()
        {
            return score;
        }

        // Retourne le numéro u joueur
        public int getNum()
        {
            return numero;
        }

        // Retourne le score
        public int getSpecial()
        {
            return special;
        }

        public void setScore()
        {
            //set le score
            score = score + 1;
        }

        // Retourne le nom
        public string getNom()
        {
            return nom;
        }

        public Color Couleur
        {
            get
            {
                return couleur;
            }
            set
            {
                couleur = value;
            }
        }
    }
}
