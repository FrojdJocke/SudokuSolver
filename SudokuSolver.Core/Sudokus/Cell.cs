namespace SudokuSolver.Core.Sudokus
{
    public class Cell : CellBase
    {
        public Cell(
            int column, 
            int row, 
            int value)
        {
            Column = column;
            Row = row;
            Value = value;
            IsLocked = value > 0;
        }
    }
}