using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Service.Board
{
    public interface IBoardService
    {
        List<BoardModel> CreateBoard();

    }
}
