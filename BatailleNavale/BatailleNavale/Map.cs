using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Map
    {
        const int size = 10;
        public int[,] tableauVide = {
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0,0,0,0} };

        public Bateau[,] tableauBateauVide = {
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null}};

        public Bateau[,] tableau1 = {
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,new Bateau(2, true),null,null,null,null,null,null,null,new Bateau(5, true)},
                    {null,null,null,new Bateau(1, true),null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,null,new Bateau(2, false),null,null,null,new Bateau(4, true),null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,null,new Bateau(3, false),null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null},
                    {null,new Bateau(1, true),null,null,null,null,null,null,null,null},
                    {null,null,null,null,null,null,null,null,null,null}};

        public int[,] tableauValeur;

        public int[,] TableauValeur
        {
            get { return tableauValeur; }
            set { tableauValeur = value; }
        }
 
        public Bateau[,] TableauBateau
        {
            get { return tableauBateauVide; }
            set { tableauBateauVide = value; }
        }

        public Map(Joueur target)
        {
            //Choix et attribution de la carte selon type de joueur
            if (target.Gender == true)
            {
                tableauValeur = new int[size, size];
                tableauValeur = tableauVide;
            }
            else
            {
                tableauValeur = new int[size, size];
                tableauValeur = tableauVide;
                int choix;
                var rand = new Random();
                //Choix aléatoire du niveau
                choix = rand.Next(1, 6);
                choix = 1;
                //Atribution du niveau et traduction de bateau à chiffre
                switch (choix = 1)
                {
                    case 1:
                        attributionCarte(tableau1);
                        break;
                }
            }
        }
        private void placerBateau(int taille, int u, int v, int rotation)
        {
            for (int i = 0; i < taille; i++)
            {
                if (rotation == 1) //Horizontal
                {
                    tableauValeur[u+i, v] = taille;
                }
                if (rotation == 0) //Vertical
                {
                    tableauValeur[u, i+v] = taille;
                }
            }
        }
        public bool verifierBateau(int taille, int u, int v, int rotation)
        {
            bool possible = true;
            for (int i = 0; i < taille; i++)
            {
                try
                {
                    if (tableauValeur[u + i, v] != 0)
                    {
                        possible = false;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    possible = false;
                    break;
                }
                if (rotation == 0) //Vertical
                {
                    try
                    {
                        if (tableauValeur[u, v + i] != 0)
                        {
                            possible = false;
                        }
                    }catch (IndexOutOfRangeException e)
                    {
                        possible = false;
                        break;
                    }
                }
            }
            if (possible == true) placerBateau(taille, u, v, rotation);
            else Console.WriteLine("Erreur de placement !");
            return possible;
        }
        private void attributionCarte(Bateau[,] table) //Pour l'IA sans vérification
        {
            int rotation = 0;
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    if(table[j,i] != null)
                    {
                        rotation = table[j, i].Size;
                        if (table[j, i].Sens == true) //Vertical
                        {
                            for (int z = 0; z < rotation; z++) { tableauValeur[j + z, i] = rotation; }
                        }
                        else if (table[j, i].Sens == false)
                        {
                            for (int z = 0; z < rotation; z++) { tableauValeur[j, i + z] = rotation; }
                        }//Horizontal
                    }
                }
            }
        }

        public void afficherCarte(int[,] table, Joueur target)
        {
            Console.WriteLine("Carte actuelle du joueur " + target.name + " :");
            Console.WriteLine("");
            Console.WriteLine("    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("");
            for (int j = 0; j < size; j++)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(j);
                Console.Write("  |");
                for (int i = 0; i < size; i++)
                {
                    Console.Write(table[j, i] + "|");
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine(" ");
        }
    }
}
