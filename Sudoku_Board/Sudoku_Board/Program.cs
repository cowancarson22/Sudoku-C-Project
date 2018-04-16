// File/Project Prolog
// Name:     <PUT YOUR NAME HERE>
// Period:   <PUT YOUR PERIOD HERE>
// Username: cowancar001
// Project:  Sudoku_Board
// Date:     4/16/2018 12:32:11 PM
//
// I declare that the following code was written by me or provided 
// by the instructor for this project. I understand that copying source
// code from any other source constitutes cheating, and that I will receive
// a zero on this project if I am found in violation of this policy.
// ---------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace Sudoku_Board
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SudokuGame());
        }
    }
}
