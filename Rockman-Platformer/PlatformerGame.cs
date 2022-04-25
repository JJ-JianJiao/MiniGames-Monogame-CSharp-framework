using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab06_Platformer
{
    public class PlatformerGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        internal const int Gravity = 5;

        protected SpriteFont SpriteFont;
        protected enum GameState { 
            Begin,
            Gaming,
            End,
        }
        protected GameState currentGameState = GameState.Begin;

        protected Player player;
        protected const int WindowWidth = 550;
        protected const int WindowHeight = 400;
        protected Rectangle gameBoundingBox = new Rectangle(0, 0, WindowWidth, WindowHeight);

        //internal ColliderTop ground;
        internal Platform ground;


        protected Platform[] platforms;
        protected Coin[] coins;

        Texture2D brickTexture01;
        Texture2D brickTexture02;
        public PlatformerGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();
            player = new Player(new Vector2(0, 338), gameBoundingBox);

            //ground = new ColliderTop(new Vector2(0, 370), new Vector2(WindowWidth, 1), "ColliderTop");
            ground = new Platform(new Vector2(0, 370), new Vector2(WindowWidth, 16));

            //colliderTop = new Collider(new Vector2(160, 270), new Vector2(80, 1), "ColliderTop", Collider.ColliderType.Top);
            //colliderRight = new Collider(new Vector2(250, 270), new Vector2(1, 20), "ColliderRight", Collider.ColliderType.Right);
            //colliderBottom = new Collider(new Vector2(160, 290), new Vector2(80, 1), "ColliderBottom", Collider.ColliderType.Bottom);
            //colliderLeft = new Collider(new Vector2(150, 270), new Vector2(1, 20), "ColliderLeft", Collider.ColliderType.Left);
            platforms = new Platform[12];
            platforms[0] = new Platform(new Vector2(100, 320), new Vector2(128, 16));
            platforms[1] = new Platform(new Vector2(10, 270), new Vector2(64, 16));
            platforms[2] = new Platform(new Vector2(100, 220), new Vector2(64, 16));
            platforms[3] = new Platform(new Vector2(10, 170), new Vector2(64, 16));
            platforms[4] = new Platform(new Vector2(100, 120), new Vector2(64, 16));
            platforms[5] = new Platform(new Vector2(30, 70), new Vector2(100, 16));
            platforms[6] = new Platform(new Vector2(180, 60), new Vector2(50, 16),true, 100);
            platforms[7] = new Platform(new Vector2(380, 60), new Vector2(50, 16));
            platforms[8] = new Platform(new Vector2(490, 60), new Vector2(50, 16),true,0,220);
            platforms[9] = new Platform(new Vector2(350, 220), new Vector2(80, 16));
            platforms[10] = new Platform(new Vector2(350, 170), new Vector2(16, 50));
            platforms[11] = new Platform(new Vector2(350, 154), new Vector2(80, 16));

            coins = new Coin[10];
            //coins[0].Initial(128, 128, 0, 1, Vector2.Zero, new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[0] = new Coin(128, 128, 0, 1, new Vector2(144, 300), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[1] = new Coin(128, 128, 0, 1, new Vector2(35, 250), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[2] = new Coin(128, 128, 0, 1, new Vector2(125, 200), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[3] = new Coin(128, 128, 0, 1, new Vector2(35, 150), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[4] = new Coin(128, 128, 0, 1, new Vector2(120, 100), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[5] = new Coin(128, 128, 0, 1, new Vector2(60, 50), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[6] = new Coin(128, 128, 0, 1, new Vector2(90, 50), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[7] = new Coin(128, 128, 0, 1, new Vector2(380, 40), new Rectangle(0, 0, WindowWidth, WindowHeight));
            //coins[8] = new Coin(128, 128, 0, 1, new Vector2(490, 40), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[8] = new Coin(128, 128, 0, 1, new Vector2(380, 200), new Rectangle(0, 0, WindowWidth, WindowHeight));
            //coins[9] = new Coin(128, 128, 0, 1, new Vector2(340, 150), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[9] = new Coin(128, 128, 0, 1, new Vector2(380, 134), new Rectangle(0, 0, WindowWidth, WindowHeight));


            base.Initialize();
            player.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            //ground.LoadContent(Content);

            foreach (Platform platform in platforms) {
                platform.LoadContent(Content);
            }
            brickTexture01 = Content.Load<Texture2D>("brick");
            brickTexture02 = Content.Load<Texture2D>("brick2");


            ground.Texture = brickTexture01;
            platforms[0].Texture = brickTexture01;
            platforms[1].Texture = brickTexture01;
            platforms[2].Texture = brickTexture01;
            platforms[3].Texture = brickTexture01;
            platforms[4].Texture = brickTexture01;
            platforms[5].Texture = brickTexture01;
            platforms[6].Texture = brickTexture01;
            platforms[7].Texture = brickTexture01;
            platforms[8].Texture = brickTexture01;
            platforms[9].Texture = brickTexture01;
            platforms[10].Texture = brickTexture02;
            platforms[11].Texture = brickTexture01;
            //colliderTop.LoadContent(Content);
            //colliderRight.LoadContent(Content);
            //colliderLeft.LoadContent(Content);
            //colliderBottom.LoadContent(Content);


            //coins[0].Texture = Content.Load<Texture2D>("CoinSprite");
            coins[0].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[1].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[2].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[3].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[4].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[5].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[6].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[7].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            //coins[8].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[8].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            //coins[9].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);
            coins[9].LoadContent(Content.Load<Texture2D>("CoinSprite"), -1);


            SpriteFont = Content.Load<SpriteFont>("SystemArialFont");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            //switch (currentGameState)
            //{
            //    case GameState.Begin:
            //        break;
            //    case GameState.Gaming:
            //        break;
            //    case GameState.End:
            //        break;
            //    default:
            //        break;
            //}

            ground.ProcessCollisions(player);
            foreach (Platform platform in platforms)
            {
                //platform.Update(gameTime);
                platform.ProcessCollisionsTopAndBottom(player);
            }
            //.  foreach (platform platform in platforms)
            //{
            //    platform.update(gametime);
            //    platform.processcollisionstopandbottom(player);
            //}

            if (currentGameState == GameState.Gaming)
            {
                if (kbState.IsKeyDown(Keys.Space))
                {
                    player.Jump();
                }

                if (kbState.IsKeyDown(Keys.Left))
                {
                    player.Move(new Vector2(-1, 0));
                }
                else if (kbState.IsKeyDown(Keys.Right))
                {
                    player.Move(new Vector2(1, 0));
                }
                else
                {
                    player.Stop();
                }
            }
            else if (currentGameState == GameState.Begin) {

                if (kbState.IsKeyDown(Keys.Enter))
                {
                    currentGameState = GameState.Gaming;
                }
            }
            else if (currentGameState == GameState.End)
            {

                if (kbState.IsKeyDown(Keys.N))
                {
                    currentGameState = GameState.Begin;
                    Initialization();
                }
                else if (kbState.IsKeyDown(Keys.Y))
                {
                    currentGameState = GameState.Gaming;
                    Initialization();

                }

            }


            //ground.ProcessCollisions(player);
            foreach (Platform platform in platforms)
            {
                platform.ProcessCollisionsLeftAndRight(player);
            }
            //colliderTop.ProcessCollisions(player);
            //colliderRight.ProcessCollisions(player);
            //colliderLeft.ProcessCollisions(player);
            //colliderBottom.ProcessCollisions(player);
            player.Update(gameTime);
            foreach (Coin coin in coins) {
                coin.ProcessCollisions(player);
            }
            foreach (Platform platform in platforms)
            {
                platform.Update(gameTime);
                platform.ProcessCollisionsTopAndBottom(player);
            }
            if (player.Score == 10) {
                currentGameState = GameState.End;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {


            GraphicsDevice.Clear(Color.Wheat);
            spriteBatch.Begin();



            switch (currentGameState)
            {
                case GameState.Begin:
                    spriteBatch.DrawString(SpriteFont, "-Start-", new Vector2(WindowWidth / (float)2.5, WindowHeight / (float)2.5), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case GameState.Gaming:
                    break;
                case GameState.End:
                    spriteBatch.DrawString(SpriteFont, "You Win!", new Vector2(200, 180), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(SpriteFont, "Play Again? Y/N", new Vector2(200, 210), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    break;
                default:
                    break;
            }


            player.Draw(spriteBatch);
            ground.Draw(spriteBatch);

            //coins[0].Draw(spriteBatch);
            foreach (Coin coin in coins)
            {
                coin.Draw(spriteBatch);
            }

            //colliderTop.Draw(spriteBatch);
            //colliderRight.Draw(spriteBatch);
            //colliderLeft.Draw(spriteBatch);
            //colliderBottom.Draw(spriteBatch);

            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void Initialization() {

            //coins = new Coin[10];
            //coins[0].Initial(128, 128, 0, 1, Vector2.Zero, new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[0].Position = new Vector2(144, 300);
            coins[1].Position = new Vector2(35, 250);
            coins[2].Position = new Vector2(125, 200);
            coins[3].Position = new Vector2(35, 150);
            coins[4].Position = new Vector2(120, 100);
            coins[5].Position = new Vector2(60, 50);
            coins[6].Position = new Vector2(90, 50);
            coins[7].Position = new Vector2(380, 40);
            //coins[8] = new Coin(128, 128, 0, 1, new Vector2(490, 40), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[8].Position = new Vector2(380, 200);
            //coins[9] = new Coin(128, 128, 0, 1, new Vector2(340, 150), new Rectangle(0, 0, WindowWidth, WindowHeight));
            coins[9].Position = new Vector2(380, 134);
            player.Position = new Vector2(0, 338);
            player.Score = 0;
            player.Initialize();
        }
    }
}