using SudokuSolver.Core.Sudokus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Strategies
{
    public class ColumnIsSolvedStrategy : SudokuIsSolvedStrategy
    {
        public ColumnIsSolvedStrategy(Sudoku sudoku) : base(sudoku)
        {

        }
        public override bool StrategyIsValid()
        {
            return BoundaryIsValid(_sudoku.Grid.Columns);
        }
    }
}
