using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GADE6112___Task3
{
    class RangedUnit : Unit  {
        public RangedUnit(int x, int y, string faction) : base(x, y, 70, 3, 10, 5, faction, "Bowman") { }
        public void InitUnit(int x, int y, string faction)
        {
            x = UnityEngine.Random.Range(0, 5);
            y = UnityEngine.Random.Range(0, 5);
            this.x = x;
            this.y = y;
            this.faction = faction;
        }
        
    }
}
