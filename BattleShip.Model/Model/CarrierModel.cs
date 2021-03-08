using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Model.Model
{
    public class CarrierModel:ShipModel
    {
        public CarrierModel()
        {
            size = 5;
            shipType = ShipType.Carrier;
        }
    }
}
