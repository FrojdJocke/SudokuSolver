using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core.Sudokus
{
    public class Grid
    {
        private IEnumerable<Cell> _cells;

        public Grid(string game)
        {
            if (!IsValidGameData(game))
                throw new ArgumentException(nameof(game));

            Cells = PopulateGrid(game);
        }

        internal IEnumerable<Column> Columns { get; private set; }
        internal IEnumerable<Row> Rows { get; private set; }
        internal IEnumerable<Block> Blocks { get; private set; }
        public IEnumerable<Cell> Cells { get; private set; }

        private IEnumerable<Cell> PopulateGrid(string game)
        {
            var cells = new List<Cell>();
            for (int i = 1; i <= game.Length; i++)
            {
                cells.Add(CreateCell(game[i - 1], i));
            }

            PopulateBoundaryCollections(cells);

            return cells;
        }

        private void PopulateBoundaryCollections(List<Cell> cells)
        {
            List<Column> cols = new();
            List<Row> rows = new();
            List<Block> blocks = new();
            for (int i = 1; i <= 9; i++)
            {
                cols.Add(new Column(cells.Where(x => x.Column == i).Select(x => x)));
                rows.Add(new Row(cells.Where(x => x.Row == i).Select(x => x)));

                if (i % 3 == 0)
                {
                    blocks.AddRange(PopulateBlocks(i, cells));
                }

            }
            Columns = cols;
            Rows = rows;
            Blocks = blocks;
        }

        private IEnumerable<Block> PopulateBlocks(int index, IEnumerable<Cell> cells) // always i % 3 == 0
        {
            List<Block> blocks = new();
            for (int i = 3; i <= 9; i += 3)
            {
                var rowRange = Enumerable.Range(index - 2, 3);
                var colRange = Enumerable.Range(i - 2, 3);
                var block = new Block(
                        cells.Where(x =>
                        rowRange.Contains(x.Row)
                        && colRange.Contains(x.Column)));
                blocks.Add(block);
            }
            return blocks;
        }

        private static Cell CreateCell(char cellValue, int i)
        {
            return new Cell(
                column: int.Parse($"{(i % 9 == 0 ? 9 : i % 9)}"),
                row: int.Parse($"{(Math.Ceiling((double)i / 9))}"),
                value: int.Parse($"{cellValue}"));
        }

        private static bool IsValidGameData(string game)
        {
            return
                !string.IsNullOrWhiteSpace(game)
                && game.Length == 81
                && game.All(x => char.IsDigit(x));
        }

        public Row GetRow(int rowNumber)
        {
            return Rows.First(x => x.Id == rowNumber);
        }

        public Column GetColumn(int columnNumber)
        {
            return Columns.First(x => x.Id == columnNumber);
        }

        public Block GetBlock(int column, int row)
        {
            var id = Block.GetId(column, row);
            var block = Blocks.First(x => x.Id == id);
            return block;
        }
    }
}