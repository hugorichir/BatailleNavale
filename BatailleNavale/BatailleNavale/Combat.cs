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
        //TIR durant le COMBAT
        public void Tir(Joueur target)
        {
            int x, y;
            var rand = new Random();
            //Coté IA
            if (target.name != "Computer")
            {
                do
                {
                    x = rand.Next(0, 10);
                    y = rand.Next(0, 10);
                    //IA un peu avancé, tendance à tirer autour des cases touchés
                    if (target.touchedLast == true && target.caseAutour < 4)
                    {
                        switch (target.caseAutour)
                        {
                            case 0:
                                //HAUT
                                if(target.yLast < 10) { 
                                x = target.xLast;
                                y = target.yLast+1;
                                }
                                target.caseAutour++;
                                break;
                            case 1:
                                //BAS
                                if (target.yLast > 0)
                                {
                                    x = target.xLast;
                                    y = target.yLast - 1;
                                }
                                target.caseAutour++;
                                break;
                            case 2:
                                //GAUCHE
                                if (target.xLast > 0)
                                {
                                    x = target.xLast - 1;
                                    y = target.yLast;
                                }
                                target.caseAutour++;
                                break;
                            case 3:
                                //DROITE
                                if (target.xLast < 10)
                                {
                                    x = target.xLast + 1;
                                    y = target.yLast;
                                }
                                target.caseAutour++;
                                break;
                            default:
                                x = target.xLast;
                                y = target.yLast;
                                break;
                        }
                    }
                    else
                    {
                        target.caseAutour = 0;
                    }
                } while (target.carte.tableauValeur[x, y] > 6);
                //CHOIX DE LA CASE SUR LAQUELLE TIRER
                if (target.carte.tableauValeur[x, y] > 0 && target.carte.tableauValeur[x, y] < 6)
                {//TOUCHÉ 
                    Console.Clear();
                    Console.Write("Touché ! ");
                    Console.WriteLine("L'IA à touché votre : " + kindOfBoat(target.carte.tableauValeur[x, y]));
                    target.carte.tableauValeur[x, y] = 8;
                    target.lifeRemaining--;
                    target.touchedLast = true;
                    target.xLast = x;
                    target.yLast = y;
                    //Console.WriteLine("DEBUG : " + x + "/" + y + " --> " + target.carte.tableauValeur[x, y] + " Vie : " + target.lifeRemaining);
                }
                else
                {//FAIL
                    if(target.carte.tableauValeur[x, y] == 0) target.carte.tableauValeur[x, y] = 7;
                    Console.Clear();
                    Console.WriteLine("Plouff l'IA a loupé quelle grosse merde !");
                    target.touchedLast = false;
                }
            }
            else //Coté Humain
            {
                //CHOIX DE LA CASE SUR LAQUELLE TIRER
                Console.WriteLine("Choisissez où vous voulez tirer ! ");
                y = target.coordInput("x"); x = target.coordInput("y");
                while(target.carte.TableauValeur[x, y] > 6)
                {
                    Console.WriteLine("Vous avez déjà tiré ici :");
                    y = target.coordInput("x"); x = target.coordInput("y");
                }
                //TOUCHÉ 
                if (target.carte.TableauValeur[x,y] != 0 && target.carte.TableauValeur[x, y] != 7)
                {
                    Console.Clear();
                    Console.WriteLine("Touché ! En plein sur son " + kindOfBoat(target.carte.TableauValeur[x, y]));
                    target.carte.TableauVide[x, y] = 8;
                    target.carte.TableauFront[x, y] = 8;
                    target.lifeRemaining--;
                }
                else
                { //FAIL
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
        //PAUSE POUR L'HUMAIN
        public void NextStep()
        {
            Console.WriteLine("Appuyez sur entrée pour passer à l'étape suivante");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
