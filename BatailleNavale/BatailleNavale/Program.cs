using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BatailleNavale
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialiser
            Init();
            string nickname = Console.ReadLine();
            Console.WriteLine("");
            Joueur humain = new Joueur(nickname);
            Joueur ordinateur = new Joueur();

            //Placement bateau du Joueur + Selection carte pour IA
            humain.deployement();
            //Début de game (tirage au sort + tour par tour)
            Combat fight = new Combat(humain, ordinateur);
            while (humain.lifeRemaining > 1 || ordinateur.lifeRemaining > 1)
            {
                if (fight.tirage == true) {
                    //fight.Tir(ordinateur);
                    fight.Tir(humain);
                    humain.displayMap();
                    Thread.Sleep(250);
                    Console.Clear();
                }
                else
                {
                    fight.Tir(humain);
                    //fight.Tir(ordinateur);
                    humain.displayMap();
                    Thread.Sleep(250);
                    Console.Clear();
                }
            }
            //Vérification de qui a gagné
            if (ordinateur.lifeRemaining == 0) Console.WriteLine("Vous etes le VAINQUER ! CONGRATULATION !");
            else Console.WriteLine("Vous etes PERDANT ! SHAME ON YOU !");
            Console.WriteLine("Appuyez sur une touche pour quiter !");
            Console.ReadLine();
        }

        public static void Init()
        {
            Console.WriteLine("Bonjour et bienvenue dans la Bataille Navale Human Vs IA !");
            Console.WriteLine("Get ready 4 ze battle!");
            Console.WriteLine("");
            Console.Write("Entre ici ton pseudo : ");
        }
    }
}
