using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2
{
    class Pause : RC_GameStateParent
    {

        SpriteFont spriteFont;
        TextRenderable textPause;

        Texture2D texBackground;
        Texture2D texForeground;


        public override void LoadContent()
        {
            base.LoadContent();

            spriteFont = Content.Load<SpriteFont>("file");
            texBackground = Content.Load<Texture2D>("background");
            texForeground = Content.Load<Texture2D>("foreground");


            textPause = new TextRenderable("PAUSE - Press P to unpause", new Vector2(Game1.SCREEN_WIDTH / 2 - 100, Game1.SCREEN_HEIGHT / 2), spriteFont, Color.Red);



        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.P))
            {
                gameStateManager.popLevel();
            }
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(texBackground, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.White);
            spriteBatch.Draw(texForeground, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.White);

            textPause.Draw(spriteBatch);


            spriteBatch.End();
        }
    }

    class SplashScreen : RC_GameStateParent
    {
        float timer = 0;
        int state = 0;

        SoundEffect soundIntro;
        Texture2D back;

        Texture2D splashLogo;
        Color splashColor;
        float splashFade = 0;


        public override void LoadContent()
        {
            base.LoadContent();

            soundIntro = Content.Load<SoundEffect>("mt2sounds/intro");
            splashLogo = Content.Load<Texture2D>("SShip4m");
            back = Content.Load<Texture2D>("pixel");
            //splashColor = Color.White;
            splashColor = Color.Black;

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (state)
            {
                case 0:
                    if (timer < 3)
                    {

                    }
                    else
                    {
                        soundIntro.Play();
                        timer = 0;
                        state++;

                    }

                    break;
                case 1:
                    if (timer > 3)
                    {
                        gameStateManager.setLevel(4);

                    }
                    else
                    {

                    }
                    break;
            }



        }
        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Black);

            if (state == 1 && timer < 3)
            {
                splashFade += 0.01f;

                splashColor = Color.Lerp(Color.Black, Color.White, splashFade);


                spriteBatch.Begin();

                spriteBatch.Draw(back, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.Black);

                spriteBatch.Draw(splashLogo, new Vector2(Game1.SCREEN_WIDTH / 2 - splashLogo.Width / 2, Game1.SCREEN_HEIGHT / 2 - splashLogo.Height / 2), splashColor);
                spriteBatch.End();
            }
        }
    }

    class StartScreen : RC_GameStateParent
    {
        SpriteFont spriteFont;
        TextRenderable textEnter;


        Texture2D texBackground;
        Texture2D texForeground;

        float timer = 0;

        public override void LoadContent()
        {
            base.LoadContent();

            spriteFont = Content.Load<SpriteFont>("file");
            texBackground = Content.Load<Texture2D>("background");
            texForeground = Content.Load<Texture2D>("foreground");





        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputManager.Instance.KeyPressed(Keys.Enter))
            {
                gameStateManager.pushLevel(1);
            }
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(texBackground, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.White);
            spriteBatch.Draw(texForeground, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.White);


            



            if ((int)timer % 2 == 0)
            {
                textEnter = new TextRenderable("PRESS ENTER TO START", new Vector2(Game1.SCREEN_WIDTH / 2 - 100, Game1.SCREEN_HEIGHT / 2), spriteFont, Color.Red);
                textEnter.Draw(spriteBatch);
                textEnter = new TextRenderable("PRESS ESCAPE TO QUIT", new Vector2(Game1.SCREEN_WIDTH / 2 - 100, Game1.SCREEN_HEIGHT / 2 + 40), spriteFont, Color.Red);
                
                textEnter.Draw(spriteBatch);
            }



            spriteBatch.End();
        }
    }

    class EndScreen : RC_GameStateParent
    {
        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }

    class GameLevel1 : RC_GameStateParent
    {
        //player
        Player player;
        Texture2D playerTex;

        //info to be passed between new levels
        public int level = 1;
        public int score;


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

        //SOUNDS
        LimitSound soundBulletLimit;
        SoundEffect soundBullet;
        SoundEffect soundEnemyHit;
        LimitSound soundEnemyHitLimit;
        SoundEffect soundExplosion;
        LimitSound soundExplosionLimit;



        //set spawninterval to 0.01f to see effects of "spritelist" (NOT A LIST) its a 200 default array pretending to be a list.  original value = 2;
        float timer = 0;
        float spawnInterval = 2;
        int enemyCounter = 0;
        public int enemyToSpawn;


        //0 - start of level
        //1 - Play
        //2 - finished level
        //3 - playerDied

        enum LEVELSTATE
        {
            LEVELSTART,
            LEVELPLAY,
            LEVELFINISH,
            LEVELGAMEOVER
        }

        LEVELSTATE levelState;


        //scrolling backgrounds
        ScrollBackGround scrollBack;
        ScrollBackGround scrollFore;
        Texture2D texBackground = null;
        Texture2D texForeground = null;

        //text
        TextRenderable textScore;
        TextRenderable textStartGame;
        TextRenderable textVarious;
        TextRenderable textLevel;
        
        SpriteFont spriteFont;

        public List<Color> colorList;



        float deltaTime = 0;
        //randomclass
        Random random;
        public override void LoadContent()
        {



            // Create a new SpriteBatch, which can be used to draw textures.


            texBackground = Content.Load<Texture2D>("background");
            texForeground = Content.Load<Texture2D>("foreground");

            spriteFont = Content.Load<SpriteFont>("file");
            playerTex = Content.Load<Texture2D>("SShip4m");

            explosionTex = Content.Load<Texture2D>("Boom3");
            enemyTex = Content.Load<Texture2D>("Black1M");

            bulletTex = Content.Load<Texture2D>("purple32x32d");





            random = new Random();


            //spritePlayer.setBB(0,0,200, 200);




            textScore = new TextRenderable("Score : " + score.ToString(), new Vector2(100, 100), spriteFont, Color.Red);
            textStartGame = new TextRenderable("PRESS ENTER TO START. P TO PAUSE", new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2), spriteFont, Color.Red);



            //SOUNDS
            soundBullet = Content.Load<SoundEffect>("mt2sounds/laser");
            soundBulletLimit = new LimitSound(soundBullet, 3);

            soundEnemyHit = Content.Load<SoundEffect>("mt2sounds/hit");
            soundEnemyHitLimit = new LimitSound(soundEnemyHit, 3);

            soundExplosion = Content.Load<SoundEffect>("mt2sounds/explosion");
            soundExplosionLimit = new LimitSound(soundExplosion, 3);

        }



        public override void InitializeLevel(GraphicsDevice g, SpriteBatch s, ContentManager c, RC_GameStateManager lm)
        {
            base.InitializeLevel(g, s, c, lm);
        }

        public override void EnterLevel(int fromLevelNum)
        {
            base.EnterLevel(fromLevelNum);
            Console.WriteLine("BEGINNING ");
            if (gameStateManager.getLevel(fromLevelNum) is GameLevel1)
            {
                GameLevel1 lastGameLevel = gameStateManager.getLevel(fromLevelNum) as GameLevel1;
                level = lastGameLevel.level + 1;
                Console.WriteLine("LEVEL: " + level.ToString());
                score = lastGameLevel.score;
                enemyToSpawn = lastGameLevel.enemyToSpawn + 3;
                
            }
            else
            {
                level = 1;
                score = 0;
                enemyToSpawn = 10;
            }

            enemyCounter = 0;
            //initialize level texts
            textLevel = new TextRenderable("LEVEL: " + level.ToString(), new Vector2(20, 20), spriteFont, Color.Red);
            textScore.text = "Score : " + score.ToString();

            //initialize player for new level
            player = new Player(true, playerTex, 50, Game1.SCREEN_HEIGHT / 2 - 50);
            player.setWidthHeight(100, 100);
            player.setFlip(SpriteEffects.FlipHorizontally);
            
            //initalize level state
            levelState = LEVELSTATE.LEVELSTART;

            //initialize lists
            particleList = new SpriteList();
            enemyList = new SpriteList();
            playerBulletList = new SpriteList();

            Console.WriteLine("enemyCounter" + enemyCounter);
            Console.WriteLine("enemyToSpawn" + enemyToSpawn);


            scrollBack = new ScrollBackGround(texBackground, texBackground.Bounds, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), -1 + player.playerBackgroundSpeed, 2);
            scrollFore = new ScrollBackGround(texForeground, texForeground.Bounds, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), -2 + player.playerBackgroundSpeed, 2);

            //initialize eneemy player targetting.
            EnemyEasy.player = player;


        }

        public override void Update(GameTime gameTime)
        {

            //deltaTime is in seconds, so calculations = pixels per second.
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Press enter to start game/level
            switch (levelState)
            {
                case LEVELSTATE.LEVELSTART:


                    if (InputManager.Instance.KeyPressed(Keys.Enter))
                    {
                        levelState = LEVELSTATE.LEVELPLAY;
                    }
                    return;
                case LEVELSTATE.LEVELPLAY:
                    timer += deltaTime;



                    //update player
                    player.Update(gameTime);


                    //PAUSE
                    if (InputManager.Instance.KeyPressed(Keys.P))
                    {
                        gameStateManager.pushLevel(3);
                        
                    }


                    //PLAYER SHOOT
                    if (InputManager.Instance.KeyPressed(Keys.Space))
                    {
                        AddBullet((int)player.getPosX(), (int)player.getPosY());
                        soundBulletLimit.playSoundIfOk();
                    }


                    break;
                case LEVELSTATE.LEVELFINISH:
                    player.Update(gameTime);
                    if (InputManager.Instance.KeyPressed(Keys.Enter))
                    {
                        gameStateManager.pushLevel(1);
                    }
                    break;
                case LEVELSTATE.LEVELGAMEOVER:
                    timer += deltaTime;

                    if (InputManager.Instance.KeyPressed(Keys.Q))
                    {
                        gameStateManager.setLevel(4);
                    }
                    break;
                default:
                    throw new Exception("hahaha");
            }

            //Every 2 seconds, spawn an enemy randomly.
            if (timer > spawnInterval && enemyCounter < enemyToSpawn)
            {
                AddEnemy(enemyList);
                timer = 0;
                enemyCounter++;
            }

            //update backgrounds
            scrollBack.Update(gameTime);
            scrollFore.Update(gameTime);
            scrollBack.setScrollSpeed(player.playerBackgroundSpeed - 2);
            scrollFore.setScrollSpeed(player.playerBackgroundSpeed);


            //Update/remove enemies
            enemyList.Update(gameTime);
            enemyList.removeIfOutside(new Rectangle(-100, -100, Game1.SCREEN_WIDTH + 200, Game1.SCREEN_HEIGHT + 200));
            if(enemyList.countActive() == 0 && enemyCounter >= enemyToSpawn)
            {
                levelState = LEVELSTATE.LEVELFINISH;
            }

            //update/remove bullets
            playerBulletList.moveDeltaX(5);
            playerBulletList.removeIfOutside(new Rectangle(-100, -100, Game1.SCREEN_WIDTH + 200, Game1.SCREEN_HEIGHT + 200));

            //If player collides with enemy
            int collisionIndex = enemyList.collisionAA(player);
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
                    player.TakeDamage();

                    if (player.hitPoints <= 0)
                    {
                        levelState = LEVELSTATE.LEVELGAMEOVER;
                        AddExplosion((int)player.getPosX(), (int)player.getPosY());
                        player.setPos(0, -300); // offscreen
                        player.setActive(false);


                    }

                    //TODO: GET HITSOUND
                    soundEnemyHitLimit.playSoundIfOk();

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

                    currentEnemy.hitPoints -= 1;
                    //DEAD ENEMY
                    if (currentEnemy.hitPoints <= 0)
                    {
                        currentEnemy.active = false;
                        currentEnemy.visible = false;
                        

                        //add particle
                        AddExplosion((int)currentEnemy.getPosX(), (int)currentEnemy.getPosY());

                        //add score
                        score += 10;
                        textScore.text = "Score : " + score.ToString();
                    }
                    //DAMAGED ENEMY
                    else
                    {
                        currentEnemy.varInt0 = 100;
                        soundEnemyHitLimit.playSoundIfOk();
                    }

                    //REMOVE BULLET
                    currentBullet.active = false;
                    currentBullet.visible = false;
                }
            }

            //update particles
            particleList.animationTick(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.DeepSkyBlue);



            // TODO: Add your drawing code here
            spriteBatch.Begin();



            scrollBack.colour = Color.Aqua;
            scrollBack.Draw(spriteBatch);
            scrollFore.Draw(spriteBatch);
            particleList.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemyList.Draw(spriteBatch);
            playerBulletList.Draw(spriteBatch);
            textScore.Draw(spriteBatch);


            //display bounding boxes
            if (Game1.showbb)
            {
                player.drawBB(spriteBatch, Color.Red);
                LineBatch.drawLineRectangle(spriteBatch, player.playArea, Color.Purple);
                enemyList.drawInfo(spriteBatch, Color.Red, Color.Blue);
                playerBulletList.drawInfo(spriteBatch, Color.Red, Color.Blue);
            }

            switch (levelState)
            {
                case LEVELSTATE.LEVELSTART:
                    textVarious = new TextRenderable("LEVEL: " + level.ToString(), new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2), spriteFont, Color.Red);
                    textVarious.Draw(spriteBatch);
                    textVarious = new TextRenderable("Enemies: " + enemyToSpawn.ToString(), new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2 + 30), spriteFont, Color.Red);
                    textVarious.Draw(spriteBatch);
                    break;
                case LEVELSTATE.LEVELGAMEOVER:
                    textVarious = new TextRenderable("GAME OVER", new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2), spriteFont, Color.Red);
                    textVarious.Draw(spriteBatch);
                    textVarious = new TextRenderable("Press Q to go to menu.", new Vector2(Game1.SCREEN_WIDTH / 2, (Game1.SCREEN_HEIGHT / 2) + 30), spriteFont, Color.Red);
                    textVarious.Draw(spriteBatch);
                    break;
                case LEVELSTATE.LEVELFINISH:
                    textVarious = new TextRenderable("LEVEL FINISHED", new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2), spriteFont, Color.Red);
                    textVarious.Draw(spriteBatch);
                    textVarious = new TextRenderable("Press Enter to go to Next Level.", new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2 + 30), spriteFont, Color.Red);
                    textVarious.Draw(spriteBatch);
                    break;
            }
            textLevel.Draw(spriteBatch);
            spriteBatch.End();
        }

        //Adds bullet to bullet list based on parameters
        public void AddBullet(int x, int y)
        {
            int xOffset = (int)player.getWidth();
            int yOffset = (int)player.getHeight() / 2;

            //int bulletSpeed = 100;

            Sprite3 bullet = new Sprite3(true, bulletTex, x + xOffset, y + yOffset);
            bullet.setWidthHeight(10, 10);


            playerBulletList.addSpriteReuse(bullet);


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

            soundExplosionLimit.playSoundIfOk();
        }
        //Spawns enemy randomly in a list
        public void AddEnemy(SpriteList sl)
        {

            Sprite3 newEnemy = new EnemyEasy(true, enemyTex, Game1.SCREEN_WIDTH + 30, (int)((player.playArea.Top - player.playArea.Bottom - 50) * random.NextDouble() + player.playArea.Bottom), random.Next() % level);
            newEnemy.setColor(Color.LightYellow);
            newEnemy.setWidthHeight(50, 50);
            newEnemy.setFlip(SpriteEffects.FlipHorizontally);
            sl.addSpriteReuse(newEnemy);
        }
    }
}
