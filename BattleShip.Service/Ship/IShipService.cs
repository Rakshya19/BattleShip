using BattleShip.Model;
using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Service.Ship
{
    public interface IShipService

    {
        BoardShipViewModel AddShipToBoard(ShipModel ship, ShipOrientation shipOrientation, int row, int column, List<BoardModel> ListBoardModel);
        void CheckBoardOccupied(ShipModel ship, ShipOrientation shipOrientation, int row, int column, List<BoardModel> ListBoardModel);
        ShipAndBoardstatus AttackShip(int row, int column, List<BoardModel> ListBoardModel, List<ShipViewModel> shipViewModel);
    }
}
