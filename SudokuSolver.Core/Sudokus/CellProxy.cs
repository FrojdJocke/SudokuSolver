using System;

namespace SudokuSolver.Core.Sudokus
{
    public class CellProxy : CellBase
    {
        private readonly Cell _cell;

        public CellProxy(Cell cell)
        {
            _cell = cell;
        }

        public override int Column => _cell.Column;
        public override int Row => _cell.Row;
        public override int Value 
        {
            get => _cell.Value;
            set
            {
                if (_cell.IsLocked) throw new InvalidOperationException("Unable to change value of locked cell");
                _cell.Value = value;
            }
        }
        public override bool IsLocked => _cell.IsLocked;
    }
}