using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Model.Model
{
    public class ShipViewModel : Dimensions
    {
        public int BoardShipNumber { get; set; }
        public string ShipType { get; set; }
        public bool Hit { get; set; }
    }
}
