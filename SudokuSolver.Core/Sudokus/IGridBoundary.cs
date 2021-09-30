using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Sudokus
{
    public interface IGridBoundary
    {
        int Id { get; }

        bool IsValid();
    }
}
