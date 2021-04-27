using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathingGameConsole
{
    public class Board : Grid
    {
        private LinkedList<int[]> mPath;
        private int[] playerLocation;
        private bool masking;

        public Board(int sizeX, int sizeY, bool masking) :
            base(sizeX, sizeY, masking)
        {
            mPath = new LinkedList<int[]>();
            mGrid[0, 0].Player = true;
            playerLocation = new int[] { 0, 0 };
            this.masking = masking;
            if (this.masking)
                unMaskAroundPoint(playerLocation);
        }

        private void unMaskAroundPoint(int[] point)
        {
            if (pointExsist(point))
            {
                int x = point[0];
                int y = point[1];

                mGrid[x , y].Masked = false;

                if (pointExsist(new int[] { x - 1, y }))
                    mGrid[x - 1, y].Masked = false;

                if (pointExsist(new int[] { x - 1, y + 1 }))
                    mGrid[x - 1, y + 1].Masked = false;

                if (pointExsist(new int[] { x, y + 1 }))
                    mGrid[x, y + 1].Masked = false;

                if (pointExsist(new int[] { x + 1, y + 1 }))
                    mGrid[x + 1, y + 1].Masked = false;

                if (pointExsist(new int[] { x + 1, y }))
                    mGrid[x + 1, y].Masked = false;

                if (pointExsist(new int[] { x + 1, y - 1 }))
                    mGrid[x + 1, y - 1].Masked = false;

                if (pointExsist(new int[] { x, y - 1 }))
                    mGrid[x, y - 1].Masked = false;

                if (pointExsist(new int[] { x - 1, y - 1}))
                    mGrid[x - 1, y - 1].Masked = false;
            }
        }

        private void maskAroundPoint(int[] point)
        {
            if (pointExsist(point))
            {
                int x = point[0];
                int y = point[1];

                mGrid[x , y].Masked = true;

                if (pointExsist(new int[] { x - 1, y }))
                    mGrid[x - 1, y].Masked = true;

                if (pointExsist(new int[] { x - 1, y + 1 }))
                    mGrid[x - 1, y + 1].Masked = true;

                if (pointExsist(new int[] { x, y + 1 }))
                    mGrid[x, y + 1].Masked = true;

                if (pointExsist(new int[] { x + 1, y + 1 }))
                    mGrid[x + 1, y + 1].Masked = true;

                if (pointExsist(new int[] { x + 1, y }))
                    mGrid[x + 1, y].Masked = true;

                if (pointExsist(new int[] { x + 1, y - 1 }))
                    mGrid[x + 1, y - 1].Masked = true;

                if (pointExsist(new int[] { x, y - 1 }))
                    mGrid[x, y - 1].Masked = true;

                if (pointExsist(new int[] { x - 1, y - 1 }))
                    mGrid[x - 1, y - 1].Masked = true;
            }
        }

        public void movePlayer(int direction)
        {
            switch (direction) 
            {
                case 0:
                    if(pointExsist(goLEFT(playerLocation)) && mGrid[goLEFT(playerLocation)[0], goLEFT(playerLocation)[1]].Marked)
                    {
                        mGrid[goLEFT(playerLocation)[0], goLEFT(playerLocation)[1]].Player = true;
                        mGrid[playerLocation[0], playerLocation[1]].Player = false;
                        if (masking) 
                            maskAroundPoint(playerLocation);
                        playerLocation = goLEFT(playerLocation);
                        if (masking)
                            unMaskAroundPoint(playerLocation);
                    }
                    break;
                case 1:
                    if (pointExsist(goUP(playerLocation)) && mGrid[goUP(playerLocation)[0], goUP(playerLocation)[1]].Marked)
                    {
                        mGrid[goUP(playerLocation)[0], goUP(playerLocation)[1]].Player = true;
                        mGrid[playerLocation[0], playerLocation[1]].Player = false;
                        if (masking)
                            maskAroundPoint(playerLocation);
                        playerLocation = goUP(playerLocation);
                        if (masking)
                            unMaskAroundPoint(playerLocation);
                    }
                    break;
                case 2:
                    if (pointExsist(goRIGHT(playerLocation)) && mGrid[goRIGHT(playerLocation)[0], goRIGHT(playerLocation)[1]].Marked)
                    {
                        mGrid[goRIGHT(playerLocation)[0], goRIGHT(playerLocation)[1]].Player = true;
                        mGrid[playerLocation[0], playerLocation[1]].Player = false;
                        if (masking)
                            maskAroundPoint(playerLocation);
                        playerLocation = goRIGHT(playerLocation);
                        if (masking)
                            unMaskAroundPoint(playerLocation);
                    }
                    break;
                case 3:
                    if (pointExsist(goDOWN(playerLocation)) && mGrid[goDOWN(playerLocation)[0], goDOWN(playerLocation)[1]].Marked)
                    {
                        mGrid[goDOWN(playerLocation)[0], goDOWN(playerLocation)[1]].Player = true;
                        mGrid[playerLocation[0], playerLocation[1]].Player = false;
                        if (masking)
                            maskAroundPoint(playerLocation);
                        playerLocation = goDOWN(playerLocation);
                        if (masking)
                            unMaskAroundPoint(playerLocation);
                    }
                    break;
            }
        }

        public void GeneratePathWithOverlap(int startX, int startY, int endX, int endY)
        {
            if ((startX > -1 && startX < getXLength()) && (startY > -1 && startY < getYLength()) &&
                (endX > -1 && endX < getXLength()) && (endY > -1 && endY < getYLength()))
            {
                // Create random number generator
                Random rd = new Random();

                //Create point holder
                int[] currentPoint = new int[] { startX, startY };

                mPath.Clear();

                //Add starting point to path
                mPath.Append(currentPoint);

                //Create stuck counter
                int stuckCounter = 0;

                //Create switch marker
                bool switched = true;

                //Create array of directions
                int[] possibleDirections = { 1, 2, 3, 4 };

                int directionIndex = 0;

                //loop until currentPoint is end point
                while (currentPoint[0] != endX || currentPoint[1] != endY)
                {

                    // randomly choose direction
                    directionIndex = rd.Next(0, 4);

                    //Create nextPoint
                    int[] nextPoint = new int[2] { -1, -1 };

                    //set next point
                    switch (possibleDirections[directionIndex])
                    {
                        case 1:
                            if (mGrid[currentPoint[0], currentPoint[1]].LEFT)
                                nextPoint = goLEFT(currentPoint);
                            break;
                        case 2:
                            if (mGrid[currentPoint[0], currentPoint[1]].UP)
                                nextPoint = goUP(currentPoint);
                            break;
                        case 3:
                            if (mGrid[currentPoint[0], currentPoint[1]].RIGHT)
                                nextPoint = goRIGHT(currentPoint);
                            break;
                        case 4:
                            if (mGrid[currentPoint[0], currentPoint[1]].DOWN)
                                nextPoint = goDOWN(currentPoint);
                            break;
                    }


                    // if direction has been viewed or neighbor to viewed block or out of bounds
                    if (pointExsist(nextPoint))
                    {
                        //Mark current position
                        markPos(currentPoint);

                        //add current point to path
                        mPath.AddLast(currentPoint);

                        //set current pos to nextpos
                        currentPoint = nextPoint;

                        //we moved forward, set switched to true
                        switched = true;

                        //System.Threading.Thread.Sleep(15);
                        Console.SetCursorPosition(0, 0);
                        //Console.Clear();
                        Console.Write(this);
                    }
                    else
                    {
                        //This direction did not work, switched is false.
                        switched = false;

                        switch (possibleDirections[directionIndex])
                        {
                            case 1:
                                mGrid[currentPoint[0], currentPoint[1]].LEFT = false;
                                break;
                            case 2:
                                mGrid[currentPoint[0], currentPoint[1]].UP = false;
                                break;
                            case 3:
                                mGrid[currentPoint[0], currentPoint[1]].RIGHT = false;
                                break;
                            case 4:
                                mGrid[currentPoint[0], currentPoint[1]].DOWN = false;
                                break;
                        }
                    }

                    if (mGrid[currentPoint[0], currentPoint[1]].nDirections == 0 &&
                        (currentPoint[0] != endX || currentPoint[1] != endY))
                    {
                        //if so, move back in path once and try again
                        int[] tempPoint = mPath.Last();

                        int differenceX = currentPoint[0] - tempPoint[0];
                        int differenceY = currentPoint[1] - tempPoint[1];

                        if (differenceX == 1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].RIGHT = false;
                        }
                        else if (differenceX == -1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].LEFT = false;
                        }
                        else if (differenceY == 1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].UP = false;
                        }
                        else if (differenceY == -1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].DOWN = false;
                        }

                        currentPoint = mPath.Last();

                        mPath.RemoveLast();

                        unMarkPos(currentPoint);

                        //System.Threading.Thread.Sleep(15);
                        Console.SetCursorPosition(0, 0);
                        //Console.Clear();
                        Console.Write(this);

                    }

                }

                markPos(currentPoint);


            }
        }

        public void GeneratePath(int startX, int startY, int endX, int endY)
        {
            if((startX > -1 && startX < getXLength()) && (startY > -1 && startY < getYLength()) &&
                (endX > -1 && endX < getXLength()) && (endY > -1 && endY < getYLength()))
            {
                // Create random number generator
                Random rd = new Random();

                //Create point holder
                int[] currentPoint = new int[] { startX, startY };

                //Add starting point to path
                mPath.Append(currentPoint);

                //Create stuck counter
                int stuckCounter = 0;

                //Create switch marker
                bool switched = true;

                //Create array of directions
                int[] possibleDirections = { 1, 2, 3, 4 };

                int directionIndex = 0;

                //loop until currentPoint is end point
                while (currentPoint[0] != endX || currentPoint[1] != endY)
                {


                    //********* Go until stuck
                    while (mGrid[currentPoint[0], currentPoint[1]].nDirections != 0)
                    {
                        
                        // randomly choose direction
                        directionIndex = rd.Next(0, 4);

                        //Create nextPoint
                        int[] nextPoint  = new int[2] { -1, -1};

                        //set next point
                        switch (possibleDirections[directionIndex])
                        {
                            case 1:
                                if (mGrid[currentPoint[0], currentPoint[1]].LEFT)
                                    nextPoint = goLEFT(currentPoint);
                                break;
                            case 2:
                                if (mGrid[currentPoint[0], currentPoint[1]].UP)
                                    nextPoint = goUP(currentPoint);
                                break;
                            case 3:
                                if (mGrid[currentPoint[0], currentPoint[1]].RIGHT)
                                    nextPoint = goRIGHT(currentPoint);
                                break;
                            case 4:
                                if (mGrid[currentPoint[0], currentPoint[1]].DOWN)
                                    nextPoint = goDOWN(currentPoint);
                                break;
                        }


                        // if direction has been viewed or neighbor to viewed block or out of bounds
                        if (pointExsist(nextPoint) &&
                            !isMarked(goLEFT(nextPoint)) &&
                            !isMarked(goUP(nextPoint)) &&
                            !isMarked(goRIGHT(nextPoint)) &&
                            !isMarked(goDOWN(nextPoint)))
                        {
                            //Mark current position
                            markPos(currentPoint);

                            //add current point to path
                            mPath.AddLast(currentPoint);

                            //set current pos to nextpos
                            currentPoint = nextPoint;

                            //we moved forward, set switched to true
                            switched = true;

                            //System.Threading.Thread.Sleep(15);
                            //Console.SetCursorPosition(0, 0);
                            //Console.Clear();
                            //Console.Write(this);
                        }
                        else
                        {
                            //This direction did not work, switched is false.
                            switched = false;

                            switch (possibleDirections[directionIndex])
                            {
                                case 1:
                                    mGrid[currentPoint[0], currentPoint[1]].LEFT = false;
                                    break;
                                case 2:
                                    mGrid[currentPoint[0], currentPoint[1]].UP = false;
                                    break;
                                case 3:
                                    mGrid[currentPoint[0], currentPoint[1]].RIGHT = false;
                                    break;
                                case 4:
                                    mGrid[currentPoint[0], currentPoint[1]].DOWN = false;
                                    break;
                            }
                        }
                    }
                    //********** End Go until stuck

                    if(mGrid[currentPoint[0], currentPoint[1]].nDirections == 0 && 
                        (currentPoint[0] != endX || currentPoint[1] != endY))
                    {
                        //if so, move back in path once and try again
                        int[] tempPoint = mPath.Last();

                        int differenceX = currentPoint[0] - tempPoint[0];
                        int differenceY = currentPoint[1] - tempPoint[1];

                        if(differenceX == 1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].RIGHT = false;
                        }
                        else if(differenceX == -1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].LEFT = false;
                        }
                        else if(differenceY == 1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].UP = false;
                        }
                        else if(differenceY == -1)
                        {
                            mGrid[tempPoint[0], tempPoint[1]].DOWN = false;
                        }

                        currentPoint = mPath.Last();

                        mPath.RemoveLast();

                        unMarkPos(currentPoint);

                        //System.Threading.Thread.Sleep(15);
                        //Console.SetCursorPosition(0, 0);
                        //Console.Clear();
                        //Console.Write(this);

                    }

                }

                markPos(currentPoint);


            }
        }

        public void GenerateRandoms(int number)
        {
            Random rd = new Random();

            for(int ii = 0; ii < number; ++ii)
            {
                int x = rd.Next(0, getXLength());
                int y = rd.Next(0, getYLength());
                mGrid[x, y].Marked = true;
            }
        }

        public int[] goUP(int[] point)
        {
            return new int[] { point[0], point[1] + 1 };
        }

        public int[] goDOWN(int[] point)
        {
            return new int[] { point[0], point[1] - 1 };
        }

        public int[] goLEFT(int[] point)
        {
            return new int[] { point[0] - 1, point[1]};
        }

        public int[] goRIGHT(int[] point)
        {
            return new int[] { point[0] + 1, point[1]};
        }

        public void markPos(int[] point)
        {
            if (pointExsist(point))
            {
                mGrid[point[0], point[1]].Marked = true;
            }
        }

        public void unMarkPos(int[] point)
        {
            if (pointExsist(point))
            {
                mGrid[point[0], point[1]].Marked = false;
            }
        }

        public bool isMarked(int[] point)
        { 
            if(pointExsist(point))
            {
                return mGrid[point[0], point[1]].Marked;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            String returnValue = String.Empty;
            returnValue += "\u250c";
            for(int ii = 0; ii < getXLength(); ++ii)
            {
                returnValue += "\u2500";
            }
            returnValue += "\u2510\n";
            for (int jj = getYLength() - 1; jj >= 0; --jj)
            {
                returnValue += "\u2502";
                for (int ii = 0; ii < getXLength(); ++ii)
                {
                    returnValue += mGrid[ii, jj];
                }
                returnValue += "\u2502\n";
            }
            returnValue += "\u2514";
            for (int ii = 0; ii < getXLength(); ++ii)
            {
                returnValue += "\u2500";
            }
            returnValue += "\u2518\n";
            return returnValue;
        }
    }
}
