using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShip.Model.Enum
{
    public class Enum
    {
        public enum ShipType
        {
            Carrier,
            BattleShip,
            Cruiser,
            Submarine,
            Destroyer
        }
        public enum Boardstatus
        {
            Unoccupied,
            Occupied
        }

        public enum Shipstatus
        {
            Hit,
            Miss
        }

    }
}
