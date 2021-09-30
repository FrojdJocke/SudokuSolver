using Moq;
using Shouldly;
using SudokuSolver.Core.Interfaces;
using SudokuSolver.Core.Sudokus;
using Xunit;

namespace SudokuSolver.Core.Tests.Solver
{
    public class SudokuSolverTests
    {
        private Mock<IDisplayBoardService> _displayService;
        private readonly SudokuSolver _sut;

        public SudokuSolverTests()
        {
            _displayService = new Mock<IDisplayBoardService>();
            var sudoku = new Sudoku("010020300004005060070000008006900070000100002030048000500006040000800106008000000");
            _sut = new SudokuSolver(sudoku, _displayService.Object);
            _displayService.Setup(x => x.Display(sudoku));
            _displayService.Setup(x => x.Refresh());

        }
        [Fact]
        public void SolveShouldReturnSudoku()
        {
            Sudoku result = _sut.Solve();

            result.ShouldBeOfType(typeof(Sudoku));
        }

        [Fact]
        public void SolveShouldReturnSolvedSudoku()
        {
            Sudoku result = _sut.Solve();

            result.IsSolved().ShouldBeTrue();
        }
    }
}
