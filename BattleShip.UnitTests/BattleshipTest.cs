using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleShip.Service.Board;
using BattleShip.Service.Ship;
using static BattleShip.Model.Enum.Enum;
using Xunit;

namespace BattleShip.UnitTests
{
    [TestClass]
    public class BattleshipTest
    {
        [Theory]
       [InlineData(1,2,"BattleShip","Vertical",1,5, ShipAndBoardstatus.Miss)]
        [InlineData(1, 2, "BattleShip", "Vertical", 2, 5, ShipAndBoardstatus.Miss)]
        [InlineData(1, 2, "BattleShip", "Horizontal", 1, 5, ShipAndBoardstatus.Miss)]
        [InlineData(1, 2, "BattleShip", "Horizontal", 2, 5, ShipAndBoardstatus.Hit)]
        [TestMethod]
        public void TestBattleShipAttack(int shipRow, int ShipColumn,string ShipType,string shipOrientation,int attackRow,int attackColumn, ShipAndBoardstatus status)
        {
            //First create board
            var boardService = new BoardService();
            var board = boardService.CreateBoard();

            //Then create ship
            var shipService = new ShipService();
            var ship = shipService.CreateShip(ShipType);

            //Now place the ship in the board
            var PlaceShipInBoard = shipService.PlaceShipInBoard(ship, shipOrientation, shipRow, ShipColumn, board);

            var AttackAndBoardStatus = shipService.AttackShip(attackRow, attackColumn, PlaceShipInBoard.ListBoardModel, PlaceShipInBoard.ListShipViewModel);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(AttackAndBoardStatus == status);

        }
    }
}
