using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab06_Platformer
{
    public class AnimationObject
    {
        private int _width;
        private int _height;
        private Texture2D _texture;
        private float _speed;
        private float _scale;
        private Vector2 _position;
        private SpriteEffects _spriteEffect;
        private Rectangle _gameBorder;
        protected float celTime = -1;
        public Texture2D ObjectFrameTexture { get; set; }
        public bool ObjectFrameActive { get; set; }

        public virtual Rectangle ObjectBorder
        {
            get
            {
                return new Rectangle((int)(Position.X), (int)(Position.Y), (int)(Width * Scale), (int)(Height * Scale));
            }
        }

        public CelAnimationPlayer celAnimationPlayer;
        public CelAnimationSequence cannonSeq;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }


        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Rectangle GameBorder { get => _gameBorder; set => _gameBorder = value; }
        public float Scale { get => _scale; set => _scale = value; }
        public SpriteEffects SpriteEffect { get => _spriteEffect; set => _spriteEffect = value; }

        public AnimationObject()
        {
            ObjectFrameActive = false;
        }

        public AnimationObject(int width, int height, float speed, int scale, Vector2 position, Rectangle gameBorder)
        {
            ObjectFrameActive = false;
            this._width = width;
            this._height = height;
            this._speed = speed;
            this._scale = scale;
            this._position = position;
            this._gameBorder = gameBorder;
        }

        public virtual void Initial(int width, int height, float speed, float scale, Vector2 position, Rectangle gameBorder)
        {
            this._width = width;
            this._height = height;
            this._speed = speed;
            this._scale = scale;
            this._position = position;
            this._gameBorder = gameBorder;
        }


        public virtual void LoadContent(Texture2D texture, float celTime)
        {
            this.celTime = celTime;
            this._texture = texture;
            if (celTime != -1)
            {
                cannonSeq = new CelAnimationSequence(Texture, Width, celTime);
                celAnimationPlayer = new CelAnimationPlayer();
                celAnimationPlayer.Play(cannonSeq);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.celTime != -1)
            {
                celAnimationPlayer.Update(gameTime);
            }
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draw Frame


            //spriteBatch.Draw()
            if (this.celTime != -1)
            {
                //spriteBatch.Draw(cannonSeq.Texture, ObjectBorder, Color.Black);
                celAnimationPlayer.Draw(spriteBatch, _position, Scale, _spriteEffect);
            }
            //spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, new Color(100, 100, 100, 0));
            if (this.ObjectFrameTexture != null && ObjectFrameActive)
            {
                spriteBatch.Draw(this.ObjectFrameTexture, ObjectBorder, Color.Red * 0.5f);
            }
        }

    }
}
