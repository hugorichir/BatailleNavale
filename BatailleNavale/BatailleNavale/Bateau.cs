using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    class Bateau
    {
        public int size;
        public bool rotation; //True = Verticale & False = Horizontal
        public string name;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        public bool Sens
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Bateau(int lenght, bool sens)
        {
            size = lenght;
            rotation = sens;
            switch (lenght)
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
            }
        }

        public Bateau()
        {
            //Constructeur Vide
            size = 0;
        }
    }
}
