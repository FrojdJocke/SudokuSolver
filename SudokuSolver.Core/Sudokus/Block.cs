using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core.Sudokus
{
    public class Block : GridBoundary
    {

        public Block(IEnumerable<Cell> cells) : base(cells)
        {
            Id = GetId(
                cells.Min(c => c.Column), 
                cells.Min(c => c.Row));
        }

        public static int GetId(int colValue, int rowValue)
        {
            int col = GetSmallestCoordinateValueForBlock(colValue);
            int row = GetSmallestCoordinateValueForBlock(rowValue);

            return (col / 3)
                + (col % 3)
                + ((row / 3) * 3);
        }

        private static int GetSmallestCoordinateValueForBlock(int value)
        {
            do
            {
                value--;
            } while (value % 3 != 0);
            
            return value + 1;
        }
    }
}