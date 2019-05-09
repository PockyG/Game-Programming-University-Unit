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
    public class EnemyHitbox : Sprite3
    {
        public static Texture2D texHitbox;
        public static SpriteList particleList;
        Enemy enemyParent;
        Vector2 offset = Vector2.Zero;
        static Color hitboxColor = Color.Transparent;
        

        public EnemyHitbox(Enemy parent) : base()
        {
            enemyParent = parent;
            setTexture(texHitbox, false);
            setColor(hitboxColor);
        }

        public void SetOffset(Vector2 _offset)
        {
            offset = _offset;
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            setPos(enemyParent.getPos() + offset);
            if(enemyParent.active == false)
            {
                setActive(false);
            }
            
            
            
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            //sb.Draw(texHitbox, getPos(), new Color(10,10,10,10));
            sb.Draw(texHitbox, new Rectangle((int)getPos().X, (int)getPos().Y, (int)getWidth(), (int)getHeight()), hitboxColor);


        }

        public void AddTextureDeathParticles(Vector2 cutPosition, CutType typeOfCut)
        {
            switch (typeOfCut)
            {
                case CutType.HORIZONTAL:
                    break;
                case CutType.VERTICAL:
                    break;
                case CutType.DIAGONALPOSITIVE:
                    break;
                case CutType.DIAGONALNEGATIVE:
                    break;
            }
            //add two particles with the split texture.
            //particleList.addSpriteReuse();
        }


    }
}
