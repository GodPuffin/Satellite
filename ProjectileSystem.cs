using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Input;
using Satellite.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Satellite
{
    public class ProjectileSystem : EntityUpdateSystem
    {
        private ComponentMapper<PositionComponent> _positionMapper;
        private ComponentMapper<ProjectileComponent> _projectileMapper;

        public ProjectileSystem() : base(Aspect.All(typeof(ProjectileComponent), typeof(PositionComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _positionMapper = mapperService.GetMapper<PositionComponent>();
            _projectileMapper = mapperService.GetMapper<ProjectileComponent>();
        }

        public override void Update(GameTime gameTime)
        {

            foreach (var entityId in ActiveEntities)
            {
                Vector2 Pos = _positionMapper.Get(entityId).location.Position;
                Vector2 Spd = _positionMapper.Get(entityId).velocity.Position;
                float Rot = _positionMapper.Get(entityId).location.Rotation;
                float spdRot = _positionMapper.Get(entityId).velocity.Rotation;

                Pos.X += Spd.X;
                Pos.Y += Spd.Y;

                Rot += spdRot;

                if (Pos.X < -80 || Pos.X > 2000)
                {
                    DestroyEntity(entityId);
                }
                if (Pos.Y < -80 || Pos.Y > 1160)
                {
                    DestroyEntity(entityId);
                }

                _positionMapper.Get(entityId).location.Position = Pos;
                _positionMapper.Get(entityId).velocity.Position = Spd;
                _positionMapper.Get(entityId).location.Rotation = Rot;
                _positionMapper.Get(entityId).velocity.Rotation = spdRot;

            }
        }
    }
}

