using BattleShip.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Model
{
    public class BoardModel:Dimensions
    {
        public LevelType Level { get; set; }
        public bool Occupied { get; set; }


        //public BoardCellStatus[,] BorderCellStatus { get; set; }
        //public int GetOccupied { get; set; }
       // public int HitCount { get; set; }
        //public int MissCount { get; set; }

    }
}
