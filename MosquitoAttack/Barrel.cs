using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class Barrel:AnimationObject
    {
        public float Rotation { get; set; }
        public Barrel() {
            Rotation = 0;
            //Position = Position + new Vector2(Texture.Width / 2.0f, Texture.Height + 2) * Scale;
        }

        public Barrel(int width, int height, int speed, int scale, Vector2 position, Rectangle gameBorder) : base(width, height, speed, scale, position, gameBorder) {
            Rotation = 0;
        }

        public override void Initial(int width, int height, float speed, float scale, Vector2 position, Rectangle gameBorder)
        {
            base.Initial(width, height, speed, scale, position, gameBorder);
        }

        public override void Update(GameTime gameTime)
        {
            //Vector2 Position2 = Position + new Vector2(Texture.Width / 2.0f, Texture.Height + 2) * Scale;
            //Vector2 Position2 = Position + new Vector2(Texture.Width / 2.0f, Texture.Height + 2) * Scale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            //Vector2 Position2 = Position + new Vector2(Texture.Width/2.0f, Texture.Height+5f)*Scale;
            //Vector2 Position2 = Position + new Vector2(Texture.Width / 2.0f, Texture.Height + 2) * Scale;
            Vector2 Position2 = Position + new Vector2(Texture.Width / 2.0f, Texture.Height+3) * Scale;
            //spriteBatch.Draw(Texture, Position, null, Color.White, MathHelper.PiOver4, Vector2.Zero, Scale, SpriteEffect, 0f);
            //spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Width / 2, Height) * Scale, Scale, SpriteEffect, 0f);
            //spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Texture.Width / 4.0f, Texture.Height / 2.0f) * Scale, Scale, SpriteEffect, 0f);
            spriteBatch.Draw(Texture, Position2, null, Color.White, Rotation, new Vector2(Texture.Width / 2.0f, Texture.Height), Scale, SpriteEffect, 0f);
            //spriteBatch.Draw(Texture, Position2, null, Color.White, Rotation, new Vector2(Texture.Width / 2.0f, Texture.Height) * Scale, Scale, SpriteEffect, 0f);
            //spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Vector2.Zero, Scale, SpriteEffect, 0f);
            if (this.ObjectFrameTexture != null && ObjectFrameActive)
            {
                spriteBatch.Draw(ObjectFrameTexture, Position2, new Rectangle((int)Position2.X, (int)Position2.Y, Width, Height), Color.Red*0.5f, Rotation, new Vector2(Texture.Width / 2.0f, Texture.Height), Scale, SpriteEffect, 0f);
                //spriteBatch.Draw( this.ObjectFrameTexture, new Rectangle((int)Position2.X, (int)Position2.Y, Width,Height), ObjectBorder, Color.Black, Rotation, new Vector2(Texture.Width / 2.0f, Texture.Height), SpriteEffect, 0f);
            }
        }
    }
}
