using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson04BackgroundAndAnimotion
{
    public class Lesson04 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroudImg;
        Texture2D walkingman;
        KeyboardState keyState;

        CelAnimationSequence animationSequence;
        CelAnimationPlayer animationPlayer;

        static float walkManXMovement = 2;
        static float walkmanYMovement = 0;
        bool walkDirection = true;
        Vector2 walkingPosition = new Vector2(0, 100);
        const int WindowHeight = 768;
        const int WindowWidth = 1366;

        public Lesson04()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroudImg = Content.Load<Texture2D>("Station");
            walkingman = Content.Load<Texture2D>("Walking");
            //walkingman = Content.Load<Texture2D>("multiWalking");

            animationSequence = new CelAnimationSequence(walkingman, 81, 1 / 8.0f);
            //animationSequence = new CelAnimationSequence(walkingman, 61, 1 / 12.0f);
            animationPlayer = new CelAnimationPlayer();
            animationPlayer.Play(animationSequence);
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //// TODO: Add your update logic here
            //if(walkingPosition.X > (640-81) || walkingPosition.X < 0){
            //        walkManXMovement = walkManXMovement * walkDirection;
            //}
            //animationPlayer.Update(gameTime);
            //walkingPosition.X += walkManXMovement;

            keyState = Keyboard.GetState();
            if (walkingPosition.X <= (640 - 81) || walkingPosition.X >= 0)
            {
                if (keyState.IsKeyDown(Keys.Left))
                {
                    animationPlayer.Update(gameTime);
                    walkingPosition.X -= walkManXMovement;
                    walkDirection = false;
                }
                else if (keyState.IsKeyDown(Keys.Right))
                {
                    animationPlayer.Update(gameTime);
                    walkDirection = true;
                    walkingPosition.X += walkManXMovement;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroudImg, Vector2.Zero, Color.White);
            if(walkDirection){
                animationPlayer.Draw(spriteBatch, walkingPosition, SpriteEffects.None);
            }else{
                animationPlayer.Draw(spriteBatch, walkingPosition, SpriteEffects.FlipHorizontally);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
