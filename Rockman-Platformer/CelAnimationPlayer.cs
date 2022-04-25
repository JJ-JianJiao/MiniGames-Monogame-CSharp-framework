using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab06_Platformer
{
    /// <summary>
    /// Controls playback of a CelAnimationSequence.
    /// </summary>
    public class CelAnimationPlayer
    {
        private CelAnimationSequence celAnimationSequence;
        private int celIndex;
        private float celTimeElapsed;
        private Rectangle celSourceRectangle;

        /// <summary>
        /// Begins or continues playback of a CelAnimationSequence.
        /// </summary>
        public void Play(CelAnimationSequence celAnimationSequence)
        {
            if (celAnimationSequence == null)
            {
                throw new Exception("CelAnimationPlayer.PlayAnimation received null CelAnimationSequence");
            }
            // If this animation is already running, do not restart it...
            if (celAnimationSequence != this.celAnimationSequence)
            {
                this.celAnimationSequence = celAnimationSequence;
                celIndex = 0;
                celTimeElapsed = 0.0f;

                celSourceRectangle.X = 0;
                celSourceRectangle.Y = 0;
                celSourceRectangle.Width = this.celAnimationSequence.CelWidth;
                celSourceRectangle.Height = this.celAnimationSequence.CelHeight;
            }
        }

        /// <summary>
        /// Update the state of the CelAnimationPlayer.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, int type = 0)
        {
            if (celAnimationSequence != null)
            {
                if (type == 0)
                {
                    celTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (celTimeElapsed >= celAnimationSequence.CelTime)
                    {
                        celTimeElapsed -= celAnimationSequence.CelTime;

                        // Advance the frame index looping as appropriate...
                        celIndex = (celIndex + 1) % celAnimationSequence.CelCount;

                        celSourceRectangle.X = celIndex * celSourceRectangle.Width;
                    }
                }
                else if (type == 1)
                {
                    celTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (celTimeElapsed >= celAnimationSequence.CelTime && celIndex < celAnimationSequence.CelCount-1)
                    {
                        celTimeElapsed -= celAnimationSequence.CelTime;

                        // Advance the frame index looping as appropriate...
                        celIndex = (celIndex + 1) % celAnimationSequence.CelCount;

                        celSourceRectangle.X = celIndex * celSourceRectangle.Width;
                    }
                }
            }
        }


        /// <summary>
        /// Draws the current cel of the animation.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (celAnimationSequence != null)
            {
                spriteBatch.Draw(celAnimationSequence.Texture, position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, spriteEffects, 0.0f);
            }
        }
        public void Draw(SpriteBatch SpriteBatch, Vector2 Position, float scale, SpriteEffects SpriteEffects, Color color)
        {
            if (celAnimationSequence != null)
                SpriteBatch.Draw(celAnimationSequence.Texture, Position, celSourceRectangle, color, 0.0f, Vector2.Zero, scale, SpriteEffects, 0.0f);
        }

        public void Draw(SpriteBatch SpriteBatch, Vector2 Position, float scale, SpriteEffects SpriteEffects)
        {
            if (celAnimationSequence != null)
                SpriteBatch.Draw(celAnimationSequence.Texture, Position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects, 0.0f);
        }
    }
}
