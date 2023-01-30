using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Particles;
using Satellite.Components;

namespace Satellite
{
    public class ParticleRenderSystem : EntityDrawSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<ParticleComponent> _particleComponentMapper;

        public ParticleRenderSystem(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(ParticleComponent)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _particleComponentMapper = mapperService.GetMapper<ParticleComponent>();
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.DarkBlue * 0.2f);
            _spriteBatch.Begin();
            
            foreach (var entityId in ActiveEntities)
            {
                var particleComponent = _particleComponentMapper.Get(entityId);
                _spriteBatch.Draw(particleComponent._particleEffect);
            }

            _spriteBatch.End();
        }

    }
}