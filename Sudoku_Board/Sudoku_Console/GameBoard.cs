using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sudoku_Board
{
    class GameBoard
    {
        static void Main(string[] args)
        {
            // use File Handler to load save files
            FileHandler fh = new FileHandler();
            string filename = "board.txt";
            PuzzleGrid grid;
            if (File.Exists(filename))
            {
                grid = fh.OpenFile("filename");
            } else
            {
                grid = new PuzzleGrid(); // fix
            }

            do
            {
                //File.Open("filename", GameBoard);
                PrintBoard(grid);
                Console.ReadKey();
            } while (true);
            // control code:
            // while the game is not over:
            //    print out the board
            //    let the user type in a row/column and number
            //    change the board
            //    check if they won the game

        }

        static void PrintBoard(PuzzleGrid grid)
        {
            int numRows = grid.Grid.GetLength(0);
            int numColumns = grid.Grid.GetLength(1);
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    Console.Write(grid.Grid[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
