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

        PlayerSystem playerSystem = new PlayerSystem();

        public ParticleComponent StarEmitter = new ParticleComponent();

        //PLayer 2
        public ParticleComponent Trail2EmitterPnt = new ParticleComponent();
        public ParticleComponent Trail2EmitterPnt2 = new ParticleComponent();

        public SpriteComponent Player2Sprite = new SpriteComponent();

        public PositionComponent player2Pos = new PositionComponent();
        public InputComponent player2Input = new InputComponent(2);

        //PLayer 1
        public ParticleComponent Trail1EmitterSpr = new ParticleComponent();
        public ParticleComponent Trail1EmitterPnt = new ParticleComponent();

        public SpriteComponent Player1Sprite = new SpriteComponent();

        public PositionComponent player1Pos = new PositionComponent();
        public InputComponent player1Input = new InputComponent(1);

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
            .AddSystem(playerSystem)
            .AddSystem(new ParticleSystem())
            .AddSystem(new ParticleRenderSystem(GraphicsDevice))
            .AddSystem(new RenderSystem(GraphicsDevice))
            .AddSystem(new ProjectileSystem())
            .Build();

            var starsEntity = _world.CreateEntity();
            starsEntity.Attach(StarEmitter);

            var player = _world.CreateEntity();
            player.Attach(Player1Sprite);
            player.Attach(player1Pos);
            player.Attach(new PlayerComponent(1));
            player.Attach(player1Input);

            var player2 = _world.CreateEntity();
            player2.Attach(Player2Sprite);
            player2.Attach(player2Pos);
            player2.Attach(new PlayerComponent(2));
            player2.Attach(player2Input);

            var trail1Spr = _world.CreateEntity();
            trail1Spr.Attach(Trail1EmitterSpr);
            trail1Spr.Attach(new PositionComponent());

            var trail1Pnt = _world.CreateEntity();
            trail1Pnt.Attach(Trail1EmitterPnt);
            trail1Pnt.Attach(new PositionComponent());

            var trail2Pnt = _world.CreateEntity();
            trail2Pnt.Attach(Trail2EmitterPnt);
            trail2Pnt.Attach(new PositionComponent());

            var trail2Pnt2 = _world.CreateEntity();
            trail2Pnt2.Attach(Trail2EmitterPnt2);
            trail2Pnt2.Attach(new PositionComponent());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSystem.PassLaserTexture(Content.Load<Texture2D>("Laser"), 1);
            playerSystem.PassLaserTexture(Content.Load<Texture2D>("Laser-2"), 2);

            StarEmitter.LoadParticleContent("stars", GraphicsDevice);


            Player1Sprite.Texture = Content.Load<Texture2D>("Spaceship");

            Trail1EmitterSpr.LoadParticleContent("trail-spr", GraphicsDevice, Content.Load<Texture2D>("Thrust"));
            Trail1EmitterPnt.LoadParticleContent("trail", GraphicsDevice, Content.Load<Texture2D>("Thrust"));


            Player2Sprite.Texture = Content.Load<Texture2D>("Spaceship-3");

            Trail2EmitterPnt.LoadParticleContent("trail", GraphicsDevice, Content.Load<Texture2D>("Thrust-2"));
            Trail2EmitterPnt2.LoadParticleContent("trail", GraphicsDevice, Content.Load<Texture2D>("Thrust-2"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            //The trails kinda look better together then two seperate trails...

            //TODO: 
            //
            //Make it so that trail are related to player
            //
            //Move this into playerSystem {
            Trail1EmitterPnt._particleEffect.Position = player1Pos.location.Position;
            Trail1EmitterSpr._particleEffect.Position = player1Pos.location.Position;

            Trail1EmitterPnt._particleEffect.Rotation = -player1Pos.location.Rotation;
            Trail1EmitterSpr._particleEffect.Rotation = -player1Pos.location.Rotation;

            Trail1EmitterSpr._emitter.Profile = Profile.Spray(new Vector2((float)Math.Sin(player1Pos.location.Rotation - Math.PI / 4), (float)Math.Cos(player1Pos.location.Rotation - Math.PI / 4)), 1.5f);

            if (KeyboardExtended.GetState().IsKeyDown(player1Input.forwardKey) || KeyboardExtended.GetState().IsKeyDown(player1Input.backwardKey)){
                Trail1EmitterPnt._emitter.Parameters.Opacity = 1;
                Trail1EmitterSpr._emitter.Parameters.Opacity = 1;
            } else
            {
                Trail1EmitterPnt._emitter.Parameters.Opacity = 0;
                Trail1EmitterSpr._emitter.Parameters.Opacity = 0;
            }



            Trail2EmitterPnt._particleEffect.Position = new Vector2((float)(player2Pos.location.Position.X + (15 * Math.Cos(-player2Pos.location.Rotation))), (float)(player2Pos.location.Position.Y + (15 * Math.Sin(-player2Pos.location.Rotation))));
            Trail2EmitterPnt._particleEffect.Rotation = -player2Pos.location.Rotation;
            Trail2EmitterPnt2._particleEffect.Position = new Vector2((float)(player2Pos.location.Position.X + (-15 * Math.Cos(-player2Pos.location.Rotation))), (float)(player2Pos.location.Position.Y + (-15 * Math.Sin(-player2Pos.location.Rotation))));
            Trail2EmitterPnt2._particleEffect.Rotation = -player2Pos.location.Rotation;

            if (KeyboardExtended.GetState().IsKeyDown(player2Input.forwardKey) || KeyboardExtended.GetState().IsKeyDown(player2Input.backwardKey))
            {
                Trail2EmitterPnt._emitter.Parameters.Opacity = 1;
                Trail2EmitterPnt2._emitter.Parameters.Opacity = 1;
            }
            else
            {
                Trail2EmitterPnt._emitter.Parameters.Opacity = 0;
                Trail2EmitterPnt2._emitter.Parameters.Opacity = 0;
            }
            //}

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