using System;
using System.Collections.Generic;
using System.Text;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Model.Model
{
    public class ShipBoardStatusModel
    {
        public ShipAndBoardstatus shipAndBoardstatus { get; set; }
        public ErrorModel errorModel { get; set; }
    }
}
