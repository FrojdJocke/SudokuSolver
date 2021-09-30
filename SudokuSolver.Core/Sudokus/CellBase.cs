namespace SudokuSolver.Core.Sudokus
{
    public abstract class CellBase : ICell
    {
        public virtual int Column { get; init; }
        public virtual int Row { get; init; }
        public virtual int Value { get; set; }
        public virtual bool IsLocked { get; init; }
    }
}