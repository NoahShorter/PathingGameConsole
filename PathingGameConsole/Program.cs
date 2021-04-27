using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathingGameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("This is the best test");
            Board grid = new Board(50, 50);
            grid.GeneratePath(0, 0, 25, 25);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(grid);
            ConsoleKeyInfo input;
            while ((input = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                if (input.Key == ConsoleKey.W)
                {
                    grid.movePlayer(1);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(grid);
                }
                if (input.Key == ConsoleKey.A)
                {
                    grid.movePlayer(0);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(grid);
                }
                if (input.Key == ConsoleKey.S)
                {
                    grid.movePlayer(3);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(grid);
                }
                if (input.Key == ConsoleKey.D)
                {
                    grid.movePlayer(2);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(grid);
                }
            }
        }
    }
}
