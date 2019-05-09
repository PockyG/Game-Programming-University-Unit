using Assignment.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Objects
{
    public class Enemy : Sprite3
    {
        public SpriteList listHitbox;

        public Enemy() : base()
        {
            Initialize();
        }

        public void Initialize()
        {
            listHitbox = new SpriteList();

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            listHitbox.Update(gameTime);




        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            listHitbox.Draw(sb);
            if (BaseLevel.showbb)
            {
                listHitbox.drawInfo(sb, Color.Blue, Color.Yellow);
            }

            
        }


    }
}
