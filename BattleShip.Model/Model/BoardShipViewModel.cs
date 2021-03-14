using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Model.Model
{
    public class BoardShipViewModel:ErrorModel
    {
        public List<BoardModel> ListBoardModel { get; set; }
        public List<ShipViewModel> ListShipViewModel { get; set; }
    }
}
