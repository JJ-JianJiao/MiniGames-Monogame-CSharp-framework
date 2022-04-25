using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Porn
{
    class Court
    {
        private int _courtWidth;
        private int _courtHeight;
        private float scale;
        private Texture2D courtBackground;
        private int _courtBoard;

        public int CourtWidth {
            get 
            {
                return _courtWidth;
            }
        }

        public int CourtBoard
        {
            get
            {
                return _courtBoard;
            }
        }

        public int CourHeight
        {
            get
            {
                return _courtHeight;
            }
            set
            {
                _courtHeight = value;
            }
        }

        public Court(int width, int height, int scale, int courtBoard) {
            _courtWidth = width;
            _courtHeight = height;
            this.scale = (float)scale;
            this._courtBoard = courtBoard;
        }

        public void LoadContent(Texture2D courtBackground) {
            this.courtBackground = courtBackground;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(courtBackground, Vector2.Zero, null, Color.MonoGameOrange, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
