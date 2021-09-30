using SudokuSolver.Core.Strategies;
using System;
using System.Linq;

namespace SudokuSolver.Core.Sudokus
{
    public class Sudoku
    {

        public Sudoku(string game)
        {
            Grid = new Grid(game);
        }

        public Grid Grid { get; set; }
        public bool Solved => IsSolved();

        public bool IsSolved()
        {
            return Grid.Cells.All(x => x.Value != 0)
                && IsSolved(new ColumnIsSolvedStrategy(this))
                && IsSolved(new RowIsSolvedStrategy(this))
                && IsSolved(new BlockIsValidStrategy(this));
        }

        internal bool RowIsFilled(int row)
        {
            return Grid.Rows.First(x => x.Id == row).Cells.All(x => x.Value > 0);
        }

        public bool Validate(ICell cell)
        {
            return Grid.Columns.First(x => x.Cells.Select(c => c.Column).Contains(cell.Column)).IsValid()
                && Grid.Columns.First(x => x.Id == cell.Column).Cells.Where(x => x.Column < cell.Column).All(x => x.Value > 0)
                && Grid.Rows.First(x => x.Cells.Select(c => c.Row).Contains(cell.Row)).IsValid()
                && Grid.Blocks.First(x => x.Id == Grid.GetBlock(cell.Column, cell.Row).Id).IsValid();
        }

        private bool IsSolved(SudokuIsSolvedStrategy strategy)
        {
            return strategy.StrategyIsValid();
        }

        public ICell GetCell(int col, int row)
        {
            var cell = Grid.Cells.First(x => x.Column == col && x.Row == row);
            return new CellProxy(cell);
        }
    }
}
