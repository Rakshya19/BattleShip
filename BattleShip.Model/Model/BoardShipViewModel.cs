using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShip.Model.Model
{
    public class BoardShipViewModel
    {
        public List<BoardModel> ListBoardModel { get; set; }
        public List<ShipViewModel> ListShipViewModel { get; set; }
    }
}
