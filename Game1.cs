using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Debug;
using System.Collections.Generic;

namespace running_out_of_time
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private RenderTarget2D target;
        private int gameWidth = 100, gameHeight = 100;
        FrameCounter frameCounter = new FrameCounter();

        SpriteFont font;
        Player player;
        Camera2D camera;

        public static List<CollisionManager.AABB> colliders = new List<CollisionManager.AABB>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            target = new RenderTarget2D(GraphicsDevice, gameWidth, gameHeight);
            camera = new Camera2D(GraphicsDevice.Viewport);
            colliders.Add(new CollisionManager.AABB(Vector2.One * 50, new Vector2(4000, 10)));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");
            player = new Player(Content.Load<Texture2D>("player"))
            {
                running = new Animation(Content.Load<Texture2D>("player_walk"), 16, 16, 1),
                position = Vector2.One * 40,
                speed = 10,
                gravity = new Vector2(0, 5f)
            };
            player.SetAnimation(player.running);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(target);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var viewMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: viewMatrix);

            player.Draw(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(target, new Rectangle(0, 0, 800, 800), Color.White);
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);

            spriteBatch.DrawString(font, fps, new Vector2(1, 1), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
