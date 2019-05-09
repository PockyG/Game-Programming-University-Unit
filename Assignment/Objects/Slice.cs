using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC_Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment.Objects
{
    public class Slice : Sprite3
    {
        float fadeTime = 1;
        float counter = 0;
        public static Texture2D pixel;
        public static float sliceWidth = 170;

        public bool hasHit = false;
        public bool canHit = true;
        

        public Slice(float x, float y) : base()
        {

            setTexture(pixel, true);
            setPos(new Vector2(x, y));
            setWidthHeight(sliceWidth, 1);
            setWidthHeightOfTex(sliceWidth, 1);
            setBBToWH();
           
            setColor(Color.White);
            setActive(true);
            setVisible(true);





            setFadeDetails(true, Color.Red, Color.Transparent, 20, false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            

            counter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(counter >= 0.1f)
            {
                setColor(Color.Red);
                canHit = false;
                

                doTheFade();
            }

            if(counter >= fadeTime)
            {
                setActive(false);
            }

            

            
            


        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            

          
        }


    }
}
