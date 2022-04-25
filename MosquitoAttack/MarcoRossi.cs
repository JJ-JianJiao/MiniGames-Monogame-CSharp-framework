using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class MarcoRossi :AnimationObject
    {
        private Vector2 CannonPositon;
        private Rectangle cannonBorder;

        private SpriteFont arial;

        //static Random random = new Random();
        private float createBulletTimer = 0;
        private float createBulletDuring = 2;

        private float helpingTimer = 0;
        private float helpingDuring = 10;

        private float buddyColdDownTimer = 0f;
        private float buddyColdDownDuring = 6.0f;

        private bool GoToWashroom = false;
        private int PeeTimer = 0;

        private bool buddyComeBack = false;

        public bool BuddyReadyToHelp = false;

        private Vector2 BuddyRestPosition;

        private string buddyRestStr = "";
        private bool controlRestStr = true;

        public List<CannonBall> normalBalls = new List<CannonBall>();
        public List<SineWaveCannonBall> sinWaveBalls = new List<SineWaveCannonBall>();

        public List<SplitCannonBall> splitBalls = new List<SplitCannonBall>();

        private Texture2D ballTexture;
        private int ballWidth;
        private int ballHeigfht;
        private float ballSpeed;
        private Vector2 ballPosition;
        private Vector2 ballDirection;



        private int MarcoRossiSpeed = 100;
        private Vector2 MarcoRossiDirection = new Vector2(1, 0);
        private enum MarcoRossiState
        {
            Active,
            Inactive
        }
        public override Rectangle ObjectBorder
        {
            get
            {
                //return new Rectangle( (int)(CannonPositon.X + CannonBorder.Width),(int)(CannonPositon.Y - this.Height * 0.1f * Scale / 2f), 
                //    (int)(this.Width * 0.1f * Scale), (int)(this.Height * 0.1f * Scale)); 
                return new Rectangle( (int)(CannonPositon.X),(int)(CannonPositon.Y), 
                    (int)(this.Width * 0.1f * Scale), (int)(this.Height * 0.1f * Scale));
            }
        }

        private MarcoRossiState currentState = MarcoRossiState.Active;

        public Rectangle CannonBorder { get => cannonBorder; set => cannonBorder = value; }

        public override void Initial(int width, int height, float speed, float scale, Vector2 position, Rectangle gameBorder)
        {
            base.Initial(width, height, speed, scale, position, gameBorder);
            SpriteEffect = SpriteEffects.FlipHorizontally;
            buddyRestStr = "";
            controlRestStr = true;
            normalBalls.Clear();
            sinWaveBalls.Clear();
            splitBalls.Clear();
            MarcoRossiSpeed = 100;
            MarcoRossiDirection = new Vector2(1, 0);
            currentState = MarcoRossiState.Active;
            BuddyReadyToHelp = false;
            buddyComeBack = false;
            PeeTimer = 0;
            GoToWashroom = false;
            buddyColdDownDuring = 6.0f;
            buddyColdDownTimer = 0f;
            helpingDuring = 10;
            helpingTimer = 0;
            createBulletDuring = 2;
            createBulletTimer = 0;
        }

        public void GetBallProperty(int width, int height, float speed, Texture2D   ballTexture) {
            this.ballTexture = ballTexture;
            this.ballWidth = width;
            this.ballHeigfht = height;
            this.ballSpeed = speed * Scale;
            this.ballDirection = new Vector2(0, -1);
        }

        public void GetFontTextrue(SpriteFont font) {
            arial = font;
        }

        public void Update(GameTime gameTime, Vector2 cannonPosition, int attackModel)
        {

            switch (currentState)
            {
                case MarcoRossiState.Active:
                    if (buddyComeBack) {
                        BuddyRestPosition -= MarcoRossiDirection * (float)(MarcoRossiSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                        this.SpriteEffect = SpriteEffects.FlipHorizontally;
                        if (BuddyRestPosition.X <= cannonPosition.X + +CannonBorder.Width) {
                            buddyComeBack = false;
                            //BuddyReadyToHelp = false;
                            helpingTimer = 0;
                        }

                    }


                    createBulletTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;
                    helpingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;




                    if (helpingTimer > helpingDuring && !buddyComeBack) {
                        CannonPositon += MarcoRossiDirection *(float)( MarcoRossiSpeed * gameTime.ElapsedGameTime.TotalSeconds );
                        GoToWashroom = true;
                        this.SpriteEffect = SpriteEffects.None;
                        if (controlRestStr) { 
                            PeeTimer++;
                            controlRestStr = false;
                        }
                        if (this.ObjectBorder.Left > this.GameBorder.Right + 2 * Scale) {
                            currentState = MarcoRossiState.Inactive;
                            buddyColdDownTimer = 0;
                            //BuddyReadyToHelp = false;
                            BuddyRestPosition = new Vector2(this.GameBorder.Right + 2 * Scale, ObjectBorder.Y);
                            GoToWashroom = false;
                            //this.normalBalls.Clear();
                            //this.splitBalls.Clear();
                            //this.sinWaveBalls.Clear();
                        }
                    }
                    else if(helpingTimer <= helpingDuring && !buddyComeBack)
                    { 
                        this.CannonPositon = new Vector2(cannonPosition.X + CannonBorder.Width, cannonPosition.Y - this.Height * 0.1f * Scale / 2f);
                        if (this.ObjectBorder.Right > this.GameBorder.Right)
                        {
                            this.CannonPositon = new Vector2(cannonPosition.X - CannonBorder.Width - 10 * Scale, cannonPosition.Y - this.Height * 0.1f * Scale / 2f);
                            this.SpriteEffect = SpriteEffects.None;
                        }
                        this.SpriteEffect = SpriteEffects.FlipHorizontally;
                    }

                    //this.CannonPositon = new Vector2(cannonPosition.X + CannonBorder.Width , cannonPosition.Y - CannonBorder.Height);

                    if (createBulletTimer > createBulletDuring && !buddyComeBack)
                    {
                        createBulletTimer -= createBulletDuring;
                        switch (attackModel)
                        {
                            case 1:
                                CannonBall aNormalBall = new CannonBall(ballWidth, ballHeigfht, (int)this.ballSpeed, (int)this.Scale*2, CannonPositon + new Vector2(10*Scale,0), this.GameBorder);
                                aNormalBall.LoadContent(this.ballTexture, -1);
                                aNormalBall.Direction = this.ballDirection;
                                aNormalBall.ObjectFrameTexture = this.ObjectFrameTexture;
                                aNormalBall.ObjectFrameActive = this.ObjectFrameActive;
                                normalBalls.Add(aNormalBall);
                                break;
                            case 2:
                                SineWaveCannonBall sineWaveCannonBall = new SineWaveCannonBall(ballWidth, ballHeigfht, (int)this.ballSpeed, (int)this.Scale * 2, CannonPositon + new Vector2(10 * Scale, 0), this.GameBorder, ballTexture);
                                sineWaveCannonBall.ObjectFrameTexture = this.ObjectFrameTexture;
                                sineWaveCannonBall.ObjectFrameActive = this.ObjectFrameActive;
                                sineWaveCannonBall.Direction = this.ballDirection;
                                sineWaveCannonBall.Position = CannonPositon + new Vector2(10 * Scale, 0);
                                this.sinWaveBalls.Add(sineWaveCannonBall);
                                break;
                            case 3:
                                SplitCannonBall splitBall = new SplitCannonBall(ballWidth, ballHeigfht, (int)this.ballSpeed, (int)this.Scale * 2, CannonPositon + new Vector2(10 * Scale, 0), this.GameBorder, ballTexture);
                                splitBall.ObjectFrameTexture = this.ObjectFrameTexture;
                                splitBall.ObjectFrameActive = this.ObjectFrameActive;
                                splitBall.Direction = this.ballDirection;
                                splitBall.Position = CannonPositon + new Vector2(10 * Scale, 0);
                                splitBall.Rotation = 0f;
                                this.splitBalls.Add(splitBall);
                                break;
                        }

                    }
                    break;
                case MarcoRossiState.Inactive:
                    buddyColdDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (buddyColdDownTimer >= 1 && buddyColdDownTimer < 2)
                    {
                        buddyRestStr = "I need 4 more second!!";
                    }
                    else if (buddyColdDownTimer >= 2 && buddyColdDownTimer < 3) {
                        buddyRestStr = "I need 3 more second!!";
                    }
                    else if (buddyColdDownTimer >= 3 && buddyColdDownTimer < 4)
                    {
                        buddyRestStr = "I need 2 more second!!";
                    }
                    else if (buddyColdDownTimer >= 4 && buddyColdDownTimer < 5)
                    {
                        buddyRestStr = "I need 1 more second!!";
                    }
                    else if (buddyColdDownTimer > buddyColdDownDuring) {
                        buddyRestStr = "I am ready!! Press 'Q' \nCall me!!";
                        controlRestStr = true;
                        BuddyReadyToHelp = true;
                    }
                    break;
                default:
                    break;
            }
            //base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case MarcoRossiState.Active:


                    if (buddyComeBack)
                    {
                        if (this.ObjectFrameTexture != null && ObjectFrameActive)
                        {

                            spriteBatch.Draw(this.ObjectFrameTexture, new Rectangle((int)BuddyRestPosition.X, (int)BuddyRestPosition.Y, (int)(this.Width * 0.1f * Scale), (int)(this.Height * 0.1f * Scale)), Color.Red * 0.5f);
                        }
                        DisplayComingBackWords(spriteBatch);
                        spriteBatch.Draw(this.Texture, BuddyRestPosition, null, Color.White, 0f, Vector2.Zero, 0.1f * Scale, SpriteEffect, 0f);

                    }
                    else {

                        if (this.ObjectFrameTexture != null && ObjectFrameActive)
                        {

                            spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, Color.Red * 0.5f);
                        }
                        if (GoToWashroom) {
                            DisPlayWordsOFBathRoom(spriteBatch);
                        }
                        spriteBatch.Draw(this.Texture, CannonPositon, null, Color.White, 0f, Vector2.Zero, 0.1f * Scale, SpriteEffect, 0f);

                    }
                    break;
                case MarcoRossiState.Inactive:
                    if (this.ObjectFrameTexture != null && ObjectFrameActive)
                    {

                        spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, Color.Red * 0.5f);
                    }
                    DisplayWaitingWords(spriteBatch);
                    break;
                default:
                    break;
            }


            foreach (CannonBall ball in this.normalBalls)
            {
                ball.Draw(spriteBatch);
            }

            foreach (SineWaveCannonBall ball in this.sinWaveBalls)
            {
                ball.Draw(spriteBatch);
            }

            foreach (SplitCannonBall ball in this.splitBalls)
            {
                ball.Draw(spriteBatch);
            }
            //base.Draw(spriteBatch);
        }

        public void CallBuddy() {
            if (BuddyReadyToHelp) {
                BuddyReadyToHelp = false;
                this.currentState = MarcoRossiState.Active;
                helpingTimer = 0;
                buddyComeBack = true;
                this.normalBalls.Clear();
                this.splitBalls.Clear();
                this.sinWaveBalls.Clear();
                buddyRestStr = "";
            }
        }

        private void DisPlayWordsOFBathRoom(SpriteBatch spriteBatch) {
            if (PeeTimer%4 == 1)
            {
                //spriteBatch.DrawString(arial, "I need to go to pee!!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow);
                spriteBatch.DrawString(arial, "I need to go to pee!!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);
            }
            else if(PeeTimer % 4 == 2){ 
                //spriteBatch.DrawString(arial, "I need to go to pee! Again!!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow);
                spriteBatch.DrawString(arial, "I need to go to pee! Again!!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);

            }
            else if (PeeTimer % 4 == 3)
            {
                //spriteBatch.DrawString(arial, "This time is not to poo!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow);
                spriteBatch.DrawString(arial, "This time is not to poo!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);

            }
            else if (PeeTimer % 4 == 0)
            {
                //spriteBatch.DrawString(arial, "Ohh! God, my stomach!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow);
                spriteBatch.DrawString(arial, "Ohh! God, my stomach!!", new Vector2(this.CannonPositon.X - 20 * Scale, this.CannonPositon.Y - 10 * Scale), Color.Yellow, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);

            }
        }

        private void DisplayWaitingWords(SpriteBatch sprite) {
            //sprite.DrawString(arial, buddyRestStr, new Vector2(this.BuddyRestPosition.X - 120 *Scale, this.BuddyRestPosition.Y - 10 * Scale), Color.Yellow);

            sprite.DrawString(arial, buddyRestStr, new Vector2(this.BuddyRestPosition.X - 120 * Scale, this.BuddyRestPosition.Y - 10 * Scale), Color.Yellow, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);
        }

        private void DisplayComingBackWords(SpriteBatch sprite)
        {
            //sprite.DrawString(arial, "actually, I made a lot of poo!\nHahahah!!!!", new Vector2(this.BuddyRestPosition.X - 20 * Scale, this.BuddyRestPosition.Y - 20 * Scale), Color.Yellow);

            sprite.DrawString(arial, "actually, I made a lot of poo!\nHahahah!!!!", new Vector2(this.BuddyRestPosition.X - 20 * Scale, this.BuddyRestPosition.Y - 20 * Scale), Color.Yellow, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);


        }
    }
}
