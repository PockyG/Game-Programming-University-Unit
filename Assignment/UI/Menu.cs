using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Levels;
using Assignment.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace Assignment.UI
{
    public class MenuScreen : RC_GameStateParent
    {
        SpriteFont titleFont;
        public static SpriteFont menuFont;

        Texture2D selectorTex;

        TestEnemy selector;

        TextRenderable textRenderable;
        public static SoundEffect soundMenu;

        static List<String> menuStrings = new List<String>();

        int menuSelector = 0;
        string titleString = "HypeReset";

        protected RC_RenderableList particleList;
        protected SpriteList enemyList;
        protected SpriteList sliceList;

        private bool notSelected = true;
        public static bool unlockChallengeMode = false;

        public static void UnlockChallengeMode()
        {
            if (!unlockChallengeMode)
            {
                unlockChallengeMode = true;
                menuStrings.Add("CHALLENGE MODE");

            }
        }

        public override void LoadContent()
        {
            base.LoadContent();

            menuStrings = new List<String>();
            menuStrings.Add("START GAME");
            menuStrings.Add("TUTORIAL");
            menuStrings.Add("EXIT GAME");

            titleFont = Content.Load<SpriteFont>("File");
            menuFont = Content.Load<SpriteFont>("File");
            selectorTex = Content.Load<Texture2D>("Target");

            soundMenu = Content.Load<SoundEffect>("sounds/MenuMove");

            TestEnemy.texTestEnemy = selectorTex;
           
            particleList = new RC_RenderableList();
            sliceList = new SpriteList();
            enemyList = new SpriteList();

            





        }

        public override void EnterLevel(int fromLevelNum)
        {
            base.EnterLevel(fromLevelNum);
            selector = new TestEnemy(new Vector2(0, 0));
            enemyList.addSpriteReuse(selector);
            menuSelector = 0;
            notSelected = true;
            Game1.TimeScore = 0;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            selector.Update(gameTime);

            //MOVE OPTION
            if (InputManager.Instance.KeyPressed(Keys.Up) || InputManager.Instance.ButtonPressed(Buttons.DPadUp))
            {
                menuSelector -= 1;
                if(menuSelector < 0)
                {
                    menuSelector = menuStrings.Count - 1;
                }
                soundMenu.Play();
            }
            if (InputManager.Instance.KeyPressed(Keys.Down) || InputManager.Instance.ButtonPressed(Buttons.DPadDown))
            {
                menuSelector += 1;
                if (menuSelector > menuStrings.Count - 1)
                {
                    menuSelector = 0;
                }
                soundMenu.Play();
            }

            //SELECT OPTION
            if(notSelected)
            if(InputManager.Instance.KeyPressed(Keys.Z) || InputManager.Instance.ButtonPressed(Buttons.A)){
                    notSelected = false;
                    switch (menuStrings[menuSelector])
                {
                    case "START GAME":
                            BaseLevel.isChallengeMode = false;
                            Task.Delay(1000).ContinueWith(t => gameStateManager.setLevel(1));
                            break;
                    case "TUTORIAL":
                            BaseLevel.isChallengeMode = false;
                            Task.Delay(1000).ContinueWith(t => gameStateManager.setLevel(0));
                            break;
                    case "EXIT GAME":
                            Task.Delay(1000).ContinueWith(t => Game1.game1.Exit());
                        break;
                        case "CHALLENGE MODE":
                            BaseLevel.isChallengeMode = true;
                            Task.Delay(1000).ContinueWith(t => gameStateManager.setLevel(1));
                            break;

                        default:
                        Game1.game1.Exit();
                        break;
                }
                sliceList.addSpriteReuse(new Slice(selector.getPosX(), selector.getPosY() + selector.getHeight()/2));
            }

            //
            particleList.Update(gameTime);
            sliceList.Update(gameTime);

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

                Enemy currentEnemy = currentSprite as Enemy;


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
                                        BaseLevel.soundSliceHit.Play();
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

                                        topSlice = new SliceParticle(topDest, currentEnemy.getTextureBase(), topSourceRect, ScaleVec, 0.8f, new Vector2(200, -50 - Math.Abs(100)));
                                        particleList.addReuse(topSlice);

                                        SliceParticle bottomSlice;
                                        Rectangle bottomDest = new Rectangle(currentHitBox.getBoundingBoxAA().X, (int)currentSlice.getBoundingBoxAA().Y, currentHitBox.getBoundingBoxAA().Width, currentHitBox.getBoundingBoxAA().Y + currentHitBox.getBoundingBoxAA().Height - currentSlice.getBoundingBoxAA().Y);
                                        Rectangle bottomSourceRect = new Rectangle(0, (int)((currentSlice.getBoundingBoxMiddle().Y - currentHitBox.getBoundingBoxAA().Y) / scaleHeight), (int)(currentHitBox.getBoundingBoxAA().Width / scaleWidth), (int)(currentEnemy.texBase.Height - ((currentSlice.getBoundingBoxMiddle().Y - currentHitBox.getBoundingBoxAA().Y) / scaleHeight)));
                                        bottomSlice = new SliceParticle(bottomDest, currentEnemy.getTextureBase(), bottomSourceRect, ScaleVec, -0.8f, new Vector2(-100, 0 - Math.Abs(-5)));
                                        particleList.addReuse(bottomSlice);
                                        //Console.WriteLine("BOTTOMSLICE: " + bottomDest.ToString());

                                        currentEnemy.setActive(false);
                                        currentEnemy.setVisible(false);
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

            Vector2 middleScreen = new Vector2(Game1.SCREEN_WIDTH / 2, Game1.SCREEN_HEIGHT / 2);

            spriteBatch.Begin();
            
            textRenderable = new TextRenderable(titleString, middleScreen + new Vector2(0, -100), titleFont, Color.White);
            textRenderable.Draw(spriteBatch);
            for(int i = 0; i < menuStrings.Count; i++)
            {
                textRenderable = new TextRenderable(menuStrings[i], middleScreen + new Vector2(0, i * 50), menuFont, Color.White);
                textRenderable.Draw(spriteBatch);
            }

            textRenderable = new TextRenderable("HIGHSCORES" , middleScreen + new Vector2(-300, -50), menuFont, Color.White);
            textRenderable.Draw(spriteBatch);
            for (int i = 0; i < 5; i++)
            {
                textRenderable = new TextRenderable( (i+1).ToString() + ".  " +  Game1.Scores[i].ToString(), middleScreen + new Vector2(-300, i * 50), menuFont, Color.White);
                textRenderable.Draw(spriteBatch);
            }

            if(unlockChallengeMode == true)
            {
                textRenderable = new TextRenderable("CHALLENGE HIGHSCORES", middleScreen + new Vector2(300, -50), menuFont, Color.Red);
                textRenderable.Draw(spriteBatch);
                for (int i = 0; i < 5; i++)
                {
                    textRenderable = new TextRenderable((i + 1).ToString() + ".  " + Game1.ChallengeScores[i].ToString(), middleScreen + new Vector2(300, i * 50), menuFont, Color.Red);
                    textRenderable.Draw(spriteBatch);
                }
            }

            selector.Position = middleScreen + new Vector2(200, menuSelector * 50 - selector.getWidth()/2);
            selector.Draw(spriteBatch);

            particleList.Draw(spriteBatch);
            sliceList.Draw(spriteBatch);
            spriteBatch.End();


        }
    }
}
