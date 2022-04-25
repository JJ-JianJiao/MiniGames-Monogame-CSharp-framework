using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MosquitoAttack
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
        public void Play(CelAnimationSequence CelAnimationSequence)
        {
            if (CelAnimationSequence == null)
                throw new Exception("CelAnimationPlayer.PlayAnimation received null CelAnimationSequence");

            // If this animation is already running, do not restart it...
            if (CelAnimationSequence != celAnimationSequence)
            {
                celAnimationSequence = CelAnimationSequence;
                celIndex = 0;
                celTimeElapsed = 0.0f;

                celSourceRectangle.X = 0;
                celSourceRectangle.Y = 0;
                celSourceRectangle.Width = celAnimationSequence.CelWidth;
                celSourceRectangle.Height = celAnimationSequence.CelHeight;
            }
        }

        /// <summary>
        /// Update the state of the CelAnimationPlayer.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime GameTime)
        {
            if (celAnimationSequence != null)
            {
                celTimeElapsed += (float)GameTime.ElapsedGameTime.TotalSeconds;

                if (celTimeElapsed >= celAnimationSequence.CelTime)
                {
                    celTimeElapsed -= celAnimationSequence.CelTime;

                    // Advance the frame index looping as appropriate...
                    celIndex = (celIndex + 1) % celAnimationSequence.CelCount;

                    celSourceRectangle.X = celIndex * celSourceRectangle.Width;
                }
            }
        }

        /// <summary>
        /// Draws the current cel of the animation.
        /// </summary>
        public void Draw(SpriteBatch SpriteBatch, Vector2 Position, SpriteEffects SpriteEffects)
        {
            if (celAnimationSequence != null)
                SpriteBatch.Draw(celAnimationSequence.Texture, Position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects, 0.0f);
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
