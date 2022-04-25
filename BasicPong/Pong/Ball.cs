using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Ball
    {
        protected const int WidthAndHeight = 7;
        protected const int Speed = 200;
        protected int collisionTimerMillis = 0;
        protected const int CollisionTImerIntervalMillis = 200;
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 direction;
        protected Vector2 dimensions = new Vector2(WidthAndHeight, WidthAndHeight);
        protected int scale;
        protected Rectangle gameBoundingBox;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X * scale, (int)dimensions.Y * scale);
            }
        }

        internal void Initialize(int scale, Rectangle gameBoundingBox, Vector2 initialPosition, Vector2 initialDirection)
        {
            this.scale = scale;
            this.gameBoundingBox = gameBoundingBox;
            position = initialPosition;
            direction = initialDirection;
        }

        internal void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Ball");
        }

        internal void Update(GameTime gameTime)
        {
            position += Speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

            collisionTimerMillis += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (position.X <= gameBoundingBox.Left)
            {
                direction.X *= -1;
            }

            if (position.Y + dimensions.Y * scale >= gameBoundingBox.Bottom ||
                position.Y <= gameBoundingBox.Top)
            {
                direction.Y *= -1;
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero,scale, SpriteEffects.None, 0f);
        }

        internal void ProcessCollision(Rectangle boundingBox) {
            if (collisionTimerMillis > CollisionTImerIntervalMillis && this.BoundingBox.Intersects(boundingBox)) {
                //collision!
                collisionTimerMillis = 0;
                //returns the rectangle that represents the overlapping parts
                Rectangle intersection = Rectangle.Intersect(BoundingBox, boundingBox);
                //if (intersection.Width > intersection.Height)
                //{
                //    //top or botton
                //    direction.Y *= -1;
                //}
                //else if (intersection.Width < intersection.Height)
                //{
                //    direction.X *= -1;
                //}
                //else
                //{
                //    direction.X *= -1;
                //    direction.Y *= -1;
                //}

                //if (intersection.Width > intersection.Height)
                //{
                //    //top or botton
                //    direction.Y *= -1;
                //}
                //else
                //{
                //    direction.X *= -1;
                //}
                if ( intersection.Width > intersection.Height)
                {
                    //top or botton
                    direction.Y *= -1;
                }
                else if ( intersection.Width < intersection.Height)
                {
                    direction.X *= -1;
                }
                else
                {
                    direction.X *= -1;
                    direction.Y *= -1;
                }

                //direction.X *= -1;
            }
        }
    }
}
