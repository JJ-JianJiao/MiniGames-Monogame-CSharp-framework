using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{

    class Paddle
    {
        protected const int Width = 2;
        protected const int Height = 18;
        protected const float Speed = 100;

        protected Texture2D texture;
        protected Vector2 position;
        private Vector2 direction;
        protected Vector2 dimensions;
        protected int scale;
        protected Rectangle gameBoundingBox;
        internal Rectangle BoundingBox {
            get {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X * scale, (int)dimensions.Y * scale);
            }
        }


        public Vector2 Direction {
            set {
                direction = new Vector2(0, value.Y);
            }
        }

        internal void Initialize(int scale, Rectangle gameBoundingBox, Vector2 initialPosition)
        {
            this.scale = scale;
            this.gameBoundingBox = gameBoundingBox;
            this.position = initialPosition;
            this.dimensions = new Vector2(Width, Height);
        }

        internal void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Paddle");
        }

        internal void Update(GameTime gameTime)
        {
            position += direction * Speed * scale* (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (position.Y <= gameBoundingBox.Top) {
                position.Y = gameBoundingBox.Top;
            }
            if(position.Y >= gameBoundingBox.Bottom - dimensions.Y * scale)
                position.Y = gameBoundingBox.Bottom - dimensions.Y * scale;

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

    }


}
