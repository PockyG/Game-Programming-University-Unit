using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace Assignment.UI
{
    public class MenuScreen : RC_GameStateParent
    {
        SpriteFont titleFont;
        SpriteFont menuFont;

        Texture2D selectorTex;

        TestEnemy selector;

        TextRenderable textRenderable;

        List<String> menuStrings;

        int menuSelector = 0;
        string titleString = "HypeReset";

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

            TestEnemy.texTestEnemy = selectorTex;
            selector = new TestEnemy(new Vector2(0,0));

        }

        public override void EnterLevel(int fromLevelNum)
        {
            base.EnterLevel(fromLevelNum);
            menuSelector = 0;

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
            }
            if (InputManager.Instance.KeyPressed(Keys.Down) || InputManager.Instance.ButtonPressed(Buttons.DPadDown))
            {
                menuSelector += 1;
                if (menuSelector > menuStrings.Count - 1)
                {
                    menuSelector = 0;
                }
            }

            //SELECT OPTION
            if(InputManager.Instance.KeyPressed(Keys.Z) || InputManager.Instance.ButtonPressed(Buttons.A)){
                switch (menuSelector)
                {
                    case 0:
                        gameStateManager.setLevel(1);
                        break;
                    case 1:
                        gameStateManager.setLevel(0);
                        break;
                    case 2:
                        Game1.game1.Exit();
                        break;
                    default:
                        Game1.game1.Exit();
                        break;
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
                textRenderable = new TextRenderable(menuStrings[i], middleScreen + new Vector2(0, i * 30), menuFont, Color.White);
                textRenderable.Draw(spriteBatch);
            }
            selector.Position = middleScreen + new Vector2(200, menuSelector * 30 - selector.getWidth()/2);
            selector.Draw(spriteBatch);

            spriteBatch.End();


        }
    }
}
