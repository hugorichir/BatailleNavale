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
            //Début de game (pile ou face pour savoir qui commence)
            //Phase de tire tour par tour until plus de vie
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
