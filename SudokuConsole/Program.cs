using SudokuSolver.Core.Interfaces;
using SudokuSolver.Core.Sudokus;
using System;
using System.Diagnostics;

namespace SudokuConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SUDOKU SOLVER 2.0");
            var easy = "010020300004005060070000008006900070000100002030048000500006040000800106008000000";
            var expert = "003009004670020030054000020048906050000005048000800079000090002000600005020400000";
            var games = new(string level, string data)[] { ("Easy", easy), ("Expert", expert) };

            foreach (var game in games)
            {
                IDisplayBoardService displayService = new ConsoleDisplayService();
                var sudoku = new Sudoku(game.data);

                var solver = new SudokuSolver.Core.SudokuSolver(sudoku, new VoidDisplayService()); // pass ConsoleDIsplayService for realtime updates
                var timer = Stopwatch.StartNew();
                var result = solver.Solve();
                timer.Stop();
                Console.WriteLine($"Result of {game.level}\n-----------------------");
                Console.WriteLine($"Result: { (result.IsSolved() ? "SOLVED!" : "Unable to solve") }");
                Console.WriteLine($"Elapsed time: {timer.ElapsedMilliseconds}ms\n\n");
            }

            Console.ReadKey();
        }
    }
}
