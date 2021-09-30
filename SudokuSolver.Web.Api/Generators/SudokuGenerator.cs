using SudokuSolver.Core.Sudokus;
using SudokuSolver.Web.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuSolver.Web.Api.Generators
{
    public static class SudokuGenerator
    {
        private static (string level, string data)[] Games = new (string level, string data)[]
        {
            ("Easy", "200000000000006200001000070006008000300090007000600400040000800005200000000000003"),
            ("Easy", "010020300004005060070000008006900070000100002030048000500006040000800106008000000"),
            ("Easy", "003020600900305001001806400008102900700000008006708200002609500800203009005010300"),
            ("Easy", "400950270090007000001002409000300724300006000000009368860000032709060140020590000"),
            ("Easy", "000540000008020000002731840501080070030006100000300000200003780000005034009060020"),
            ("Expert", "790000006008050010200070083000500762600204000080007300900065100010008000807040009"),
            ("Expert", "400085090130090050907000003800009000200003008070001000090000710060050400000920030"),
            ("Expert", "000000000003257000000489500070000000000010900908030002135890060800001005092000183"),
            ("Expert", "027080100000005090000600003030000070000020830700000642908000000402753080570090000"),
            ("Expert", "003009004670020030054000020048906050000005048000800079000090002000600005020400000")
        };

        public static Sudoku GetGameByQuery(string q)
        {
            (string level, string data) seededGame = GetRandomGameByLevel(q);

            Sudoku game = new(seededGame.data);
            return game;
        }

        private static (string level, string data) GetRandomGameByLevel(string q)
        {
            IEnumerable<(string level, string data)> gamesOfFilteredDifficulty = GetGamesByLevel(q);

            (string level, string data) seededGame = gamesOfFilteredDifficulty.GetRandomInstance();

            return seededGame;
        }

        private static IEnumerable<(string level, string data)> GetGamesByLevel(string q)
        {
            var gamesOfFilteredDifficulty = Games.Where(x => x.level.Equals(q, StringComparison.OrdinalIgnoreCase));            
            
            return gamesOfFilteredDifficulty;
        }
    }
}
