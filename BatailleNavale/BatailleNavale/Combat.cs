using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Combat
    {
        public bool tirage;
        public Combat(Joueur player1, Joueur player2)
        {
            Init();
        }
    
        private void Init()
        {
            Console.WriteLine("La phase de combat commence !!!");
            Console.Write("Allez-vous commencer ? La réponse est ");
            var rand = new Random();
            tirage = Convert.ToBoolean(rand.Next(0, 2));
            if (tirage == true) Console.WriteLine("Oui !");
            else Console.WriteLine("Non ! L'ordinateur commence !");
        }
    
        public void Tir(Joueur target)
        {
            int x, y;
            var rand = new Random();
            //Coté IA
            if(target.name != "Computer")
            {
                x = rand.Next(0, 10);
                y = rand.Next(0, 10);
                string name;

                Console.WriteLine("DEBUG : " + x + "/" + y + " --> " + target.carte.tableauValeur[x, y]);

                if (target.carte.tableauValeur[x, y] > 0)
                {
                    Console.Write("Touché ! ");
                    switch(target.carte.tableauValeur[x, y])
                    {
                        case 1:
                            name = "Submarine";
                            break;
                        case 2:
                            name = "Battleship";
                            break;
                        case 3:
                            name = "Cruiser";
                            break;
                        case 4:
                            name = "Destroyer";
                            break;
                        case 5:
                            name = "Aircraft Carrier";
                            break;
                        default:
                            name = "case déjà touché";
                            break;
                    }
                    Console.WriteLine("L'IA à touché votre : " + name);
                    target.carte.tableauValeur[x, y] = 8;
                    target.lifeRemaining--;
                }
                else
                {
                    Console.WriteLine("Plouffffff l'IA a loupé quelle grosse merde !");
                }
            }
            else //Coté Humain
            {
                Console.WriteLine("Vous ne pouvez rien faire car vous êtes PD!");
            }
        }

    }
}
