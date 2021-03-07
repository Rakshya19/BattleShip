
using BattleShip.Model;

namespace BattleShip.Service.Board
{
    public interface IBoardService
    {
        BoardModel CreateBoard(int rows, int columns);
        
    }
}
