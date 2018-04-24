﻿using System;
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
            }
        }



    }
}
