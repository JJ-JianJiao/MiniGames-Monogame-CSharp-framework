using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BouncingSprite
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BouncingSprite : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont systemArialFont;

        Texture2D shipSprite;

        float xPosition = 3.0f;
        float yPosition = 3.0f;
        float amountToMoveShipX = 5f;
        float amountToMoveShipY = 5f;
        float rotation = 0;



        public BouncingSprite()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            systemArialFont = Content.Load<SpriteFont>("SystemArialFont");
            shipSprite = Content.Load<Texture2D>("Beetle");

        }

        protected override void Update(GameTime gameTime)
        {

            if (
                xPosition >= (graphics.GraphicsDevice.Viewport.Width - shipSprite.Bounds.Width)
                ||
                xPosition <= 0
                )
            {
                amountToMoveShipX *= -1;
            }
            if (
                yPosition >= (graphics.GraphicsDevice.Viewport.Height - shipSprite.Bounds.Height)
                ||
                yPosition <= 0
                )
            {
                amountToMoveShipY *= -1;
            }
            xPosition += amountToMoveShipX;
            yPosition += amountToMoveShipY;
            rotation += 1f;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSteelBlue);

            //spriteBatch represents a batch of sprite operations
            spriteBatch.Begin();

            //spriteBatch.Draw(shipSprite, new Vector2(xPosition, yPosition), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);

            spriteBatch.Draw(shipSprite, new Vector2(xPosition + (shipSprite.Bounds.Width / 2), yPosition + (shipSprite.Bounds.Height / 2)), null, Color.White, rotation, new Vector2(shipSprite.Bounds.Width / 2, shipSprite.Bounds.Height / 2), 1, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
