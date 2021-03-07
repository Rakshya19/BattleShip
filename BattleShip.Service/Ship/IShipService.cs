using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Service.Ship
{
    public interface IShipService
    {
        void AddShipToBoard(int row, int column);
        Shipstatus AttackShip(int row, int column);
    }
}
