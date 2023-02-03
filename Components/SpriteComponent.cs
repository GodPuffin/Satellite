using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite.Components
{
    public class SpriteComponent
    {

        public Texture2D Texture;

        public SpriteComponent() { }
        public SpriteComponent(Texture2D texture)
        {
            Texture = texture;
        }

    }
}
