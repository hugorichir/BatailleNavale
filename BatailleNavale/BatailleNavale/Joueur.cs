using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BatailleNavale
{
    class Joueur
    {
        public int lifeRemaining;
        bool isHuman;
        public string name;
        public List<Bateau> floteInnactive;
        public Map carte;
        //Variables pour l'IA avancé
        public bool touchedLast;
        public int caseAutour;
        public int xLast, yLast;

        public bool Gender
        {
            get { return isHuman; }
            set { isHuman = value; }
        }

        public Joueur(string nom)
        {
            lifeRemaining = 18;
            isHuman = true;
            name = nom;
            touchedLast = false;
            caseAutour = 0;
            carte = new Map(this);
            floteInnactive = new List<Bateau>();
            floteInnactive.Add(new Bateau(5, true));
            floteInnactive.Add(new Bateau(4, true));
            floteInnactive.Add(new Bateau(3, true));
            floteInnactive.Add(new Bateau(2, true));
            floteInnactive.Add(new Bateau(2, true));
            floteInnactive.Add(new Bateau(1, true));
            floteInnactive.Add(new Bateau(1, true));
        }
        public Joueur()
        {
            lifeRemaining = 18;
            isHuman = false;
            name = "Computer";
            carte = new Map(this);
        }

        public void displayMap()
        {
            if (isHuman == true) carte.afficherCarte(carte.TableauValeur, this);
            else carte.afficherCarte(carte.TableauFront, this);
        }

        //DEPLOYEMENT
        public void deployement()
        {
            int x ,y;
            int choixSens;
            int compteur = -1;
            int choixBato;
            Console.WriteLine("Deployons à présent votre flotte !");
            while (floteInnactive.Count() != 0)
            {
                displayMap();
                displayListBato();
                //Choix du type à deployer
                Console.Write("Choisisez un type de bateau {1,2,3,4,5} : ");
                choixBato = int.Parse(Console.ReadLine());
                //Vérifier si bateau est disponible
                if (bateauDispo(choixBato) == false)
                {
                    Console.WriteLine("Bateau Indisponible");
                    do { Console.Write("Try again :"); choixBato = int.Parse(Console.ReadLine()); } while (bateauDispo(choixBato) == false);
                }
                //Si il est dispo, le supprimer de la liste, l'ajouter dans le tableau
                if (bateauDispo(choixBato) == true)
                {
                    foreach (Bateau bato in floteInnactive)
                    {
                        compteur++;
                        if (bato.size == choixBato)
                        {
                            //Choix des coordonnées et du sens
                            y = coordInput("x"); x = coordInput("y"); choixSens = sensInput("Sens");
                            //Vérification si ça passe sur la carte
                            if(carte.verifierBateau(bato.size, x,y,choixSens) == true)
                            {
                                floteInnactive.RemoveAt(compteur);
                            }
                            compteur = -1;
                            break;
                        }
                    }
                }
                //Thread.Sleep(500);
                Console.Clear();
            }
        }

        private int sensInput(string thing)
        {
            int w;
            Console.Write("Choisisez le sens du bateau [le point d'ancrage est en haut à gauche] (0 = horizontal / 1 = vertical) : ");
            try
            {
                w = int.Parse(Console.ReadLine());
            }catch (FormatException e)
            {
                w = int.Parse(Console.ReadLine());
            }
            while (w < 0 || w > 1)
            {
                Console.Write("Valeur impossible ! ");
                Console.Write(thing + " : ");
                w = int.Parse(Console.ReadLine());
            }
            return w;
        }
        public int coordInput(string thing)
        {
            int v;
            Console.Write(thing + " : ");
            try
            {
                v = int.Parse(Console.ReadLine());
            }catch (FormatException e)
            {
                v = int.Parse(Console.ReadLine());
            }
            while(v < 0 || v > 9)
            {
                Console.Write("Valeur impossible ! ");
                Console.Write(thing + " : ");
                v = int.Parse(Console.ReadLine());
            }
            return v;
        }
        private void displayListBato()
        {
            Console.WriteLine("Il vous reste :");
            Console.WriteLine(" ");
            foreach (Bateau bato in floteInnactive)
            {
                Console.WriteLine("(" + bato.size + ") - " + bato.name);
            }
            Console.WriteLine(" ");
        }
        private bool bateauDispo(int numero)
        {
            bool result = false;
            foreach (Bateau bato in floteInnactive)
            {
                if (bato.size == numero) result = true;
            }
            return result;
        }
    }
}
