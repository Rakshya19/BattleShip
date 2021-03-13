using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Model.Model
{
    public class ShipViewModel : Dimensions
    {
        public Guid BoardShipNumber { get; set; }
        public string ShipType { get; set; }
        public bool Hit { get; set; }
    }
}
