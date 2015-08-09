using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phopuissance4
{
    public partial class NomJoueurs : Form
    {

        private Color CouleurJoueur1;
        private Color CouleurJoueur2;

        public NomJoueurs()
        {
            InitializeComponent();
        }

        private void NomJoueurs_Load(object sender, EventArgs e)
        {
            // Initialisation des picturebox
            initialisationPictureBox();
        }

        private void initialisationPictureBox()
        {
            // Définition des pointeurs
            Image PionRouge;
            Image PionJaune;
            Image PionVert;
            Image PionBleu;

            // Création des images
            PionRouge = creationPions(Color.Red);
            PionJaune = creationPions(Color.Yellow);
            PionVert = creationPions(Color.Green);
            PionBleu = creationPions(Color.Blue);

            // Attribution des images à la fenetre
            pictureBoxJetonRouge1.Image = PionRouge;
            pictureBoxJetonJaune1.Image = PionJaune;
            pictureBoxJetonVert1.Image = PionVert;
            pictureBoxJetonBleu1.Image = PionBleu;
            pictureBoxJetonRouge2.Image = PionRouge;
            pictureBoxJetonJaune2.Image = PionJaune;
            pictureBoxJetonVert2.Image = PionVert;
            pictureBoxJetonBleu2.Image = PionBleu;

            // Met les couleurs par defaut
            CouleurJoueur1 = Color.Blue;
            CouleurJoueur2 = Color.Yellow;
        }

        // Renvoie l'image d'un pion en fonction de sa couleur
        private Image creationPions(Color couleur)
        {
            // Création d'une bitmap
            Bitmap bm = new Bitmap(32, 32);
            // Gration d'un objet graphics pour dessiner dessus
            Graphics g = Graphics.FromImage(bm);
            // Créatin d'un pinceau
            //Brush b = new SolidBrush(couleur);
            System.Drawing.Drawing2D.LinearGradientBrush b = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, 40, 30), couleur, Color.White, -45, false);

            g.FillEllipse(b, 0, 0, bm.Size.Width, bm.Size.Height);

            // Renvoie l'image
            return bm;

        }

        // Fonctions pour récupérer le nom des joueurs
        public string getNomJoueur1()
        {
            return nomJoueur1.Text;
        }
        public string getNomJoueur2()
        {
            return nomJoueur2.Text;
        }

        // Fonctions pour récupérer les couleurs des joueurs
        public Color getCouleurJoueur1()
        {
            return CouleurJoueur1;
        }
        public Color getCouleurJoueur2()
        {
            return CouleurJoueur2;
        }

        private void radioRouge1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur1 = Color.Red;
        }

        private void radioJaune1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur1 = Color.Yellow;
        }

        private void radioVert1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur1 = Color.Green;
        }

        private void radioBleu1_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur1 = Color.Blue;
        }

        private void radioRouge2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur2 = Color.Red;
        }

        private void radioJaune2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur2 = Color.Yellow;
        }

        private void radioVert2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur2 = Color.Green;
        }

        private void radioBleu2_CheckedChanged(object sender, EventArgs e)
        {
            CouleurJoueur2 = Color.Blue;
        }

    }
}
