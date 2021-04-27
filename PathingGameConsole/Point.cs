using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathingGameConsole
{
    public class Point
    {
        private bool marked;
        private bool player;

        private bool canUP;
        private bool canDOWN;
        private bool canLEFT;
        private bool canRIGHT;

        public Point() 
        {
            marked = false;
            player = false;

            canUP = true;
            canDOWN= true;
            canLEFT = true;
            canRIGHT = true;
        }

        public bool Marked
        {
            get { return this.marked; }
            set { this.marked = value; }
        }

        public bool Player
        {
            get { return this.player; }
            set { this.player = value; }
        }

        public bool UP
        {
            get { return this.canUP; }
            set { this.canUP = value; }
        }
        public bool DOWN
        {
            get { return this.canDOWN; }
            set { this.canDOWN = value; }
        }
        public bool LEFT
        {
            get { return this.canLEFT; }
            set { this.canLEFT = value; }
        }
        public bool RIGHT
        {
            get { return this.canRIGHT; }
            set { this.canRIGHT = value; }
        }

        public int nDirections
        {
            get 
            {
                int returnValue = 0;
                if (this.LEFT) { returnValue++;  }
                if (this.UP) { returnValue++; }
                if (this.RIGHT) { returnValue++; }
                if (this.DOWN) { returnValue++; }
                return returnValue; 
            }
        }

        public override string ToString()
        {
            if (this.player)
            {
                return "#";
            }
            if (!this.Marked)
            {
                return "\u2588";
            }
            else
            {
                return " ";
            }
        }
    }
}
