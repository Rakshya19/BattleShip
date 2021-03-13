using BattleShip.Model.Model;
using BattleShip.Service.Board;
using BattleShip.Service.Ship;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ShipController(IShipService shipService,IBoardService boardService)
        {
            _shipService = shipService;
            _boardService = boardService;
        }
       
        public ActionResult CreateShip(string shipType)
        {

            var ship = _shipService.CreateShip(shipType);
            if (ship != null)
            {
                var folderDetails = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ship.json");
                System.IO.File.WriteAllText(folderDetails, JsonConvert.SerializeObject(ship));

                var result = new { Result = ship, Message = "Ship is created Successfully" };

                return Ok(result);

            }
            else
            {
                return BadRequest(new { status = "Error", message = "Failed to create " + shipType + " Ship!!" });
            }
        }
        public ActionResult PlaceShipInBoard(ShipPlacementRequestModel model)
        {
            var boardModel = _boardService.GetBoard();

            var shipList = _shipService.GetShipList();

            var shipModel = shipList.Where(x => x.shipType == model.ShipType).FirstOrDefault();

            var data = _shipService.PlaceShipInBoard(shipModel, model.shipOrientation, model.Row, model.Column, boardModel);

            var shipPlacementDetails = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "shipPlacement.json");           
            System.IO.File.WriteAllText(shipPlacementDetails, JsonConvert.SerializeObject(data));
            
            if (data != null)
            {
                var result = new { Result = data, Message = "Ship is successfully added to the board" };
                return Ok(result);

            }
            else
            {
                return BadRequest(new { status = "Error", message = "Failed to create " + model.ShipType + " Ship!!" });
            }

        }

        
        public ActionResult AttackShip( Dimensions model)
        {
            var boardList = _boardService.GetBoard();

            var shipList = _shipService.GetShipList();

            var shipAddedList = _shipService.GetPlacedShip();

            var boardShipNumber=shipAddedList.Where(x => x.Row == model.Row & x.Column == model.Column).Select(x=>x.BoardShipNumber).FirstOrDefault();
            
            
            var attackShipRequestModel = new AttackShipRequestModel
            {
                Row = model.Row,
                Column = model.Column,
                BoardCellList = boardList,
                ShipInBoard = shipAddedList.Where(x => x.BoardShipNumber == boardShipNumber).ToList()
            };

            var data = _shipService.AttackShip(attackShipRequestModel);
            var result = Enum.GetName(typeof(ShipAndBoardstatus), data);


            return Ok(result);


                
        }

        //[HttpPost]
        //public ActionResult AttackShip(AttackShipRequestModel model)
        //{
        //    var data = _shipService.AttackShip(model);
        //    var result = Enum.GetName(typeof(ShipAndBoardstatus), data);


        //    return Ok(result);

        //}


    }
}
