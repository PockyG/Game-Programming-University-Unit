using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RC_Framework;

namespace Assignment.Objects
{
    public class SliceParticle: RC_Renderable
    {

        float gravity = 500;
        Texture2D texture;
        Vector2 position;
        Vector2 velocity = Vector2.Zero;
        Vector2 acceleration;
        float gravityCap = 1000;
        Rectangle sourceRect;
        Rectangle destRect;
        float currentRotation = 0;
        float rotationRate = 0;
        
        float width;
        float height;
        Vector2 ScaleVec;
        

        public SliceParticle(Rectangle _destRect, Texture2D _texture, Rectangle _texSourceRect, Vector2 _scaleVec, float _startRotation, Vector2 _startVelocity) : base()
        {
            float randomNum = (float)Game1.Random.NextDouble() * 300;
            texture = _texture;
            position = new Vector2(_destRect.X, _destRect.Y);
            sourceRect = _texSourceRect;
            rotationRate = _startRotation + (float)Game1.Random.NextDouble() * 2;
            acceleration = new Vector2(0, gravity + randomNum);
            width = _destRect.Width;
            height = _destRect.Height;
            ScaleVec = _scaleVec;
            velocity = new Vector2(_startVelocity.X + (float)Game1.Random.NextDouble() * 20 , _startVelocity.Y);

            







        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            

            //move
            
            //cap
            if(velocity.Y > gravityCap)
            {
                velocity.Y = gravityCap;
            }

            position += velocity * deltaTime;
            velocity += acceleration * deltaTime;

            destRect = new Rectangle((int)position.X - (int)width/2, (int)position.Y - (int)height/2, (int)width, (int)height);

            currentRotation += rotationRate * deltaTime;





        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            //sb.Draw(texture, position, sourceRect, colour, currentRotation, new Vector2(sourceRect.Center.X, sourceRect.Center.Y), 0.5f, SpriteEffects.None, 0);
            // sb.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 100, 100), Color.White);
            //sb.Draw(texture, null, destRect, sourceRect, new Vector2(destRect.Width/2, destRect.Height/2), currentRotation, Vector2.One, Color.White, SpriteEffects.None, 0);
            //sb.Draw(texture, null, destRect, null, new Vector2(destRect.Width / 2, destRect.Height / 2), currentRotation, Vector2.One, Color.White, SpriteEffects.None, 0);

            //sb.Draw(texture, new Rectangle((int)position.X , (int)position.Y , (int)width, (int)height), Color.White);

            
            
            //sb.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height), null , Color.White, currentRotation,Vector2.Zero, SpriteEffects.None, 0);
            sb.Draw(texture, position + ((ScaleVec* new Vector2(texture.Width, texture.Height)) * new Vector2(0.5f,0.5f)), sourceRect, Color.White, currentRotation, new Vector2(texture.Width/2, texture.Height/2), ScaleVec, SpriteEffects.None, 0);
        }


    }
}
