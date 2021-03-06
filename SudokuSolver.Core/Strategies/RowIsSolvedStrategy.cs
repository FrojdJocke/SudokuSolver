using SudokuSolver.Core.Sudokus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Strategies
{
    public class RowIsSolvedStrategy : SudokuIsSolvedStrategy
    {
        public RowIsSolvedStrategy(Sudoku sudoku) : base(sudoku)
        {

        }
        public override bool StrategyIsValid()
        {
            return BoundaryIsValid(_sudoku.Grid.Rows);
        }
    }
}
