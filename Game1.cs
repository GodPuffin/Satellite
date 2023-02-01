using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Input;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using Satellite.Components;
using System;
using System.Threading;

namespace Satellite
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World _world;

        public Vector2 windowSize = new Vector2(1920, 1080);

        public ParticleComponent StarEmitter = new ParticleComponent();
        public ParticleComponent TrailEmitterSpr = new ParticleComponent();
        public ParticleComponent TrailEmitterPnt = new ParticleComponent();

        public SpriteComponent PlayerSprite = new SpriteComponent();

        public PositionComponent playerPos = new PositionComponent();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = (int) windowSize.X;
            _graphics.PreferredBackBufferHeight = (int) windowSize.Y;
            _graphics.ToggleFullScreen();

            _world = new WorldBuilder()
            .AddSystem(new PlayerSystem())
            .AddSystem(new ParticleSystem())
            .AddSystem(new ParticleRenderSystem(GraphicsDevice))
            .AddSystem(new RenderSystem(GraphicsDevice))
            .Build();

            var starsEntity = _world.CreateEntity();
            starsEntity.Attach(StarEmitter);

            var player = _world.CreateEntity();
            player.Attach(PlayerSprite);
            player.Attach(playerPos);
            player.Attach(new PlayerComponent());

            var trailSpr = _world.CreateEntity();
            trailSpr.Attach(TrailEmitterSpr);
            trailSpr.Attach(new PositionComponent());

            var trailPnt = _world.CreateEntity();
            trailPnt.Attach(TrailEmitterPnt);
            trailPnt.Attach(new PositionComponent());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerSprite.Texture = Content.Load<Texture2D>("Spaceship");

            StarEmitter.LoadParticleContent("stars", GraphicsDevice);

            TrailEmitterSpr.LoadParticleContent("trail-spr", GraphicsDevice, Content.Load<Texture2D>("Thrust"));
            TrailEmitterPnt.LoadParticleContent("trail", GraphicsDevice, Content.Load<Texture2D>("Thrust"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            //The trails kinda look better together then two seperate trails...
            TrailEmitterPnt._particleEffect.Position = playerPos.location.Position;
            TrailEmitterSpr._particleEffect.Position = playerPos.location.Position;

            TrailEmitterPnt._particleEffect.Rotation = -playerPos.location.Rotation;
            TrailEmitterSpr._particleEffect.Rotation = -playerPos.location.Rotation;

            TrailEmitterSpr._emitter.Profile = Profile.Spray(new Vector2((float)Math.Sin(playerPos.location.Rotation - Math.PI / 4), (float)Math.Cos(playerPos.location.Rotation - Math.PI / 4)), 1.5f);

            if (KeyboardExtended.GetState().IsKeyDown(Keys.W) || KeyboardExtended.GetState().IsKeyDown(Keys.S)){
                TrailEmitterPnt._emitter.Parameters.Opacity = 1;
                TrailEmitterSpr._emitter.Parameters.Opacity = 1;
            } else
            {
                TrailEmitterPnt._emitter.Parameters.Opacity = 0;
                TrailEmitterSpr._emitter.Parameters.Opacity = 0;
            }


            _world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _world.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}