using Assignment.Levels;
using Assignment.Objects;
using Assignment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;

namespace Assignment
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Statemanager
        RC_GameStateManager levelManager;

        public static Game1 game1;

        //Random
        public static Random Random = new Random();

        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 800;

        public static float TimeScore = 0;

        public static float[] Scores = new float[5];
        public static float[] ChallengeScores = new float[5];

        public bool toggleHelp = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;



            this.Window.Title = "Assignment HypeReset";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
            game1 = this;
            for (int i = 0; i < 5; i++)
            {
                Scores[i] = 0;
                ChallengeScores[i] = 0;
            }





            }

        public static void AddScore(float newTimeScore)
        {
            for(int i = 0; i < 5; i++)
            {
                if(Scores[i] == 0) {
                    Scores[i] = newTimeScore;
                    break;
                }
                if (Scores[i] > newTimeScore)
                {
                    //move everything down by one then assign.
                    for (int j = 4; j >= i + 1; j--)
                    {
                        Scores[j] = Scores[j - 1];
                    }

                    Scores[i] = newTimeScore;
                    break;
                }
            }
        }

        public static void AddChallengeScore(float newTimeScore)
        {
            for (int i = 0; i < 5; i++)
            {
                if (ChallengeScores[i] == 0)
                {
                    ChallengeScores[i] = newTimeScore;
                    break;
                }
                if (ChallengeScores[i] > newTimeScore)
                {
                    //move everything down by one then assign.
                    for (int j = 4; j >= i + 1; j--)
                    {
                        ChallengeScores[j] = ChallengeScores[j - 1];
                    }

                    ChallengeScores[i] = newTimeScore;
                    break;
                }
            }
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

            // TODO: use this.Content to load your game content here
            levelManager = new RC_GameStateManager();
            levelManager.AddLevel(0, new TutorialLevel());
            levelManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(0).LoadContent();


            levelManager.AddLevel(1, new Level1());
            levelManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(1).LoadContent();


            levelManager.AddLevel(2, new Level2());
            levelManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(2).LoadContent();

            levelManager.AddLevel(3, new Level3());
            levelManager.getLevel(3).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(3).LoadContent();

            levelManager.AddLevel(4, new Level4());
            levelManager.getLevel(4).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(4).LoadContent();

            levelManager.AddLevel(5, new Level5());
            levelManager.getLevel(5).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(5).LoadContent();

            levelManager.AddLevel(6, new MenuScreen());
            levelManager.getLevel(6).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(6).LoadContent();

            levelManager.setLevel(6);

            Player.soundDash = Content.Load<SoundEffect>("sounds/Dash");
            Player.soundJump = Content.Load<SoundEffect>("sounds/Jump");
            Player.soundDoubleJump = Content.Load<SoundEffect>("sounds/DoubleJump");

            BaseLevel.soundDeath = Content.Load<SoundEffect>("sounds/Death");
            BaseLevel.soundSlice = Content.Load<SoundEffect>("sounds/Slice");
            BaseLevel.soundRespawn = Content.Load<SoundEffect>("sounds/Respawn");
            BaseLevel.soundSliceHit = Content.Load<SoundEffect>("sounds/SliceHit");
            Player.soundGroundHit = Content.Load<SoundEffect>("sounds/GroundHit");
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
            
            InputManager.Instance.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (InputManager.Instance.KeyPressed(Keys.F1))
            {
                toggleHelp = !toggleHelp;
            }

            levelManager.getCurrentLevel().Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            levelManager.getCurrentLevel().Draw(gameTime);

            if (toggleHelp)
            {
                spriteBatch.Begin();
                TextRenderable helpStuff = new TextRenderable("GAMEPAD: A to jump/select. B to dash. RB to slice.", new Vector2(100, 100), MenuScreen.menuFont, Color.Black);
                helpStuff.Draw(spriteBatch);
                helpStuff = new TextRenderable("KEYBOARD: Z to jump/select. C to dash. X to slice. ARROWS to move", new Vector2(100, 150), MenuScreen.menuFont, Color.Black);
                helpStuff.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
