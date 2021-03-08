
using BattleShip.Model;
using System.Collections.Generic;

namespace BattleShip.Service.Board
{
    public interface IBoardService
    {
        List<BoardModel> CreateBoard();
        
    }
}
