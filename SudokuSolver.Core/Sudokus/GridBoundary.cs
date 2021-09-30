using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Sudokus
{
    public abstract class GridBoundary : IGridBoundary
    {
        public GridBoundary(IEnumerable<Cell> cells)
        {
            Cells = cells.ToArray();
        }
        public int Id { get; protected set; }
        public Cell[] Cells { get; set; }

        public virtual bool IsValid()
        {
            var values = Cells.Where(x => x.Value != 0).Select(x => x.Value);
            var distinctValues = new HashSet<int>(values);
            return distinctValues.Count == values.Count();
        }
    }
}
