using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phopuissance4
{
    public partial class JeuxPuissance4 : Form
    {
        private Partie unePartie;
        public JeuxPuissance4()
        {
            InitializeComponent();
            // Met le nom des joueur à rien du tout
            lblJoueur1.Text = "";
            lblJoueur2.Text = "";
        }

        private void JeuxPuissance4_Load(object sender, EventArgs e)
        {
            // Affichage de la boite de dialogue pour créer une nouvelle partie
            creerNouvellePartie();
        }

        private void creerNouvellePartie()
        {
            // Déclare les joueurs
            Joueur joueur1 = null;
            Joueur joueur2 = null;
            Joueur joueurEnCours = null;

            //Enabled les bouton pour être pret a jouer
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

            do{
                // Crée une boite de dialogue pour demander le nom des joueurs
                NomJoueurs DiagNom = new NomJoueurs();

                // Lance la boite de dialogue
                if (DiagNom.ShowDialog() == DialogResult.OK)
                {
                    // Récupère le résultat et crée les joueurs
                    joueur1 = new Joueur(DiagNom.getNomJoueur1(), DiagNom.getCouleurJoueur1(), 1);
                    joueur2 = new Joueur(DiagNom.getNomJoueur2(), DiagNom.getCouleurJoueur2(), 2);

                    if (joueur1.Couleur == joueur2.Couleur)
                    {
                        MessageBox.Show("Vous ne pouvez pas avoir la même couleur(C'est mélangeant!).", "Problème");
                    }
                    else if (DiagNom.getNomJoueur1().Equals(DiagNom.getNomJoueur2()))
                    {
                        MessageBox.Show("Vous ne pouvez pas avoir le même pseudo.", "Problème");
                    }
                    else if (DiagNom.getNomJoueur1().Equals("") || DiagNom.getNomJoueur2().Equals(""))
                    {
                        MessageBox.Show("Vous devez remplir vos pseudos !", "Problème");
                    }
                    else
                    {
                        // Vide le plateau
                        tableLayoutPanel1.Controls.Clear();

                        // Met à jour le nom des joueurs
                        lblJoueur1.Text = joueur1.getNom();
                        lblJoueur2.Text = joueur2.getNom();


                        //Met a Jour les scores
                        label2.Text = "0";
                        label4.Text = "0";

                        // Crée la partie
                        unePartie = new Partie(joueur1, joueur2);
                        joueurEnCours = unePartie.tirerAuSortJoueur();
                        MessageBox.Show(joueurEnCours.getNom() + " commence la partie");
                        statusStrip1.Items[0].Text = joueurEnCours.getNom() + " à toi de jouer";

                        // Création des images pour la sélection des joueurs
                        creationPictureBoxSelections();
                       
                        // Metà jour la fenetre
                        MettreAJourFenetre();
                    }
                }
                else
                {
                    joueur1 = new Joueur("", Color.Blue, 1);
                    joueur2 = new Joueur("", Color.Yellow, 2);
                    Application.Exit();
                    break;
                }

            } while (joueur1.Couleur == joueur2.Couleur || joueur1.getNom().Equals(joueur2.getNom()) || joueur1.getNom().Equals("") || joueur2.getNom().Equals(""));
            //} while (joueur1.Couleur == joueur2.Couleur);
        }

        //faire un partie avec les mêmes joueurs
        private void refaireUnePartie()
        {
            Joueur joueur1 = null;
            Joueur joueur2 = null;
            Joueur joueurEnCours = null;

            //Enabled les bouton pour être pret a jouer
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

            joueur1 = unePartie.getJoueur(0);
            joueur2 = unePartie.getJoueur(1);

            // Vide le plateau
            tableLayoutPanel1.Controls.Clear();

            // Met à jour le nom des joueurs
            lblJoueur1.Text = joueur1.getNom();
            lblJoueur2.Text = joueur2.getNom();

            // Crée la partie
            unePartie = new Partie(joueur1, joueur2);
            joueurEnCours = unePartie.tirerAuSortJoueur();
            MessageBox.Show(joueurEnCours.getNom() + " commence la partie");
            statusStrip1.Items[0].Text = joueurEnCours.getNom() + " à toi de jouer";

            // Création des images pour la sélection des joueurs
            creationPictureBoxSelections();

            // Metà jour la fenetre
            MettreAJourFenetre();
        }

        private bool verifierFinPartie()
        {
            return unePartie.getPlateau().verifierFinPartie();
        }

        // Met à jour la fenetre
        private void MettreAJourFenetre()
        {
            // Si la partie est lancée
            if (unePartie != null)
            {
                Joueur joueurEnCours = unePartie.getJoueurEnCours();

                // Si la partie n'est pas finie
                if (!unePartie.getPlateau().verifierFinPartie())
                {
                    // Met à jour la barre d'état
                    statusStrip1.Items[0].Text = joueurEnCours.getNom() + " à toi de jouer";

                    // Encadre le joueur en cours
                    // Si c'est le joueur 1
                    if (unePartie.NumeroJoueurEnCours == 0)
                    {
                        pictureBoxSelectionJoueur1.Visible = true;
                        pictureBoxSelectionJoueur2.Visible = false;
                    }
                    // Si c'est le joueur 2
                    else if (unePartie.NumeroJoueurEnCours == 1)
                    {
                        pictureBoxSelectionJoueur1.Visible = false;
                        pictureBoxSelectionJoueur2.Visible = true;
                    }
                    // SInon il y a un problème
                    else
                    {
                        throw new Exception("Problème au niveau du tour des joueurs...");
                    }

                }
                // Si la partie est finie
                else
                {
                    // Si il y a match nul
                    if (unePartie.getPlateau().getMatchNul())
                    {
                        statusStrip1.Items[0].Text = "Match nul";
                    }
                    // Sinon
                    else
                    {
                        // On affiche le gagnant
                        statusStrip1.Items[0].Text = unePartie.getPlateau().getJoueurGagnant().getNom() + " a gagné!";
                    }
                }

            }
        }

        private void creationPictureBoxSelections()
        {
            // Création de l'image de selection
            Bitmap bmJoueur1 = new Bitmap(pictureBoxSelectionJoueur1.Width, pictureBoxSelectionJoueur1.Height);
            Bitmap bmJoueur2 = new Bitmap(pictureBoxSelectionJoueur1.Width, pictureBoxSelectionJoueur1.Height);

            Graphics gJoueur1 = Graphics.FromImage(bmJoueur1);
            Graphics gJoueur2 = Graphics.FromImage(bmJoueur2);


            // Création du crayon
            Pen penJoueur1 = new Pen(unePartie.getPlateau().Joueur1.Couleur);
            Pen penJoueur2 = new Pen(unePartie.getPlateau().Joueur2.Couleur);

            // Dessine la selection
            gJoueur1.DrawRectangle(penJoueur1, pictureBoxSelectionJoueur1.Margin.All, pictureBoxSelectionJoueur1.Margin.All,
                pictureBoxSelectionJoueur1.Width - pictureBoxSelectionJoueur1.Margin.All - 1,
                pictureBoxSelectionJoueur1.Height - pictureBoxSelectionJoueur1.Margin.All - 1);

            gJoueur2.DrawRectangle(penJoueur2, pictureBoxSelectionJoueur1.Margin.All, pictureBoxSelectionJoueur1.Margin.All,
                            pictureBoxSelectionJoueur1.Width - pictureBoxSelectionJoueur1.Margin.All - 1,
                            pictureBoxSelectionJoueur1.Height - pictureBoxSelectionJoueur1.Margin.All - 1);

            // Attribue l'image de selection aux pictures box de selections
            pictureBoxSelectionJoueur1.Image = bmJoueur1;
            pictureBoxSelectionJoueur2.Image = bmJoueur2;


        }

        private PictureBox creationPions(Color couleur)
        {
            PictureBox PBPion = new PictureBox();


            // Création d'une bitmap
            Bitmap bm = new Bitmap(32, 32);
            // Gration d'un objet graphics pour dessiner dessus
            Graphics g = Graphics.FromImage(bm);
            // Créatin d'un pinceau
            //Brush b = new SolidBrush(couleur);
            LinearGradientBrush b = new LinearGradientBrush(new Rectangle(0, 0, 40, 30), couleur, Color.White, -45, false);

            g.FillEllipse(b, 0, 0, bm.Size.Width, bm.Size.Height);

            PBPion.Image = bm;
            PBPion.SizeMode = PictureBoxSizeMode.StretchImage;

            return PBPion;

        }

        private void btnColonne_Click(object sender, EventArgs e)
        {
            Point positionPion;         // Position du pion qui a été ajouté
            Point positionPionTableLayout;  // Position du pion dans le table layout
            PictureBox PBPion;          // Picture box du pion a ajouter
            // Si la partie est lancée
            if (unePartie != null)
            {
                    int ColonneJouee = 0;
                    Joueur joueurEnCours = unePartie.getJoueurEnCours();
                    switch (((Button)sender).Name.ToString())
                    {
                        case "button1":
                            ColonneJouee = 1;
                            break;
                        case "button2":
                            ColonneJouee = 2;
                            break;
                        case "button3":
                            ColonneJouee = 3;
                            break;
                        case "button4":
                            ColonneJouee = 4;
                            break;
                        case "button5":
                            ColonneJouee = 5;
                            break;
                        case "button6":
                            ColonneJouee = 6;
                            break;
                        case "button7":
                            ColonneJouee = 7;
                            break;
                        default:
                            throw new Exception("pb de boutons");
                    }

                    // Joue la colonne
                    positionPion = joueurEnCours.jouer(unePartie.getPlateau(), ColonneJouee);

                    // Si le pion a été ajouté correctement sur le plateau
                    if (positionPion.X != 0 && positionPion.Y != 0)
                    {
                        // Crée un nouveau pion a ajouter
                        PBPion = creationPions(joueurEnCours.Couleur);

                        // Calcule les position dans le table layout
                        positionPionTableLayout = new Point((positionPion.Y - 1), (MyPlateau.nbLignes - 1) - (positionPion.X - 1));

                        // Ajoute le pion au tableLayout
                        tableLayoutPanel1.Controls.Add(PBPion, positionPionTableLayout.X, positionPionTableLayout.Y);

                        // Si la partie n'est pas finie
                        if (!verifierFinPartie())
                        {
                            // Passe au joueur suivant
                            unePartie.PasserJoueurSuivant();

                            //On regarde si l'abilité a été utiliser au dernier tour, si oui on repare
                            if (unePartie.getJoueurEnCours().cd == true)
                            {
                                button1.Enabled = true;
                                button2.Enabled = true;
                                button3.Enabled = true;
                                button4.Enabled = true;
                                button5.Enabled = true;
                                button6.Enabled = true;
                                button7.Enabled = true;

                                unePartie.getJoueurEnCours().cd = false;
                            }

                        }
                        // Sinon on affiche un message de félicitations 
                        else
                        {
                            //Disable les buttons pour empecher les joueurs de continuer a mettre des jetons
                            button1.Enabled = false;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            button4.Enabled = false;
                            button5.Enabled = false;
                            button6.Enabled = false;
                            button7.Enabled = false;

                            // Si c'est un match nul
                            if (unePartie.getPlateau().getMatchNul())
                            {
                                MessageBox.Show("Vous avez fait un match nul!");
                            }
                            else
                            {
                                int y;
                                MessageBox.Show(joueurEnCours.getNom() + " a gagné");

                                //a joute la vicotire au total
                                joueurEnCours.setScore();
                                y = joueurEnCours.getNum();

                                //affiche la victoire au bon joueur
                                if (y == 1)
                                {
                                    label2.Text = joueurEnCours.getScore().ToString();
                                }
                                else
                                {
                                    label4.Text = joueurEnCours.getScore().ToString();
                                }


                            }
                        }

                    }

                

                MettreAJourFenetre();

            } 
        }

        public static DialogResult InputBox(string title, string promptText/*, ref string value*/)
        {
            Form form = new Form();
            Label label = new Label();
            //TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            //textBox.Text = value;

            buttonOk.Text = "Mêmes joueurs";
            buttonCancel.Text = "Nouveaux Joueurs";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            /*textBox.SetBounds(12, 36, 372, 20);*/
            buttonOk.SetBounds(170, 72, 115, 23);
            buttonCancel.SetBounds(250, 72, 115, 23);

            label.AutoSize = true;
            //textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, /*textBox,*/ buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            //value = textBox.Text;
            return dialogResult;
        }

        private void boutonNouvellePartie_Click(object sender, EventArgs e)
        {
            if (InputBox("Nouvelle Partie", "Voulez-vous effectuer une nouvelle partie avec de nouveaux joueur ou avec les même joueurs?") == DialogResult.OK)
            {
                refaireUnePartie();
            }
            else
            {
                creerNouvellePartie();
            }  
        }
    }
}
