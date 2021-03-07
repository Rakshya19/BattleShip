using BattleShip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShip.Service.Board
{
    public class BoardService:IBoardService
    {
        public BoardModel CreateBoard(int rows, int columns)
        {
            return new BoardModel();
        }
    }
}
