using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Service.Board
{
    public interface IBoardService
    {
        List<BoardModel> CreateBoard(int rows,int columns);
        List<BoardModel> GetBoard();
    }
}
