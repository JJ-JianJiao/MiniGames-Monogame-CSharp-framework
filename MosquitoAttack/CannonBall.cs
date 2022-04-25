using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class CannonBall:AnimationObject
    {
        private Vector2 direction = new Vector2(0, -1);
        private Vector2 changeDirec = new Vector2(0, 0);
        private bool SineMoveActive = false;
        private float SineDegree = 60;
        private float currentSine = 60;
        private bool quarterOne = true;
        private bool quarterTwo = false;
        private bool quarterThree = false;
        private bool quarterFour = false;
        private enum CannonBallState { 
            Flying,
            Dispose
        }

        private CannonBallState currentCannonBallState = CannonBallState.Flying;
        //private Vector2 direction = new Vector2(1, -1);
        public CannonBall() { 
        
        }

        public CannonBall(int width, int height, int speed, int scale, Vector2 position, Rectangle gameBorder) : base(width, height, speed, scale, position, gameBorder)
        {

        }

        public Vector2 Direction { set => direction = value; get => direction; }

        public override void Update(GameTime gameTime)
        {
            switch (currentCannonBallState)
            {
                case CannonBallState.Flying:
                    if (direction.X > 0)
                    {
                        Position += Speed * (changeDirec + direction) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (direction.X < 0)
                    {
                        Position += Speed * (-changeDirec + direction) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else {
                        Position += Speed * (direction - changeDirec) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (this.Position.X < 0 || this.Position.X > GameBorder.Right - this.Width * Scale)
                    {
                        if(Math.Abs( this.direction.Y )> 0.000001) { 
                            this.direction.X *= -1;
                            this.Speed += 100;
                        }
                    }

                     if (this.Position.Y < 0 - Height * Scale || this.Position.X < -10*Scale || this.Position.X > (this.GameBorder.Right + 10) * Scale || this.Position.Y >= (378 - Height) * Scale) {
                        currentCannonBallState = CannonBallState.Dispose;
                        Position = new Vector2(500, 500) * Scale;
                    }

                    if (SineMoveActive)
                    {
                        if (quarterOne)
                        {
                            currentSine+=25;
                            changeDirec = new Vector2(1,0) * (float)Math.Sin(MathHelper.ToRadians((float)currentSine));
                            if (currentSine <= 0)
                            {
                                quarterOne = false;
                                quarterTwo = true;
                            }
                        }
                        else if (quarterTwo) {
                            currentSine+=25;
                            changeDirec = new Vector2(1, 0) * (float)Math.Sin(MathHelper.ToRadians((float)currentSine));
                            if (currentSine >= SineDegree)
                            {
                                quarterThree = true;
                                quarterTwo = false;
                            }
                        }
                        else if (quarterThree)
                        {
                            currentSine+=25;
                            changeDirec = new Vector2(1, 0) * (float)Math.Sin(MathHelper.ToRadians((float)currentSine));
                            if (currentSine <=0)
                            {
                                quarterFour = true;
                                quarterThree = false;
                            }
                        }
                        else if (quarterFour)
                        {
                            currentSine+=25;
                            changeDirec = new Vector2(1, 0) * (float)Math.Sin(MathHelper.ToRadians((float)currentSine));
                            if (currentSine >= SineDegree)
                            {
                                quarterOne = true;
                                quarterFour = false;
                            }
                        }
                    }
                    break;
                case CannonBallState.Dispose:
                    break;
                default:
                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentCannonBallState)
            {
                case CannonBallState.Flying:
                    if (this.ObjectFrameTexture != null && ObjectFrameActive)
                    {
                        spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, Color.Red * 0.5f);
                    }
                    base.Draw(spriteBatch);
                    if (Texture != null) {
                        //spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 0.01f *Scale, SpriteEffect, 0.0f);
                        if (Texture.Name.Equals("CannonBall_01"))
                        {
                            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 0.01f * Scale, SpriteEffect, 0.0f);
                        }
                        else if (Texture.Name.Equals("CannonBall_02"))
                        {
                            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 0.01f * Scale, SpriteEffect, 0.0f);
                        }
                        else if (Texture.Name.Equals("CannonBall_03"))
                        {
                            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 0.01f * Scale, SpriteEffect, 0.0f);
                        }
                        else if (Texture.Name.Equals("CannonBall_04"))
                        {
                            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 0.01f * Scale, SpriteEffect, 0.0f);
                        }
                        else if (Texture.Name.Equals("CannonBall"))
                        {
                            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffect, 0.0f);
                        }
                    }
                    break;
                case CannonBallState.Dispose:
                    break;
                default:
                    break;
            }

        }

        public void CannonBallHitEvent(bool isHit) {
            if (isHit) {
                currentCannonBallState = CannonBallState.Dispose;
                Position = new Vector2(500, 500) * Scale;
            }
        }

        public void SetSinProperty(bool activeSin, int sinDegree) {
            this.SineMoveActive = true;
            this.SineDegree = sinDegree;
            this.currentSine = sinDegree;
        }

        public bool IsDispose() {
            if (this.currentCannonBallState == CannonBallState.Dispose)
            {
                return true;
            }
            else {
                return false;
            }
        }

        internal bool Collision(Rectangle objectBorder)
        {
            if (this.currentCannonBallState == CannonBallState.Flying && this.ObjectBorder.Intersects(objectBorder))
            {
                this.CannonBallHitEvent(true);
                return true;
            }
            else
                return false;
        }
    }
}
