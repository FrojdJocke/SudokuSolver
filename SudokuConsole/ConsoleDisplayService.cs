using SudokuSolver.Core.Interfaces;
using SudokuSolver.Core.Sudokus;
using System;
using System.Text;

namespace SudokuConsole
{
    internal class ConsoleDisplayService : IDisplayBoardService
    {
        Sudoku _sudoku;
        public void Display(Sudoku sudoku)
        {
            if (_sudoku is null) _sudoku = sudoku;
            Print();
        }

        public void Refresh()
        {
            //Console.Clear();

            Print();
        }

        private void Print()
        {
            Console.SetCursorPosition(0, 3);
            var builder = new StringBuilder("\r");
            string devider = "-------------------------\n";
            //Console.Write(devider);
            builder.Append(devider);
            for (int r = 1; r <= 9; r++)
            {
                for (int c = 0; c <= 9; c++)
                {
                    if (c == 0)
                    {
                        //Console.Write('|');
                        builder.Append("|");
                        continue;
                    }
                    //Console.Write($" {_sudoku.GetCell(c, r).Value}");
                    builder.Append($" {_sudoku.GetCell(c, r).Value}");
                    if (c == 9) builder.Append(" |\n");//Console.Write(" |\n");
                    else if (c % 3 == 0) builder.Append(" |");//Console.Write(" |");
                }
                if (r % 3 == 0) builder.Append(devider);//Console.Write(devider);
            }
            Console.WriteLine(builder.ToString());
        }
    }
}