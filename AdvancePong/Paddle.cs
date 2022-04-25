using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Porn
{
    class Paddle
    {
        private int _paddleWidth;
        private int _paddleHeight;
        private int _paddleSpeed;
        private float scale;
        private Vector2 currentPosition;
        private Texture2D paddleTexture;
        private Vector2 paddleDirection;
        private Court court;
        //private Rectangle _currentboundingBox;
        private Keys moveUp;
        private Keys moveDown;
        private Vector2 originalPosition;
        private Vector2 movement;
        private float _movementY;

        public float MovementY {
            get {
                return _movementY;
            }
        }

        public Rectangle CurrentboundingBox {
            get {
                return  new Rectangle((int)currentPosition.X, (int)currentPosition.Y, _paddleWidth, _paddleHeight);
            }
        }


        public Paddle(int width, int height, int speed, Vector2 positon, int scale, Court court, Keys up, Keys down) {
            _paddleWidth = width;
            _paddleHeight = height;
            _paddleSpeed = speed;
            currentPosition = positon * scale;
            originalPosition = currentPosition;
            this.scale = (float)scale;
            this.court = court;
            moveUp = up;
            moveDown = down;
        }

        public void LoadContent(Texture2D texture) {
            paddleTexture = texture;
        }

        public void Update(GameTime gametime, KeyboardState kbState) {
            movement = paddleDirection * _paddleSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
            _movementY = movement.Y;
            currentPosition += movement;

            if (kbState.IsKeyDown(moveUp))
            {
                paddleDirection = new Vector2(0, -1);
            }
            else if (kbState.IsKeyDown(moveDown))
            {
                paddleDirection = new Vector2(0, 1);
            }else
                paddleDirection = new Vector2(0, 0);

            if (currentPosition.Y >= court.CourHeight - court.CourtBoard - _paddleHeight) { 
                currentPosition.Y = court.CourHeight - court.CourtBoard - _paddleHeight;
                _movementY = 0;
            }
            if (currentPosition.Y <= 0 + court.CourtBoard) { 
                currentPosition.Y = 0 + court.CourtBoard;
                _movementY = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(paddleTexture, currentPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None,0f);
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(paddleTexture, currentPosition, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public void Reset()
        {
            this.currentPosition = originalPosition;
        }

    }
}
