using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class Fireball:AnimationObject
    {
        private Vector2 _direction;
        private float hitAffectTimer = 0.0f;
        private float hitAffectDuring = 0.5f;
        internal enum FireBallState { 
            Fyling,
            Affect,
            Dispose
        }

        internal FireBallState currentFireBallState = FireBallState.Fyling;
        public Vector2 Direction { get => _direction; set => _direction = value; }

        public Fireball() {
            _direction = new Vector2(0, 1);
        }

        public Fireball(int width, int height, int speed, int scale, Vector2 position, Rectangle gameBorder) : base(width, height, speed, scale, position, gameBorder)
        {
            _direction = new Vector2(0, 1);
        }

        public override void Update(GameTime gameTime)
        {
            switch (currentFireBallState)
            {
                case FireBallState.Fyling:
                    Position += Speed * _direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    base.Update(gameTime);
                    if (Position.Y >= (378 - Height) * Scale) {
                        currentFireBallState = FireBallState.Dispose;
                        Position = new Vector2(500, 500) * Scale;
                    }
                    break;
                case FireBallState.Affect:
                    hitAffectTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (hitAffectTimer > hitAffectDuring)
                    {
                        this.Position = new Vector2(500, 500) * Scale;
                        this.currentFireBallState = FireBallState.Dispose;
                    }
                    else {
                        base.Update(gameTime);
                    }
                    break;
                case FireBallState.Dispose:
                    break;
                default:
                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentFireBallState)
            {
                case FireBallState.Fyling:
                    base.Draw(spriteBatch);
                    break;
                case FireBallState.Affect:
                    base.Draw(spriteBatch);
                    break;
                case FireBallState.Dispose:
                    break;
                default:
                    break;
            }
        }

        internal void ChangeTexture(Texture2D texture, int width, int height, float celTime, float scale)
        {
            if (this.currentFireBallState == FireBallState.Fyling)
            {
                cannonSeq = new CelAnimationSequence(texture, width, celTime);
                this.Texture = texture;
                this.Width = width;
                this.Height = height;
                this.celTime = celTime;
                this.Scale = scale;
                this.currentFireBallState = FireBallState.Affect;
                hitAffectTimer = 0;
                celAnimationPlayer.Play(cannonSeq);
            }
        }
    }
}
