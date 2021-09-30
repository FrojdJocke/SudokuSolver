using SudokuSolver.Core.Sudokus;

namespace SudokuSolver.Core.Interfaces
{
    public interface IDisplayBoardService
    {
        void Display(Sudoku sudoku);
        void Refresh();
    }
}
