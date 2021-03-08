using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Model.Model
{
    public class ShipViewModel :Dimensions
    {
        public int BoardShipNumber { get; set; }
        public ShipType ShipType { get; set; }
        public bool Hit { get; set; }
    }
}
