using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class Shield : AnimationObject
    {
        private Vector2 CannonPositon;
        private Color shieldColor = Color.White;
        private int ShieldLife = 10;
        private float coldDownTimer = 0;
        private float coldDonwDuring = 10.0f;
        public override Rectangle ObjectBorder{
            get
            {
                return new Rectangle( (int)(this.CannonPositon.X - this.Height* this.Scale * 0.125 / 2f), (int)(this.CannonPositon.Y - this.Height * this.Scale * 0.125 / 2f)
                    , (int)(this.Width * 0.125f * Scale), (int)(this.Height * 0.125f * Scale));
            }
        }

        private enum CannonSheild { 
            Active,
            Inactive
        }

        private CannonSheild currentSheildState = CannonSheild.Inactive;

        public void Update(GameTime gameTime, Vector2 cannonPosition)
        {
            switch (currentSheildState)
            {
                case CannonSheild.Active:
                    this.CannonPositon = cannonPosition + new Vector2(20 * Scale, -8 * Scale);
                    if (ShieldLife <= 10 && ShieldLife > 6)
                    {
                        shieldColor = Color.White;
                    }
                    else if (ShieldLife <= 6 && ShieldLife > 3)
                    {
                        shieldColor = Color.Red;
                    }
                    else if (ShieldLife <= 3 && ShieldLife > 0)
                    {
                        shieldColor = Color.Black;
                    }
                    else if (ShieldLife <= 0) {
                        currentSheildState = CannonSheild.Inactive;
                        coldDownTimer = 0;
                    }
                    break;
                case CannonSheild.Inactive:
                    coldDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (coldDownTimer > coldDonwDuring) {
                        ShieldLife = 10;
                        shieldColor = Color.White;
                    }
                    break;
                default:
                    break;
            }

            //this.CannonPositon = cannonPosition ; 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentSheildState)
            {
                case CannonSheild.Active:
                    if (this.ObjectFrameTexture != null && ObjectFrameActive)
                    {
                        spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, Color.Red * 0.5f);
                    }
                    spriteBatch.Draw(this.Texture, this.CannonPositon, null, shieldColor, 0f, new Vector2(this.Height / 2.0f, this.Width / 2.0f), 0.125f * Scale, SpriteEffect, 0f);
                    break;
                case CannonSheild.Inactive:
                    spriteBatch.Draw(this.Texture, new Vector2(-500,-500) * Scale, null, Color.White, 0f, new Vector2(this.Height / 2.0f, this.Width / 2.0f), 0.125f * Scale, SpriteEffect, 0f);
                    break;
                default:
                    break;
            }


        }

        internal bool Collision(Fireball aFireball)
        {
            if (this.currentSheildState == CannonSheild.Active)
            {
                if (this.ObjectBorder.Intersects(aFireball.ObjectBorder))
                {
                    ShieldLife--;
                    return true;
                }
                else return false;
            }
            else 
                return false;
        }

        public bool ShieldReadyUse() {
            if (this.ShieldLife <= 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        internal void Active()
        {
            this.currentSheildState = CannonSheild.Active;
        }

        internal int getShieldLife()
        {
            return this.ShieldLife;
        }

        //public void GetCannonPositon(Vector2 cannonPosition) {
        //    this.CannonPositon = cannonPosition;
        //}
    }
}
