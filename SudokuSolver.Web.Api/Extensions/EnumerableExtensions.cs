using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuSolver.Web.Api.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetRandomInstance<T>(this IEnumerable<T> enumerable)
        {
            Random rnd = new();
            int rndNumber = rnd.Next(enumerable.Count());

            return enumerable.ToArray()[rndNumber];
        }
    }
}
