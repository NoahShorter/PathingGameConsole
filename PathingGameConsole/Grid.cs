using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathingGameConsole
{
    public class Grid
    {
        public Grid(int sizeX, int sizeY, bool masking)
        {
            mGrid = new Point[sizeX, sizeY];
            for(int ii = 0; ii < sizeX; ++ii)
            {
                for(int jj = 0; jj < sizeY; ++jj)
                {
                    mGrid[ii, jj] = new Point(masking);
                }
            }
        }

        public override string ToString()
        {
            String returnValue = String.Empty;
            for(int jj = getYLength() - 1; jj >= 0; --jj)
            {
                for (int ii = 0; ii < getXLength(); ++ii)
                {
                    returnValue += " " + mGrid[ii, jj];
                }
                returnValue += "\n";
            }
            return returnValue;
        }

        public int getXLength()
        {
            return mGrid.GetLength(0);
        }

        public int getYLength()
        {
            return mGrid.GetLength(1);
        }

        public bool pointExsist(int[] point)
        {
            int x = point[0];
            int y = point[1];
            return ((x >= 0 && x < getXLength()) && (y >= 0 && y < getYLength()));
        }

        protected Point[,] mGrid;

    }
}
