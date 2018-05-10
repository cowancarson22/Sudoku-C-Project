using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Board
{
    public enum Difficulty
    {
        Easy, Medium, Hard
    }

    class GeneratingBoard
    {
        private PuzzleSolver puzzleSolver;

        public PuzzleGrid PermaGrid;

        public PuzzleGrid SolutionGrid;

        private Difficulty difficulty;

        public PuzzleGenerator(Difficulty difficultyIn)
        {
            puzzleSolver = new PuzzleSolver();
            difficulty = difficultyIn;
        }

        public PuzzleGrid InitGrid()
        {
            PuzzleGrid tempGrid = new PuzzleGrid { };
            int row = 0;
            int col = 0;
            int newVal;
            bool solved;
            List<int> valueSet = new List<int>(Enumerable.Range(-9, 9));

            List<int> valueSet2 = new List<int>();
            Random rnd = new Random();
            int randIndex = 0;
            newVal = valueSet[randIndex];
            tempGrid.InitsetCell(row, col, newVal);
            valueSet.Remove(newVal);
            for(row =1; row <9; row++)
            {
                randIndex = rnd.Next(0, valueSet.Count);
                newVal = valueSet[randIndex];
                valueSet2.Add(newVal);
                valueSet.Remove(newVal);
                tempGrid.InitSetCell(row, col, newVal);
            }
            row = 0;
            for (col = 1; col < 3; col++)
            {
                randIndex = rnd.Next(0, valueSet.Count);
                newVal = valueSet2[randIndex];
                while ((newVal == tempGrid[1, 0] || (newVal == tempGrid.Grid[2, 0])))
                {
                    randIndex = rnd.Next(0, valueSet2.Count);
                    newVal = valueSet2[randIndex];
                }
                valueSet2.Remove(newVal);
            }
            for (col = 3; col < 9; col++)
            {
                randIndex = rnd.Next(0, valueSet2.Count);
                newVal = valueSet2[randIndex];
                valueSet2.Remove(newVal);
                tempGrid.InitSetCell(row, col, newVal);
            }
            for(col=3; col <9; col++)
            {
                randIndex = rnd.Next(0, valueSet2.Count);
                newVal = valueSet2[randIndex];
                valueSet2.Remove(newVal);
                tempGrid.InitSetCell(row, col, newVal);
            }
            do
            {
                puzzleSolver = new PuzzleSolver();
                puzzleSolver.SolveGrid((PuzzleGrid)tempGrid.Clone(), false);
                SolutionGrid = puzzleSolver.SolutionGrid;
            } while (SolutionGrid == null || SolutionGrid.IsBlank());
            PermaGrid = Blanker(SolutionGrid);
            return PermaGrid;
        }

        public PuzzleGrid Blanker(PuzzleGrid solveGrid)
        {
            PuzzleGrid tempGrid;
            PuzzleGrid saveCopy;

            bool unique = true;
            int totalBlanks = 0;
            int tries = 0;
            int desiredBlanks;
            int symmetry = 0;
            tempGrid = (PuzzleGrid)solveGrid.Clone();

            Random rnd = new Random();

            switch (difficulty)
            {
                case Difficulty.Easy:
                    desiredBlanks = 40;
                    break;
                case Difficulty.Medium:
                    desiredBlanks = 45;
                    break;
                case Difficulty.Hard:
                    desiredBlanks = 50;
                    break;
                default:
                    desiredBlanks = 40;
                    break;
            }

            symmetry = rnd.Next(0, 2);
            do
            {
                saveCopy = (PuzzleGrid)tempGrid.Clone();
                tempGrid = RandomlyBlank(tempGrid, symmetry, ref totalBlanks);
                puzzleSolver = new PuzzleSolver();
                unique = puzzleSolver.SolveGrid((PuzzleGrid)tempGrid.Clone(), true);
                if (!unique)
                {
                    tempGrid = (PuzzleGrid)saveCopy.Clone();
                    tries++;
                }
            } while ((totalBlanks < desiredBlanks) && (tries < 1000));
            solveGrid = tempGrid;
            solveGrid.Finish();
            return solveGrid;
        }

        public PuzzleGrid RandomlyBlank(PuzzleGrid tempGrid, int sym, ref int blankCount)
        {
            Random rnd = new Random();
            int row = rnd.Next(0, 8);
            int column = rnd.Next(0, 8);
            while (tempGrid.Grid[row, column] == 0)
            {
                row = rnd.Next(0, 8);
                column = rnd.Next(0, 8);
            }
            tempGrid.InitSetCell(row, column, 0);
            blankCount++;
            switch (sym)
            {
                case 0:
                    if(tempGrid.Grid[row, 8 - column] != 0)
                    {
                        blankCount++;
                    }
                    tempGrid.InitSetCell(row, 8 - column, 0);
                    break;
                case 1:
                    if (tempGrid.Grid[column, row] != 0)
                    {
                        blankCount++;
                    }
                    tempGrid.InitSetCell(column, row, 0);
                    break;
                case 2:
                    if (tempGrid.Grid[column, row] != 0)
                    {
                        blankCount++;
                    }
                    tempGrid.InitSetCell(column, row, 0);
                    break;
                default:
                    if (tempGrid.Grid[row, 8-column] != 0)
                    {
                        blankCount++;
                    }
                    tempGrid.InitSetCell(column, row, 0);
                    break;
            }
            return tempGrid;
        }

    }
}
