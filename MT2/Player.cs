using Microsoft.Xna.Framework;
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
    class Player:Sprite3
    {

        //Area player can move in
        public Rectangle playArea;
        int lhs = 20;
        //int rhs = SCREEN_WIDTH - 50;
        int rhs = 250;
        int bot = 50;
        int top = Game1.SCREEN_HEIGHT - 50;



        int playerSpeed = 4;



        //This changes the scroll background speed
        public float playerBackgroundSpeed = 0;



        public Player(bool visibleZ, Texture2D texZ, float x, float y):base(visibleZ, texZ, x, y)
        {
            hitPoints = 5;
            playArea = new Rectangle(lhs, top, rhs - lhs, bot - top);

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            

            //PLAYER MOVEMENT
            if (InputManager.Instance.KeyDown(Keys.Left))
            {
                if (this.getPosX() > playArea.Left)
                {
                    this.setPosX(this.getPosX() - playerSpeed);
                    playerBackgroundSpeed += 0.1f;
                }
                
            }
            if (InputManager.Instance.KeyDown(Keys.Right))
            {
                if (this.getPosX() < playArea.Right - this.getWidth())
                {
                    this.setPosX(this.getPosX() + playerSpeed);
                    playerBackgroundSpeed += -0.1f;
                }
                
            }
            if (InputManager.Instance.KeyDown(Keys.Up))
            {

                if (this.getPosY() > playArea.Bottom)
                {
                    this.setPosY(this.getPosY() - playerSpeed);
                }
            }
            if (InputManager.Instance.KeyDown(Keys.Down))
            {
                if (this.getPosY() < playArea.Top - this.getHeight())
                {
                    this.setPosY(this.getPosY() + playerSpeed);
                }
            }


            //limit scroll speed
            if (playerBackgroundSpeed > -1)
            {
                playerBackgroundSpeed = -1;
            }
            else if (playerBackgroundSpeed < -10)
            {
                playerBackgroundSpeed = -10;
            }



            if(varInt0 > 0)
            {
                varInt0 -= 3;
                if(varInt0 < 0)
                {
                    varInt0 = 0;
                    colour = Color.White;
                }
                else
                {
                    colour = new Color(255, 0 + (255 / 100) * (100 - varInt0), 0 + (255 / 100) * (100 - varInt0));
                }

                
            }


        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

        }

        public void TakeDamage()
        {
            hitPoints -= 1;
            varInt0 = 100;
        }


    }
}
