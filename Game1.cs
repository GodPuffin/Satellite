using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Input;
using MonoGame.Extended.Particles;
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
        public ParticleComponent TrailEmitterR = new ParticleComponent();
        public ParticleComponent TrailEmitterL = new ParticleComponent();

        public SpriteComponent PlayerSprite = new SpriteComponent();
        //public SpriteComponent TrailSprite = new SpriteComponent();

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

            var trailR = _world.CreateEntity();
            //trailR.Attach(TrailSprite);
            trailR.Attach(TrailEmitterR);
            trailR.Attach(new PositionComponent());

            var trailL = _world.CreateEntity();
            //trailL.Attach(TrailSprite);
            trailL.Attach(TrailEmitterL);
            trailL.Attach(new PositionComponent());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerSprite.Texture = Content.Load<Texture2D>("Spaceship");
            //TrailSprite.Texture = Content.Load<Texture2D>("Thrust");

            StarEmitter.LoadParticleContent("stars", GraphicsDevice);

            TrailEmitterR.LoadParticleContent("trail", GraphicsDevice, Content.Load<Texture2D>("Thrust"));
            TrailEmitterL.LoadParticleContent("trail", GraphicsDevice, Content.Load<Texture2D>("Thrust"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //The trails kinda look better together then two seperate trails...
            TrailEmitterL._particleEffect.Position = playerPos.location.Position;
            TrailEmitterR._particleEffect.Position = playerPos.location.Position;
            TrailEmitterL._particleEffect.Rotation = -playerPos.location.Rotation;
            TrailEmitterR._particleEffect.Rotation = -playerPos.location.Rotation;

            if (KeyboardExtended.GetState().IsKeyDown(Keys.W) || KeyboardExtended.GetState().IsKeyDown(Keys.S)){
                TrailEmitterL._emitter.Parameters.Opacity = 1;
                TrailEmitterR._emitter.Parameters.Opacity = 1;
            } else
            {
                TrailEmitterL._emitter.Parameters.Opacity = 0;
                TrailEmitterR._emitter.Parameters.Opacity = 0;
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