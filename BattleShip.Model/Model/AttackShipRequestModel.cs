using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Model.Model
{
    public class AttackShipRequestModel:Dimensions
    {
        public List<BoardModel> BoardCellList { get; set; }
        public List<ShipViewModel> ShipInBoard { get; set; }
    }
}
