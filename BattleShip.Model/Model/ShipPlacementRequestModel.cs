using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Model.Model
{
    public class ShipPlacementRequestModel:Dimensions
    {
       public string ShipType { get; set; }
       public string shipOrientation { get; set; }
       
    }
}
