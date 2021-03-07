using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShip.Model
{
    public class BoardModel
    {
        public int Rows { get; set; }
        public int Column { get; set; }
        public int GetOccupied { get; set; }
        public int HitCount { get; set; }
        public int MissCount { get; set; }
    }
}
