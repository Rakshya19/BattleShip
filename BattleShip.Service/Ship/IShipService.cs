using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Service.Ship
{
    public interface IShipService

    {
        ShipModel CreateShip(string shipType);
        BoardShipViewModel AddShipToBoard(ShipModel ship, string shipOrientation, int row, int column, List<BoardModel> ListBoardModel);
        ShipAndBoardstatus AttackShip(int row, int column, List<BoardModel> ListBoardModel, List<ShipViewModel> shipViewModel);
        ShipModel GetShip(string shipType);
    }
}
