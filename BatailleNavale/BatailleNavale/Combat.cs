using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

                //AMELIORER l'IA --> Ne peux pas tirer sur case déjà tiré

                if (target.carte.tableauValeur[x, y] > 0 && target.carte.tableauValeur[x, y] < 6)
                {
                    Console.Clear();
                    Console.Write("Touché ! ");
                    Console.WriteLine("L'IA à touché votre : " + kindOfBoat(target.carte.tableauValeur[x, y]));
                    target.carte.tableauValeur[x, y] = 8;
                    target.lifeRemaining--;
                    //Console.WriteLine("DEBUG : " + x + "/" + y + " --> " + target.carte.tableauValeur[x, y] + " Vie : " + target.lifeRemaining);
                }
                else
                {
                    if(target.carte.tableauValeur[x, y] == 0) target.carte.tableauValeur[x, y] = 7;
                    Console.Clear();
                    Console.WriteLine("Plouff l'IA a loupé quelle grosse merde !");
                }
            }
            else //Coté Humain
            {
                Console.WriteLine("Choisissez où vous voulez tirer ! ");
                y = target.coordInput("x"); x = target.coordInput("y");
                while(target.carte.TableauValeur[x, y] > 6)
                {
                    Console.WriteLine("Vous avez déjà tiré ici :");
                    y = target.coordInput("x"); x = target.coordInput("y");
                }

                if (target.carte.TableauValeur[x,y] != 0 && target.carte.TableauValeur[x, y] != 7)
                {
                    Console.Clear();
                    Console.WriteLine("Touché ! En plein sur son " + kindOfBoat(target.carte.TableauValeur[x, y]));
                    target.carte.TableauVide[x, y] = 8;
                    target.carte.TableauFront[x, y] = 8;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Raté ! Plouufff mon gars, dans ta...l'eau");
                    target.carte.TableauVide[x, y] = 7;
                    target.carte.TableauFront[x, y] = 7;
                }
            }
        }

        private string kindOfBoat(int value)
        {
            string name;
            switch (value)
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
            return name;
        }

        public void NextStep()
        {
            Console.WriteLine("Appuyez sur entrée pour passer à l'étape suivante");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
