using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2
{
    class EnemyEasy : Sprite3
    {
        public static Player player;
        int pattern = 0;
        int patternCount = 3;
        

        public EnemyEasy(bool visibleZ, Texture2D texZ, float x, float y, int _pattern) : base( visibleZ,  texZ,  x,  y)
        {
            hitPoints = 3;
            if(_pattern > patternCount)
            {
                _pattern = patternCount;
            }
            pattern = _pattern;
            varInt0 = 0; // GET HIT PARTICLE
           
        }

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);



            switch (pattern)
            {
                case 0:
                    setPosX(getPosX() - 3); // basic just move left
                    break;
                case 1: // seek player
                    setPosX(getPosX() - 3);
                    if(player.getPosY() + player.getHeight()/2 < getPosY() + getHeight()/2)
                    {
                        moveByDeltaY(-1);
                    }
                    else
                    {
                        moveByDeltaY(1);
                    }
                    break;
                case 2: // seek player with variance
                    setPosX(getPosX() - moveSpeed);
                    if(varInt2 == 0)
                    {
                        varInt2 = Game1.random.Next() % 4 + 1;
                    }
                    if (player.getPosY() + player.getHeight() / 2 < getPosY() + getHeight() / 2)
                    {
                        moveByDeltaY(-1 - varInt2);
                    }
                    else
                    {
                        moveByDeltaY(1 + varInt2);
                    }
                    break;
                default:
                    setPosX(getPosX() - 3);
                    break;

            }
            





            if (varInt0 > 0)
            {
                varInt0 -= 5;
                if (varInt0 < 0)
                {
                    varInt0 = 0;
                }

                colour = new Color(255, 0 + (255 / 100) * (100 - varInt0), 0 + (255 / 100) * (100 - varInt0));
            }
        }

    }
}
