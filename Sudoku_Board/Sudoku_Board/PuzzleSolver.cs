using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Board
{
    class PuzzleSolver 
    {
        public PuzzleGrid SolutionGrid;
        int[] gridRow;
        bool[] list;
        PuzzleGrid[] final;
        int numSolns;
        bool stoplooking;
        int recursions;
        const int MaxDepth = 1000;

        public PuzzleSolver()
        {
            list = new bool[10];
            gridRow = new int[9];
            final = new PuzzleGrid[2];
        }

        private bool IsSolved(PuzzleGrid grid)
        {
            bool result = true;
            int r, c;
            r = 0;
            while (result == true && r < 9)
            {
                c = 0;
                while (result == true && c < 9)
                {
                    result = (result && grid.Grid[r, c] != 0);
                    c++;
                }
                r++;
            }
            return result;
        }

        private int FirstTrue()
        {
            int i = 1;
            int result = 0;
            while (result == 0 && i < 10)
            {
                if (list[i] == true)
                {
                    result = i;
                }
                i++;
            }
            return result;
        }

        private int PickOneTrue()
        {
            int i;
            int result = 0;
            Random r = new Random();
            if (FirstTrue() != 0)
            {
                i = r.Next(1, 9);
                while (result == 0)
                {
                    if (list[i] == true)
                    {
                        result = i;
                    }
                    else
                    {
                        i++;
                        if (i > 9)
                        {
                            i = 1;
                        }
                    }
                }
            }
            else
            {
                result = 0;
            }
            return result;
        }

        private bool IsInRow(PuzzleGrid grid, int row, int value)
        {
            bool result = false;
            for (int i =0; i<9; i++)
            {
                result = result || (Math.Abs(grid.Grid[row, i]) == value);
            }
            return result;
        }

        private bool IsInCol(PuzzleGrid grid, int col, int value)
        {
            bool result = false;
            for(int i =0; i<9; i++)
            {
                result = result || (Math.Abs(grid.Grid[i, col]) == value);
            }
            return result;
        }
    }
}
