using BattleShip.Model.Model;
using BattleShip.Service.Board;
using BattleShip.Service.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Web.Controllers
{
    public class ShipController : Controller
    {
        // GET: Ship
        private readonly IShipService _shipService;
        private readonly IBoardService _boardService;
        public ShipController()
        {
            this._shipService = new ShipService();
            this._boardService = new BoardService();



        }
        public ActionResult CreateShip(string shipType)
        {
            
            var ship = _shipService.CreateShip(shipType);
            if (ship != null)
            {
                var result = new { Result = ship, Message = "Ship is created Successfully" };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { status = "Error", message = "Failed to create "+shipType+" Ship!!" });
            }
        }
        public ActionResult  PlaceShipInBoard(string shipType, string shipOrientation, int shipRow=1, int shipColumn=1)
        {
            var board = _boardService.CreateBoard();  //Actually we need to get the board by passing the boardID in the parameter.Since we do not have database, I am creating the board.

            var ship = _shipService.GetShip(shipType);

            var data = _shipService.PlaceShipInBoard(ship, shipOrientation, shipRow, shipColumn, board);
            if (data != null)
            {
                var result = new { Result = data, Message = "Ship is successfully added to the board" };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { status = "Error", message = "Failed to create " + shipType + " Ship!!" });
            }

        }

        //BoardCellList parameter must have list of cells in the board which are occupied

        // shipInBoard is the cells of ship which is placed in the board and which is attacked.
        //This attacked ship result can be get by passing the row and column parameter to a function and get the ship if it is in the database.
     
        public ActionResult AttackOneShipOnBoard(string shipType,string shipOrientation,int shipRow=1,int shipColumn=1, int attackRow=1, int attackColumn=1)
        {
            var board = _boardService.CreateBoard();

            var ship = _shipService.CreateShip(shipType);

            var placeShipInBoard = _shipService.PlaceShipInBoard(ship, shipOrientation, shipRow, shipColumn, board);

            var data = _shipService.AttackShip(attackRow, attackColumn, placeShipInBoard.ListBoardModel, placeShipInBoard.ListShipViewModel);
            var result = Enum.GetName(typeof(ShipAndBoardstatus), data);


            return Json(result, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        public ActionResult AttackShip(int attackRow, int attakColumn, List<BoardModel> boardCellList, List<ShipViewModel> shipInBoard)
        {
            var data = _shipService.AttackShip(attackRow, attakColumn, boardCellList, shipInBoard);
            var result = Enum.GetName(typeof(ShipAndBoardstatus), data);


                return Json(result, JsonRequestBehavior.AllowGet);

           

        }
    }
}