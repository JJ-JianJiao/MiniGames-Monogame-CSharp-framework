using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    public class TicTacToeGame : Game
    {
        public enum GameSpaceState
        {
            Empty, X, O
        }

        public enum GameState
        {
            Initialize,
            WaitForPlayerMove,
            MakePlayerMove,
            EvaluatePlayerMove,
            GameOver
        }

        protected const int WindowHeight = 210;
        protected const int GamePlayAreaHeight = 170;

        protected const int WindowWidth = 170;
        protected const int BoardRows = 3;
        protected const int BoardCols = 3;
        protected const int RowHeight = 50;
        protected const int RowWidth = 50;
        protected const int LineWidth = 10;
        protected const int LineDisplayTime = 1000;

        protected HUD hud; 

        protected GameState currentGameState = GameState.Initialize;
        protected GameSpaceState nextTokenToBePlayed;
        protected GameSpaceState winner;
        protected float lineDisplayTimer = 0;

        protected GameSpaceState[,] gameBoard = new GameSpaceState[,]
            {
                {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
                {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
                {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty}
            };

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        protected Texture2D backgroundImage;
        protected Texture2D blankBackgroundImage;
        protected Texture2D xImage;
        protected Texture2D oImage;
        protected Texture2D resultsLine;
        protected SpriteFont systemArialFont;

        protected MouseState currentMouseState;
        protected MouseState previousMouseState;
        protected Vector2 currentMousePosition = Vector2.Zero;
        protected Vector2 previousMousePosition = Vector2.Zero;
        public TicTacToeGame()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            nextTokenToBePlayed = GameSpaceState.X;
            winner = GameSpaceState.Empty;

            hud = new HUD();
            hud.Initialize(new Vector2(0, GamePlayAreaHeight));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundImage = Content.Load<Texture2D>("TicTacToeBoard");
            blankBackgroundImage = Content.Load<Texture2D>("BlankBoard");
            xImage = Content.Load<Texture2D>("X");
            oImage = Content.Load<Texture2D>("O");
            resultsLine = Content.Load<Texture2D>("blackLine");
            systemArialFont = Content.Load<SpriteFont>("SystemArialFont");
            hud.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            //get mouse state
            currentMouseState = Mouse.GetState();
            currentMousePosition = new Vector2(currentMouseState.Position.X, currentMouseState.Position.Y);

            switch (currentGameState)
            {
                case GameState.Initialize:

                    for (int row = 0; row < gameBoard.GetLength(0); row++)
                    {
                        for (int col = 0; col < gameBoard.GetLength(1); col++)
                        {
                            gameBoard[row, col] = GameSpaceState.Empty;
                        }
                    }
                    nextTokenToBePlayed = GameSpaceState.X;
                    winner = GameSpaceState.Empty;
                    lineDisplayTimer = 0;
                    hud.OTurnCount = 0;
                    hud.XTurnCount = 0;
                    hud.Message = "";
                    currentGameState = GameState.WaitForPlayerMove;
                    break;
                case GameState.WaitForPlayerMove:
                    if ((currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton != ButtonState.Pressed))
                    {
                        currentGameState = GameState.MakePlayerMove;
                    }
                    break;
                case GameState.MakePlayerMove:
                    int rowClicked = (int)(previousMousePosition.Y / (GamePlayAreaHeight / BoardRows));
                    int colClicked = (int)(previousMousePosition.X / (WindowWidth / BoardCols));

                    if (!((rowClicked > BoardRows - 1) || (colClicked > BoardCols - 1)))
                    {
                        if (gameBoard[rowClicked, colClicked] == GameSpaceState.Empty)
                        {
                            gameBoard[rowClicked, colClicked] = nextTokenToBePlayed;
                            if (nextTokenToBePlayed == GameSpaceState.X)
                            {
                                hud.XTurnCount++;
                                nextTokenToBePlayed = GameSpaceState.O;
                            }
                            else if (nextTokenToBePlayed == GameSpaceState.O)
                            {
                                hud.OTurnCount++;
                                nextTokenToBePlayed = GameSpaceState.X;
                            }
                        }
                    }
                    currentGameState = GameState.EvaluatePlayerMove;
                    break;
                case GameState.EvaluatePlayerMove:
                    //check for X wins
                    if (
                        (gameBoard[0, 0] == GameSpaceState.X && gameBoard[0, 1] == GameSpaceState.X && gameBoard[0, 2] == GameSpaceState.X) ||
                        (gameBoard[1, 0] == GameSpaceState.X && gameBoard[1, 1] == GameSpaceState.X && gameBoard[1, 2] == GameSpaceState.X) ||
                        (gameBoard[2, 0] == GameSpaceState.X && gameBoard[2, 1] == GameSpaceState.X && gameBoard[2, 2] == GameSpaceState.X) ||
                        (gameBoard[0, 0] == GameSpaceState.X && gameBoard[1, 0] == GameSpaceState.X && gameBoard[2, 0] == GameSpaceState.X) ||
                        (gameBoard[0, 1] == GameSpaceState.X && gameBoard[1, 1] == GameSpaceState.X && gameBoard[2, 1] == GameSpaceState.X) ||
                        (gameBoard[0, 2] == GameSpaceState.X && gameBoard[1, 2] == GameSpaceState.X && gameBoard[2, 2] == GameSpaceState.X) ||
                        (gameBoard[0, 0] == GameSpaceState.X && gameBoard[1, 1] == GameSpaceState.X && gameBoard[2, 2] == GameSpaceState.X) ||
                        (gameBoard[0, 2] == GameSpaceState.X && gameBoard[1, 1] == GameSpaceState.X && gameBoard[2, 0] == GameSpaceState.X))
                    {
                        winner = GameSpaceState.X;
                        hud.Message = "Player X Wins";
                        currentGameState = GameState.GameOver;
                    }
                    //check for O wins
                    else if (
                              (gameBoard[0, 0] == GameSpaceState.O && gameBoard[0, 1] == GameSpaceState.O && gameBoard[0, 2] == GameSpaceState.O) ||
                              (gameBoard[1, 0] == GameSpaceState.O && gameBoard[1, 1] == GameSpaceState.O && gameBoard[1, 2] == GameSpaceState.O) ||
                              (gameBoard[2, 0] == GameSpaceState.O && gameBoard[2, 1] == GameSpaceState.O && gameBoard[2, 2] == GameSpaceState.O) ||
                              (gameBoard[0, 0] == GameSpaceState.O && gameBoard[1, 0] == GameSpaceState.O && gameBoard[2, 0] == GameSpaceState.O) ||
                              (gameBoard[0, 1] == GameSpaceState.O && gameBoard[1, 1] == GameSpaceState.O && gameBoard[2, 1] == GameSpaceState.O) ||
                              (gameBoard[0, 2] == GameSpaceState.O && gameBoard[1, 2] == GameSpaceState.O && gameBoard[2, 2] == GameSpaceState.O) ||
                              (gameBoard[0, 0] == GameSpaceState.O && gameBoard[1, 1] == GameSpaceState.O && gameBoard[2, 2] == GameSpaceState.O) ||
                              (gameBoard[0, 2] == GameSpaceState.O && gameBoard[1, 1] == GameSpaceState.O && gameBoard[2, 0] == GameSpaceState.O))
                    {
                        winner = GameSpaceState.O;
                        hud.Message = "Player O Wins";
                        currentGameState = GameState.GameOver;
                    }
                    //check for ties
                    else if (
                              (gameBoard[0, 0] != GameSpaceState.Empty) && (gameBoard[0, 1] != GameSpaceState.Empty) && (gameBoard[0, 2] != GameSpaceState.Empty) &&
                              (gameBoard[1, 0] != GameSpaceState.Empty) && (gameBoard[1, 1] != GameSpaceState.Empty) && (gameBoard[1, 2] != GameSpaceState.Empty) &&
                              (gameBoard[2, 0] != GameSpaceState.Empty) && (gameBoard[2, 1] != GameSpaceState.Empty) && (gameBoard[2, 2] != GameSpaceState.Empty))
                    {
                        winner = GameSpaceState.Empty;
                        hud.Message = "Tie!";
                        currentGameState = GameState.GameOver;
                    }
                    //game not over
                    else
                    {
                        currentGameState = GameState.WaitForPlayerMove;
                    }
                    break;
                case GameState.GameOver:
                    KeyboardState kbState;
                    kbState = Keyboard.GetState();
                    if (kbState.IsKeyDown(Keys.Space))
                    {
                        currentGameState = GameState.Initialize;
                    }
                    break;
            }

            previousMouseState = currentMouseState;
            previousMousePosition = currentMousePosition;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
            hud.Draw(spriteBatch);
            //draw all X's and O's to the board
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    if (gameBoard[row, col] == GameSpaceState.X)
                    {
                        spriteBatch.Draw(xImage, new Vector2((xImage.Width + LineWidth) * col, (xImage.Height + LineWidth) * row), Color.White);
                    }
                    else if (gameBoard[row, col] == GameSpaceState.O)
                    {
                        spriteBatch.Draw(oImage, new Vector2((oImage.Width + LineWidth) * col, (oImage.Height + LineWidth) * row), Color.White);
                    }

                }
            }

            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.WaitForPlayerMove:
                    Texture2D imageToDraw = xImage;
                    if (nextTokenToBePlayed == GameSpaceState.O)
                    {
                        imageToDraw = oImage;
                    }
                    else if (nextTokenToBePlayed == GameSpaceState.X)
                    {
                        imageToDraw = xImage;
                    }
                    Vector2 adjustedPosition = new Vector2(currentMousePosition.X - (imageToDraw.Width / 2), currentMousePosition.Y - (imageToDraw.Height / 2));
                    spriteBatch.Draw(imageToDraw, adjustedPosition, Color.White);
                    break;
                case GameState.MakePlayerMove:
                    break;
                case GameState.EvaluatePlayerMove:
                    break;
                case GameState.GameOver:
                    lineDisplayTimer += gameTime.ElapsedGameTime.Milliseconds;

                    //we've displayed the lines for long enough and there are lines to display
                    if (lineDisplayTimer < LineDisplayTime && winner != GameSpaceState.Empty)
                    {
                        //draw the lines
                        for (int row = 0; row < gameBoard.GetLength(0); row++)
                        {
                            if (gameBoard[row, 0] != GameSpaceState.Empty && gameBoard[row, 0] == gameBoard[row, 1] && gameBoard[row, 1] == gameBoard[row, 2])
                            {
                                spriteBatch.Draw(resultsLine, new Vector2(20, (20 + (60f * row))), Color.White);
                            }
                        }
                        for (int col = 0; col < gameBoard.GetLength(1); col++)
                        {
                            if (gameBoard[0, col] != GameSpaceState.Empty && gameBoard[0, col] == gameBoard[1, col] && gameBoard[1, col] == gameBoard[2, col])
                            {
                                spriteBatch.Draw(resultsLine, new Vector2((30 + (60f * col)), 20), null, Color.White, 3.14f / 2, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                            }
                        }
                        if (gameBoard[0, 0] != GameSpaceState.Empty && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2])
                        {
                            spriteBatch.Draw(resultsLine, new Vector2(35, 30), null, Color.White, 3.14f / 4, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                        }
                        if (gameBoard[0, 2] != GameSpaceState.Empty && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
                        {
                            spriteBatch.Draw(resultsLine, new Vector2(135, 40), null, Color.White, 3 * (3.14f / 4), Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                        }
                    }
                    else
                    {
                        spriteBatch.Draw(blankBackgroundImage, Vector2.Zero, Color.White);
                        spriteBatch.DrawString(systemArialFont, "Replay: Space", new Vector2(25, 125), Color.SteelBlue);
                    }
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
