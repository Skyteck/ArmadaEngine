using ArmadaEngine.Camera;
using ArmadaEngine.Scenes.TestScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmadaEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scenes.SceneManager _SM = new Scenes.SceneManager();
        ArmadaCamera _Camera;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _Camera = new ArmadaCamera(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            //TestScene ts = new TestScene(Content, _SM, _Camera);
            //_SM.AddScene(ts);

            //SceneTwo st = new SceneTwo(Content, _SM, _Camera);
            //_SM.AddScene(st);

            //tmTestScene tm = new tmTestScene(Content, _SM, _Camera);
            //_SM.AddScene(tm);

            Scenes.Mega.MegaScene ms = new Scenes.Mega.MegaScene(Content, _SM, _Camera);
            _SM.AddScene(ms);

            //ParticleTestScene pts = new ParticleTestScene(Content, _SM, _Camera);
            //_SM.AddScene(pts);
            ////_SM.ActivateScene("Particle Test");

            Scenes.mm.mmScene mm = new Scenes.mm.mmScene(Content, _SM, _Camera);
            _SM.AddScene(mm);
            //_SM.ActivateScene("mm");

            Scenes.Sagey.SageyMainScene _sagey = new Scenes.Sagey.SageyMainScene(Content, _SM, _Camera);
            _SM.AddScene(_sagey);
            _SM.ActivateScene("Sagey");

            _Camera._Position = new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _SM.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //spriteBatch.Begin(SpriteSortMode.Deferred,
            //            BlendState.AlphaBlend,
            //            null,
            //            null,
            //            null,
            //            null,
            //            _Camera.GetTransform());

            //spriteBatch.Begin();

            // TODO: Add your drawing code here
            _SM.Draw(spriteBatch, GraphicsDevice.Viewport.Bounds);
        }
    }
}
