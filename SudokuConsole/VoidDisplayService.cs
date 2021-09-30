using SudokuSolver.Core.Interfaces;
using SudokuSolver.Core.Sudokus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuConsole
{
    public class VoidDisplayService : IDisplayBoardService
    {
        public void Display(Sudoku sudoku)
        {
            return;
        }

        public void Refresh()
        {
            return;
        }
    }
}
