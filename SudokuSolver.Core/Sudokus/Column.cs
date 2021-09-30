using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core.Sudokus
{
    public class Column : GridBoundary
    {
        public Column(IEnumerable<Cell> cells) : base(cells)
        {
            Id = cells.First().Column;
            if (!cells.All(x => x.Column == Id))
                throw new ArgumentException("Cells with different column values was provided", nameof(cells));
        }
    }
}