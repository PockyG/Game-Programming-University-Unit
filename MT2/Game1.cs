using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using RC_Framework;

//I asked my friend for an idea of a boss
//big plane doesnt move covers most screen
//shoots big  delayed lazers get bigger.
//When killed splits into two different middle sized planes(same tex maybe dif color)
//releases a bullet that travels to middle of screenish and then explodes into smaller bullets that rotate
//when killed 3 smaller planes fly out towards the screen boundaries and explode into radius of bullets.

namespace MT2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;

        public static bool showbb = false;



        //Statemanager
        RC_GameStateManager levelManager;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            this.Window.Title = "mt2 wow";



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


            //base initialize calls loadcontent
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
            LineBatch.init(GraphicsDevice);

            levelManager = new RC_GameStateManager();

            levelManager.AddLevel(0, new SplashScreen());
            levelManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(0).LoadContent();
            levelManager.setLevel(0);

            levelManager.AddLevel(1, new GameLevel1());
            levelManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(1).LoadContent();

            levelManager.AddLevel(2, new EndScreen());
            levelManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(2).LoadContent();

            levelManager.AddLevel(3, new Pause());
            levelManager.getLevel(3).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(3).LoadContent();

            levelManager.AddLevel(4, new StartScreen());
            levelManager.getLevel(4).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(4).LoadContent();
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
            // TODO: Add your update logic here
            InputManager.Instance.Update();

            //inputs
            if (InputManager.Instance.KeyPressed(Keys.B))
            {
                Game1.showbb = !Game1.showbb;
            }
            if (InputManager.Instance.KeyPressed(Keys.Escape))
            {
                Exit();
            }

            levelManager.getCurrentLevel().Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            levelManager.getCurrentLevel().Draw(gameTime);

            base.Draw(gameTime);
        }


     


    }
}
