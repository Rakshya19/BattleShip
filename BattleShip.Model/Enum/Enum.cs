using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleShip.Model.Enum
{
    public class Enum
    {
        public enum ShipType
        {
            [Description("Carrier")]
            Carrier,
            [Description("BattleShip")]
            BattleShip,
            [Description("Cruiser")]
            Cruiser,
            [Description("Destroyer")]
            Destroyer,
            [Description("Submarine")]
            Submarine,
        }
        public enum ShipOrientation
        {
            [Description("Horizontal")]
            Horizontal,
            [Description("Vertical")]
            Vertical
        }

        public enum ShipAndBoardstatus
        {
            [Description("Hit")]
            Hit,
            [Description("Miss")]
            Miss,
            [Description("Ship is Sunk")]
            ShipSunk,
            [Description("Game Over")]
            GameOver

        }

    }
}
