using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SudokuSolver.Core.Sudokus;
using SudokuSolver.Web.Api.Generators;
using SudokuSolver.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SudokuSolver.Web.Api.Controllers
{
    [Route("sudoku")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        private readonly ILogger<SudokuController> _logger;

        public SudokuController(ILogger<SudokuController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("seed")]
        public IActionResult Get(string q)
        {
            _logger.LogInformation($"Request handled at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                Sudoku game = SudokuGenerator.GetGameByQuery(q);

                return Ok(game.Grid);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("generate")]
        public IActionResult Generate([FromBody] GenerateSudokuRequest request)
        {
            try
            {
                Sudoku sudoku = new (request.Data);
                return Ok(sudoku.Grid);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("solve")]
        public IActionResult Solve([FromBody] SolveSudokuRequest request)
        {
            try
            {
                Sudoku sudoku = new(request.ToString());
                var solver = new SudokuSolver.Core.SudokuSolver(sudoku);
                var result = solver.Solve();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
