using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    abstract class Projectile
    {
        protected float Speed=100;
        protected int Width=4;
        protected int Height=4;

        protected float InitialDirectionX = 0;
        protected float InitialDirectionY = -1;

        //protected string Texture = "CannonBall";

        protected enum State
        {
            Flying,
            NotFlying
        }
        protected State state = State.NotFlying;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 dimensions;
        protected Rectangle gameBoundingBox;

        protected Texture2D texture;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        internal virtual void Initialize(Vector2 positions, Rectangle gameBoundingBox, Vector2 direction, Vector2 initialVelocity)
        {
            this.position = positions;
            this.gameBoundingBox = gameBoundingBox;
            velocity = initialVelocity;
            dimensions = new Vector2(Width, Height);
            velocity += direction * Speed;
            state = State.Flying;
        }

        internal abstract void LoadContent(ContentManager Content);
        //internal virtual void LoadContent(ContentManager Content)
        //{
        //    texture = Content.Load<Texture2D>(Texture);
        //}

        internal virtual void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Flying:
                    position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (!BoundingBox.Intersects(gameBoundingBox))
                    {
                        //I'm no longer in play, destroy myself!!!
                        state = State.NotFlying;
                    }
                    break;
                case State.NotFlying:
                    break;
            }
        }

        internal virtual void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case State.Flying:
                    spriteBatch.Draw(texture, position, Color.White);
                    break;
                case State.NotFlying:
                    break;
            }
        }

        internal bool Shootable()
        {
            return state == State.NotFlying;
        }

        internal virtual void CheckForHit(GameBot shipPlayer)
        {
            if (state == State.Flying)
            {
                if (shipPlayer.alive() && shipPlayer.BoundingBoxRotated.Intersects(BoundingBox))
                {
                    shipPlayer.ProcessHit();
                    state = State.NotFlying;
                }
            }
        }
    }
}
