using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite.Components
{
    public class PlayerComponent
    {
        public float currentSpd = 0;
        public int LsrCoolDwn = 1;

        public float forwardSpd, backwardSpd, rotSpd;
        public int shipNum;

        public PlayerComponent(int type)
        {
            shipNum = type;

            if(type == 1)
            {
                rotSpd = 0.007f;
                forwardSpd = 0.4f;
                backwardSpd = 0.15f;
            } else if(type == 2)
            {
                rotSpd = 0.005f;
                forwardSpd = 0.3f;
                backwardSpd = 0.1f;
            }
        }
    }
}
