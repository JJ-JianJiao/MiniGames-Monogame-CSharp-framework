using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class SplitCannonBall
    {
        public List<CannonBall> SplitBalls = new List<CannonBall>();
        private int width;
        private int height;
        private float speed;
        private int scale;
        public Vector2 Direction { get; set; }
        private Vector2 position;
        private Rectangle gameBorder;
        private Texture2D texture;
        private float CreatBulletTimer = 0;
        private float CreatBulletDuring = 0.5f;
        private int DisposedBalls;
        private bool firstBall = false;
        private bool spilitBalls = false;
        private float rotation;


        public Texture2D ObjectFrameTexture { get; set; }
        public bool ObjectFrameActive { get; set; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public float Speed { get => speed; set => speed = value; }
        public int Scale { get => scale; set => scale = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Rectangle GameBorder { get => gameBorder; set => gameBorder = value; }
        public Texture2D Texture { get => texture; set => texture = value; }
        public float Rotation { get => rotation; set => rotation = value; }

        private enum SplitBallsCannonBallState
        {
            Flying,
            Dispose
        }
        private SplitBallsCannonBallState currentSplitBallState = SplitBallsCannonBallState.Flying;


        public SplitCannonBall()
        {

        }

        public SplitCannonBall(int width, int height, float speed, int scale, Vector2 position, Rectangle gameBorder, Texture2D texture)
        {
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Scale = scale;
            this.Position = position;
            this.GameBorder = gameBorder;
            this.Texture = texture;
            this.DisposedBalls = 0;
            this.CreatBulletTimer = 0;
            firstBall = false;
            //headBall.Initial(width, height, speed, scale, position, gameBorder);
            //headBall.LoadContent(texture, -1f);
        }


        public void Update(GameTime gameTime)
        {
            switch (currentSplitBallState)
            {
                case SplitBallsCannonBallState.Flying:
                    CreatBulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (!firstBall) {
                        CannonBall normalCannonBall = new CannonBall();
                        normalCannonBall.Initial(this.width, this.height, this.speed, this.scale, this.position, this.gameBorder);
                        normalCannonBall.LoadContent(texture, -1f);
                        //normalCannonBall.SetSinProperty(true, 90);
                        normalCannonBall.Direction = this.Direction;
                        SplitBalls.Add(normalCannonBall);
                        firstBall = true;
                    }


                    if (CreatBulletTimer > CreatBulletDuring && !spilitBalls)
                    {
                        for (int i = 0; i < 4; i++) {
                            CannonBall normalCannonBall = new CannonBall();
                            normalCannonBall.Initial(this.width, this.height, this.speed, this.scale, SplitBalls[0].Position, this.gameBorder);
                            normalCannonBall.LoadContent(texture, -1f);
                            //normalCannonBall.SetSinProperty(true, 90);
                            switch (i)
                            {
                                case 0:
                                    normalCannonBall.Direction = new Vector2((float)Math.Sin(this.rotation - MathHelper.ToRadians(30)), -(float)Math.Cos(this.rotation - MathHelper.ToRadians(30)));
                                    break;
                                case 1:
                                    normalCannonBall.Direction = new Vector2((float)Math.Sin(this.rotation - MathHelper.ToRadians(60)), -(float)Math.Cos(this.rotation - MathHelper.ToRadians(60)));
                                    break;
                                case 2:
                                    normalCannonBall.Direction = new Vector2((float)Math.Sin(this.rotation + MathHelper.ToRadians(30)), -(float)Math.Cos(this.rotation + MathHelper.ToRadians(30)));
                                    break;
                                case 3:
                                    normalCannonBall.Direction = new Vector2((float)Math.Sin(this.rotation + MathHelper.ToRadians(60)), -(float)Math.Cos(this.rotation + MathHelper.ToRadians(60)));
                                    break;
                                default:
                                    break;
                            }

                            SplitBalls.Add(normalCannonBall);
                        }
                        spilitBalls = true;
                    }
                    foreach (CannonBall ball in SplitBalls)
                    {
                        //if (ball.IsDispose())
                        //{
                            //DisposedBalls++;
                        //}
                        ball.Update(gameTime);
                    }
                    //if (DisposedBalls == this.sineWaveBalls.Count)
                    //{
                    //    this.currentSineWaveCannonBallState = SineWaveCannonBallState.Dispose;
                    //}
                    //sineWaveBalls[0].Update(gameTime);
                    break;
                case SplitBallsCannonBallState.Dispose:
                    break;
                default:
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CannonBall ball in SplitBalls)
            {
                ball.Draw(spriteBatch);
            }
        }

        internal void Collision(NetBarrier netBarrierOne)
        {
            foreach (CannonBall ball in this.SplitBalls)
            {
                if (ball.Collision(netBarrierOne.ObjectBorder))
                {
                    netBarrierOne.HitByPlayer(10);
                }
            }
        }
    }
}
