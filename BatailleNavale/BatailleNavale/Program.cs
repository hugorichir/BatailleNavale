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
            Console.Clear();
            Joueur humain = new Joueur(nickname);
            Joueur ordinateur = new Joueur();

            //Placement bateau du Joueur + Selection carte pour IA
            humain.deployement();
            //Début de game (tirage au sort + tour par tour)
            Combat fight = new Combat(humain, ordinateur);
            fight.NextStep();
            while (humain.lifeRemaining > 0 && ordinateur.lifeRemaining > 0)
            {
                Console.Clear();
                if (fight.tirage == true) {
                    PhaseTir(true, humain, ordinateur, fight);
                    PhaseTir(false, humain, ordinateur, fight);
                }
                else
                {
                    PhaseTir(false, humain, ordinateur, fight);
                    PhaseTir(true, humain, ordinateur, fight);
                }
            }
            //Vérification de qui a gagné
            Console.WriteLine(" ");
            if (ordinateur.lifeRemaining == 0) Console.WriteLine("Vous etes le VAINQUER ! CONGRATULATION !");
            else Console.WriteLine("Vous etes PERDANT ! SHAME ON YOU !");
            Console.WriteLine("Appuyez sur entrée pour quiter !");
            Console.ReadLine();
        }

        public static void Init()
        {
            Console.WriteLine("Bonjour et bienvenue dans la Bataille Navale Human Vs IA !");
            Console.WriteLine("Get ready 4 ze battle!");
            Console.WriteLine("");
            Console.Write("Entre ici ton pseudo : ");
        }

        public static void PhaseTir(bool vivant, Joueur jPlayer, Joueur jOrdi, Combat game)
        {
            if(vivant == true)
            {
                //Tir Humain
                jOrdi.displayMap();
                game.Tir(jOrdi);
                jOrdi.displayMap();
                game.NextStep();
            }
            else
            {
                //Tir IA
                game.Tir(jPlayer);
                jPlayer.displayMap();
                game.NextStep();
            }
        }
    }
}
