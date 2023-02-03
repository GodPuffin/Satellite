using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite.Components
{
    public class InputComponent
    {
        public Keys forwardKey, backwardKey, rotLeftKey, rotRightKey, shootKey, abilityKey;

        public InputComponent(int playerNum)
        {
            if (playerNum == 1)
            {
                forwardKey = Keys.Up;
                backwardKey = Keys.Down;
                rotLeftKey = Keys.Left;
                rotRightKey = Keys.Right;
                shootKey = Keys.OemPeriod;
                abilityKey = Keys.OemComma;
            } else if(playerNum == 2)
            {
                forwardKey = Keys.W;
                backwardKey = Keys.S;
                rotLeftKey = Keys.A;
                rotRightKey = Keys.D;
                shootKey = Keys.C;
                abilityKey = Keys.X;
            }
        }
    }
}
