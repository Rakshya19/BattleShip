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
        public ActionResult  AddShipToBoard(string shipType, string shipOrientation, int row=1, int column=1)
        {
            var board = _boardService.CreateBoard();  //Actually we need to get the board by passing the boardID in the parameter.Since we do not have database, I am creating the board.
            var ship = _shipService.GetShip(shipType);
            var data = _shipService.AddShipToBoard(ship, shipOrientation, row, column, board);
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

    [HttpPost]
        public ActionResult AttackShip(int row, int column, List<BoardModel> boardCellList, List<ShipViewModel> shipInBoard)
        {
            var data = _shipService.AttackShip(row, column, boardCellList, shipInBoard);
            var result = Enum.GetName(typeof(ShipAndBoardstatus), data);


                return Json(result, JsonRequestBehavior.AllowGet);

           

        }
    }
}