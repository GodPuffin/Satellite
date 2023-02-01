using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Particles;
using Satellite.Components;

namespace Satellite
{
    public class ParticleSystem : EntityUpdateSystem
    {
        private ComponentMapper<ParticleComponent> _particleComponentMapper;

        public ParticleSystem() : base(Aspect.One(typeof(ParticleComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _particleComponentMapper = mapperService.GetMapper<ParticleComponent>();
        }

        public override void Update(GameTime gameTime)
        {

            foreach (var entityId in ActiveEntities)
            {
                var particleComponent = _particleComponentMapper.Get(entityId);

                particleComponent._particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}