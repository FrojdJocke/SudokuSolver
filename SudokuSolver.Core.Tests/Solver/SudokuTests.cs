using Moq;
using Shouldly;
using SudokuSolver.Core.Interfaces;
using SudokuSolver.Core.Sudokus;
using System;
using Xunit;

namespace SudokuSolver.Core.Tests.Solver
{
    public class SudokuTests
    {
        private readonly Sudoku _sut;
        private readonly Mock<IDisplayBoardService> _displayService;

        public SudokuTests()
        {
            _displayService = new Mock<IDisplayBoardService>();
            _sut = new Sudoku("010020300004005060070000008006900070000100002030048000500006040000800106008000000");
            _displayService.Setup(x => x.Display(_sut));
            _displayService.Setup(x => x.Refresh());
        }

        [Fact]
        public void ShouldNotBeSolvedAfterCreate()
        {
            _sut.IsSolved().ShouldBeFalse();
        }

        [Fact]
        public void SolvedGameDataShouldBeSolvedAfterCreate()
        {
            Sudoku sudoku = new("954823716261974385738516249682437591319652874475189623846295137527341968193768452");
            sudoku.IsSolved().ShouldBeTrue();
        }

        [Fact]
        public void InvalidGameDataShouldNotBeSolvedAfterCreate()
        {
            // bad data
            Sudoku sudoku = new("594823716261974385738516249682437591319652874475189623846295137527341968193768452");
            sudoku.IsSolved().ShouldBeFalse();
        }

        [Fact]
        public void UpdateLockedCellShouldThrowInvalidOperation()
        {
            var cell = _sut.GetCell(col: 2, row: 1);

            Action a = () => cell.Value = 4;

            a.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void UpdateUnlockedCellShouldUpdateValue()
        {
            var cell = _sut.GetCell(col: 3, row: 1);
            cell.Value = 4;

            cell.Value.ShouldBe(4);
        }

        [Fact]
        public void ValidateValidCellShouldReturnTrue()
        {
            Cell cell = new(1, 1, 6);
            _sut.Validate(cell).ShouldBeTrue();
        }

        [Fact]
        public void ValidateInvalidCellShouldReturnFalse()
        {
            Cell cell = new(1, 1, 7);
            _sut.Validate(cell).ShouldBeTrue();
        }
    }
}
