namespace SudokuSolver.Core.Sudokus
{
    public interface ICell
    {
        int Column { get; init; }
        bool IsLocked { get; init; }
        int Row { get; init; }
        int Value { get; set; }
    }
}