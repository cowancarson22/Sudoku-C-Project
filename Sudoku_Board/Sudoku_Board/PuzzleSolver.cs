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

        private int GroupNum(int rc)
        {
            int result;
            result = (int)(rc / 3);
            return result;
        }

        private bool IsIn3X3(PuzzleGrid g, int row, int col, int value)
        {
            int rLow;
            int cLow;
            rLow = 3 * GroupNum(row);    
            cLow = 3 * GroupNum(col);
            bool result = false;
            for (int i = rLow; i < rLow + 3; i++) 
            {
                for (int j = cLow; j < cLow + 3; j++)     
                {               
                    if (Math.Abs(g.Grid[i, j]) == value)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool IsPossible(PuzzleGrid g, int row, int col, int value)
        {                     
            bool result;
            result = (!IsInRow(g, row, value) && !IsInCol(g, col, value) &&
                !IsIn3X3(g, row, col, value));
            return result;
        }

        private int ListPossible(int row, int col, PuzzleGrid g)
        {
            int count = 0;
            ClearList();              
            for (int i = 1; i < 10; i++)          
            {
                if (IsPossible(g, row, col, i) == true)   
                {
                    list[i] = true;
                    count++;
                }
                else                    
                {
                    list[i] = false;
                }
            }
            return count;
        }

        private void FillSingleChoices(PuzzleGrid grid)
        {
            bool anyChanges = false;                    
            int numChoices;                           
            do            
            {
                anyChanges = false;
                for (int i = 0; i < 9; i++)         
                {
                    for (int j = 0; j < 9; j++)  
                    {
                        if (grid.Grid[i, j] == 0)
                        {       
                            numChoices = ListPossible(i, j, grid);
                            if (numChoices == 1) 
                            {
                                grid.UserSetCell(i, j, FirstTrue());
                                anyChanges = (grid.Grid[i, j] != 0);
                            }
                        }
                    }
                }
            } while (anyChanges == true && !IsSolved(grid));
        }

        private bool FindFewestChoices(PuzzleGrid grid, out int r, out int c, out int numChoices)
        {
            bool[] minList = new bool[10];
            int numCh, minR, minC, minChoice, i, j;
            bool bad, result;
            minChoice = 10;
            minR = 0;
            minC = 0;
            for (i = 1; i < 10; i++)              
                minList[i] = false;
            
            bad = false;
            i = 0;
            while (!bad && i < 9) 
            {
                j = 0;
                while (!bad && j < 9) 
                {
                    if (grid.Grid[i, j] == 0)
                    {
                        numCh = ListPossible(i, j, grid);    
                        if (numCh == 0)     
                        {
                            bad = true;
                        }
                        else                             
                        {
                            if (numCh < minChoice)   
                            {
                                minChoice = numCh;          
                                list.CopyTo(minList, 0);
                                minR = i;          
                                minC = j;          
                            }
                        }
                    }
                    j++;
                }
                i++;
            }
            if (bad || minChoice == 10)  //If bad solutn or minChoice never set
            {
                result = false;                    //No fewest possible choices
                r = 0;
                c = 0;
                numChoices = 0;
            }
            else
            {
                result = true; //Valid cell found, return information to caller
                r = minR;
                c = minC;
                numChoices = minChoice;
                minList.CopyTo(list, 0);
            }
            return result;
        }
        /// <summary>
        /// ClearList clears the values currently held in list[] by setting all
        /// values to false.
        /// </summary>
        private void ClearList()
        {
            for (int i = 0; i < 10; i++)
            {
                list[i] = false;
            }
        }
        /// <summary>
        /// SolveGrid attempts to solve a puzzle by checking through all
        /// possible values for each cell, discarding values that do not lead
        /// to a valid solution. On a try, it recursively calls itself to
        /// maintain previous states of the grid to back track to if the
        /// current path fails. It creates a local version of the grid to
        /// facilitate this. It also checks if the puzzle is uniquely solvable.
        /// </summary>
        /// <param name="g">Current state of the grid</param>
        /// <param name="checkUnique">Do we care if it has unique soln?</param>
        /// <returns></returns>
        public bool SolveGrid(PuzzleGrid g, bool checkUnique)
        {
            PuzzleGrid grid = new PuzzleGrid();
            grid = (PuzzleGrid)g.Clone();                 //Copy the input grid
            int i, choice, r, c, numChoices;
            bool done, got_one, solved, result;
            got_one = false;
            recursions++;
            FillSingleChoices(grid);  //First, fill in all single choice values
            if (IsSolved(grid))                        //If it's already solved
            {
                if (numSolns > 0)               //If another soln already found
                {
                    stoplooking = true;                   //Don't look for more
                    result = false;              //Return false, no UNIQUE soln
                }
                else                               //If no other soln found yet
                {
                    numSolns++;
                    final[numSolns] = (PuzzleGrid)g.Clone();  //Save found soln
                    result = true;
                    SolutionGrid = grid;
                }
            }
            else                                            //If not solved yet
            {
                if (!FindFewestChoices(grid, out r, out c, out numChoices))
                {
                    result = false;                          //Invalid solution
                }
                else                                 //Current grid still valid
                {
                    i = 1;
                    done = false;
                    got_one = false;
                    while (!done && i <= numChoices)
                    {
                        choice = PickOneTrue();         //Pick a possible value
                        list[choice] = false;      //Won't want to use it again
                        grid.UserSetCell(r, c, choice);

                        if (recursions < MaxDepth)
                        {
                            //-----------We must go deeper. SUDCEPTION!-----------//
                            solved = (SolveGrid(grid, checkUnique)); //Recurse
                        }
                        else
                        {
                            solved = false;
                        }
                        if (stoplooking == true)
                        {
                            done = true;
                            got_one = true;
                        }
                        else
                        {
                            got_one = (got_one || solved);
                            if (!checkUnique)  //If not looking for unique soln
                            {
                                done = got_one;       //Then we have a solution
                            }
                        }
                        i++;
                    }
                    result = got_one;
                }
            }
            return result;
        }
    }//End CLASS
}//End NAMESPACE
