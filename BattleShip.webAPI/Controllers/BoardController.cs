using BattleShip.Service.Board;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BattleShip.webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]


    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IConfiguration _config;

        public BoardController(IBoardService boardService, IConfiguration config)
        {
            _boardService = boardService;
            _config = config;

        }
        [HttpPost]

        public ActionResult CreateBoard()
        {
            int rows = _config.GetValue<int>("Rows");
            int columns = _config.GetValue<int>("Columns");

            var data = _boardService.CreateBoard(rows, columns);


            if (data != null)
            {

                var result = new { Result = data, Message = "Success!! Board is created with 10 rows and 10 columns" };
                return Ok(result);

            }
            else
            {
                return BadRequest(new { status = "Error", message = "Failed to create a board!!" });
            }

        }
    }
}
