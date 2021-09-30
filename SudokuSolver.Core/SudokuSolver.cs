using SudokuSolver.Core.Interfaces;
using SudokuSolver.Core.Sudokus;

namespace SudokuSolver.Core
{
    public class SudokuSolver
    {
        private Sudoku _sudoku;
        private readonly IDisplayBoardService _displayService;

        public SudokuSolver(Sudoku sudoku)
        {
            _sudoku = sudoku;
        }
        public SudokuSolver(Sudoku sudoku, IDisplayBoardService displayService) : this(sudoku)
        {
            _displayService = displayService;            
        }

        public Sudoku Solve()
        {
            Display();
            SolveWithBacktracking();
            return _sudoku;
        }

        private bool SolveWithBacktracking()
        {
            return SolveWithBacktracking(1, 1);
        }

        private bool SolveWithBacktracking(int row, int colIndex)
        {
            if (_sudoku.IsSolved())
                return true;

            bool solved = SolveRow(row, colIndex);
            if (!solved) return false;
            else return true;
        }

        private bool SolveRow(int row, int colIndex)
        {
            for (int col = colIndex; col <= 9; col++)
            {
                if (col != 1 && _sudoku.GetCell(col - 1, row).Value == 0) break;
                ICell cell = _sudoku.GetCell(col, row);
                if (cell.IsLocked || cell.Value > 0) continue;
                for (int value = 1; value <= 9; value++)
                {
                    if (_sudoku.RowIsFilled(row)) continue; ;
                    bool res = SetValueWithBacktracking(cell, value);
                    if (res is true) return true;
                }
            }
            return false;
        }

        private bool SetValueWithBacktracking(ICell cell, int value)
        {
            cell.Value = value;
            bool isValid = _sudoku.Validate(cell);
            RefreshDisplay();
            if (isValid)
            {
                bool rowIsFilled = _sudoku.RowIsFilled(cell.Row);//|| cell.Column == 9;                
                var solved = SolveWithBacktracking(
                    rowIsFilled ? cell.Row + 1 : cell.Row,
                    rowIsFilled ? 1 : cell.Column + 1);
                if (solved) return true;
            }
            cell.Value = 0;
            RefreshDisplay();
            return false;
        }

        private void Display()
        {
            if (_displayService is not null)
                _displayService.Display(_sudoku);
        }
        private void RefreshDisplay()
        {
            if (_displayService is not null)
                _displayService.Refresh();
        }
    }
}
