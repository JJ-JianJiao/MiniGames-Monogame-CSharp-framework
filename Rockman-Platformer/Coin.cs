using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06_Platformer
{
    public class Coin : AnimationObject
    {
        public Coin()
        {
        }

        public Coin(int width, int height, float speed, int scale, Vector2 position, Rectangle gameBorder) : base(width, height, speed, scale, position, gameBorder)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);  
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            //spriteBatch.Draw(this.Texture, Vector2.Zero, Color.White);
            spriteBatch.Draw(this.Texture,this.Position,null,Color.White,0,Vector2.Zero,0.2f,SpriteEffects.None,0);
        }

        public void ProcessCollisions(Player player) {
            Rectangle newBorder = new Rectangle(this.ObjectBorder.X+ (int)(Width * 0.05), this.ObjectBorder.Y + (int)(Width * 0.05), (int)(Width * 0.05), (int)(Height * 0.05));
            //if (this.ObjectBorder.Intersects(player.BoundingBox)) {
            if (newBorder.Intersects(player.BoundingBox)) {
                this.Position = new Vector2(2000, 2000);
                player.Score++;
            }
        }
    }
}
