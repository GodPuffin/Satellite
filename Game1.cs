using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using Satellite.Components;

namespace Satellite
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World _world;

        public Vector2 windowSize = new Vector2(1920, 1080);
        public ParticleComponent StarEmitter = new ParticleComponent();

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
            .Build();

            var starsEntity = _world.CreateEntity();
            starsEntity.Attach(StarEmitter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            StarEmitter.LoadParticleContent("stars", GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _world.Update(gameTime);
            StarEmitter._particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _world.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}