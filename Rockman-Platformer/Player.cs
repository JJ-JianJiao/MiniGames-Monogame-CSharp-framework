using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lab06_Platformer
{
    public class Player
    {
        public int Score { get; set; }
        protected enum State
        {
            Idle,
            Walking,
            Jumping
        }
        protected State state;
        protected CelAnimationSequence idleSequence;
        protected CelAnimationSequence jumpSequence;
        protected CelAnimationSequence walkSequence;

        protected CelAnimationPlayer animationPlayer;

        private Vector2 position;
        protected Vector2 velocity;
        protected Vector2 dimensions;

        protected bool playOnce = false;

        protected SpriteEffects spriteEffects = SpriteEffects.None;

        internal Vector2 Velocity
        {
            get { return velocity; }
            //set { velocity = value; }
        }
        protected Rectangle gameBoundingBox;


        protected const int Speed = 100;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        public Vector2 Position { get => position; set => position = value; }

        public Player(Vector2 position, Rectangle gameBoundingBox)
        {
            this.Position = position;
            this.gameBoundingBox = gameBoundingBox;
            dimensions = new Vector2(30, 26);
            animationPlayer = new CelAnimationPlayer();
        }
        internal void Initialize()
        {
            state = State.Idle;
            animationPlayer.Play(idleSequence);
            spriteEffects = SpriteEffects.None;
        }
        internal void LoadContent(ContentManager Content)
        {
            idleSequence = new CelAnimationSequence(Content.Load<Texture2D>("RockmanIdl"), 23, 1 / 11f);
            //idleSequence = new CelAnimationSequence(Content.Load<Texture2D>("RockmanRuning2"), 30, 1 / 11f);
            walkSequence = new CelAnimationSequence(Content.Load<Texture2D>("RockmanRuning2"), 30, 1 / 4f);
            jumpSequence = new CelAnimationSequence(Content.Load<Texture2D>("RockmanJump"), 27, 1 / 1f);

            //PlayOnce
            //jumpSequence = new CelAnimationSequence(Content.Load<Texture2D>("RockmanIdl"), 23, 1 / 11f);
        }



        internal void Update(GameTime GameTime)
        {
            switch (state)
            {
                case State.Jumping:
                    if (playOnce) { 
                        animationPlayer.Update(GameTime,1);
                    }
                    break;
                case State.Idle:
                    animationPlayer.Update(GameTime);
                    break;
                case State.Walking:
                    animationPlayer.Update(GameTime);
                    break;
            }
            if(this.Position.X + velocity.X *(float)GameTime.ElapsedGameTime.TotalSeconds <0 || this.Position.X + velocity.X * (float)GameTime.ElapsedGameTime.TotalSeconds+30 > 550)
            {
                
            }
            else
            {
                Position += velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
            }

            velocity.Y += PlatformerGame.Gravity;


            if (Math.Abs(velocity.Y) > PlatformerGame.Gravity)
            {
                state = State.Jumping;
            }



        }



        internal void Draw(SpriteBatch SpriteBatch)
        {
            animationPlayer.Draw(SpriteBatch, Position, spriteEffects);
        }

        internal void Jump()
        {
            if (state != State.Jumping)
            {
                state = State.Jumping;
                velocity.Y = -180;
                animationPlayer.Play(jumpSequence);
                PlayOnce();
            }
            //state = State.Jumping;
            //velocity.Y = -200;
        }

        internal void PlayOnce() {
            playOnce = true;
        }

        internal void Move(Vector2 direction) {

            

                velocity.X = direction.X * Speed;

            if (direction.X >= 0) {
                spriteEffects = SpriteEffects.None;
            }
            else if (direction.X < 0) {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            if (state == State.Idle) {
                state = State.Walking;
                animationPlayer.Play(walkSequence);
            }
        }

        internal void Stop()
        {
            if (state == State.Walking)
            {
                velocity = Vector2.Zero;
                state = State.Idle;
                animationPlayer.Play(idleSequence);
            }
        }


        internal void Land(Rectangle whatILandOn)
        {
            if(state == State.Jumping) { 
                position.Y = whatILandOn.Top - dimensions.Y + 1;
                velocity = Vector2.Zero;
                state = State.Walking;
            }
        }

        internal void StandOn() {
            velocity.Y -= PlatformerGame.Gravity;
        }

        internal void MoveHorizontally(float direction)
        {
            velocity.X = direction * Speed;
        }
        internal void MoveVertically(float direction)
        {
            velocity.Y = direction * Speed;
        }
    }
}
