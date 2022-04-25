using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lab03_Porn
{
    public class Lab03_Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont arialFont;

        Random rad = new Random();
        const int Scale = 4;
        const int MenuWidth = 482 / 2 * Scale;
        const int MenuHeight = 196 / 2 * Scale;
        const int CountWidth = 241;
        const int CountHeight = 150;
        const int HUDBoardWidth = 241;
        const int HUDBoardHeight = 57;
        const int WindowsWidth = CountWidth * Scale;
        const int WindowsHeight = (CountHeight + HUDBoardHeight)* Scale;
        const int CourtWidth = 241 * Scale;
        const int CourtHeight = 150 * Scale;
        const int CourtBorder = 4 * Scale;
        const int BallWidthAndHeight = 7 * Scale;
        const int PaddleWidth = 2 * Scale;
        const int PaddleHeight = 18 * Scale;
        int paddleSpeed = 100* Scale;
        int ballSpeed = 50*Scale;
        Vector2 ballStartPosition = new Vector2(118, 72);
        Vector2 paddleOneStartPosition = new Vector2(8, 63);
        Vector2 paddleTwoStartPosition = new Vector2(241-8, 63);
        Vector2 HUDStartPosition = new Vector2(0,CountHeight * Scale);
        Point ballDirection;
        bool activeMenu = true;
        int menuState;
        Point menuStartPoint = new Point(0, 0);
        Point menuListPoint = new Point(100 * Scale, 75 * Scale);


        Color startBgColor = new Color(25, 28, 33);
        KeyboardState kbState;
        KeyboardState KbPreState;

        Texture2D menuBackgroundTexture;
        Texture2D courtBackgroundTexture;
        Texture2D ballTexture;
        Texture2D paddleTexture;
        Texture2D HUDTexture;

        Menu pongMenu;
        Court pongCourt;
        Ball pongBall;
        List<Ball> ballList = new List<Ball>();
        Paddle pongPaddleOne;
        Paddle pongPaddleTwo;
        HUD pongHUD;

        bool isOver = true;
        bool activeStick = false;
        //int isStick = 0;

        private float paddleColorTime = 0;
        //private float paddleColorDuration = 0.5f;
        private float paddleColorDuration = 500f;
        private float currentTime = 0;
        private float countDuration = 4f;
        private float countContinueDuration = 9f;
        Vector2 servedStrPosistion = new Vector2(112, 60);

        Color PaddleOneColor;
        Color PaddleTwoColor;

        public enum GameState { 
            Start,
            Initialize,
            Serving,
            Playing,
            GameOver
        }

        protected GameState currentGameState = GameState.Start;
        public Lab03_Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowsWidth;
            graphics.PreferredBackBufferHeight = WindowsHeight;
            graphics.ApplyChanges();
            pongMenu = new Menu(MenuWidth,MenuHeight,menuStartPoint, Scale);
            pongCourt = new Court(CourtWidth, CourtHeight, Scale, CourtBorder);
            ballDirection = new Point(rad.Next(1, 3), rad.Next(1, 4));
            pongBall = new Ball(BallWidthAndHeight, Scale, ballSpeed, ballStartPosition,pongCourt, ballDirection);
            pongPaddleOne = new Paddle(PaddleWidth, PaddleHeight, paddleSpeed, paddleOneStartPosition, Scale, pongCourt, Keys.W,Keys.S);
            pongPaddleTwo = new Paddle(PaddleWidth, PaddleHeight, paddleSpeed, paddleTwoStartPosition, Scale, pongCourt, Keys.Up, Keys.Down);
            pongHUD = new HUD(HUDBoardWidth, HUDBoardHeight, Scale, HUDStartPosition);
            PaddleOneColor = Color.White;
            PaddleTwoColor = Color.White;

            //test
            //bool ccccccc = true;
            //bool ddddddd = true;
            //bool[] a = {true, false, true, true };
            //foreach (bool b in a) {
            //    ccccccc &= b;
            //    ddddddd |= b;
            //}

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuBackgroundTexture = Content.Load<Texture2D>("Menu");
            pongMenu.LoadContent(menuBackgroundTexture);

            arialFont = Content.Load<SpriteFont>("SystemArialFont");
            pongMenu.LoadContent(arialFont, menuListPoint);

            courtBackgroundTexture = Content.Load<Texture2D>("Court");
            pongCourt.LoadContent(courtBackgroundTexture);

            ballTexture = Content.Load<Texture2D>("Ball");
            pongBall.LoadContent(ballTexture);
            ballList.Add(pongBall);

            paddleTexture = Content.Load<Texture2D>("Paddle");
            pongPaddleOne.LoadContent(paddleTexture);
            pongPaddleTwo.LoadContent(paddleTexture);

            HUDTexture = Content.Load<Texture2D>("HUDBackground");
            pongHUD.LoadContexnt(HUDTexture, arialFont);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            int isWin = -1;
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if () { 
                paddleColorTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //}
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            kbState = Keyboard.GetState();
            if (!activeMenu)
            {
                if(currentTime >= countDuration) {
                    //foreach (Ball ball in ballList) {
                    //    if(ballList.Count==1)
                    //        isWin = ball.Update(gameTime, kbState, KbPreState);
                    //    else { 
                    //        isWin = ball.Update(gameTime, kbState, KbPreState);
                    //        if (isWin == 1 || isWin == 2) {
                    //            //ballList.Remove(ball);
                    //        }
                    //    }
                    //}

                    for(int i = 0; i <ballList.Count; i++)
                    {
                        if (ballList.Count == 1)
                            isWin = ballList[0].Update(gameTime, kbState, KbPreState, pongPaddleOne.MovementY, pongPaddleTwo.MovementY, WindowsWidth);
                        else
                        {
                            isWin = ballList[i].Update(gameTime, kbState, KbPreState, pongPaddleOne.MovementY, pongPaddleTwo.MovementY, WindowsWidth);
                            if (isWin == 1 || isWin == 2)
                            {
                                ballList.RemoveAt(i);
                            }
                            isWin = 0;
                        }
                    }
                    //isWin = pongBall.Update(gameTime, kbState, KbPreState);
                }
                pongPaddleOne.Update(gameTime, kbState);
                pongPaddleTwo.Update(gameTime, kbState);
            }
            //if (kbState.IsKeyUp(Keys.Space) && KbPreState.IsKeyDown(Keys.Space))
            //{
            //    isStick = 0;
            //}

            if (paddleColorTime > paddleColorDuration)
            {
                PaddleOneColor = Color.White;
                PaddleTwoColor = Color.White;
            }
            if (kbState.IsKeyUp(Keys.F1) && KbPreState.IsKeyDown(Keys.F1))
            {
                activeStick = !activeStick;
            }

            if (kbState.IsKeyUp(Keys.F2) && KbPreState.IsKeyDown(Keys.F2))
            {
                Ball newBall = new Ball(BallWidthAndHeight, Scale, ballList[0].BallSpeed, ballStartPosition, pongCourt, ballDirection);
                newBall.LoadContent(ballTexture);
                newBall.Reset(ballStartPosition, new Point(rad.Next(1, 3), rad.Next(1, 4)));
                ballList.Add(newBall);
            }
            else if (kbState.IsKeyUp(Keys.F3) && KbPreState.IsKeyDown(Keys.F3))
            {
                if(ballList.Count >=2)
                    ballList.RemoveAt(ballList.Count-1);
            }
            switch (currentGameState)
            {
                case GameState.Start:
                    if (activeMenu)
                    {
                        menuState = pongMenu.Update(gameTime, kbState, KbPreState);
                        if (menuState == 1)
                        {
                            activeMenu = false;
                            currentTime = 0;
                            currentGameState = GameState.Initialize;
                        }
                        else if (menuState == 2)
                        {
                            currentGameState = GameState.GameOver;
                        }
                    }else 
                        currentGameState = GameState.Initialize;
                    break;
                case GameState.Initialize:
                    if (!activeMenu)
                    {
                        if (isOver)
                        {
                            if (pongHUD.CurrentRound >= 3)
                            {
                                foreach (Ball ball in ballList) {
                                    ball.Reset(ballStartPosition, new Point(rad.Next(1, 3), rad.Next(1, 4)));
                                }
                                //pongBall.Reset(ballStartPosition, new Point(rad.Next(1, 2), rad.Next(1, 3)));
                                if (kbState.IsKeyUp(Keys.N) && KbPreState.IsKeyDown(Keys.N) || currentTime >= countContinueDuration)
                                {
                                    activeMenu = true;
                                    pongHUD.Reset();
                                    currentTime = 0;
                                    currentGameState = GameState.Start;
                                }
                                else if (kbState.IsKeyUp(Keys.Y) && KbPreState.IsKeyDown(Keys.Y))
                                {
                                    pongHUD.Reset();
                                    currentTime = 0;
                                    pongPaddleOne.Reset();
                                    pongPaddleTwo.Reset();
                                    currentGameState = GameState.Serving;
                                }
                            }
                            else
                            {
                                pongHUD.Reset();
                                foreach (Ball ball in ballList)
                                {
                                    ball.Reset(ballStartPosition, new Point(rad.Next(1, 3), rad.Next(1, 4)));
                                }
                                //pongBall.Reset(ballStartPosition, new Point(rad.Next(1, 2), rad.Next(1, 3)));
                                currentGameState = GameState.Serving;
                            }

                        }
                        else
                        {
                            pongHUD.PlayerOneHitTimes = 0;
                            pongHUD.PlayerTwoHitTimes = 0;
                            foreach (Ball ball in ballList)
                            {
                                ball.Reset(ballStartPosition, new Point(rad.Next(1, 3), rad.Next(1, 4)));
                            }
                            //pongBall.Reset(ballStartPosition, new Point(rad.Next(1, 2), rad.Next(1, 3)));
                            currentGameState = GameState.Serving;
                        }
                    }
                    break;
                case GameState.Serving:
                    if (pongHUD.CurrentRound < 3)
                    {
                        if(currentTime > 4f)
                            currentGameState = GameState.Playing;
                    }

                    break;
                case GameState.Playing:
                    if(!activeMenu) {
                        foreach (Ball ball in ballList) {
                            if (ball.ProcessCollision(pongPaddleOne.CurrentboundingBox))
                            {
                                PaddleOneColor = Color.MonoGameOrange;
                                paddleColorTime = 0;
                                if (activeStick)
                                {
                                    ball.BallStick = 1;
                                    //isStick = 1;
                                }
                                else {
                                    ball.BallStick = 0;
                                    pongHUD.PlayerOneHitTimes++;
                                }
                            }
                            if (ball.ProcessCollision(pongPaddleTwo.CurrentboundingBox))
                            {

                                PaddleTwoColor = Color.MonoGameOrange;
                                paddleColorTime = 0;
                                if (activeStick)
                                {
                                    ball.BallStick = 2;
                                }
                                else
                                {
                                    ball.BallStick = 0;
                                    pongHUD.PlayerTwoHitTimes++;
                                }
                            }
                        }

                        //if (pongBall.ProcessCollision(pongPaddleOne.CurrentboundingBox))
                        //{
                        //    pongHUD.PlayerOneHitTimes++;
                        //    PaddleOneColor = Color.MonoGameOrange;
                        //    paddleColorTime = 0;
                        //}
                        //if (pongBall.ProcessCollision(pongPaddleTwo.CurrentboundingBox))
                        //{
                        //    pongHUD.PlayerTwoHitTimes++;
                        //    PaddleTwoColor = Color.MonoGameOrange;
                        //    paddleColorTime = 0;
                        //}

                        if (isWin == 1)
                        {
                            pongHUD.PlayerOneWinTimes++;
                            pongHUD.CurrentRound++;
                            currentGameState = GameState.GameOver;
                        }
                        else if (isWin == 2)
                        {
                            pongHUD.PlayerTwoWinTimes++;
                            pongHUD.CurrentRound++;
                            currentGameState = GameState.GameOver;
                        }
                    }
                    break;
                case GameState.GameOver:

                    if (activeMenu) {
                        if (menuState == 2) {
                            Exit();
                        }
                    } 
                    else
                    {
                        int currentHightHit = pongHUD.PlayerOneHitTimes >= pongHUD.PlayerTwoHitTimes ? pongHUD.PlayerOneHitTimes : pongHUD.PlayerTwoHitTimes;
                        pongHUD.HighestHit = currentHightHit >= pongHUD.HighestHit ? currentHightHit : pongHUD.HighestHit;
                        if (pongHUD.CurrentRound < 3)
                        {
                            isOver = false;
                            currentGameState = GameState.Initialize;
                            currentTime = 0;
                        }
                        else {
                            isOver = true;
                            currentTime = 0;
                            currentGameState = GameState.Initialize;
                        }

                    }
                    break;
                default:
                    break;
            }

            KbPreState = kbState; 
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(startBgColor);
            spriteBatch.Begin();


            if (!activeMenu)
            {
                pongCourt.Draw(spriteBatch);
                foreach (Ball ball in ballList) {
                    ball.Draw(spriteBatch);
                }
                //pongBall.Draw(spriteBatch);
                pongPaddleOne.Draw(spriteBatch, PaddleOneColor);
                pongPaddleTwo.Draw(spriteBatch, PaddleTwoColor);
                pongHUD.Draw(spriteBatch);
                pongHUD.Draw(spriteBatch, ballList[0].BallSpeed);
                pongHUD.DrawShowCountBalls(spriteBatch, ballList.Count);
                pongHUD.DrawShowSticks(spriteBatch,activeStick);
                //pongHUD.Draw(spriteBatch,pongBall.BallSpeed);
            }
            switch (currentGameState)
            {
                case GameState.Start:
                    if (activeMenu)
                    {
                        pongMenu.Draw(spriteBatch);
                    }
                    //else
                    break;
                case GameState.Initialize:
                    if (!activeMenu) {
                        if (isOver && pongHUD.CurrentRound >= 3)
                        {
                            spriteBatch.DrawString(arialFont, "Play Again? (Y / N )", (servedStrPosistion - new Vector2(70, 40)) * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1f, SpriteEffects.None, 0f);
                            if (currentTime <= 1.0f)
                            {
                                spriteBatch.DrawString(arialFont, "9", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 2.0f)
                            {
                                spriteBatch.DrawString(arialFont, "8", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 3.0f)
                            {
                                spriteBatch.DrawString(arialFont, "7", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 4.0f)
                            {
                                spriteBatch.DrawString(arialFont, "6", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 5.0f)
                            {
                                spriteBatch.DrawString(arialFont, "5", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 6.0f)
                            {
                                spriteBatch.DrawString(arialFont, "4", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 7.0f)
                            {
                                spriteBatch.DrawString(arialFont, "3", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 8.0f)
                            {
                                spriteBatch.DrawString(arialFont, "2", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 9.0f)
                            {
                                spriteBatch.DrawString(arialFont, "1", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    break;
                case GameState.Serving:
                    if (!activeMenu) { 
                        if (pongHUD.CurrentRound < 3) 
                        {
                            if (currentTime <= 1.0f && pongHUD.CurrentRound < 3)
                            {
                                spriteBatch.DrawString(arialFont, "3", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 2.0f)
                            {
                                spriteBatch.DrawString(arialFont, "2", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 3.0f)
                            {
                                spriteBatch.DrawString(arialFont, "1", servedStrPosistion * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                            else if (currentTime <= 4.0f)
                            {
                                spriteBatch.DrawString(arialFont, "Go!", (servedStrPosistion - new Vector2(10, 0)) * Scale, Color.Red, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    break;
                case GameState.Playing:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
