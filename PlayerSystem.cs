using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using Satellite.Components;
using System;
using System.Diagnostics;

namespace Satellite
{
    public class PlayerSystem : EntityUpdateSystem
    {

        private ComponentMapper<PositionComponent> _positionMapper;
        private ComponentMapper<PlayerComponent> _playerMapper;

        private const float _friction = 1.03f;
        public PlayerSystem() : base(Aspect.All(typeof(PlayerComponent), typeof(PositionComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<PositionComponent>();
            _playerMapper = mapperService.GetMapper<PlayerComponent>();
        }

        public override void Update(GameTime gameTime)
        {

            foreach (var entityId in ActiveEntities)
            {

                Vector2 Pos = _positionMapper.Get(entityId).location.Position;
                Vector2 Spd = _positionMapper.Get(entityId).velocity.Position;

                float playerSpd = _playerMapper.Get(entityId).spd;
                float Rot = _positionMapper.Get(entityId).location.Rotation;
                float spdRot = _positionMapper.Get(entityId).velocity.Rotation;

                if (KeyboardExtended.GetState().IsKeyDown(Keys.A))
                {
                    spdRot += 0.007f;
                }
                if (KeyboardExtended.GetState().IsKeyDown(Keys.D))
                {
                    spdRot -= 0.007f;
                }
                if (KeyboardExtended.GetState().IsKeyDown(Keys.W))
                {
                    playerSpd += 0.4f;
                }
                if (KeyboardExtended.GetState().IsKeyDown(Keys.S))
                {
                    playerSpd -= 0.15f;
                }

                Spd.X -= (float)(playerSpd * Math.Sin(Rot));
                Spd.Y -= (float)(playerSpd * Math.Cos(Rot));

                Pos.X += Spd.X;
                Pos.Y += Spd.Y;

                Rot += spdRot;

                //playerSpd /= _friction; - for like a car-ish drift effect
                playerSpd = 0;
                Spd.X /= _friction;
                Spd.Y /= _friction;
                spdRot /= _friction;

                _positionMapper.Get(entityId).location.Position = Pos;
                _positionMapper.Get(entityId).velocity.Position = Spd;
                _playerMapper.Get(entityId).spd = playerSpd;
                _positionMapper.Get(entityId).location.Rotation = Rot;
                _positionMapper.Get(entityId).velocity.Rotation = spdRot;

            }
        }
    }
}