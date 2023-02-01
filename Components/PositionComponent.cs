using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite.Components
{
    public class PositionComponent
    {

        public Transform2 location = new Transform2(new Vector2(1920/2,1080/2));
        public Transform2 velocity = new Transform2();

    }
}
    

