using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TopDownShooter
{
    internal class ShipPlayer : GameBot
    {
        new protected const int MaxSpeed = 200;
        new protected const int MaxProjectiles = 100;
        protected const float TimeBetweenShots = 0.1f;
        //protected Texture2D rectangleTexture;
        public ShipPlayer():base()
        {
            this.rotation = InitialRotation;
            this.maxSpeed = MaxSpeed;
            this.maxProjectiles = MaxProjectiles;
        }

        internal override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            //rectangleTexture = Content.Load<Texture2D>("Square");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (state)
            {
                case State.Alive:
                    position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (velocity.Length() < 500)
                    {
                        initialThrust = 100f;
                    }
                    timeBetweenShots += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //accelerations is updated when the player presses the keys. Now updateMovementVars
                    updateMovementVars((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
            }
        }

        internal void Shoot()
        {
            if (timeBetweenShots >= TimeBetweenShots && alive())
            {
                //converts from a radian-based orientation, to a vector-based orientation
                Vector2 projectileDirection =
                    new Vector2((float)Math.Cos(orientation + MathHelper.ToRadians(90f)), (float)Math.Sin(orientation + MathHelper.ToRadians(90f)));

                //position the projectile at the "front" of the sprite
                Vector2 projectilePosition = position + projectileDirection * BoundingBox.Width / 2;

                bool shot = false;
                int projectileCounter = 0;   //?????????
                while (!shot && projectileCounter < Projectiles.Length)
                {
                    if (projectiles[projectileCounter].Shootable())
                    {
                        projectiles[projectileCounter].Initialize(projectilePosition, gameBoundingBox, projectileDirection, velocity);
                        shot = true;
                        timeBetweenShots = 0;
                    }
                    projectileCounter++;
                }
            }
        }
        public bool dead()
        {
            return state == State.Dead;
        }

        public void rotateRight()
        {
            if (accelerations.angular < 0)
            {
                rotation = 0;
                accelerations.angular = 0;
            }
            else
            {
                accelerations.angular += 1;
            }
        }

        public void rotateLeft()
        {
            if (accelerations.angular > 0)
            {
                rotation = 0;
                accelerations.angular = 0;
            }
            else
            {
                accelerations.angular -= 1;
            }
        }

        public void decreaseAbsoluteSpeed()
        {
            accelerations.linear /= 1.1f;
        }
        public void increaseSpeed()
        {
            Vector2 thrustDirection = new Vector2((float)(-Math.Sin(orientation)), (float)(Math.Cos(orientation)));
            accelerations.linear += thrustDirection;
        }

        public void decreaseSpeed()
        {
            Matrix rotationMatrix = Matrix.CreateRotationZ(orientation);
            Vector2 thrustDirection = new Vector2(0, -1);
            thrustDirection = Vector2.Transform(thrustDirection, rotationMatrix);//new Vector2((float)(Math.Sin(orientation)), (float)( Math.Cos(orientation)));
            accelerations.linear += thrustDirection;
        }

        public void stopMoving()
        {
            accelerations.linear = Vector2.Zero;
            velocity.X = 0;
            velocity.Y = 0;
        }

        public void die()
        {
            state = State.Dying;
        }

        internal void Shoot(Vector2 projectileDirection, Vector2 projectilePosition, Vector2 initialVelocity)
        {
            bool shot = false;
            int projectileCounter = 0;
            while (!shot && projectileCounter < Projectiles.Length)
            {
                if (projectiles[projectileCounter].Shootable())
                {
                    projectiles[projectileCounter].Initialize(projectilePosition, gameBoundingBox, projectileDirection, initialVelocity);
                    shot = true;
                    timeBetweenShots = 0;
                }
                projectileCounter++;
            }
        }
    }
}