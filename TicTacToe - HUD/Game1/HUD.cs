using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe
{
    public class HUD
    {
        protected SpriteFont textFont;
        protected Texture2D background;

        protected Vector2 hudPosition;

        protected Vector2 xTurnCountPosition;
        protected Vector2 oTurnCountPosition;
        protected Vector2 messagePosition;

        protected string message;
        protected int xTurnCount;
        protected int oTurnCount;
        internal string Message
        {
            get { return message; }
            set { message = value; }
        }
        internal int XTurnCount
        {
            get { return xTurnCount; }
            set { xTurnCount = value; }
        }

        internal int OTurnCount
        {
            get { return oTurnCount; }

            set { oTurnCount = value; }
        }
        internal void Initialize(Vector2 hudPosition)
        {
            this.hudPosition = hudPosition;
            xTurnCountPosition = new Vector2(hudPosition.X + 5, hudPosition.Y + 5);
            oTurnCountPosition = new Vector2(hudPosition.X + 5, hudPosition.Y + 18);
            messagePosition = new Vector2(hudPosition.X + 52, hudPosition.Y + 10);
            xTurnCount = 0;
            oTurnCount = 0;
            message = "";
        }

        internal void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("HUDBackground");
            textFont = content.Load<SpriteFont>("SystemArialFont");
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, hudPosition, Color.White);
            spriteBatch.DrawString(textFont, "X = " + xTurnCount, xTurnCountPosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(textFont, "O = " + oTurnCount, oTurnCountPosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(textFont, message, messagePosition, Color.Blue, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
        }
    }
}
