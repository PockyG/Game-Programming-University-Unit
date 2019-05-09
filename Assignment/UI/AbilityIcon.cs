using Assignment.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.UI
{
    public class AbilityIcon
    {
        Texture2D texAbility;
        Vector2 position;
        Color currentColor = Color.White;

        private float width = 80;


        public bool isReady = true;

        

        public AbilityIcon(Texture2D _texAbility, Vector2 _position)
        {
            position = _position;
            texAbility = _texAbility;
            
        }

        


        public void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;

            if (isReady)
            {
                currentColor = Color.White;
            }
            else
            {
                currentColor = new Color(100, 100, 100, 100);
            }


        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texAbility, new Rectangle((int)position.X, (int)position.Y, (int)width, (int)width),currentColor);
        }

        
    }
}
