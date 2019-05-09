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
    public class GoalZone
    {
        Vector2 position;
        public Rectangle rectGoalZone;
        float zoneWidth;
        float zoneHeight;
        Sprite3 parent = null;


        public GoalZone(Vector2 _position, float _width, float _height)
        {
            zoneWidth = _width;
            zoneHeight = _height;


            position = _position;
        }

        public GoalZone(Sprite3 _parent)
        {
            parent = _parent;
        }

        public void Update(GameTime _gameTime)
        {
            if (parent == null)
            {
                rectGoalZone = new Rectangle((int)position.X, (int)position.Y, (int)zoneWidth, (int)zoneHeight);
            }
            else
            {
                rectGoalZone = new Rectangle((int)parent.getPosX(), (int)parent.getPosY() - 70, (int)parent.getWidth(), 70);
            }

        }

        public void Draw(SpriteBatch sb)
        {
            LineBatch.drawLineRectangle(sb,rectGoalZone, Color.Black);
        }



    }
}
