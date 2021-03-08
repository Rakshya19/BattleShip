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
        public enum ShipOrientation
        {
            Horizontal,
            Vertical
        }

        public enum ShipAndBoardstatus
        {
            Hit,
            Miss,
            ShipSunk,
            GameOver

        }
        public enum LevelType
        {
            Level1 = 1,
            Level2 = 2,
            Level3 = 3
        }

    }
}
