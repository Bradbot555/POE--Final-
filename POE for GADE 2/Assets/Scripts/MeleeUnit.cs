using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GADE6112___Task3
{
    class MeleeUnit : Unit {
        public MeleeUnit(int x, int y, string faction) : base(x,y,100,2,20,1,faction,"Axeman") { }
        public void InitUnit(int x, int y, string faction)
        {
            this.x = x;
            this.y = y;
            this.faction = faction;
        }
       
    }
}
