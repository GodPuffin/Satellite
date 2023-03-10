using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Satellite.Components;
using MonoGame.Extended.TextureAtlases;

namespace Satellite
{
    public class RenderSystem : EntityDrawSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<PositionComponent> _positionMapper;
        private ComponentMapper<SpriteComponent> _spriteMapper;

        public RenderSystem(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(PositionComponent), typeof(SpriteComponent)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<PositionComponent>();
            _spriteMapper = mapperService.GetMapper<SpriteComponent>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            foreach (var entityID in ActiveEntities)
            {
                TextureRegion2D textureRegion2D = new TextureRegion2D(_spriteMapper.Get(entityID).Texture);
                _spriteBatch.Draw(_spriteMapper.Get(entityID).Texture, _positionMapper.Get(entityID).location.Position, null, Color.White, -_positionMapper.Get(entityID).location.Rotation, new Vector2(textureRegion2D.Width/2, textureRegion2D.Height/2), new Vector2(1, 1), new SpriteEffects(), 0f);
            }
            _spriteBatch.End();
        }
    }
}
