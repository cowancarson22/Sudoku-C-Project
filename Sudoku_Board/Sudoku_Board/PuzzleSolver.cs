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

        public PuzzleSolver(int numberSolver, bool initalValue)
        {
            values = new bool[numberSolver];
            count = 0;
            this.numberSolver = numberSolver;

            for (int i = 1; i <= numberSolver; i++)
                values[i] = initalValue;
        }

        public bool value[int key]{
            }
    }
}
