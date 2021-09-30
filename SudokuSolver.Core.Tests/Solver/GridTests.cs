using Shouldly;
using SudokuSolver.Core.Sudokus;
using System;
using System.Linq;
using Xunit;

namespace SudokuSolver.Core.Tests.Solver
{
    public class GridTests
    {
        private readonly Grid _sut;
        private string _game = "010020300004005060070000008006900070000100002030048000500006040000800106008000000";

        public GridTests()
        {
            _sut = new Grid(_game);
        }

        [Fact]
        public void NewGridShouldHaveCells()
        {
            _sut.Cells.ShouldNotBeNull();
        }

        [Fact]
        public void EmptyGameStringShouldThrow()
        {
            Action a = () => new Grid("");
            a.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void TooShortGameStringShouldThrow()
        {
            Action a = () => new Grid("690007000010000203");
            a.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void TooLongGameStringShouldThrow()
        {
            Action a = () => new Grid("010020300004005060070000008006900070000100002030048000500006040000800106008000000690007000010000203");
            a.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("010020300004005060070000008006900070000100002030048000500006040000800106008000000", false)]
        [InlineData("010020300004005060070000-080069000700001000020300480005000x6040000800106008000000", true)]
        public void GameStringShouldOnlyContainDigits(string data, bool shouldThrow)
        {
            Action a = () => new Grid(data);
            if (shouldThrow)
            {
                a.ShouldThrow<ArgumentException>();
            }
            else
            {
                a.ShouldNotThrow();
            }
        }

        [Fact]
        public void NewGridShouldHave81Cells()
        {
            _sut.Cells.Count().ShouldBe(81);
        }

        [Fact]
        public void CellsShouldHaveData()
        {
            foreach (var cell in _sut.Cells)
            {
                cell.Column.ShouldNotBe<int>(0);
                cell.Row.ShouldNotBe<int>(0);
            }
            var assignedValues = _sut.Cells.Where(x => x.Value > 0).Select(x => x.Value);
            assignedValues.ShouldAllBe(x => x > 0);
        }

        [Fact]
        public void CellsShouldHaveCorrectRowAndColumnValues()
        {
            // gamedata:
            // 010 020 300
            // 004 005 060
            // 070 000 008

            // 006 900 070
            // 000 100 002
            // 030 048 000

            // 500 006 040
            // 000 800 106
            // 008 000 000
            _sut.Cells.First(x => x.Column == 2 && x.Row == 1).Value.ShouldBe(1);
            _sut.Cells.First(x => x.Column == 5 && x.Row == 1).Value.ShouldBe(2);
            _sut.Cells.First(x => x.Column == 4 && x.Row == 4).Value.ShouldBe(9);
            _sut.Cells.First(x => x.Column == 2 && x.Row == 6).Value.ShouldBe(3);
            _sut.Cells.First(x => x.Column == 5 && x.Row == 6).Value.ShouldBe(4);
            _sut.Cells.First(x => x.Column == 6 && x.Row == 6).Value.ShouldBe(8);
            _sut.Cells.First(x => x.Column == 3 && x.Row == 9).Value.ShouldBe(8);
        }

        [Fact]
        public void PrePopulatedValuesShouldBeLocked()
        {
            _sut.Cells.First(x => x.Column == 1 && x.Row == 1).IsLocked.ShouldBeFalse();
            _sut.Cells.First(x => x.Column == 2 && x.Row == 1).IsLocked.ShouldBeTrue();
        }

        [Fact]
        public void GetRowShouldReturnRow()
        {
            var row = _sut.GetRow(3);
            row.ShouldBeOfType(typeof(Row));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        public void GetRowShouldReturnCorrectRow(int rowNumber)
        {
            var row = _sut.GetRow(rowNumber);
            row.Cells.ShouldAllBe(x => x.Row == rowNumber);
            row.Cells.Count().ShouldBe(9);
        }

        [Theory]
        [InlineData("010020300004005060070000008006900070000100002030048000500006040000800106008000000", 7, true)]
        [InlineData("010020300004005060070000008006900070000100002030048000500006045000800106008000000", 7, false)]
        public void RowsShouldNotHaveDuplicateValues(string data, int row, bool expectedResult)
        {
            Grid grid = new(data);
            grid.GetRow(row).IsValid().ShouldBe(expectedResult);
        }

        [Fact]
        public void GetColumnShouldReturnColumn()
        {
            Column col = _sut.GetColumn(3);
            col.ShouldBeOfType(typeof(Column));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        public void GetColumnShouldReturnCorrectColumn(int colNumber)
        {
            var col = _sut.GetColumn(colNumber);
            col.Cells.ShouldAllBe(x => x.Column == colNumber);
            col.Cells.Count().ShouldBe(9);
        }

        [Theory]
        [InlineData("010020300004005060070000008006900070000100002030048000500006040000800106008000000", 4, true)]
        [InlineData("010020300004005060070000008006900070000100002030048000500006040000800106008900000", 4, false)]
        public void ColumnsShouldNotHaveDuplicateValues(string data, int col, bool expectedResult)
        {
            Grid grid = new(data);
            grid.GetColumn(col).IsValid().ShouldBe(expectedResult);
        }

        [Fact]
        public void GetBlockShouldReturnBlock()
        {
            Block block = _sut.GetBlock(4, 4);
            block.ShouldBeOfType(typeof(Block));
        }

        [Theory]
        [InlineData(4, 8, 8)]
        [InlineData(9, 2, 3)]
        [InlineData(9, 9, 9)]
        public void GetBlockShouldReturnCorrectBlock(int col, int row, int expectedResult)
        {
            var block = _sut.GetBlock(col, row);

            block.Id.ShouldBe(expectedResult);
            block.Cells.Count().ShouldBe(9);
        }

        [Theory]
        [InlineData("010020300004005060070000008006900070000100002030048000500006040000800106008000000", 4, 3, true)]
        [InlineData("010020300004205060070000008006900070000100002030048000500006040000800106008000000", 4, 3, false)]
        public void BlocksShouldNotHaveDuplicateValues(string data, int col, int row, bool expectedResult)
        {
            Grid grid = new(data);
            grid.GetBlock(col, row).IsValid().ShouldBe(expectedResult);
        }

    }
}
