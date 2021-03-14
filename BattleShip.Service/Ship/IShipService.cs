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
        BoardShipViewModel PlaceShipInBoard(ShipPlacementRequestModel shipPlacementModel);
        ShipAndBoardstatus AttackShip(Dimensions model);
        List<ShipModel> GetShipList();
        List<ShipViewModel> GetPlacedShip();
    }
}
