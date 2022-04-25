using System;
using Microsoft.Xna.Framework.Graphics;

namespace Lab06_Platformer
{
    /// <summary>
    /// Represents a cel animated texture.
    /// </summary>
    public class CelAnimationSequence
    {
        // The texture containing the animation sequence...
        protected Texture2D texture;

        // The length of time a cel is displayed...
        protected float celTime;

        // Sequence metrics
        protected int celWidth;
        protected int celHeight;

        // Calculated count of cels in the sequence
        protected int celCount;

        /// <summary>
        /// Constructs a new CelAnimationSequence.
        /// </summary>        
        public CelAnimationSequence(Texture2D texture, int celWidth, float celTime)
        {
            this.texture = texture;
            this.celWidth = celWidth;
            this.celTime = celTime;

            celHeight = Texture.Height;
            celCount = Texture.Width / celWidth;
        }

        /// <summary>
        /// All frames in the animation arranged horizontally.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Duration of time to show each cel.
        /// </summary>
        public float CelTime
        {
            get { return celTime; }
        }

        /// <summary>
        /// Gets the number of cels in the animation.
        /// </summary>
        public int CelCount
        {
            get { return celCount; }
        }

        /// <summary>
        /// Gets the width of a frame in the animation.
        /// </summary>
        public int CelWidth
        {
            get { return celWidth; }
        }

        /// <summary>
        /// Gets the height of a frame in the animation.
        /// </summary>
        public int CelHeight
        {
            get { return celHeight; }
        }
    }
}
