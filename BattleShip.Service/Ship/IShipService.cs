using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Service.Ship
{
    public interface IShipService

    {
        ShipModel CreateShip(string shipType);
        BoardShipViewModel PlaceShipInBoard(ShipModel ship, string shipOrientation, int row, int column, List<BoardModel> ListBoardModel);
        ShipAndBoardstatus AttackShip(AttackShipRequestModel model);
        List<ShipModel> GetShipList();
        List<ShipViewModel> GetPlacedShip();
    }
}
