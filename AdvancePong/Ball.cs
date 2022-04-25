using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Porn
{
    class Ball
    {
        private int _widthAndHeight;
        private float scale;
        private int ballSpeed;
        private Vector2 currentPosition;
        Texture2D ballTexture;
        private Court court;
        private Vector2 ballDirection;
        //private float rotation = 0;
        private int collisionTimerMillis = 0;
        private int originalSpeed;
        private int CollisionTimerIntervalMillis = 50;
        private int _ballStick;

        //private Rectangle _currentboundingBox;

        public int BallStick {
            set {
                _ballStick = value;
            }
        }

        public Rectangle CurrentboundingBox
        {
            get
            {
                return new Rectangle((int)currentPosition.X, (int)currentPosition.Y, _widthAndHeight, _widthAndHeight);
            }
        }

        public int BallSpeed {
            get {
                return ballSpeed;
            }
        }

        public Ball(int widthAndHeight, int scale, int ballSpeed, Vector2 startPosition, Court court, Point direction) {
            //activeStick = false;
            CollisionTimerIntervalMillis *= scale;
            int vectorX = 0;
            int vectorY = 0;
            this._widthAndHeight = widthAndHeight;
            this.scale = (float)scale;
            this.ballSpeed = ballSpeed;
            originalSpeed = this.ballSpeed;
            this.currentPosition = startPosition * scale;
            this.court = court;
            if (direction.X == 1)
                vectorX = 1;
            else if (direction.X == 2)
                vectorX = -1;
            if (direction.Y == 1)
                vectorY = 0;
            else if (direction.Y == 2)
                vectorY = 1;
            else if (direction.Y == 3)
                vectorY = -1;
            if(vectorX !=0 || vectorY != 0)
                this.ballDirection = new Vector2(vectorX, vectorY);
            else
                this.ballDirection = new Vector2(1, 1);
        }

        public void LoadContent(Texture2D ball)
        {
            this.ballTexture = ball;
        }

        public int Update(GameTime gametime) {
            currentPosition += ballDirection * ballSpeed* (float)gametime.ElapsedGameTime.TotalSeconds;

            collisionTimerMillis += (int)gametime.ElapsedGameTime.TotalMilliseconds;

            if (currentPosition.Y >= court.CourHeight - court.CourtBoard - _widthAndHeight || currentPosition.Y <= 0 + court.CourtBoard)
                ballDirection.Y *= -1;

            if (currentPosition.X >= court.CourtWidth + _widthAndHeight*2)
                return 1;
            else if (currentPosition.X <= 0 - +_widthAndHeight*2)
                return 2;
            else
                return 0;
            //rotation += 1f;
        }

        public int Update(GameTime gametime, KeyboardState kbState, KeyboardState kbPreState, float paddleOneMovementY, float paddleTwoMovementY, int windosWidth)
        {
            collisionTimerMillis += (int)gametime.ElapsedGameTime.TotalMilliseconds;
            if (kbState.IsKeyUp(Keys.OemPlus) && kbPreState.IsKeyDown(Keys.OemPlus))
            {
                ballSpeed += 50;
                if (ballSpeed >= 500* (int)scale)
                {
                    ballSpeed = 500 * (int)scale;
                }
            }
            else if (kbState.IsKeyUp(Keys.OemMinus) && kbPreState.IsKeyDown(Keys.OemMinus))
            {
                ballSpeed -= 50;
                if (ballSpeed <= 0) {
                    ballSpeed = 0;
                }
            }
            if (kbState.IsKeyUp(Keys.Space) && kbPreState.IsKeyDown(Keys.Space) && currentPosition.X < windosWidth/2)
            {
                _ballStick = 0;
            }

            if (kbState.IsKeyUp(Keys.Enter) && kbPreState.IsKeyDown(Keys.Enter) && currentPosition.X > windosWidth / 2)
            {
                _ballStick = 0;
            }


            if (_ballStick == 0) { 
                currentPosition += ballDirection * ballSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                //collisionTimerMillis += (int)gametime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (_ballStick == 1)
            {
                //collisionTimerMillis = 0;
                currentPosition.Y += paddleOneMovementY;
            }
            else if (_ballStick == 2) {
                //collisionTimerMillis = 0;
                currentPosition.Y += paddleTwoMovementY;
            }

                       


            if (currentPosition.Y >= court.CourHeight - court.CourtBoard - _widthAndHeight || currentPosition.Y <= 0 + court.CourtBoard)
                ballDirection.Y *= -1;

            if (currentPosition.X >= court.CourtWidth + _widthAndHeight * 2)
                return 1;
            else if (currentPosition.X <= 0 - +_widthAndHeight * 2)
                return 2;
            else
                return 0;
            //rotation += 1f;
        }

        public void Draw(SpriteBatch spriteBatch) {
            //spriteBatch.Draw(ballTexture, currentPosition, null, Color.White, rotation, new Vector2(3, 3),scale,SpriteEffects.None, 0f);
            spriteBatch.Draw(ballTexture, currentPosition, null, Color.White, 0f,Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public bool ProcessCollision(Rectangle boudingBox)
        {
            if (collisionTimerMillis > CollisionTimerIntervalMillis && this.CurrentboundingBox.Intersects(boudingBox))
            {
                collisionTimerMillis = 0;

                Rectangle intersection = Rectangle.Intersect(this.CurrentboundingBox, boudingBox);
                if (intersection.Width > intersection.Height)
                {
                    ballDirection.Y *= -1;
                    return true;
                }
                else if (intersection.Width < intersection.Height)
                {
                    ballDirection.X *= -1;
                    return true;
                }
                else
                {
                    ballDirection *= -1;
                    return true;
                }
            }
            else
                return false;

        }

        public void Reset(Vector2 startPosition, Point direction) {
            this.currentPosition = startPosition * scale;
            this.ballSpeed = this.originalSpeed;
            int vectorX = 0;
            int vectorY = 0;
            if (direction.X == 1)
                vectorX = 1;
            else if (direction.X == 2)
                vectorX = -1;
            if (direction.Y == 1)
                vectorY = 0;
            else if (direction.Y == 2)
                vectorY = 1;
            else if (direction.Y == 3)
                vectorY = -1;
            if (vectorX != 0 || vectorY != 0)
                this.ballDirection = new Vector2(vectorX, vectorY);
            else
                this.ballDirection = new Vector2(1, 1);
            //this.ballDirection = new Vector2(1, 0);
        }
    }
}
