using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Model
{
    public abstract class ShipModel
    {
        public ShipType ShipType { get; set; }
        public int size { get; set; }
    }
}
