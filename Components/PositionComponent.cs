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
        public Transform2 location = new Transform2(new Vector2(1920 / 2, 1080 / 2));
        public Transform2 velocity = new Transform2();

        public PositionComponent() { }

        public PositionComponent(Vector2 pos)
        {
            location.Position = pos;
        }

        public PositionComponent(Vector2 pos, float rot, Vector2 spd, float rotSpd = 0) {
            location.Position = pos;
            location.Rotation = rot;
            velocity.Position = spd;
            velocity.Rotation = rotSpd;
        }

    }
}
    

