using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Objects;
using Assignment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace Assignment.Levels
{
    public class TutorialLevel : BaseLevel
    {
        public override void LoadLevel()
        {
            Sprite3 newPlatform;
            base.LoadLevel();
            OutOfBounds = new Rectangle(-100, 0, 7000, 1000);
            //TUTORIAL START
            newPlatform = new Sprite3(true, texPixel, 0, 700);
            newPlatform.setWidthHeight(800, 30);
            platformList.addSpriteReuse(newPlatform);
            //pit between these two
            newPlatform = new Sprite3(true, texPixel, 1700, 700);
            newPlatform.setWidthHeight(2000, 30);
            platformList.addSpriteReuse(newPlatform);
            //the double jump test. player has to hold A on one or both of the jumps to pass.
            newPlatform = new Sprite3(true, texPixel, 500, 500);
            newPlatform.setWidthHeight(30, 200);
            platformList.addSpriteReuse(newPlatform);
            //tutorial target
            targetSpawnPositions.Add(new Vector2(2051, 507));
            //High wall
            newPlatform = new Sprite3(true, texPixel, 2551, 300);
            newPlatform.setWidthHeight(30, 400);
            platformList.addSpriteReuse(newPlatform);
            //targets to make the high wall
            targetSpawnPositions.Add(new Vector2(2432, 587));
            targetSpawnPositions.Add(new Vector2(2427, 493));
            targetSpawnPositions.Add(new Vector2(2427, 389));
            targetSpawnPositions.Add(new Vector2(2430, 300));
            targetSpawnPositions.Add(new Vector2(2436, 196));
            //-----------
            newPlatform = new Sprite3(true, texPixel, 3000, 700);
            newPlatform.setWidthHeight(800, 30);
            platformList.addSpriteReuse(newPlatform);
            //bigger target pit
            newPlatform = new Sprite3(true, texPixel, 4850, 700);
            newPlatform.setWidthHeight(3000, 30);
            platformList.addSpriteReuse(newPlatform);
            //tutorial target. respawn?
            targetSpawnPositions.Add(new Vector2(4274, 463));
            //checkpoint
            targetSpawnPositions.Add(new Vector2(3527, 627));
            //goalPlatform
            Sprite3 goalPlatform = new Sprite3(true, texPixel, 5150, 700);
            goalPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(goalPlatform);
            goal = new GoalZone(goalPlatform);
            //Wall behind goal
            newPlatform = new Sprite3(true, texPixel, 5350, -10);
            newPlatform.setWidthHeight(800, 900);
            platformList.addSpriteReuse(newPlatform);
        }

        public override void SpawnTargets()
        {
            enemyList = new SpriteList();
            for (int i = 0; i < targetSpawnPositions.Count; i++)
            {
                TestEnemy currentTestEnemy = new TestEnemy(targetSpawnPositions[i]);
                currentTestEnemy.canRespawn = true;
                enemyList.addSprite(currentTestEnemy);
            }
        }
    }

    public class Level1 : BaseLevel
    {
        public override void LoadLevel()
        {
            Game1.TimeScore = 0;
            base.LoadLevel();
            Sprite3 newPlatform;
            OutOfBounds = new Rectangle(0, 0, Game1.SCREEN_WIDTH, 1000);


            newPlatform = new Sprite3(true, texPixel, 200, 500);
            newPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(newPlatform);

            PlayerSpawnPosition = new Vector2(newPlatform.getBoundingBoxAA().X + newPlatform.getBoundingBoxAA().Width / 2 - player.getWidth() / 2, newPlatform.getBoundingBoxAA().Y - 100);

            newPlatform = new Sprite3(true, texPixel, 900, 500);
            newPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(newPlatform);

            if (!isChallengeMode)
            {
                targetSpawnPositions.Add(new Vector2(380, 362));
                targetSpawnPositions.Add(new Vector2(552, 359));
                targetSpawnPositions.Add(new Vector2(717, 354));
            }
            else
            {
                targetSpawnPositions.Add(new Vector2(74, 396));
                targetSpawnPositions.Add(new Vector2(69, 537));
                targetSpawnPositions.Add(new Vector2(70, 682));
                targetSpawnPositions.Add(new Vector2(272, 675));
                targetSpawnPositions.Add(new Vector2(471, 664));
                targetSpawnPositions.Add(new Vector2(668, 656));
                targetSpawnPositions.Add(new Vector2(850, 654));
                targetSpawnPositions.Add(new Vector2(1018, 647));
                targetSpawnPositions.Add(new Vector2(1158, 650));
                targetSpawnPositions.Add(new Vector2(1144, 545));
                targetSpawnPositions.Add(new Vector2(1144, 420));

            }

            goal = new GoalZone(newPlatform);


        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    public class Level2 : BaseLevel
    {
        public override void LoadLevel()
        {
            base.LoadLevel();
            Sprite3 newPlatform;
            OutOfBounds = new Rectangle(0, 0, Game1.SCREEN_WIDTH, 1000);
            platformList = new SpriteList();

            newPlatform = new Sprite3(true, texPixel, 200, 700);
            newPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(newPlatform);
            targetSpawnPositions = new List<Vector2>();
            PlayerSpawnPosition = new Vector2(newPlatform.getBoundingBoxAA().X + newPlatform.getBoundingBoxAA().Width / 2 - player.getWidth() / 2, newPlatform.getBoundingBoxAA().Y - 100);


            newPlatform = new Sprite3(true, texPixel, 700, 150);
            newPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(newPlatform);

            if (!isChallengeMode)
            {
                targetSpawnPositions.Add(new Vector2(280, 586));
                targetSpawnPositions.Add(new Vector2(325, 487));
                targetSpawnPositions.Add(new Vector2(367, 394));
                targetSpawnPositions.Add(new Vector2(414, 294));
                targetSpawnPositions.Add(new Vector2(469, 188));
                targetSpawnPositions.Add(new Vector2(526, 82));
            }
            else
            {
                targetSpawnPositions.Add(new Vector2(68, 608));
                targetSpawnPositions.Add(new Vector2(68, 497));
                targetSpawnPositions.Add(new Vector2(67, 399));
                targetSpawnPositions.Add(new Vector2(72, 293));
                targetSpawnPositions.Add(new Vector2(79, 185));
                targetSpawnPositions.Add(new Vector2(78, 90));
            }


            goal = new GoalZone(newPlatform);
            player.LoadPlatformsPlayer(platformList);

        }
    }


    public class Level3 : BaseLevel
    {
        public override void LoadLevel()
        {
            base.LoadLevel();
            Sprite3 newPlatform;
            //PIT LEVEL.
            OutOfBounds = new Rectangle(-100, 0, Game1.SCREEN_WIDTH + 100, 1000);

            newPlatform = new Sprite3(true, texPixel, 200, 600);
            newPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(newPlatform);

            PlayerSpawnPosition = new Vector2(newPlatform.getBoundingBoxAA().X + newPlatform.getBoundingBoxAA().Width / 2 - player.getWidth() / 2, newPlatform.getBoundingBoxAA().Y - 100);



            targetSpawnPositions.Add(new Vector2(135, 79));
            targetSpawnPositions.Add(new Vector2(411, 53));
            targetSpawnPositions.Add(new Vector2(295, 295));
            targetSpawnPositions.Add(new Vector2(270, 145));
            targetSpawnPositions.Add(new Vector2(515, 376));
            targetSpawnPositions.Add(new Vector2(484, 213));
            targetSpawnPositions.Add(new Vector2(123, 461));
            targetSpawnPositions.Add(new Vector2(164, 246));
            targetSpawnPositions.Add(new Vector2(400, 502));
            targetSpawnPositions.Add(new Vector2(399, 368));
            targetSpawnPositions.Add(new Vector2(701, 511));
            targetSpawnPositions.Add(new Vector2(562, 461));
            targetSpawnPositions.Add(new Vector2(642, 261));
            targetSpawnPositions.Add(new Vector2(907, 494));
            targetSpawnPositions.Add(new Vector2(730, 381));
            targetSpawnPositions.Add(new Vector2(628, 93));
            targetSpawnPositions.Add(new Vector2(839, 52));
            targetSpawnPositions.Add(new Vector2(945, 378));
            targetSpawnPositions.Add(new Vector2(819, 201));
            targetSpawnPositions.Add(new Vector2(847, 410));
            targetSpawnPositions.Add(new Vector2(976, 200));
            targetSpawnPositions.Add(new Vector2(1082, 499));
            targetSpawnPositions.Add(new Vector2(1077, 293));

            newPlatform = new Sprite3(true, texPixel, 1100, 150);
            newPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(newPlatform);
            //make goal on platform
            // goal = new GoalZone(new Vector2(700, PlayerSpawnPosition.Y), 100, 100);
            goal = new GoalZone(newPlatform);
            player.LoadPlatformsPlayer(platformList);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    public class Level4 : BaseLevel
    {
        public override void LoadLevel()
        {
            base.LoadLevel();
            Sprite3 newPlatform;
            //DASH LEVEL
            OutOfBounds = new Rectangle(-100, 0, 7000, 1000);


            ////debug
            //newPlatform = new Sprite3(true, texPixel, 0, 700);
            //newPlatform.setWidthHeight(7000, 30);
            //platformList.addSpriteReuse(newPlatform);
            //start platform
            newPlatform = new Sprite3(true, texPixel, 200, 600);
            newPlatform.setWidthHeight(100, 30);
            platformList.addSpriteReuse(newPlatform);
            PlayerSpawnPosition = new Vector2(newPlatform.getBoundingBoxAA().X + newPlatform.getBoundingBoxAA().Width / 2 - player.getWidth() / 2, newPlatform.getBoundingBoxAA().Y - 100);


            newPlatform = new Sprite3(true, texPixel, 3000, 600);
            newPlatform.setWidthHeight(300, 30);
            platformList.addSpriteReuse(newPlatform);
            //make goal on last platform
            goal = new GoalZone(newPlatform);
           

            if (!isChallengeMode)
            {
                targetSpawnPositions.Add(new Vector2(512, 429));
                targetSpawnPositions.Add(new Vector2(833, 422));
                targetSpawnPositions.Add(new Vector2(1119, 424));
                //targetSpawnPositions.Add(new Vector2(1430, 422));
                targetSpawnPositions.Add(new Vector2(1745, 422));
                //targetSpawnPositions.Add(new Vector2(2065, 422));
                targetSpawnPositions.Add(new Vector2(2378, 422));
                //targetSpawnPositions.Add(new Vector2(2725, 422));


            }
            else
            {
                //ceiling
                newPlatform = new Sprite3(true, texPixel, 0, 400);
                newPlatform.setWidthHeight(7000, 30);
                platformList.addSpriteReuse(newPlatform);


                targetSpawnPositions.Add(new Vector2(512, 429));
                targetSpawnPositions.Add(new Vector2(833, 422));
                targetSpawnPositions.Add(new Vector2(1119, 424));
                targetSpawnPositions.Add(new Vector2(1430, 422));
                targetSpawnPositions.Add(new Vector2(1745, 422));
                targetSpawnPositions.Add(new Vector2(2065, 422));
                targetSpawnPositions.Add(new Vector2(2378, 422));
                targetSpawnPositions.Add(new Vector2(2725, 422));
                targetSpawnPositions.Add(new Vector2(2992, 422));

            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    public class Level5 : BaseLevel
    {


        public override void LoadLevel()
        {
            base.LoadLevel();

            Sprite3 newPlatform;
            //PIT LEVEL.
            OutOfBounds = new Rectangle(-100, 0, Game1.SCREEN_WIDTH + 100, 1000);
            //ceiling
            newPlatform = new Sprite3(true, texPixel, 0, 0);
            newPlatform.setWidthHeight(2000, 30);
            platformList.addSpriteReuse(newPlatform);
            //start platform
            newPlatform = new Sprite3(true, texPixel, PlayerSpawnPosition.X - player.getWidth(), PlayerSpawnPosition.Y + 100);
            newPlatform.setWidthHeight(150, 30);
            platformList.addSpriteReuse(newPlatform);
            //wall blocking way
            newPlatform = new Sprite3(true, texPixel, 500, 0);
            newPlatform.setWidthHeight(30, 500);
            platformList.addSpriteReuse(newPlatform);
            //goalplatform
            newPlatform = new Sprite3(true, texPixel, 700, PlayerSpawnPosition.Y + 100);
            newPlatform.setWidthHeight(150, 30);
            platformList.addSpriteReuse(newPlatform);

            if (!isChallengeMode)
            {
                //EASY PIT LEVEL
                targetSpawnPositions.Add(new Vector2(368, 175));
                targetSpawnPositions.Add(new Vector2(360, 316));
                targetSpawnPositions.Add(new Vector2(346, 448));
                targetSpawnPositions.Add(new Vector2(337, 594));
                targetSpawnPositions.Add(new Vector2(468, 592));
                targetSpawnPositions.Add(new Vector2(592, 590));
                targetSpawnPositions.Add(new Vector2(587, 479));
                targetSpawnPositions.Add(new Vector2(580, 358));
                targetSpawnPositions.Add(new Vector2(583, 243));
                targetSpawnPositions.Add(new Vector2(590, 149));
            }
            else
            {
                //TAEWOOKS LEVEL
                targetSpawnPositions.Add(new Vector2(736, 459));
                targetSpawnPositions.Add(new Vector2(1039, 574));
                targetSpawnPositions.Add(new Vector2(910, 389));
                targetSpawnPositions.Add(new Vector2(83, 711));
                targetSpawnPositions.Add(new Vector2(509, 599));
                targetSpawnPositions.Add(new Vector2(577, 298));
                targetSpawnPositions.Add(new Vector2(577, 180));
            }
            //make goal on platform
            // goal = new GoalZone(new Vector2(700, PlayerSpawnPosition.Y), 100, 100);
            goal = new GoalZone(newPlatform);

        }

        public override void NextLevel()
        {

            if(isChallengeMode == true)
            {
                Game1.AddChallengeScore(Game1.TimeScore);
            }
            else
            {
                Game1.AddScore(Game1.TimeScore);
            }
            
            gameStateManager.setLevel(6);

            MenuScreen.UnlockChallengeMode();

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    public abstract class BaseLevel : RC_GameStateParent
    {
        public static bool showbb = false;

        //list for particles
        protected RC_RenderableList particleList;
        protected SpriteList enemyList;
        protected SpriteList sliceList;
        protected SpriteList platformList;
        public static Texture2D texTest;
        public static Texture2D texPixel;
        public static Texture2D texTarget;
        public static Texture2D texMouseCursor;
        public static SpriteFont spriteFontFile;

        public static SoundEffect soundDeath;
        public static SoundEffect soundSlice;
        public static SoundEffect soundRespawn;
        public static SoundEffect soundSliceHit;
        


        public static Texture2D texPlayerDoubleJump;
        public static Texture2D texPlayerDash;
        public static Texture2D texPlayerSlice;

        protected Vector2 mousePosition = Vector2.Zero;

        protected Player player;
        public static AbilityIcon abilityIconDoubleJump;
        public static AbilityIcon abilityIconDash;
        public static AbilityIcon abilityIconSlice;

        //Level specific things
        protected Rectangle OutOfBounds;
        public Vector2 PlayerSpawnPosition = new Vector2(100, 100);
        bool isPlayerAlive;
        protected List<Vector2> targetSpawnPositions = new List<Vector2>();
        protected GoalZone goal;
        

        protected Camera2d cam = new Camera2d();
        protected Vector2 CamOriginalPosition = new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2);
        bool CamPlayerPositiveVelocity = true;

        public static bool isChallengeMode = false;

        public override void LoadContent()
        {
            base.LoadContent();

            texTarget = Content.Load<Texture2D>("Target");
            texTest = Content.Load<Texture2D>("SShip4m");
            texPixel = Content.Load<Texture2D>("pixel");
            texMouseCursor = Content.Load<Texture2D>("sword_02");
            spriteFontFile = Content.Load<SpriteFont>("File");

            texPlayerDoubleJump = Content.Load<Texture2D>("doublejump");
            texPlayerDash = Content.Load<Texture2D>("dash");
            texPlayerSlice = Content.Load<Texture2D>("slice2");

            Slice.pixel = texPixel;
            EnemyHitbox.texHitbox = texPixel;
            TestEnemy.texTestEnemy = texTarget;
            Player.texPlayer = texPixel;
            
            particleList = new RC_RenderableList();
            sliceList = new SpriteList();
            platformList = new SpriteList();
            enemyList = new SpriteList();

            //Initialize abilities
            abilityIconDoubleJump = new AbilityIcon(texPlayerDoubleJump, new Vector2(900, Game1.SCREEN_HEIGHT - 100));
            abilityIconDash = new AbilityIcon(texPlayerDash, new Vector2(1000, Game1.SCREEN_HEIGHT - 100));
            abilityIconSlice = new AbilityIcon(texPlayerSlice, new Vector2(800, Game1.SCREEN_HEIGHT - 100));

            Player.iconDoubleJump = abilityIconDoubleJump;
            Player.iconDash = abilityIconDash;
            Player.iconSlice = abilityIconSlice;

            //player = new Player(new Vector2(100, 100));
            //LoadLevel();

            //ResetPlayer();

            //cam.Pos = new Vector2(0, 0);

            ////cam.Zoom = 2.0f // Example of Zoom in
            //cam.Zoom = 1f; // Example of Zoom out

            //SpawnTargets();
        }

        public virtual void LoadLevel()
        {
            platformList = new SpriteList();
            targetSpawnPositions = new List<Vector2>();
            player.LoadPlatformsPlayer(platformList);


            for (int i = 0; i < particleList.Count(); i++)
            {
                this.particleList.getRenderable(i).active = false;
            }




        }


        public void ResetPlayer()
        {
            player.setPos(PlayerSpawnPosition);
            player.canDash = true;
            player.canDoubleJump = true;
            player.canSlice = true;
            player.velocity = Vector2.Zero;
            player.acceleration = new Vector2(0, 1000f);
            SpawnTargets();

            //play spawn animations
            //Player cant move for a second while respawning.
            
            Task.Delay(500).ContinueWith(t => player.SetCanPlayerMove(true));
            //Task.Delay(500).ContinueWith(t => soundRespawn.Play());
        }

        public virtual void SpawnTargets()
        {
            enemyList = new SpriteList();
            for (int i = 0; i < targetSpawnPositions.Count; i++)
            {
                enemyList.addSprite(new TestEnemy(targetSpawnPositions[i]));
            }
        }


        public override void EnterLevel(int fromLevelNum)
        {
            base.EnterLevel(fromLevelNum);
            player = new Player(new Vector2(100, 100));

            LoadLevel();


            ResetPlayer();

            cam.Pos = new Vector2(0, 0);

            //cam.Zoom = 2.0f // Example of Zoom in
            cam.Zoom = 1f; // Example of Zoom out

        }

        public virtual void NextLevel()
        {
            gameStateManager.setLevel(gameStateManager.getCurrentLevelNum() + 1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Game1.TimeScore += deltaTime;

           cam.Pos = new Vector2(cam.Pos.X, Game1.SCREEN_HEIGHT / 2);

            if (InputManager.Instance.KeyDown(Keys.W))
            {
                cam.Pos = new Vector2(cam.Pos.X, cam.Pos.Y - 5);
            }
            if (InputManager.Instance.KeyDown(Keys.S))
            {
                cam.Pos = new Vector2(cam.Pos.X, cam.Pos.Y + 5);
            }
            if (InputManager.Instance.KeyDown(Keys.A))
            {
                cam.Pos = new Vector2(cam.Pos.X - 5, cam.Pos.Y);
            }
            if (InputManager.Instance.KeyDown(Keys.D))
            {
                cam.Pos = new Vector2(cam.Pos.X + 5, cam.Pos.Y);
            }


            if (InputManager.Instance.KeyPressed(Keys.Back))
            {
                gameStateManager.setLevel(gameStateManager.getCurrentLevelNum() - 1);
            }

            //CAMERA STICK ON PLAYER.
            if (cam.Pos.X < player.getPosX() - 100)
            {
                cam.Pos = new Vector2(player.getPosX() - 100, cam.Pos.Y);
            }
            else if (cam.Pos.X > player.getPosX() + player.getWidth() + 100)
            {
                cam.Pos = new Vector2(player.getPosX() + player.getWidth() + 100, cam.Pos.Y);
            }

            ////Move camera to infront of player.
            //if (player.velocity.X > 0)
            //{
            //    CamPlayerPositiveVelocity = true;
            //}
            //else if (player.velocity.X < 0)
            //{
            //    CamPlayerPositiveVelocity = false;
            //}

            //if (CamPlayerPositiveVelocity)
            //{
            //    cam.Pos = new Vector2(cam.Pos.X + 10, cam.Pos.Y);
            //}
            //else
            //{
            //    cam.Pos = new Vector2(cam.Pos.X - 10, cam.Pos.Y);
            //}



            if (cam.Pos.X < CamOriginalPosition.X)
            {
                cam.Pos = new Vector2(CamOriginalPosition.X, cam.Pos.Y);
            }
            else if (cam.Pos.X + Game1.SCREEN_WIDTH / 2 > OutOfBounds.X + OutOfBounds.Width)
            {
                cam.Pos = new Vector2(OutOfBounds.X + OutOfBounds.Width - CamOriginalPosition.X, cam.Pos.Y);

            }

            //cam.Pos = new Vector2(Game1.SCREEN_WIDTH/2, Game1.SCREEN_HEIGHT/2);
            if (InputManager.Instance.KeyPressed(Keys.B))
            {
                showbb = !showbb;
            }


            if (InputManager.Instance.MousePressed(MouseButton.Left))
            {
                Slice newSlice = new Slice((float)InputManager.Instance.GetMousePositionX() - Slice.sliceWidth / 2 + cam.Pos.X - Game1.SCREEN_WIDTH / 2, (float)InputManager.Instance.GetMousePositionY());
                sliceList.addSpriteReuse(newSlice);

                for (int i = 0; i < enemyList.count(); i++)
                {
                    Sprite3 currentEnemy = enemyList.getSprite(i);
                }

            }

            //PLAYER SLICE
            if (InputManager.Instance.KeyPressed(Keys.X) || InputManager.Instance.ButtonPressed(Buttons.RightShoulder))
            {
                if (player.canSlice == true)
                {
                    Slice newSlice = new Slice(player.getBoundingBoxMiddle().X - Slice.sliceWidth / 2, player.getBoundingBoxMiddle().Y);
                    sliceList.addSpriteReuse(newSlice);
                    player.Slice();
                    soundSlice.Play();
                }

            }

            if (InputManager.Instance.MousePressed(MouseButton.Right))
            {
                TestEnemy newEnemy = new TestEnemy(InputManager.Instance.GetMousePosition() + cam.Pos - new Vector2(Game1.SCREEN_WIDTH / 2 + 70 / 2, Game1.SCREEN_HEIGHT / 2 + 70 / 2));

                enemyList.addSpriteReuse(newEnemy);
                Console.WriteLine("targetSpawnPositions.Add(new Vector2(" + newEnemy.getPos().X + "," + newEnemy.getPos().Y + "));");


            }


            player.Update(gameTime);
            sliceList.Update(gameTime);
            particleList.Update(gameTime);
            enemyList.Update(gameTime);
            platformList.Update(gameTime);
            goal.Update(gameTime);

            //GOAL CONDITION
            if (player.getBoundingBoxAA().Intersects(goal.rectGoalZone))
            {
                if (player.IsOnGround)
                {
                    //WIN
                    player.SetCanPlayerMove(false);
                    player.velocity = Vector2.Zero;
                    player.acceleration = new Vector2(0, 1000f);

                    NextLevel();
                    

                }
            }



            //update abilities
            abilityIconDoubleJump.Update(gameTime);
            abilityIconDash.Update(gameTime);
            abilityIconSlice.Update(gameTime);


            //IF PLAYER IS OUT OF BOUNDS
            if (player.getPosX() < OutOfBounds.X || player.getPosX() > OutOfBounds.X + OutOfBounds.Width || player.getPosY() + player.getHeight() * 2 < OutOfBounds.Y || player.getPosY() > OutOfBounds.Y + OutOfBounds.Height)
            {
                soundDeath.Play();
                player.SetCanPlayerMove(false);
                player.AbilityReset();
                ResetPlayer();
            }



            //DETECT COLLISION BETWEEN SLICES AND TARGETS
            for (int i = 0; i < enemyList.count(); i++)
            {
                //Get current enemy to test.
                //If not active, skip.
                Sprite3 currentSprite = enemyList.getSprite(i);
                if (currentSprite == null) continue;
                if (currentSprite.active != true) continue;
                if (currentSprite.visible != true) continue;
                if (currentSprite is Enemy == false) continue;

                TestEnemy currentEnemy = currentSprite as TestEnemy;


               

                //Get current slice to test.
                //If not active, skip.
                for (int j = 0; j < sliceList.count(); j++)
                {
                    currentSprite = sliceList.getSprite(j);
                    if (currentSprite == null) continue;
                    if (currentSprite.active != true) continue;
                    if (currentSprite.visible != true) continue;
                    if (currentSprite is Slice)
                    {
                        Slice currentSlice = currentSprite as Slice;
                        //loop thru all the current enemy's hitboxes until a slice collides with it.
                        for (int k = 0; k < currentEnemy.listHitbox.count(); k++)
                        {
                            Sprite3 currentHitBox = currentEnemy.listHitbox.getSprite(k);
                            if (currentHitBox == null) continue;
                            if (currentHitBox.active != true) continue;
                            if (currentEnemy.active == false) continue;


                            //Check if the slice has hit anything else yet.
                            if (currentSlice.canHit == true)
                            {
                                //SLICE HIT THE ENEMYHITBOX.
                                if (currentSlice.collision(currentHitBox))
                                {


                                   
                                    float sliceWidth = currentSlice.getBB().Width;
                                    float sliceLeft = currentSlice.getBoundingBoxMiddle().X - sliceWidth / 2;
                                    float sliceRight = currentSlice.getBoundingBoxMiddle().X + sliceWidth / 2;
                                    float hitboxWidth = currentHitBox.getBB().Width;
                                    float hitboxLeft = currentHitBox.getBoundingBoxMiddle().X - hitboxWidth / 2;
                                    float hitboxRight = currentHitBox.getBoundingBoxMiddle().X + hitboxWidth / 2;

                                    if (sliceLeft <= hitboxLeft && sliceRight >= hitboxRight)
                                    {
                                        //Console.WriteLine("FULL CUT");
                                        soundSliceHit.Play();
                                        SliceParticle topSlice;
                                        float scaleWidth = currentHitBox.getWidth() / currentEnemy.getTextureBase().Width;
                                        float scaleHeight = currentHitBox.getHeight() / currentEnemy.getTextureBase().Height;
                                        Vector2 ScaleVec = new Vector2(scaleWidth, scaleHeight);


                                        Rectangle topDest = new Rectangle(currentHitBox.getBoundingBoxAA().X, currentHitBox.getBoundingBoxAA().Y, currentHitBox.getBoundingBoxAA().Width, (int)currentSlice.getBoundingBoxMiddle().Y - currentHitBox.getBoundingBoxAA().Y);
                                        float topScaleX;
                                        float topScaleY;
                                        Rectangle topSourceRect = new Rectangle(0, 0, (int)(currentHitBox.getBoundingBoxAA().Width / scaleWidth), (int)((currentSlice.getBoundingBoxMiddle().Y - currentHitBox.getBoundingBoxAA().Y) / scaleHeight));
                                        //Rectangle topSourceRect = currentHitBox.texBase.Bounds;
                                        //Console.WriteLine("TOPSLICE: " + topDest.ToString());

                                        topSlice = new SliceParticle(topDest, currentEnemy.getTextureBase(), topSourceRect, ScaleVec, 0.8f, new Vector2(player.velocity.X / 2, -50 - Math.Abs(player.velocity.X)));
                                        particleList.addReuse(topSlice);

                                        SliceParticle bottomSlice;
                                        Rectangle bottomDest = new Rectangle(currentHitBox.getBoundingBoxAA().X, (int)currentSlice.getBoundingBoxAA().Y, currentHitBox.getBoundingBoxAA().Width, currentHitBox.getBoundingBoxAA().Y + currentHitBox.getBoundingBoxAA().Height - currentSlice.getBoundingBoxAA().Y);
                                        Rectangle bottomSourceRect = new Rectangle(0, (int)((currentSlice.getBoundingBoxMiddle().Y - currentHitBox.getBoundingBoxAA().Y) / scaleHeight), (int)(currentHitBox.getBoundingBoxAA().Width / scaleWidth), (int)(currentEnemy.texBase.Height - ((currentSlice.getBoundingBoxMiddle().Y - currentHitBox.getBoundingBoxAA().Y) / scaleHeight)));
                                        bottomSlice = new SliceParticle(bottomDest, currentEnemy.getTextureBase(), bottomSourceRect, ScaleVec, -0.8f, new Vector2(player.velocity.X / 2, 0 - Math.Abs(player.velocity.X) / 2));
                                        particleList.addReuse(bottomSlice);
                                        //Console.WriteLine("BOTTOMSLICE: " + bottomDest.ToString());

                                        if (!currentEnemy.canRespawn)
                                        {
                                            currentEnemy.setActive(false);
                                        }
                                        else
                                        {
                                            currentEnemy.isDead = true;
                                        }
                                        
                                        
                                        currentEnemy.setVisible(false);

                                        player.AbilityReset();


                                        break;

                                    }




                                }
                            }


                        }



                    }
                    else { continue; }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {

            graphicsDevice.Clear(Color.LightCoral);


            //implements a camera
            spriteBatch.Begin(SpriteSortMode.Immediate,
                                    BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(graphicsDevice));
            //background here

            platformList.Draw(spriteBatch);
            enemyList.Draw(spriteBatch);
            sliceList.Draw(spriteBatch);
            particleList.Draw(spriteBatch);
          
            player.Draw(spriteBatch);
            if (showbb)
            {
                //enemyList.drawInfo(spriteBatch, Color.Red, Color.Yellow);
                sliceList.drawInfo(spriteBatch, Color.Red, Color.Yellow);
                player.drawBB(spriteBatch, Color.Red);
                platformList.drawInfo(spriteBatch, Color.Red, Color.Yellow);
                LineBatch.drawLineRectangle(spriteBatch, new Rectangle((int)PlayerSpawnPosition.X, (int)PlayerSpawnPosition.Y, 20, 20), Color.Black);
                LineBatch.drawLineRectangle(spriteBatch, OutOfBounds, Color.Red);
            }

          

            

            goal.Draw(spriteBatch);
            spriteBatch.End();


            //DRAW UI STUFF HERE
            spriteBatch.Begin();
            abilityIconDoubleJump.Draw(spriteBatch);
            abilityIconDash.Draw(spriteBatch);
            abilityIconSlice.Draw(spriteBatch);

            TextRenderable score = new TextRenderable("SCORE: " + Game1.TimeScore.ToString(), new Vector2(100, 100), MenuScreen.menuFont, Color.Black);
            score.Draw(spriteBatch);
            //spriteBatch.Draw(texMouseCursor, new Vector2(InputManager.Instance.GetMousePositionX(), InputManager.Instance.GetMousePositionY()), Color.White);
            spriteBatch.Draw(texMouseCursor, null, new Rectangle(InputManager.Instance.GetMousePositionX(), InputManager.Instance.GetMousePositionY(), 30, 30), null, null, 0, null, Color.White, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
        }
    }
}
