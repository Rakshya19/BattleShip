using BattleShip.Model.Model;
using BattleShip.Service.Board;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BattleShip.Web.Controllers
{
    public class BoardController : Controller
    {
        // GET: Board
        private readonly IBoardService _boardService;
        public BoardController()
        {
            this._boardService = new BoardService();

        }
        // GET api/values
        [HttpGet]
        public ActionResult CreateBoard()
        {
            var data = _boardService.CreateBoard();
            if (data != null)
            {
                var result = new { Result = data, Message = "Success!! Board is created with 10 rows and 10 columns" };             
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { status = "Error", message = "Failed to create a board!!" });
            }

        }
    }
}