using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment.Objects
{
    public class TestEnemy : Enemy
    {
        public static Texture2D texTestEnemy;
        public float testWidth = 70;
        public float testHeight = 70;

        private float currentLerpTime = 0;
        private float lerpTime = 0.8f;

        private Vector2 hoverPointTop;
        private Vector2 hoverPointBottom;
        bool posDirection = true;

        public bool canRespawn = false;
        public float respawnAmount = 3;
        private float respawnTimer = 0;
        public bool isDead = false;

        public Vector2 Position
        {
            get { return getPos(); }
            set
            {
                 hoverPointTop = value + new Vector2(0, -5);
                hoverPointBottom = value + new Vector2(0, 5);
            }
        }



        public TestEnemy(Vector2 _position) : base()
        {
            setTexture(texTestEnemy, true);
            setActive(true);
            setVisible(true);

            setWidthHeight(testWidth, testHeight);
            setBBToWH();
            setPos(_position);
            EnemyHitbox hitbox1 = new EnemyHitbox(this);

            //hitbox1.setBBToWH();
            hitbox1.setWidthHeight(testWidth, testHeight);
            hitbox1.setWidthHeightOfTex(testWidth, testHeight);
            hitbox1.setBBToWH();

            hitbox1.setVisible(true);
            hitbox1.setActive(true);

            hoverPointTop = pos + new Vector2(0, -5);
            hoverPointBottom = pos + new Vector2(0, 5);



            listHitbox.addSpriteReuse(hitbox1);

            currentLerpTime = (float)Game1.Random.NextDouble() % lerpTime;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(isDead && canRespawn)
            {
                respawnTimer += deltaTime;

                if(respawnTimer > respawnAmount)
                {
                    isDead = false;
                    respawnTimer = 0;
                    active = true;
                    visible = true;
                }
            }
            else
            {
                if (posDirection)
                {
                    currentLerpTime += deltaTime;
                    if (currentLerpTime > lerpTime)
                    {
                        currentLerpTime = lerpTime;
                        posDirection = false;
                    }
                }
                else
                {
                    currentLerpTime -= deltaTime;
                    if (currentLerpTime < 0)
                    {
                        currentLerpTime = 0;
                        posDirection = true;
                    }
                }



                float t = currentLerpTime / lerpTime;
                t = t * t * (3f - 2f * t);

                setPos(Vector2.Lerp(hoverPointTop, hoverPointBottom, t));
            }

           



        }


    }
}
