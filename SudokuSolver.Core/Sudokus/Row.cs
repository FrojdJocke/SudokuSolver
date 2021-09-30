using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core.Sudokus
{
    public class Row : GridBoundary
    {
        public Row(IEnumerable<Cell> cells) : base(cells)
        {
            Id = cells.First().Row;
            if (!cells.All(x => x.Row == Id))
                throw new ArgumentException("Cells with different row values was provided", nameof(cells));
        }        
    }
}