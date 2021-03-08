using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BattleShip.Model.Enum.Enum;

namespace BattleShip.Model
{
    public abstract class LevelModel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
        
}

