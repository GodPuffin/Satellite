using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using Satellite.Components;
using System;
using System.Diagnostics;
using System.Security.AccessControl;

namespace Satellite
{
    public class PlayerSystem : EntityUpdateSystem
    {

        private ComponentMapper<PositionComponent> _positionMapper;
        private ComponentMapper<PlayerComponent> _playerMapper;
        private ComponentMapper<InputComponent> _inputMapper;

        private Texture2D laserTexture1, laserTexture2;

        private const float _friction = 1.03f;
        public PlayerSystem() : base(Aspect.All(typeof(PlayerComponent), typeof(PositionComponent), typeof(InputComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<PositionComponent>();
            _playerMapper = mapperService.GetMapper<PlayerComponent>();
            _inputMapper = mapperService.GetMapper<InputComponent>();
        }

        public override void Update(GameTime gameTime)
        {

            foreach (var entityId in ActiveEntities)
            {

                Vector2 Pos = _positionMapper.Get(entityId).location.Position;
                Vector2 Spd = _positionMapper.Get(entityId).velocity.Position;

                float playerSpd = _playerMapper.Get(entityId).currentSpd;
                float Rot = _positionMapper.Get(entityId).location.Rotation;
                float spdRot = _positionMapper.Get(entityId).velocity.Rotation;

                if (KeyboardExtended.GetState().IsKeyDown(_inputMapper.Get(entityId).rotLeftKey))
                {
                    spdRot += _playerMapper.Get(entityId).rotSpd;
                }
                if (KeyboardExtended.GetState().IsKeyDown(_inputMapper.Get(entityId).rotRightKey))
                {
                    spdRot -= _playerMapper.Get(entityId).rotSpd;
                }
                if (KeyboardExtended.GetState().IsKeyDown(_inputMapper.Get(entityId).forwardKey))
                {
                    playerSpd += _playerMapper.Get(entityId).forwardSpd;
                }
                if (KeyboardExtended.GetState().IsKeyDown(_inputMapper.Get(entityId).backwardKey))
                {
                    playerSpd -= _playerMapper.Get(entityId).backwardSpd;
                }

                Spd.X -= (float)(playerSpd * Math.Sin(Rot));
                Spd.Y -= (float)(playerSpd * Math.Cos(Rot));

                Pos.X += Spd.X;
                Pos.Y += Spd.Y;

                Rot += spdRot;

                //playerSpd /= _friction; - for like a car-ish drift effect
                playerSpd = 0; // - more space-like glide
                Spd.X /= _friction;
                Spd.Y /= _friction;
                spdRot /= _friction + 0.1f;

                int Cooldown = _playerMapper.Get(entityId).LsrCoolDwn;
                Cooldown++;

                if (KeyboardExtended.GetState().IsKeyDown(_inputMapper.Get(entityId).shootKey))
                {
                    if (_playerMapper.Get(entityId).shipNum == 1)
                    {
                        if (Cooldown % 8 == 0)
                        {
                            CreateLaser(new Vector2((float)(Pos.X + (20 * Math.Cos(-Rot))), (float)(Pos.Y + (20 * Math.Sin(-Rot)))), Rot, new Vector2((float)(Spd.X + (-10 * Math.Sin(Rot))), (float)(Spd.Y + (-10 * Math.Cos(Rot)))), laserTexture1);
                        }
                        if (Cooldown % 8 == 4)
                        {
                            CreateLaser(new Vector2((float)(Pos.X + (-20 * Math.Cos(-Rot))), (float)(Pos.Y + (-20 * Math.Sin(-Rot)))), Rot, new Vector2((float)(Spd.X + (-10 * Math.Sin(Rot))), (float)(Spd.Y + (-10 * Math.Cos(Rot)))), laserTexture1);
                        }
                        if (Cooldown >= 65)
                        {
                            Cooldown = 0;
                        }
                    }
                    if (_playerMapper.Get(entityId).shipNum == 2)
                    {
                        if (Cooldown > 64)
                        {
                            CreateLaser(Pos, Rot, new Vector2((float)(Spd.X + (-25 * Math.Sin(Rot))), (float)(Spd.Y + (-25 * Math.Cos(Rot)))), laserTexture2);
                            Cooldown = 0;
                            Spd.X += (float)(5 * Math.Sin(Rot));
                            Spd.Y += (float)(5 * Math.Cos(Rot));

                        }
                    }
                }

                _playerMapper.Get(entityId).LsrCoolDwn = Cooldown;

                _positionMapper.Get(entityId).location.Position = Pos;
                _positionMapper.Get(entityId).velocity.Position = Spd;
                _playerMapper.Get(entityId).currentSpd = playerSpd;
                _positionMapper.Get(entityId).location.Rotation = Rot;
                _positionMapper.Get(entityId).velocity.Rotation = spdRot;
            }
        }

        private int CreateLaser(Vector2 pos, float rot, Vector2 spd, Texture2D sprite)
        {
            var laserEntity = CreateEntity();
            laserEntity.Attach(new PositionComponent(pos, rot, spd));
            laserEntity.Attach(new ProjectileComponent());
            laserEntity.Attach(new SpriteComponent(sprite));
            return laserEntity.Id;

        }

        public void PassLaserTexture(Texture2D texture, int num)
        {
            if (num == 1)
            {
                laserTexture1 = texture;
            }
            if (num == 2)
            {
                laserTexture2 = texture;
            }
        }
    }
}