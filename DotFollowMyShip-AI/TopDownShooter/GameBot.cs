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
    class GameBot
    {
        protected enum State
        {
            Alive = 0,
            Dying,
            Dead
        }

        protected State state;
        protected Texture2D texture;
        protected Texture2D collisionDebugTexture;

        protected int maxProjectiles;
        protected double dyingTimer = 0;
        protected int maxSpeed = 0;
        protected float frictionFactor = 0f;
        protected float timeBetweenShots = 0;

        protected float initialThrust = 100f;

        protected Rectangle gameBoundingBox;

        protected SpriteFont debugFont;

        protected struct SteeringOutput
        {
            public Vector2 linear;
            public float angular;
        }
        protected SteeringOutput accelerations;

        protected Vector2 position;
        internal Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected float orientation;
        protected Vector2 velocity;
        protected float rotation;
        protected Vector2 dimensions;
        protected const string Texture = "Ship";
        protected ProjectileCannonBall[] projectiles;
        protected ProjectileCannonBall[] Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
        }
        internal Rectangle BoundingBox
        {
            get //because of the way Monogame draws using origin, the boundingBox needs to be adjusted for collisions here
            {
                return new Rectangle(((int)position.X - (int)dimensions.X / 2), ((int)position.Y - (int)dimensions.Y / 2), (int)dimensions.X, (int)dimensions.Y);
            }
        }

        internal RotatedRectangle BoundingBoxRotated
        {
            get
            {
                return new RotatedRectangle(BoundingBox, orientation);
            }
        }

        protected const float InitialLinearAccelerationX = 0;
        protected const float InitialLinearAccelerationY = 0;
        protected const float InitialAngularAcceleration = 0;
        protected const float InitialVelocityX = 0;
        protected const float InitialVelocityY = 0;
        protected const float InitialRotation = 0;

        protected const int MaxSpeed = 50;
        protected const int MaxProjectiles = 10;


        public GameBot() {
            this.orientation = MathHelper.ToRadians(90);
            this.accelerations = new SteeringOutput();
            this.accelerations.linear = new Vector2(InitialLinearAccelerationX, InitialLinearAccelerationY);
            this.accelerations.angular = InitialAngularAcceleration;

            this.velocity = new Vector2(InitialVelocityX, InitialVelocityY);
            this.rotation = InitialRotation;

            this.maxSpeed = MaxSpeed;
            this.maxProjectiles = MaxProjectiles;

            this.projectiles = new ProjectileCannonBall[maxProjectiles];

            for (int c = 0; c < maxProjectiles; c++)
            {
                projectiles[c] = new ProjectileCannonBall();
            }
            state = State.Alive;
        }

        public void Initialize(Vector2 position, Game game, Rectangle gameBoundingBox, float frictionFactor)
        {
            this.gameBoundingBox = gameBoundingBox;
            this.position = position;
            this.frictionFactor = frictionFactor;
            this.dimensions = new Vector2(texture.Width, texture.Height);
        }

        internal virtual void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(Texture);
            //for debugging
            debugFont = Content.Load<SpriteFont>("SystemArialFont");
            collisionDebugTexture = Content.Load<Texture2D>("Square");
            foreach (ProjectileCannonBall projectile in projectiles)
            {
                projectile.LoadContent(Content);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Alive:
                    break;
                case State.Dying:
                    dyingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (dyingTimer > 2f)
                    {
                        state = State.Dead;
                    }
                    break;
                case State.Dead:
                    break;
            }
            for (int c = 0; c < projectiles.Length; c++)
            {
                projectiles[c].Update(gameTime);
            }
        }

        public virtual void Update(GameTime gameTime, ShipPlayer goodGuyShip)
        {
            switch (state)
            {
                case State.Alive:
                    break;
                case State.Dying:
                    dyingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (dyingTimer > 2f)
                    {
                        state = State.Dead;
                    }
                    break;
                case State.Dead:
                    break;
            }
            for (int c = 0; c < projectiles.Length; c++)
            {
                projectiles[c].Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch drawingSurface)
        {
            switch (state)
            {
                case State.Alive:
                    drawingSurface.Draw(texture, position, null, Color.White, orientation, new Vector2(dimensions.X / 2, dimensions.Y / 2), 1, SpriteEffects.None, 0);
                    break;
                case State.Dying:
                    break;
                case State.Dead:
                    break;
            }
            foreach (ProjectileCannonBall projectile in projectiles)
            {
                projectile.Draw(drawingSurface);
            }
        }

                public void DrawDebug(SpriteBatch drawingSurface)
        {
            BoundingBoxRotated.Draw(drawingSurface, collisionDebugTexture);
        }
        protected void updateMovementVars(float egt)
        {
            // update rotation and velocity, then orientation and position 

            //rotation: how fast our orientation is changing
            rotation += accelerations.angular * egt;

            //velocity: how fast our position is changing
            velocity += accelerations.linear * egt * initialThrust;
            velocity /= frictionFactor;

            //orientation: where we are pointed
            orientation += rotation * egt;
            //wrap around so that the radians always stay between 0 and 2Pi
            orientation %= 2 * MathHelper.Pi;

            //position: where we are
            position += velocity * egt;

            //are we speeding?
            if (velocity.Length() > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }
        }

        internal bool alive()
        {
            return state == State.Alive;
        }

        internal void ProcessHit()
        {
            if (state == State.Alive)
            {
                state = State.Dying;
            }
        }

        internal virtual void ProcessProjectiles(GameBot enemy)
        {
            for (int c = 0; c < projectiles.Length; c++)
            {
                projectiles[c].CheckForHit(enemy);
            }
        }
    }
}
