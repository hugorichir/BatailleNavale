using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BatailleNavale
{
    class Joueur
    {
        int lifeRemaining;
        bool isHuman;
        public string name;
        public List<Bateau> floteInnactive;
        Map carte;

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
            carte.afficherCarte(carte.TableauValeur, this);
        }

        public void deployement()
        {
            int x,y;
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
                            floteInnactive.RemoveAt(compteur);
                            //Choix des coordonnées et du sens
                            Console.WriteLine("Choisisez les coordonnées {Format = x,y} : ");
                            Console.Write("x : "); x = int.Parse(Console.ReadLine()); Console.Write("y : "); y = int.Parse(Console.ReadLine());
                            Console.Write("Choisisez le sens du bateau [le point d'ancrage est en haut à gauche] (1 = vertical / 0 = horizontal) : "); choixSens = int.Parse(Console.ReadLine());
                            carte.tableauValeur[x, y] = 1;
                            compteur = -1;
                            break;
                        }
                    }
                }
                Thread.Sleep(500);
                Console.Clear();
            }
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
