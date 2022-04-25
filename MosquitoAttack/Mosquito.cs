using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class Mosquito:AnimationObject
    {
        private Vector2 targetPosition;
        private Vector2 cannonPositon;
        private float prepareSwoopDown = 0.0f;
        private float prepareSwoopDownDruing = 1.5f;
        private Vector2 originalPositon;
        private Vector2 SwoopDownDirection;
        private bool AreadyBite = false;
        private Color changeColor = Color.White;

        private bool FlyBack = false;
        private float flyBackTimer = 0.0f;
        private float flyBackDruing = 1.0f;

        private bool sweepDownMosquito = false;

        private float swoopDownColdDownTimer = 0;
        private float swoopDownColdDownDuring = 10;

        private bool IsReadyLockedTarget = false;

        private Vector2 _direction;
        public List<Fireball> fireballs = new List<Fireball>();
        private float upDownTimer = 0.0f;
        private float upDuring = 1.0f;
        private float downDuring = 2.0f;
        private float dyingTimer = 0f;
        private float dyingDuring = 0.5f;
        public bool MosquitoShootActive { get; set; }
        //public Texture2D PoofTexture { get; set; }
        internal enum MosquitoState { 
            Alive,
            Dying,
            Dead
        }
        public MosquitoState currentState = MosquitoState.Alive;
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public float UpDuring { get => upDuring; set => upDuring = value; }
        public float DownDuring { get => downDuring; set => downDuring = value; }
        public bool SweepDownMosquito { get => sweepDownMosquito; set => sweepDownMosquito = value; }
        public float SwoopDownColdDownDuring { get => swoopDownColdDownDuring; set => swoopDownColdDownDuring = value; }

        public Mosquito() {
            _direction = new Vector2(1, 0);
        }

        public Mosquito(int width, int height, int speed, int scale, Vector2 position, Rectangle gameBorder) : base(width, height, speed, scale, position, gameBorder)
        {
            _direction = new Vector2(1, 0);
            AreadyBite = false;
            //originalPositon = position;
        }

        public override void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case MosquitoState.Alive:

                    if (sweepDownMosquito && !FlyBack) {
                        swoopDownColdDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (swoopDownColdDownTimer >= SwoopDownColdDownDuring) {
                            changeColor = Color.Red;
                            prepareSwoopDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (prepareSwoopDown >= prepareSwoopDownDruing) {
                                if (!IsReadyLockedTarget) {
                                    targetPosition = cannonPositon;
                                    originalPositon = this.Position;
                                    IsReadyLockedTarget = true;
                                    if (targetPosition.X - originalPositon.X > 0)
                                    {
                                        this.SpriteEffect = SpriteEffects.None;
                                    }
                                    else {
                                        this.SpriteEffect = SpriteEffects.FlipHorizontally;
                                    }
                                    this.SwoopDownDirection = new Vector2(targetPosition.X - originalPositon.X, targetPosition.Y - originalPositon.Y)/500;
                                }
                                if (this.Position.Y >= targetPosition.Y - 30 * Scale)
                                {
                                    flyBackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                    if (flyBackTimer >= flyBackDruing)
                                    {
                                        FlyBack = true;
                                        if (targetPosition.X - originalPositon.X > 0)
                                        {
                                            this.SpriteEffect = SpriteEffects.FlipHorizontally;
                                        }
                                        else
                                        {
                                            this.SpriteEffect = SpriteEffects.None;
                                        }
                                    }
                                }
                                else
                                {
                                    this.Position += SwoopDownDirection * this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                }
                            }
                        }
                    }
                    if (FlyBack) {
                        this.Position -= SwoopDownDirection * this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (this.Position.Y <= originalPositon.Y + Height*Scale) {
                            IsReadyLockedTarget = false;
                            swoopDownColdDownTimer = -1;
                            changeColor = Color.White;
                            prepareSwoopDown = 0;
                            FlyBack = false;
                            flyBackTimer = 0;
                            AreadyBite = false;
                        }
                    }
                    upDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (upDownTimer <= UpDuring)
                    {
                        _direction.Y = 1;
                    }
                    else if (upDownTimer <= DownDuring)
                    {
                        _direction.Y = -1;
                    }
                    else
                    {
                        upDownTimer -= DownDuring;
                    }
                    if (!IsReadyLockedTarget) { 
                        Position += Speed * _direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (Position.X < 0 || Position.X > this.GameBorder.Right - Width * Scale)
                    {
                        _direction.X *= -1;
                    }
                    if (Position.Y < 0 || Position.Y > this.GameBorder.Bottom - Height * Scale)
                    {
                        _direction.Y *= -1;
                    }

                    if (_direction.X > 0)
                    {
                        SpriteEffect = SpriteEffects.None;
                    }                    
                    else if (_direction.X < 0) {
                        SpriteEffect = SpriteEffects.FlipHorizontally;
                    }
                    base.Update(gameTime);
                    break;
                case MosquitoState.Dying:
                    dyingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (dyingTimer <= dyingDuring)
                    {
                        base.Update(gameTime);
                    }
                    else {
                        currentState = MosquitoState.Dead;
                        Position = new Vector2(500, 500) * Scale;
                    }
                    break;
                case MosquitoState.Dead:
                    break;
                default:
                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //foreach (Fireball aFireball in fireballs)
            //{
            //    aFireball.Draw(spriteBatch);
            //}

            switch (currentState)
            {
                case MosquitoState.Alive:
                    if (this.celTime != -1)
                    {
                        //spriteBatch.Draw(cannonSeq.Texture, ObjectBorder, Color.Black);
                        celAnimationPlayer.Draw(spriteBatch, this.Position, Scale, this.SpriteEffect, changeColor);
                    }
                    //spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, new Color(100, 100, 100, 0));
                    if (this.ObjectFrameTexture != null && ObjectFrameActive)
                    {
                        spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, Color.Red * 0.5f);
                    }
                    //base.Draw(spriteBatch, changeColor);
                    break;
                case MosquitoState.Dying:
                    base.Draw(spriteBatch);
                    break;
                case MosquitoState.Dead:
                    break;
                default:
                    break;
            }

        }

        public void GenerateFireball(Fireball fireball) {
            fireball.ObjectFrameTexture = this.ObjectFrameTexture;
            fireball.ObjectFrameActive = this.ObjectFrameActive;
            fireball.Position = this.Position+new Vector2(this.Width/2,this.Height/2)*Scale;
            fireballs.Add(fireball);
        }

        internal bool Collision(List<CannonBall> cannonNomalBalls)
        {
            foreach (CannonBall aCannonBall in cannonNomalBalls) {
                if (aCannonBall.ObjectBorder.Intersects(this.ObjectBorder)&&this.currentState==MosquitoState.Alive)
                {
                    //this.ObjectFrameActive = true;
                    aCannonBall.CannonBallHitEvent(true);
                    return true;
                }
            }
            return false;
        }

        internal void ChangeTexture(Texture2D texture, int width, int height, float celTime, float scale) {
            if (this.currentState == MosquitoState.Alive) { 
                cannonSeq = new CelAnimationSequence(texture, width, celTime);
                this.Texture = texture;
                this.Width = width;
                this.Height = height;
                this.celTime = celTime;
                this.Scale = scale;
                this.currentState = MosquitoState.Dying;
                celAnimationPlayer.Play(cannonSeq);        
            }
        }

        internal bool Collision(List<SineWaveCannonBall> sineWaveCannonBalls)
        {
            foreach (SineWaveCannonBall aSineWaveBall in sineWaveCannonBalls)
            {
                foreach(CannonBall oneElement in aSineWaveBall.sineWaveBalls)
                { 
                    if (oneElement.ObjectBorder.Intersects(this.ObjectBorder) && this.currentState == MosquitoState.Alive)
                    {
                        //this.ObjectFrameActive = true;
                        oneElement.CannonBallHitEvent(true);

                        return true;
                    }
                }
            }
            return false;
        }

        internal bool Collision(List<SplitCannonBall> splitCannonBalls)
        {
            foreach (SplitCannonBall splitCannonBall in splitCannonBalls)
            {
                foreach (CannonBall oneElement in splitCannonBall.SplitBalls)
                {
                    if (oneElement.ObjectBorder.Intersects(this.ObjectBorder) && this.currentState == MosquitoState.Alive)
                    {
                        //this.ObjectFrameActive = true;
                        oneElement.CannonBallHitEvent(true);

                        return true;
                    }
                }
            }
            return false;
        }

        internal bool Collision(Cannon cannon) {
            if (this.currentState == MosquitoState.Alive && cannon.Life != 0) {
                if (this.ObjectBorder.Intersects(cannon.ObjectBorder) && !cannon.ShieldIsOn && !AreadyBite) {
                    if (cannon.Life - 2 < 0)
                    {
                        cannon.Life = 0;
                    }
                    else {
                        cannon.Life -= 2;
                    }
                    AreadyBite = true;
                    return true;
                }
            }
            return false;
        }

        internal void GetCannonPositon(Vector2 cannonPositon) {
            this.cannonPositon = cannonPositon;
        }
    }
}
