using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Porn
{
    class HUD
    {
        private int _HUDWidth;
        private int _HUDHeight;
        private float scale;
        private Texture2D HUDTexture;
        private SpriteFont textFont;
        private Vector2 HUDBackgroundPosition;
        private int totalRound;
        private int _currentRound;
        private int _playerOneHitTimes;
        private int _playerTwoHitTimes;
        private int _playerOneWinTimes;
        private int _playerTwoWinTimes;
        private int _highestHit;


        private Vector2 totalRoundPosition;
        private Vector2 currentScorePosition;
        private Vector2 playerOneHitTimesPosistion;
        private Vector2 playerTwoHitTimesPosistion;
        private Vector2 msgSpeedUpPosition;
        private Vector2 msgSpeedDownPosition;
        private Vector2 msgCurrentSpeedPosition;
        private Vector2 msgAddBalls;
        private Vector2 msgDeleteBalls;
        private Vector2 msgCurrentBalls;
        private Vector2 msgActiveStick;
        private Vector2 stateActiveStick;
        private Vector2 msgHighestHit;

        public int PlayerOneHitTimes {
            set {
                _playerOneHitTimes = value;
            }
            get {
                return _playerOneHitTimes;
            }
        }

        public int HighestHit
        {
            set
            {
                _highestHit = value;
            }
            get
            {
                return _highestHit;
            }
        }

        public int PlayerTwoHitTimes
        {
            set
            {
                _playerTwoHitTimes = value;
            }
            get
            {
                return _playerTwoHitTimes;
            }
        }


        public int PlayerOneWinTimes
        {
            set
            {
                _playerOneWinTimes = value;
            }
            get
            {
                return _playerOneWinTimes;
            }
        }
        
        public int PlayerTwoWinTimes
        {
            set
            {
                _playerTwoWinTimes = value;
            }
            get
            {
                return _playerTwoWinTimes;
            }
        }

        public int CurrentRound
        {
            set
            {
                _currentRound = value;
            }
            get
            {
                return _currentRound;
            }
        }
        public HUD( int width, int height, int scale, Vector2 backgroundPosition) {
            this._HUDWidth = width;
            this._HUDHeight = height;
            this.scale = (float)scale;
            this.HUDBackgroundPosition = backgroundPosition;
            this.totalRoundPosition = new Vector2(HUDBackgroundPosition.X + 15/2*scale, HUDBackgroundPosition.Y + 20 / 2 * scale);
            this.currentScorePosition = new Vector2(HUDBackgroundPosition.X + 15 / 2 * scale, HUDBackgroundPosition.Y + 70 / 2 * scale);
            this.playerOneHitTimesPosistion = new Vector2(HUDBackgroundPosition.X + 170 / 2 * scale, HUDBackgroundPosition.Y + 45 / 2 * scale);
            this.playerTwoHitTimesPosistion = new Vector2(HUDBackgroundPosition.X + 300 / 2 * scale, HUDBackgroundPosition.Y + 45 / 2 * scale);
            this.msgSpeedUpPosition = new Vector2(HUDBackgroundPosition.X + 150 / 2 * scale, HUDBackgroundPosition.Y + 10 / 2 * scale);
            this.msgSpeedDownPosition = new Vector2(HUDBackgroundPosition.X + 220 / 2 * scale, HUDBackgroundPosition.Y + 10 / 2 * scale);
            this.msgCurrentSpeedPosition = new Vector2(HUDBackgroundPosition.X + 300 / 2 * scale, HUDBackgroundPosition.Y + 10 / 2 * scale);
            this.msgHighestHit = new Vector2(HUDBackgroundPosition.X + 400 / 2 * scale, HUDBackgroundPosition.Y + 10 / 2 * scale);
            this.msgAddBalls = new Vector2(HUDBackgroundPosition.X + 150 / 2 * scale, HUDBackgroundPosition.Y + 95 / 2 * scale);
            this.msgDeleteBalls = new Vector2(HUDBackgroundPosition.X + 220 / 2 * scale, HUDBackgroundPosition.Y + 95 / 2 * scale);
            this.msgCurrentBalls = new Vector2(HUDBackgroundPosition.X + 300 / 2 * scale, HUDBackgroundPosition.Y + 95 / 2 * scale);
            this.msgActiveStick = new Vector2(HUDBackgroundPosition.X + 400 / 2 * scale, HUDBackgroundPosition.Y + 95 / 2 * scale);
            this.stateActiveStick = new Vector2(HUDBackgroundPosition.X + 430 / 2 * scale, HUDBackgroundPosition.Y + 95 / 2 * scale);
        }

        public void LoadContexnt(Texture2D background, SpriteFont setFont) {
            this.HUDTexture = background;
            this.textFont = setFont;
        }

        public void Reset() {
            totalRound = 3;
            _currentRound = 0;
            _playerOneHitTimes = 0;
            _playerTwoHitTimes = 0;
            _playerOneWinTimes = 0;
            _playerTwoWinTimes = 0;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(HUDTexture,HUDBackgroundPosition,null, Color.White,0f, Vector2.Zero,scale,SpriteEffects.None, 0f);
            if (_currentRound == 3) {
                spriteBatch.DrawString(textFont, "ROUND " + _currentRound.ToString() + " / " + totalRound.ToString(), totalRoundPosition, Color.Blue, 0f, Vector2.Zero, 1f / 2 * scale, SpriteEffects.None, 0f);
            }else
                spriteBatch.DrawString(textFont, "ROUND "+ (_currentRound+1).ToString() + " / " + totalRound.ToString(), totalRoundPosition, Color.Blue, 0f, Vector2.Zero, 1f/2*scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Score: " + _playerOneWinTimes.ToString() + " - " + _playerTwoWinTimes.ToString(), currentScorePosition, Color.Blue, 0f, Vector2.Zero, 1f/2*scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Left: " + _playerOneHitTimes.ToString(), playerOneHitTimesPosistion, Color.Blue, 0f, Vector2.Zero, 1f/2*scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Right: " + _playerTwoHitTimes.ToString(), playerTwoHitTimesPosistion, Color.Blue, 0f, Vector2.Zero, 1f/2*scale, SpriteEffects.None, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, int speed)
        {
            spriteBatch.DrawString(textFont, "Speed Up ' + '", msgSpeedUpPosition, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Speed Down ' - '", msgSpeedDownPosition, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Current Speed: " + speed.ToString(), msgCurrentSpeedPosition, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Highest Hit: " + _highestHit.ToString(), msgHighestHit, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
        }

        public void DrawShowCountBalls(SpriteBatch spriteBatch, int ballCount)
        {
            spriteBatch.DrawString(textFont, "Add Ball ' F2 '", msgAddBalls, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Delete Ball ' F3 '", msgDeleteBalls, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(textFont, "Current Balls: " + ballCount.ToString(), msgCurrentBalls, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
        }

        public void DrawShowSticks(SpriteBatch spriteBatch, bool active)
        {
            Color setColor;
            spriteBatch.DrawString(textFont, "Sticks: ", msgActiveStick, Color.White, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
            if (active)
            {
                setColor = Color.Green;
            }
            else
                setColor = Color.Red;
            spriteBatch.DrawString(textFont, active?"ON":"OFF", stateActiveStick, setColor, 0f, Vector2.Zero, 1f / 4 * scale, SpriteEffects.None, 0f);
        }
    }
}
