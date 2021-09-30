using SudokuSolver.Core.Sudokus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Web.Api.Models
{
    public class SolveSudokuRequest
    {
        public IEnumerable<Cell> Cells { get; init; }

        /// <summary>
        /// Converts sudoku game data to a string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var cells = this.Cells.OrderBy(x => x.Row).ThenBy(x => x.Column);
            StringBuilder builder = new ();

            foreach (var cell in cells)
            {
                builder.Append($"{ cell.Value }");
            }

            return builder.ToString();
        }
    }
}
