using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class NetBarrier:AnimationObject
    {
        private Vector2 OriginPositon;
        private Vector2 direction;

        private enum NetBarrierState { 
            Active,
            InActive
        }

        private NetBarrierState currentNetBarrierState = NetBarrierState.Active;

        public override Rectangle ObjectBorder
        {
            get
            {
                return new Rectangle((int)(this.Position.X), (int)(this.Position.Y) + (int)(10*Scale),
                    (int)(this.Width * 0.05f * Scale), (int)(this.Height * 0.05f * Scale)- (int)(18 * Scale));
            }
        }
        public override void Initial(int width, int height, float speed, float scale, Vector2 position, Rectangle gameBorder)
        {
            this.OriginPositon = position;
            position += new Vector2(0, 40 * scale);
            base.Initial(width, height, speed*2, scale, position, gameBorder);

            direction = new Vector2(1, 0);
        }

        public override void Update(GameTime gameTime)
        {
            switch (currentNetBarrierState)
            {
                case NetBarrierState.Active:
                    this.Position += direction * this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Position.X > GameBorder.Right - this.Width * 0.05f * Scale || Position.X <= 0)
                    {
                        direction.X *= -1;
                    }
                    break;
                case NetBarrierState.InActive:
                    break;
                default:
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentNetBarrierState)
            {
                case NetBarrierState.Active:
                    spriteBatch.Draw(this.Texture, this.Position, null, Color.White, 0f, Vector2.Zero, 0.05f * Scale, SpriteEffect, 0f);
                    break;
                case NetBarrierState.InActive:
                    break;
                default:
                    break;
            }
            if (this.ObjectFrameTexture != null && ObjectFrameActive)
            {
                spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, Color.Red * 0.5f);
            }
        }

        internal bool Collision(Fireball aFireball)
        {
            if (this.currentNetBarrierState == NetBarrierState.Active)
            {
                if (this.ObjectBorder.Intersects(aFireball.ObjectBorder))
                {
                    if(this.Position.Y <= 280 * Scale) { 
                        this.Position += new Vector2(0, 2*Scale);
                    }
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        internal void HitByPlayer(int range = 5)
        {

            if (this.Position.Y >= this.OriginPositon.Y)
            {
                this.Position -= new Vector2(0, range * Scale);
            }

        }
    }
}
