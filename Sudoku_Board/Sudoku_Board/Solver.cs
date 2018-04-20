using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Board
{
    class Solver : IEnumerable
    {
        bool[] values;
        int count;
        int numberSolver;

        public int Count { get { return count; } }

        public Solver(int numberSolver, bool initalValue)
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
