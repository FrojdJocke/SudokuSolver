using SudokuSolver.Core.Sudokus;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core.Strategies
{
    public abstract class SudokuIsSolvedStrategy
    {
        protected readonly Sudoku _sudoku;

        public SudokuIsSolvedStrategy(Sudoku sudoku)
        {
            _sudoku = sudoku;
        }

        public abstract bool StrategyIsValid();

        protected bool BoundaryIsValid(IEnumerable<GridBoundary> boundary)
        {
            var result = boundary.Select(x => x.IsValid());
            return result.All(x => x is true);
        }
    }
}