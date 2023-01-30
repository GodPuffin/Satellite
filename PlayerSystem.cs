using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Satellite.Components;

namespace Satellite
{
    internal class PlayerSystem : EntityUpdateSystem
    {

        private ComponentMapper<PositionComponent> _positionMapper;
        private ComponentMapper<PlayerComponent> _playerMapper;
        private ComponentMapper<SpriteComponent> _spriteMapper;

        private const float _friction = 1.1f;
        public PlayerSystem() : base(Aspect.All(typeof(PlayerComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<PositionComponent>();
            _playerMapper = mapperService.GetMapper<PlayerComponent>();
            _spriteMapper = mapperService.GetMapper<SpriteComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            var elapsedSeconds = gameTime.GetElapsedSeconds();

            foreach (var entityId in ActiveEntities)
            {
                
            }
        }
    }
}