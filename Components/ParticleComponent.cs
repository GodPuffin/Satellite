
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace Satellite.Components
{
    public class ParticleComponent
    {

        public ParticleEffect _particleEffect;
        public Texture2D _particleTexture;
        public ParticleEmitter _emitter;

        public ParticleComponent()
        { }

        public void LoadParticleContent(string type, GraphicsDevice graphicsDevice, Texture2D texture = null) {

            if (type.Equals("null"))
            {

            }
            else if (type.Equals("trail-spr"))
            {

                _particleTexture = texture;

                TextureRegion2D textureRegion = new TextureRegion2D(_particleTexture);

                _emitter = new ParticleEmitter(textureRegion, 100, TimeSpan.FromSeconds(0.75),
                Profile.Spray(new Vector2(0, 0), 1.5f))
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(10f, 50f),
                        Quantity = 1,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(0.9f, 1.1f)
                    },
                    Modifiers =
            {
                new AgeModifier()
            {
                Interpolators = new List<Interpolator>()
                {
                    new ScaleInterpolator { StartValue = new Vector2(1,1), EndValue = new Vector2(0,0) }
                 }
            }
            }
                };

                _particleEffect = new ParticleEffect(autoTrigger: false)
                {
                    //Position = Position,
                    Emitters = new List<ParticleEmitter>
                {
                _emitter
                }
                };

            } else if (type.Equals("trail")) {

                _particleTexture = texture;

                TextureRegion2D textureRegion = new TextureRegion2D(_particleTexture);

                _emitter = new ParticleEmitter(textureRegion, 100, TimeSpan.FromSeconds(0.75),
                Profile.Point())
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(10f, 50f),
                        Quantity = 1,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(0.9f, 1.1f)
                    },
                    Modifiers =
            {
                new RotationModifier {RotationRate = -1f},
                new AgeModifier()
            {
                Interpolators = new List<Interpolator>()
                {
                    new ScaleInterpolator { StartValue = new Vector2(1,1), EndValue = new Vector2(0,0) }
                 }
            }
            }
                };

                _particleEffect = new ParticleEffect(autoTrigger: false)
                {
                   //Position = Position,
                    Emitters = new List<ParticleEmitter>
                {
                _emitter
                }
                };

            }
            else
            {
                _particleTexture = new Texture2D(graphicsDevice, 1, 1);
                _particleTexture.SetData(new[] { Color.White });

                TextureRegion2D textureRegion = new TextureRegion2D(_particleTexture);

                _emitter = new ParticleEmitter(textureRegion, 500, TimeSpan.MaxValue,
                Profile.BoxFill(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height))
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(0f, 5f),
                        Quantity = 5,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(3.0f, 4.0f)
                    },
                    Modifiers =
            {
                new RotationModifier {RotationRate = -1f},
            }
                };

                _particleEffect = new ParticleEffect(autoTrigger: false)
                {
                    Position = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2),
                    Emitters = new List<ParticleEmitter>
                {
                _emitter
                }
                };
            }

        }
    }
}
