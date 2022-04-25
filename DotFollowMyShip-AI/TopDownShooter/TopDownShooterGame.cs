using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TopDownShooter
{
    public class TopDownShooterGame : Game
    {
        protected enum TopDownShooterGameState
        {
            Playing = 0,
            Paused,
            Over
        }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private SpriteFont systemArialFont;
        private string statusMessage = "Game Over. Press space bar to start again.";

        private const int WindowWidth = 1280;
        private const int WindowHeight = 720;
        private const float AspectRatio = (float)WindowWidth / (float)WindowHeight;
        private const int MaxBadGuyShipCount = 1;
        private const int BadGuyShipSpeed = 100;
        private const float frictionFactor = 1.15f;

        private TopDownShooterGameState gameState;
        private ShipNonPlayer[] badGuyShips;
        private ShipPlayer goodGuyShip;

        Rectangle gameBoundingBox;
        protected KeyboardState kbPreviousState;
        protected KeyboardState kbState;

        public TopDownShooterGame()
        {
            gameBoundingBox = new Rectangle(0, 0, (int)(WindowWidth), (int)(WindowHeight));
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            graphics.ApplyChanges();
            gameState = TopDownShooterGameState.Playing;
            badGuyShips = new ShipNonPlayer[MaxBadGuyShipCount];

            Random RandNum = new Random();
            for (int c = 0; c < MaxBadGuyShipCount; c++)
            {
                badGuyShips[c] = new ShipNonPlayer();
            }
            goodGuyShip = new ShipPlayer();

            base.Initialize();

            goodGuyShip.Initialize(new Vector2(gameBoundingBox.Center.X, gameBoundingBox.Center.Y), this, gameBoundingBox, frictionFactor);
            for (int c = 0; c < MaxBadGuyShipCount; c++)
            {
                badGuyShips[c].Initialize(new Vector2(gameBoundingBox.Center.X - 250, gameBoundingBox.Center.Y), this, gameBoundingBox, frictionFactor);
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Background");
            systemArialFont = Content.Load<SpriteFont>("SystemArialFont");
            foreach (ShipNonPlayer badGuyShip in badGuyShips)
            {
                badGuyShip.LoadContent(Content);
            }
            goodGuyShip.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            kbPreviousState = kbState;
            kbState = Keyboard.GetState();

            if (KeyWasPressed(Keys.Pause))
            {
                if (gameState == TopDownShooterGameState.Playing)
                {
                    gameState = TopDownShooterGameState.Paused;
                }
            }
            if (KeyWasPressed(Keys.Space))
            {
                if (gameState == TopDownShooterGameState.Paused)
                {
                    gameState = TopDownShooterGameState.Playing;
                }
                else if (gameState == TopDownShooterGameState.Over)
                {
                    Initialize();
                }
            }
            if (goodGuyShip.dead())
            {
                gameState = TopDownShooterGameState.Over;
            }

            switch (gameState)
            {
                case TopDownShooterGameState.Playing:
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        goodGuyShip.rotateLeft();
                    }
                    else if (kbState.IsKeyDown(Keys.Right))
                    {
                        goodGuyShip.rotateRight();
                    }
                    else //neither the right nor the left arrow is pressed
                    {
                        //if a full key-down and key up cycle for the left or right arrow
                        //has just been completed, adjust a bit
                        if (kbPreviousState.IsKeyDown(Keys.Left))
                        {
                            goodGuyShip.rotateRight();
                        }
                        else if (kbPreviousState.IsKeyDown(Keys.Right))
                        {
                            goodGuyShip.rotateLeft();
                        }
                    }

                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        goodGuyShip.increaseSpeed();
                    }
                    else if (kbState.IsKeyDown(Keys.Down))
                    {
                        goodGuyShip.decreaseSpeed();
                    }
                    else
                    {
                        goodGuyShip.decreaseAbsoluteSpeed();

                    }
                    if (KeyWasPressed(Keys.Space))
                    {
                        goodGuyShip.Shoot();
                    }
                    //process the bad guy ships by passing them to the processHit method
                    bool atLeastOneBadGuyAlive = false;
                    foreach (ShipNonPlayer badGuyShip in badGuyShips)
                    {
                        if (badGuyShip.alive())
                        {
                            atLeastOneBadGuyAlive = true;

                            //if there were more than one good guy we would be looping through 
                            //them here
                            goodGuyShip.ProcessProjectiles(badGuyShip);
                            badGuyShip.ProcessProjectiles(goodGuyShip);
                        }
                        badGuyShip.Update(gameTime, goodGuyShip);
                        WrapGameBotAround(badGuyShip);
                    }
                    if (!atLeastOneBadGuyAlive)
                    {
                        gameState = TopDownShooterGameState.Over;
                    }
                    //if there were more than one good guy we would looping through 
                    //them here
                    goodGuyShip.Update(gameTime);
                    WrapGameBotAround(goodGuyShip);
                    break;
                case TopDownShooterGameState.Over:
                    statusMessage = "Game Over. Press space bar to start again.";
                    break;
                case (TopDownShooterGameState.Paused):
                    statusMessage = "Game Paused. Press space bar to unpause.";
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState)
            {
                case TopDownShooterGameState.Playing:
                    foreach (ShipNonPlayer badGuyShip in badGuyShips)
                    {
                        badGuyShip.Draw(spriteBatch);
                    }
                    goodGuyShip.Draw(spriteBatch);
                    break;
                case TopDownShooterGameState.Over:
                    spriteBatch.DrawString(systemArialFont, statusMessage, new Vector2(20.0f, 50.0f), Color.White);
                    break;
                case (TopDownShooterGameState.Paused):
                    spriteBatch.DrawString(systemArialFont, statusMessage, new Vector2(20.0f, 50.0f), Color.White);
                    break;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                foreach (ShipNonPlayer badGuyShip in badGuyShips)
                {
                    badGuyShip.DrawDebug(spriteBatch);
                }
                goodGuyShip.DrawDebug(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        internal void WrapGameBotAround(ShipNonPlayer ship)
        {
            Vector2 shipPosition = ship.Position;

            float rightSideOfScreen = WindowWidth;
            float leftSideOfScreen = 0;
            float topOfScreen = 0;
            float bottomOfScreen = WindowHeight;

            if (shipPosition.X > rightSideOfScreen)
                ship.Position = new Vector2(leftSideOfScreen, shipPosition.Y);
            else if (shipPosition.X < leftSideOfScreen)
                ship.Position = new Vector2(rightSideOfScreen, shipPosition.Y);

            if (shipPosition.Y < topOfScreen)
                ship.Position = new Vector2(shipPosition.X, bottomOfScreen);
            else if (shipPosition.Y > bottomOfScreen)
                ship.Position = new Vector2(shipPosition.X, topOfScreen);
        }
        internal void WrapGameBotAround(ShipPlayer ship)
        {
            Vector2 shipPosition = ship.Position;

            float rightSideOfScreen = WindowWidth;
            float leftSideOfScreen = 0;
            float topOfScreen = 0;
            float bottomOfScreen = WindowHeight;

            if (shipPosition.X > rightSideOfScreen)
                ship.Position = new Vector2(leftSideOfScreen, shipPosition.Y);
            else if (shipPosition.X < leftSideOfScreen)
                ship.Position = new Vector2(rightSideOfScreen, shipPosition.Y);

            if (shipPosition.Y < topOfScreen)
                ship.Position = new Vector2(shipPosition.X, bottomOfScreen);
            else if (shipPosition.Y > bottomOfScreen)
                ship.Position = new Vector2(shipPosition.X, topOfScreen);
        }
        protected bool KeyWasPressed(Keys key)
        {
            return kbState.IsKeyDown(key) && kbPreviousState.IsKeyUp(key);
        }
    }
    
}
