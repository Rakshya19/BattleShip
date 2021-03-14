using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleShip.Model.Enum
{
    public class Enum:ErrorModel
    {
        public enum ShipType
        {
            [Description("carrier")]
            carrier,
            [Description("battleship")]
            battleship,
            [Description("cruiser")]
            cruiser,
            [Description("destroyer")]
            destroyer,
            [Description("submarine")]
            submarine,
        }
        public enum ShipOrientation
        {
            [Description("horizontal")]
            horizontal,
            [Description("vertical")]
            vertical
        }

        public enum ShipAndBoardstatus
        {
            [Description("hit")]
            Hit,
            [Description("miss")]
            Miss,
            [Description("ship is sunk")]
            ShipSunk,
            [Description("game over")]
            GameOver

        }

    }
}
