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

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;

        }
        [HttpPost]

        public ActionResult CreateBoard()
        {
            var data = _boardService.CreateBoard();
            if (data != null)
            {
                var result = new { Message = "Success!! Board is created with 10 rows and 10 columns",Result = data };
                return Ok(result);
            }
            else
            {
                return BadRequest(new { status = "Error", message = "Failed to create a board!!" });
            }

        }
    }
}
