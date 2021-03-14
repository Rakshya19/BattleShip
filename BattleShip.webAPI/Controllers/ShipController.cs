using BattleShip.Model.Model;
using BattleShip.Service.Board;
using BattleShip.Service.File;
using BattleShip.Service.Ship;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.webAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;
        private readonly IBoardService _boardService;
        private readonly IConfiguration _config;
        private readonly IFileService _fileService;
        public ShipController(IShipService shipService, IBoardService boardService, IConfiguration config, IFileService fileService)
        {
            _shipService = shipService;
            _boardService = boardService;
            _config = config;
            _fileService = fileService;
        }
        [HttpPost]
        public ActionResult CreateShip(string shipType)
        {
            var ship = _shipService.CreateShip(shipType);

            if (ship != null)
            {
                var result = new { Message = "Ship is created Successfully", Result = ship };

                return Ok(result);
            }
            else
            {
                return BadRequest(new { status = "Error", message = "Failed to create " + shipType + " Ship!!" });
            }
        }

        [HttpPost]
        public ActionResult PlaceShipInBoard([FromBody] ShipPlacementRequestModel model)
        { 
            var shipAddedOnBoard = _shipService.PlaceShipInBoard(model);          

            if (shipAddedOnBoard != null)
            {
                var result = new { Message = "Ship is successfully added to the board",Result = shipAddedOnBoard  };
                return Ok(result);

            }
            else
            {
                return BadRequest(new { status = "Error", message = "Failed to create " + model.ShipType + " Ship!!" });
            }

        }

        [HttpPost]
        public ActionResult AttackShip(Dimensions model)
        {

            var data = _shipService.AttackShip(model);
            var result = Enum.GetName(typeof(ShipAndBoardstatus), data);


            return Ok(result);



        }

     

    }
}
