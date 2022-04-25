using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;

namespace MosquitoAttack
{
    class SineWaveCannonBall
    {
        public List<CannonBall> sineWaveBalls = new List<CannonBall>();
        private int width;
        private int height;
        private float speed;
        private int scale;
        public Vector2 Direction { get; set; }
        private Vector2 position;
        private Rectangle gameBorder;
        private Texture2D texture;
        private float CreatBulletTimer = 0;
        private float CreatBulletDuring = 0.2f;
        private int DisposedBalls;

        public Texture2D ObjectFrameTexture { get; set; }
        public bool ObjectFrameActive { get; set; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public float Speed { get => speed; set => speed = value; }
        public int Scale { get => scale; set => scale = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Rectangle GameBorder { get => gameBorder; set => gameBorder = value; }
        public Texture2D Texture { get => texture; set => texture = value; }

        private enum SineWaveCannonBallState
        {
            Flying,
            Dispose
        }

        private SineWaveCannonBallState currentSineWaveCannonBallState = SineWaveCannonBallState.Flying;
        //private Vector2 direction = new Vector2(1, -1);
        public SineWaveCannonBall()
        {

        }

        public SineWaveCannonBall(int width, int height, float speed, int scale, Vector2 position, Rectangle gameBorder,Texture2D texture) 
        {
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Scale = scale;
            this.Position = position;
            this.GameBorder = gameBorder;
            this.Texture = texture;
            this.DisposedBalls = 0;
            //headBall.Initial(width, height, speed, scale, position, gameBorder);
            //headBall.LoadContent(texture, -1f);
        }

        public void Update(GameTime gameTime)
        {
            switch (currentSineWaveCannonBallState)
            {
                case SineWaveCannonBallState.Flying:
                    CreatBulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(CreatBulletTimer <= CreatBulletDuring) { 
                        CannonBall normalCannonBall = new CannonBall();
                        normalCannonBall.Initial(this.width, this.height, this.speed, this.scale, this.position, this.gameBorder);
                        normalCannonBall.LoadContent(texture, -1f);
                        normalCannonBall.SetSinProperty(true, 90);
                        normalCannonBall.Direction = this.Direction;
                        normalCannonBall.ObjectFrameTexture = this.ObjectFrameTexture;
                        normalCannonBall.ObjectFrameActive = this.ObjectFrameActive;
                        sineWaveBalls.Add(normalCannonBall);
                    }
                    foreach (CannonBall ball in sineWaveBalls)
                    {
                        //if (ball.IsDispose()) {
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
                case SineWaveCannonBallState.Dispose:
                    break;
                default:
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(CannonBall ball in sineWaveBalls) { 
                ball.Draw(spriteBatch);
            }
        }

        internal void Collision(NetBarrier netBarrierOne)
        {
            foreach (CannonBall ball in this.sineWaveBalls)
            {
                if (ball.Collision(netBarrierOne.ObjectBorder))
                {
                    netBarrierOne.HitByPlayer();
                }
            }
        }
    }
}
