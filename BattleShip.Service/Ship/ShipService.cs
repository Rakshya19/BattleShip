using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Service.Ship
{
    public class ShipService: IShipService
    {
        public void AddShipToBoard(int row, int column)
        {

        }
        public Shipstatus AttackShip(int row, int column)
        {
            return new Shipstatus();
        }

       
    }
}
