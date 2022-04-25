using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class Cannon:AnimationObject
    {
        private float reloadTimer = 0;
        private float reloadTimer2 = 0;
        private float reloadTimer3 = 0;
        private float reloadCounter = 3.0f;
        private bool reloadActive = false;
        private bool reloadActive2 = false;
        private bool reloadActive3= false;
        public bool moveActive = false;
        private int _life = 10;
        private float dyingTimer = 0f;
        private float dyingDuring = 0.5f;
        public int shootNumBullet { get; set; }
        public int shootNumBulletTwo { get; set; }
        public int shootNumBulletThree { get; set; }
        public bool AttackModelOne { get; set; }
        public bool AttackModelTwo { get; set; }
        public bool AttackModelThree { get; set; }

        private bool shieldIsOn;
        private int needRealodModel { get; set; }
        public int Life
        {
            get { return _life; }
            set { _life = value; }
        }

        //private float shakeTimeer = 2.0f;
        //private bool shakeActive = true;
        //private float shakeFrequency = 0.0f;
        //private float shakeChangeCount = 0.5f;
        private enum CannonState { 
            Alive,
            Dying,
            Dead
        }
        public List<CannonBall> cannonNomalBalls = new List<CannonBall>();
        public List<SineWaveCannonBall> sineWaveCannonBalls = new List<SineWaveCannonBall>();
        private CannonState currentConnonState = CannonState.Alive;
        public Barrel barrel = new Barrel();
        internal List<SplitCannonBall> splitCannonBalls = new List<SplitCannonBall>();

        public Rectangle CannonAndBarrelBorder 
        {
            get {
                return new Rectangle(this.ObjectBorder.X, (int)(this.ObjectBorder.Y - barrel.Height/2 * Scale), this.ObjectBorder.Width, (int)(this.ObjectBorder.Height + barrel.Height * Scale));
            } 
        }

        public bool ShieldIsOn { get => shieldIsOn; set => shieldIsOn = value; }

        public Cannon()
        { 
        
        }

        public Cannon(int width, int height, int speed, int scale, Vector2 position, Rectangle gameBorder) : base(width, height, speed, scale, position, gameBorder)
        {
            AttackModelOne = true;
            AttackModelThree = false;
            AttackModelTwo = false;
        }

        public override void Initial(int width, int height, float speed, float scale, Vector2 position, Rectangle gameBorder)
        {
            base.Initial(width, height, speed, scale, position, gameBorder);
            AttackModelOne = true;
            AttackModelThree = false;
            AttackModelTwo = false;
            this.Life = 10;
            this.currentConnonState = CannonState.Alive;
            reloadTimer = 0;
            reloadTimer2 = 0;
            reloadTimer3 = 0;
            reloadCounter = 3.0f;
            reloadActive = false;
            reloadActive2 = false;
            reloadActive3 = false;
            moveActive = false;
            _life = 10;
            dyingTimer = 0f;
            dyingDuring = 0.5f;
            cannonNomalBalls.Clear();
            sineWaveCannonBalls.Clear();
            splitCannonBalls.Clear();
            shieldIsOn = false;
        }

        public bool Move(Vector2 direction) {
            if(Position.X + Speed*direction.X  <= GameBorder.Right- (this.Width*Scale )  && Position.X + Speed * direction.X >= GameBorder.Left) { 
                Position += Speed * direction;
                barrel.Position += Speed * direction;
            }
            if (direction.X > 0)
            {
                SpriteEffect = SpriteEffects.None;
            }
            else if (direction.X < 0) {
                SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            return true;
        }

        public void InitialBarrel(int width, int height, Texture2D barrelTexture, float barrelCelTime) {
            barrel.Initial(width,height,this.Speed,this.Scale, new Vector2(this.Position.X + this.Width/4.0f*Scale, this.Position.Y - (height)*Scale),this.GameBorder);
            barrel.LoadContent(barrelTexture,-1f);
            //barrel.ObjectFrameActive = this.ObjectFrameActive;
            barrel.ObjectFrameTexture = this.ObjectFrameTexture;
        }

        public override void Update(GameTime gameTime)
        {
            switch (currentConnonState)
            {

                case CannonState.Alive:
                    if (Life < 0) {
                        Life = 0;
                    }
                    if (reloadActive) {
                        reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (reloadTimer >= reloadCounter) {
                            reloadActive = false;
                            cannonNomalBalls.Clear();
                        }
                    }
                    if (reloadActive2)
                    {
                        reloadTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (reloadTimer2 >= reloadCounter)
                        {
                            reloadActive2 = false;
                            this.sineWaveCannonBalls.Clear();
                        }
                    }
                    if (reloadActive3)
                    {
                        reloadTimer3 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (reloadTimer3 >= reloadCounter)
                        {
                            reloadActive3 = false;
                            this.splitCannonBalls.Clear();
                        }
                    }
                    if (moveActive) { 
                        base.Update(gameTime);
                    }


                    break;
                case CannonState.Dying:
                    dyingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (dyingTimer <= dyingDuring)
                    {
                        base.Update(gameTime);
                    }
                    else
                    {
                        currentConnonState = CannonState.Dead;
                        Position = new Vector2(500, 500) * Scale;
                    }
                    break;
                case CannonState.Dead:
                    break;
                default:
                    break;
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {



            switch (currentConnonState)
            {
                case CannonState.Alive:
                    barrel.Draw(spriteBatch);
                    base.Draw(spriteBatch);
                    if (this.ObjectFrameTexture != null && ObjectFrameActive)
                    {
                        spriteBatch.Draw(this.ObjectFrameTexture, CannonAndBarrelBorder, Color.Red * 0.5f);
                    }


                    
                    foreach (CannonBall ball in cannonNomalBalls) {
                        ball.Draw(spriteBatch);
                    }



                    foreach (SineWaveCannonBall sineWaveCannonBall in sineWaveCannonBalls)
                    {
                        sineWaveCannonBall.Draw(spriteBatch);
                    }
                    break;
                case CannonState.Dying:
                    foreach (CannonBall ball in cannonNomalBalls)
                    {
                        ball.Draw(spriteBatch);
                    }
                    celAnimationPlayer.Draw(spriteBatch, new Vector2(Position.X-Width*Scale/2,Position.Y), Scale, SpriteEffects.None);
                    celAnimationPlayer.Draw(spriteBatch, new Vector2(Position.X + Width * Scale / 2, Position.Y), Scale, SpriteEffects.None);
                    celAnimationPlayer.Draw(spriteBatch, Position, Scale, SpriteEffects.None);
                    celAnimationPlayer.Draw(spriteBatch, new Vector2(Position.X, Position.Y-Height*Scale), Scale, SpriteEffects.None);
                    break;
                case CannonState.Dead:
                    break;
                default:
                    break;
            }
        }

        public void Shoot(CannonBall cannonBall) {
            cannonBall.ObjectFrameTexture = this.ObjectFrameTexture;
            cannonBall.ObjectFrameActive = this.ObjectFrameActive;


            cannonBall.Position = new Vector2(barrel.Position.X + (barrel.Width/2 - cannonBall.Width/2) * Scale, barrel.Position.Y);
            Vector2 aaaa = new Vector2(barrel.Height * (float)Math.Sin(barrel.Rotation), barrel.Height *(1- (float)Math.Cos(barrel.Rotation)))*Scale;
             cannonBall.Position += aaaa;
            cannonNomalBalls.Add(cannonBall);
            this.shootNumBullet++;
        }


        public void Shoot(SineWaveCannonBall sineWaveCannonBall)
        {
            sineWaveCannonBall.ObjectFrameTexture = this.ObjectFrameTexture;
            sineWaveCannonBall.ObjectFrameActive = this.ObjectFrameActive;
            sineWaveCannonBall.Direction = new Vector2((float)Math.Sin(this.barrel.Rotation), -(float)Math.Cos(this.barrel.Rotation));
            sineWaveCannonBall.Position = new Vector2(barrel.Position.X + (barrel.Width / 2 - sineWaveCannonBall.Width / 2) * Scale, barrel.Position.Y) 
                + new Vector2(barrel.Height * (float)Math.Sin(barrel.Rotation), barrel.Height * (1 - (float)Math.Cos(barrel.Rotation))) * Scale;


            this.sineWaveCannonBalls.Add(sineWaveCannonBall);
            this.shootNumBulletTwo++;
        }

        public void Shoot(SplitCannonBall splitCannonBall)
        {
            splitCannonBall.ObjectFrameTexture = this.ObjectFrameTexture;
            splitCannonBall.ObjectFrameActive = this.ObjectFrameActive;
            splitCannonBall.Direction = new Vector2((float)Math.Sin(this.barrel.Rotation), -(float)Math.Cos(this.barrel.Rotation));
            splitCannonBall.Position = new Vector2(barrel.Position.X + (barrel.Width / 2 - splitCannonBall.Width / 2) * Scale, barrel.Position.Y)
                + new Vector2(barrel.Height * (float)Math.Sin(barrel.Rotation), barrel.Height * (1 - (float)Math.Cos(barrel.Rotation))) * Scale;
            splitCannonBall.Rotation = this.barrel.Rotation;

            this.splitCannonBalls.Add(splitCannonBall);
            this.shootNumBulletThree++;
        }

        public void Reload(int indexNeedRelaod) {
            if (indexNeedRelaod == 1) {
                reloadActive = true;
                reloadTimer = 0;
            }
            if (indexNeedRelaod == 2)
            {
                reloadActive2 = true;
                reloadTimer2 = 0;
            }
            if (indexNeedRelaod == 3)
            {
                reloadActive3 = true;
                reloadTimer3 = 0;
            }


            this.needRealodModel = indexNeedRelaod;
            //readyShoot = false;
        }

        internal bool Collision(Fireball aFireball, out int currentLife)
        {
            if (Life > 0) { 
                if (this.CannonAndBarrelBorder.Intersects(aFireball.ObjectBorder)) {
                    _life--;
                    currentLife = _life;
                    return true;
                }
            }
            currentLife = _life;
            return false;
            //throw new NotImplementedException();
        }

        internal void ChangeTexture(Texture2D poofTexture, int poofWidth, int poofHeight, float poofCelTime, int scale)
        {
            if (this.currentConnonState == CannonState.Alive)
            {
                cannonSeq = new CelAnimationSequence(poofTexture, poofWidth, poofCelTime);
                this.Texture = poofTexture;
                this.Width = poofWidth;
                this.Height = poofHeight;
                this.celTime = poofCelTime;
                this.barrel.Position = new Vector2(500, 500) * Scale;
                this.Scale = scale;
                this.currentConnonState = CannonState.Dying;
                celAnimationPlayer.Play(cannonSeq);
            }
        }
    }
}
