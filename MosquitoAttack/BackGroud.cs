using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class BackGroud
    {
        private int _width;
        private int _height;
        private Texture2D _texture;
        private int _scale;
        public Rectangle gameBorder;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
               
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
               
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public int Scale { get => _scale; set => _scale = value; }

        public BackGroud() { 
        
        }

        public BackGroud(int width, int height, int scale=1) {
            this._width = width;
            this._height = height;
            this._scale = scale;
        }

        public void Initial(int width, int height, int scale=1) {
            this._width = width;
            this._height = height;
            this._scale = scale;
            this.gameBorder = new Rectangle(0, 0, width, height);
        }

        public void LoadContent(Texture2D texture) {
            this._texture = texture;
        }

        public void Update(GameTime gameTime) { 
        
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Vector2.Zero,null, Color.White, 0.0f, Vector2.Zero, _scale, SpriteEffects.None, 0.0f);
        }
    }
}
