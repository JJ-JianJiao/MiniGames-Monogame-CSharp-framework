using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        protected const int WindowWidth = 250 * Scale;
        protected const int WindowHeight = 150 * Scale;
        protected const int CourtLineThickness = 4 * Scale;
        protected const int PaddleLine = 215 * Scale;
        protected const int PaddleSpeed = 200 * Scale;
        protected const int Scale = 5;
        protected Texture2D courtBackground;

        Paddle paddle;


        Ball ball;

        protected Rectangle courtBoundingBox = Rectangle.Empty;

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();
            courtBoundingBox = new Rectangle(0 + CourtLineThickness, 0 + CourtLineThickness, 
                                            WindowWidth - CourtLineThickness * 2, WindowHeight - CourtLineThickness * 2);

            ball = new Ball();
            ball.Initialize(Scale, courtBoundingBox, new Vector2(50, 50), new Vector2(-1, 1));

            paddle = new Paddle();
            paddle.Initialize(Scale, courtBoundingBox, new Vector2(PaddleLine, 100));
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            courtBackground = Content.Load<Texture2D>("Court");

            ball.LoadContent(Content);
            paddle.LoadContent(Content);
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            if(kbState.IsKeyDown(Keys.Up))
            {
                paddle.Direction = new Vector2(1, -1);
            }
            else if(kbState.IsKeyDown(Keys.Down))
            {
                paddle.Direction = new Vector2(0, 1);
            }
            else
            {
                paddle.Direction = new Vector2(0, 0);
            }

            paddle.Update(gameTime);
            ball.Update(gameTime);

            ball.ProcessCollision(paddle.BoundingBox);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(courtBackground, Vector2.Zero, null, Color.MonoGameOrange, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            ball.Draw(spriteBatch);
            paddle.Draw(spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
