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
            var shipList = _shipService.GetShipList();
            if (shipList == null)
            {
                shipList = new List<ShipModel>();
            }
            if (shipList.Where(x => x.shipType.ToLower() == shipType.ToLower()).Any())
            {
                throw new Exception("Ship already exists!");
            }
            var ship = _shipService.CreateShip(shipType);

            if (ship != null)
            {
                shipList.Add(ship);

                _fileService.SetFileContent("ship.json",JsonConvert.SerializeObject(shipList));

                var result = new { Result = ship, Message = "Ship is created Successfully" };

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
            var boardModel = _boardService.GetBoard();
            if (boardModel == null)
            {
                throw new Exception("Board Doesnot exists!! Create board first.");
            }
            var shipList = _shipService.GetShipList();
            if (shipList == null)
            {
                throw new Exception("Ship Doesnot exists!! Create Ship first.");
            }
            if (!Enum.IsDefined(typeof(ShipType), model.ShipType))
            {
                throw new Exception("Ship Type must be either Carrier or Battlership or Cruiser, Destroyer or Submarine!!");
            }

            var shipAddedList = _shipService.GetPlacedShip();
            if (shipAddedList == null)
            {
                shipAddedList = new List<ShipViewModel>();
            }

            var shipModel = shipList.Where(x => x.shipType.ToLower() == model.ShipType.ToLower()).FirstOrDefault();

            var shipAddedOnBoard = _shipService.PlaceShipInBoard(shipModel, model.shipOrientation, model.Row, model.Column, boardModel);
            if (shipAddedOnBoard != null)
            {
                shipAddedList.AddRange(shipAddedOnBoard.ListShipViewModel);
                foreach (var item in shipAddedOnBoard.ListBoardModel)
                {
                    boardModel.Where(x => x.Row == item.Row && x.Column == item.Column).FirstOrDefault(x => x.Occupied = true);
                }
            }

            _fileService.SetFileContent("board.json", JsonConvert.SerializeObject(boardModel));
            _fileService.SetFileContent("shipPlacement.json", JsonConvert.SerializeObject(shipAddedList));


            if (shipAddedOnBoard != null)
            {
                var result = new { Result = shipAddedOnBoard, Message = "Ship is successfully added to the board" };
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
            int rows = _config.GetValue<int>("Rows");
            int columns = _config.GetValue<int>("Columns");

            if (model.Row > 10 || model.Column > 10)
            {
                throw new IndexOutOfRangeException("Attack cannot be out of boards!!");
            }

            var boardList = _boardService.GetBoard();

            var shipList = _shipService.GetShipList();

            var shipAddedList = _shipService.GetPlacedShip();

            if (shipAddedList == null)
            {
                shipAddedList = new List<ShipViewModel>();
            }
            //var boardShipNumber = shipAddedList.Where(x => x.Row == model.Row & x.Column == model.Column).Select(x => x.BoardShipNumber).FirstOrDefault();


            var attackShipRequestModel = new AttackShipRequestModel
            {
                Row = model.Row,
                Column = model.Column,
                BoardCellList = boardList,
                ShipInBoard = shipAddedList
                //ShipInBoard = shipAddedList.Where(x => x.BoardShipNumber == boardShipNumber).ToList()
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
