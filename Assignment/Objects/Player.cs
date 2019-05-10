using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace Assignment.Objects
{
    public enum CutType
    {
        HORIZONTAL,
        VERTICAL,
        DIAGONALPOSITIVE,
        DIAGONALNEGATIVE,
    }




    public class Player : Sprite3
    {
        public static SpriteList sliceList;
        

        public SpriteList platformList;
        public static Texture2D texPlayer;

        public static AbilityIcon iconDoubleJump;
        public static AbilityIcon iconDash;
        public static AbilityIcon iconSlice;

      
        public static SoundEffect soundJump;
        public static SoundEffect soundDoubleJump;
        public static SoundEffect soundDash;
        public static SoundEffect soundGroundHit;

        public bool canDoubleJump = true;
        public bool canDash = true;
        public bool canSlice = true;

        private bool canPlayerMove = true;

        private bool isOnGround;
        private bool justDashed = false;
        private float noGravityTimer = 0;
        private float noGravityLimit = 0.5f;
        public bool IsOnGround
        {
            get
            {
                return isOnGround;
            }
            set
            {
                if(isOnGround == false && value == true)
                {
                    soundGroundHit.Play();
                }
                isOnGround = value;
                if (value == true)
                {
                    //reset abilities
                    canDoubleJump = true;
                    canDash = true;
                    iconDash.isReady = true;
                    iconDoubleJump.isReady = true;
                    
                }

            }
        }

        public static bool isDrawbb = false;

        public bool isCheckGroundPlatforms = false;
        private Rectangle rectPlatformCheck;

        public Vector2 velocity;

        static float scaleEverything = 1f;

        private Vector2 velocityDecay = new Vector2(3000f, 0f);
        private float velocityHorizontalMovespeedCap = 400;

        public Vector2 acceleration = new Vector2(0f, 1000f);
        private Vector2 accelerationDecay = new Vector2(3000f, 0f);
        private Vector2 accelerationDefault = new Vector2(0, 1400f);
        private Vector2 accelerationPlayerFall = new Vector2(0, 900f);

        private float HorizontalMoveSpeed = 1200;

        private float horizontalDashVelocity = 1300;
        private float verticalDashVelocity = 600;

        private float jumpVelocity = 450 * scaleEverything;
        private float doubleJumpVelocity = 500 * scaleEverything;

        private float sliceCooldown = 0.25f;
        private float sliceTimer = 0;




        public Player(Vector2 _spawnPos) : base(true, texPlayer, _spawnPos.X, _spawnPos.Y)
        {
            setWidthHeight(50, 50);
            rectPlatformCheck = new Rectangle();

            velocity = new Vector2(0f, 0f);
            IsOnGround = false;
        }

        public void LoadPlatformsPlayer(SpriteList _platforms)
        {
            platformList = _platforms;
        }


        public void AbilityReset()
        {
            canDoubleJump = true;
            canDash = true;
            canSlice = true;
            iconDoubleJump.isReady = true;
            iconDash.isReady = true;
            iconSlice.isReady = true;
        }

        public void SetCanPlayerMove(bool _canPlayerMove)
        {
            canPlayerMove = _canPlayerMove;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //MOVEMENT
            //ALL CHECKS AND CHANGES BEFORE THE POSITION SET.

            //PLAYER SLICE COOLDOWN
            if (canSlice == false)
                sliceTimer += deltaTime;

            if (sliceTimer > sliceCooldown)
            {
                sliceTimer = 0;
                canSlice = true;
                iconSlice.isReady = true;
            }



            //DASH MECHANIC...no gravity for a duration
            //if (justDashed)
            //{
            //    acceleration.Y = 0;
            //    noGravityTimer += deltaTime;
            //    if(noGravityTimer > noGravityLimit)
            //    {
            //        noGravityTimer = 0;
            //        justDashed = false;
            //    }
            //}

            velocity += acceleration * deltaTime;

            if (velocity.X > velocityHorizontalMovespeedCap)
            {
                velocity.X -= accelerationDecay.X * deltaTime;
                if (velocity.X < velocityHorizontalMovespeedCap)
                {
                    velocity.X = velocityHorizontalMovespeedCap;
                }
                acceleration.X = 0;

            }
            else if (velocity.X < -velocityHorizontalMovespeedCap)
            {
                velocity.X += accelerationDecay.X * deltaTime;
                if (velocity.X > -velocityHorizontalMovespeedCap)
                {
                    velocity.X = -velocityHorizontalMovespeedCap;
                }
                acceleration.X = 0;
            }






            //FINALLY SET POSITION

            //set x pos.
            setPosX(getPosX() + velocity.X * deltaTime);



            //check horizontal collisions
            //COLLISION TEST WITH PLATFORMS
            for (int i = 0; i < platformList.count(); i++)
            {
                Sprite3 currentPlatform = platformList.getSprite(i);
                if (this.collision(currentPlatform))
                {
                    if (velocity.X > 0)
                    {
                        velocity.X = 0;
                        this.setPosX(currentPlatform.getPosX() - this.getWidth());
                    }
                    else if (velocity.X < 0)
                    {
                        velocity.X = 0;
                        this.setPosX(currentPlatform.getPosX() + currentPlatform.getWidth());
                    }


                }

            }





            setPosY(getPosY() + velocity.Y * deltaTime);



            //COLLISION TEST WITH PLATFORMS
            for (int i = 0; i < platformList.count(); i++)
            {
                Sprite3 currentPlatform = platformList.getSprite(i);
                if (this.collision(currentPlatform))
                {


                    if (velocity.Y >= 0 && this.getPosY() <= currentPlatform.getPosY())
                    {
                        this.setPos(getPosX(), currentPlatform.getPosY() - this.getHeight());
                        velocity.Y = 0;
                        IsOnGround = true;
                        
                    }
                    else if (velocity.Y < 0 && this.getPosY() >= currentPlatform.getPosY())
                    {
                        this.setPos(getPosX(), currentPlatform.getPosY() + currentPlatform.getHeight());
                        velocity.Y = 0;
                    }
                }
            }


            //If no input movement, slow down player
            if (canPlayerMove)
            {


                if (InputManager.Instance.KeyUp(Keys.Left) && InputManager.Instance.KeyUp(Keys.Right) && GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 0)
                {
                    acceleration.X = 0;

                    if (velocity.X != 0)
                    {
                        if (velocity.X > 0)
                        {
                            velocity.X -= deltaTime * velocityDecay.X;
                            if (velocity.X < 0)
                            {
                                velocity.X = 0;
                            }
                        }
                        else if (velocity.X < 0)
                        {
                            velocity.X += deltaTime * velocityDecay.X;
                            if (velocity.X > 0)
                            {
                                velocity.X = 0;
                            }
                        }
                    }
                }
                else
                {
                    //If travelling right and player moves left, increase the acceleration to opposite
                    if (InputManager.Instance.KeyDown(Keys.Left))
                    {
                        if (acceleration.X > -HorizontalMoveSpeed)
                        {
                            acceleration.X = -HorizontalMoveSpeed;
                        }

                        //If player velocity is more than the cap, slowly decay to the cap.
                        if (velocity.X > 0)
                        {
                            velocity.X -= deltaTime * velocityDecay.X;
                            if (velocity.X < 0)
                            {
                                velocity.X = 0;
                            }
                        }


                    }
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X != 0)
                    {
                        acceleration.X = HorizontalMoveSpeed * GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;

                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                        {
                            if (velocity.X < 0)
                            {
                                velocity.X += deltaTime * velocityDecay.X;
                                if (velocity.X > 0)
                                {
                                    velocity.X = 0;
                                }
                            }
                        }
                        else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                        {
                            if (velocity.X > 0)
                            {
                                velocity.X -= deltaTime * velocityDecay.X;
                                if (velocity.X < 0)
                                {
                                    velocity.X = 0;
                                }
                            }
                        }

                    }

                    if (InputManager.Instance.KeyDown(Keys.Right))
                    {
                        if (acceleration.X < HorizontalMoveSpeed)
                        {
                            acceleration.X = HorizontalMoveSpeed;
                        }
                        if (velocity.X < 0)
                        {
                            velocity.X += deltaTime * velocityDecay.X;
                            if (velocity.X > 0)
                            {
                                velocity.X = 0;
                            }
                        }


                    }
                    //If both left and right pressed down, 0 acceleration
                    if (InputManager.Instance.KeyDown(Keys.Right) && InputManager.Instance.KeyDown(Keys.Left))
                    {
                        acceleration.X = 0;
                    }
                }
            }




            //Is the player currently on a platform?
            bool isOnPlatform = false;
            for (int i = 0; i < platformList.count(); i++)
            {

                Sprite3 currentPlatform = platformList.getSprite(i);
                if (currentPlatform.getActive())
                    if (currentPlatform.getBoundingBoxAA().Intersects(rectPlatformCheck))
                    {

                        isOnPlatform = true;
                    }
            }
            if (isOnPlatform == false)
                IsOnGround = false;


            //PLAYER JUMP.
            if (canPlayerMove)
                if (InputManager.Instance.KeyPressed(Keys.Z) || InputManager.Instance.ButtonPressed(Buttons.A))
                {
                    if (IsOnGround)
                    {
                        velocity.Y = -jumpVelocity;
                        IsOnGround = false;
                        soundJump.Play();
                    }
                    else
                    {
                        if (canDoubleJump == true)
                        {
                            velocity.Y = -doubleJumpVelocity;
                            canDoubleJump = false;
                            iconDoubleJump.isReady = false;
                            soundDoubleJump.Play();

                        }
                    }
                }

            //If player holds the jump button, gets maximum jump, otherwise fall faster.
            if (canPlayerMove)
                if (InputManager.Instance.KeyDown(Keys.Z) || InputManager.Instance.ButtonDown(Buttons.A) && IsOnGround == false)
                {
                    acceleration.Y = accelerationPlayerFall.Y;
                }
                else
                {
                    acceleration.Y = accelerationDefault.Y;
                }


            //PLAYER DASH
            if (canPlayerMove)
                if (InputManager.Instance.KeyPressed(Keys.C) || InputManager.Instance.ButtonPressed(Buttons.B))
                {
                    if (canDash)
                    {
                        if (InputManager.Instance.KeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                        {

                            velocity.X = horizontalDashVelocity;
                            velocity.Y = -20;
                            canDash = false;
                            iconDash.isReady = false;
                            noGravityTimer = 0;
                            justDashed = true;
                            soundDash.Play();
                        }
                        else if (InputManager.Instance.KeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                        {
                            velocity.X = -horizontalDashVelocity;
                            velocity.Y = -20;
                            canDash = false;
                            iconDash.isReady = false;
                            noGravityTimer = 0;
                            justDashed = true;
                            soundDash.Play();
                        }


                    }
                }

            if (IsOnGround == false)
            {
                if (velocity.Y > 0)
                {
                    //check for platforms
                    isCheckGroundPlatforms = true;


                }

            }
            else
            {
                isCheckGroundPlatforms = false;
            }

            rectPlatformCheck = new Rectangle((int)this.getPosX(), (int)this.getPosY() + (int)this.getHeight(), (int)this.getWidth(), 5);

        }




        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            LineBatch.drawLineRectangle(sb, new Rectangle((int)this.getPos().X + (int)this.getWidth() / 2 - 1, (int)this.getPos().Y + (int)this.getHeight()/2 - 1,3, 3), Color.Blue);

            


            if (isDrawbb)
            {
                if (!isCheckGroundPlatforms)
                {
                    LineBatch.drawLineRectangle(sb, rectPlatformCheck, Color.Blue);
                }
                else
                {
                    LineBatch.drawLineRectangle(sb, rectPlatformCheck, Color.Yellow);
                }
            }
        }

        public void Slice()
        {
            canSlice = false;
            iconSlice.isReady = false;
            sliceTimer = 0;

        }
    }
}
