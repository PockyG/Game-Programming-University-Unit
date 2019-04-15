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

namespace MT1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texBackground = null;
        Texture2D texForeground = null;
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;

        //player
        Sprite3 spritePlayer;
        Texture2D playerTex;
        int playerSpeed = 4;

        //particles?
        SpriteList particleList;
        Texture2D explosionTex;

        //enemies
        SpriteList enemyList;
        Texture2D enemyTex;

        //bullets
        SpriteList playerBulletList;
        Texture2D bulletTex;

        //boss
        Sprite3 boss;
        Texture2D bossTex;

        //Area player can move in
        Rectangle playArea;
        int lhs = 20;
        //int rhs = SCREEN_WIDTH - 50;
        int rhs = 250;
        int bot = 50;
        int top = SCREEN_HEIGHT - 50;




        //set spawninterval to 0.01f to see effects of "spritelist" (NOT A LIST) its a 200 default array pretending to be a list.  original value = 2;
        float timer = 0;
        float spawnInterval = 2;

        bool startGame = false;

        //This changes the scroll background speed
        float playerSpeeeeeed = 0;


        bool showbb = false;


        //scrolling backgrounds
        ScrollBackGround scrollBack;
        ScrollBackGround scrollFore;

        //text
        TextRenderable textScore;
        TextRenderable textStartGame;
        int score = 0;
        SpriteFont spriteFont;



        float deltaTime = 0;
        //randomclass
        Random random;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            this.Window.Title = "mt1";



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


            random = new Random();
            playArea = new Rectangle(lhs, top, rhs - lhs, bot - top);
            spritePlayer = new Sprite3(true, playerTex, 50, SCREEN_HEIGHT / 2 - 50);
            spritePlayer.setWidthHeight(100, 100);
            spritePlayer.setFlip(SpriteEffects.FlipHorizontally);
            //spritePlayer.setBB(0,0,200, 200);



            scrollBack = new ScrollBackGround(texBackground, texBackground.Bounds, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), -1 + playerSpeeeeeed, 2);
            scrollFore = new ScrollBackGround(texForeground, texForeground.Bounds, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), -2 + playerSpeeeeeed, 2);
            textScore = new TextRenderable("Score : " + score.ToString(), new Vector2(100, 100), spriteFont, Color.Red);
            textStartGame = new TextRenderable("PRESS ENTER TO START", new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2), spriteFont, Color.Red);
            particleList = new SpriteList();
            enemyList = new SpriteList();
            playerBulletList = new SpriteList();





        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texBackground = Content.Load<Texture2D>("background");
            texForeground = Content.Load<Texture2D>("foreground");
            spriteFont = Content.Load<SpriteFont>("file");
            playerTex = Content.Load<Texture2D>("SShip4m");

            explosionTex = Content.Load<Texture2D>("Boom3");
            enemyTex = Content.Load<Texture2D>("Black1M");

            bulletTex = Content.Load<Texture2D>("purple32x32d");
            LineBatch.init(GraphicsDevice);
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



            //deltaTime is in seconds, so calculations = pixels per second.
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Press enter to start game/level
            if (!startGame)
            {
                if (InputManager.Instance.KeyPressed(Keys.Enter))
                {
                    startGame = true;
                }


                return;
            }

            timer += deltaTime;

            //update player
            spritePlayer.Update(gameTime);

            //update backgrounds
            scrollBack.Update(gameTime);
            scrollFore.Update(gameTime);
            scrollBack.setScrollSpeed(playerSpeeeeeed - 2);
            scrollFore.setScrollSpeed(playerSpeeeeeed);

            //limit scroll speed
            if (playerSpeeeeeed > -1)
            {
                playerSpeeeeeed = -1;
            }
            else if (playerSpeeeeeed < -10)
            {
                playerSpeeeeeed = -10;
            }

            //Every 2 seconds, spawn an enemy randomly.
            if (timer > spawnInterval)
            {
                AddEnemy(enemyList);
                timer = 0;
            }

            //inputs
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (InputManager.Instance.KeyPressed(Keys.B))
            {
                showbb = !showbb;
            }

            //PLAYER MOVEMENT
            if (InputManager.Instance.KeyDown(Keys.Left))
            {
                if (spritePlayer.getPosX() > playArea.Left)
                {
                    spritePlayer.setPosX(spritePlayer.getPosX() - playerSpeed);

                }
                playerSpeeeeeed += 0.1f;
            }
            if (InputManager.Instance.KeyDown(Keys.Right))
            {
                if (spritePlayer.getPosX() < playArea.Right - spritePlayer.getWidth())
                {
                    spritePlayer.setPosX(spritePlayer.getPosX() + playerSpeed);

                }
                playerSpeeeeeed += -0.1f;
            }
            if (InputManager.Instance.KeyDown(Keys.Up))
            {

                if (spritePlayer.getPosY() > playArea.Bottom)
                {
                    spritePlayer.setPosY(spritePlayer.getPosY() - playerSpeed);
                }
            }
            if (InputManager.Instance.KeyDown(Keys.Down))
            {
                if (spritePlayer.getPosY() < playArea.Top - spritePlayer.getHeight())
                {
                    spritePlayer.setPosY(spritePlayer.getPosY() + playerSpeed);
                }
            }
            //PLAYER SHOOT
            if (InputManager.Instance.KeyPressed(Keys.Space))
            {
                AddBullet((int)spritePlayer.getPosX(), (int)spritePlayer.getPosY());
            }

            //Update all enemies and bullets to move. also inactive if outofbounds
            //In the past I called this instead of having an extended class with custom update
            // enemyList.moveDeltaX(-3);

            
            //Update/remove enemies
            enemyList.Update(gameTime);
            enemyList.removeIfOutside(new Rectangle(-100, -100, SCREEN_WIDTH + 200, SCREEN_HEIGHT + 200)); 

            //update/remove bullets
            playerBulletList.moveDeltaX(5);
            playerBulletList.removeIfOutside(new Rectangle(-100, -100, SCREEN_WIDTH + 200, SCREEN_HEIGHT + 200));



            //If player collides with enemy
            int collisionIndex = enemyList.collisionAA(spritePlayer);
            if (collisionIndex != -1)
            {
                Sprite3 temp = enemyList.getSprite(collisionIndex);
                //player collides with boss
                if (temp.getName() == "boss")
                {

                }
                else
                {
                    //enemy dies?
                    temp.active = false;
                    temp.visible = false;
                    AddExplosion((int)temp.getPosX(), (int)temp.getPosY());
                    score -= 5;
                    textScore.text = "Score : " + score.ToString();
                }
            }

            //if a bullet collides with enemy
            for (int i = 0; i < playerBulletList.count(); i++)
            {
                Sprite3 currentBullet = playerBulletList.getSprite(i);
                if (currentBullet == null) continue;
                if (currentBullet.active != true) continue;
                if (currentBullet.visible != true) continue;

                //if collision success
                int collision = enemyList.collisionAA(currentBullet);
                if (collision != -1)
                {
                    Sprite3 currentEnemy = enemyList.getSprite(collision);

                    //kill both enemy and bullet
                    currentEnemy.active = false;
                    currentEnemy.visible = false;
                    currentBullet.active = false;
                    currentBullet.visible = false;

                    //add particle
                    AddExplosion((int)currentEnemy.getPosX(), (int)currentEnemy.getPosY());

                    //add score
                    score += 10;
                    textScore.text = "Score : " + score.ToString();
                }
            }

            //update particles
            particleList.animationTick(gameTime);



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);



            // TODO: Add your drawing code here
            spriteBatch.Begin();

            scrollBack.Draw(spriteBatch);
            scrollFore.Draw(spriteBatch);
            particleList.Draw(spriteBatch);
            spritePlayer.Draw(spriteBatch);
            enemyList.Draw(spriteBatch);
            playerBulletList.Draw(spriteBatch);
            textScore.Draw(spriteBatch);
            
            //enter to start text
            if (!startGame){ textStartGame.Draw(spriteBatch); }

            //display bounding boxes
            if (showbb)
            {
                spritePlayer.drawBB(spriteBatch, Color.Red);
                LineBatch.drawLineRectangle(spriteBatch, playArea, Color.Purple);
                enemyList.drawInfo(spriteBatch, Color.Red, Color.Blue);
                playerBulletList.drawInfo(spriteBatch, Color.Red, Color.Blue);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        
        //Spawns enemy randomly in a list
        public void AddEnemy(SpriteList sl)
        {
            Sprite3 newEnemy = new EnemyEasy(true, enemyTex, SCREEN_WIDTH + 30, (int)((playArea.Top - playArea.Bottom - 50) * random.NextDouble() + playArea.Bottom));
            newEnemy.setColor(Color.LightYellow);
            newEnemy.setWidthHeight(50, 50);
            newEnemy.setFlip(SpriteEffects.FlipHorizontally);
            sl.addSpriteReuse(newEnemy);
        }

        //Adds explosion to particle list
        public void AddExplosion(int x, int y)
        {
            float scale = 0.6f;
            float xOffset = -10;
            float yOffset = -5;

            Sprite3 newExplosion = new Sprite3(true, explosionTex, x + xOffset, y + yOffset);
            newExplosion.setXframes(7);
            newExplosion.setYframes(3);
            newExplosion.setWidthHeight(896 / 7 * scale, 384 / 3 * scale);

            Vector2[] anim = new Vector2[21];
            anim[0].X = 0; anim[0].Y = 0;
            anim[1].X = 1; anim[1].Y = 0;
            anim[2].X = 2; anim[2].Y = 0;
            anim[3].X = 3; anim[3].Y = 0;
            anim[4].X = 4; anim[4].Y = 0;
            anim[5].X = 5; anim[5].Y = 0;
            anim[6].X = 6; anim[6].Y = 0;
            anim[7].X = 0; anim[7].Y = 1;
            anim[8].X = 1; anim[8].Y = 1;
            anim[9].X = 2; anim[9].Y = 1;
            anim[10].X = 3; anim[10].Y = 1;
            anim[11].X = 4; anim[11].Y = 1;
            anim[12].X = 5; anim[12].Y = 1;
            anim[13].X = 6; anim[13].Y = 1;
            anim[14].X = 0; anim[14].Y = 2;
            anim[15].X = 1; anim[15].Y = 2;
            anim[16].X = 2; anim[16].Y = 2;
            anim[17].X = 3; anim[17].Y = 2;
            anim[18].X = 4; anim[18].Y = 2;
            anim[19].X = 5; anim[19].Y = 2;
            anim[20].X = 6; anim[20].Y = 2;
            newExplosion.setAnimationSequence(anim, 0, 20, 2);
            newExplosion.setAnimFinished(2); // make it inactive and invisible
            newExplosion.animationStart();

            particleList.addSpriteReuse(newExplosion);
        }

        //Adds bullet to bullet list based on parameters
        public void AddBullet(int x, int y)
        {
            int xOffset = (int)spritePlayer.getWidth();
            int yOffset = (int)spritePlayer.getHeight() / 2;

            //int bulletSpeed = 100;

            Sprite3 bullet = new Sprite3(true, bulletTex, x + xOffset, y + yOffset);
            bullet.setWidthHeight(10, 10);


            playerBulletList.addSpriteReuse(bullet);


        }
    }
}
