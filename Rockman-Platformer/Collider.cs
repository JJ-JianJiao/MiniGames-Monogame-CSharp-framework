using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab06_Platformer
{
    public abstract class Collider
    {
        //public enum ColliderType
        //{
        //    Left, Right, Top, Bottom
        //}
        //protected ColliderType colliderType;

        protected string textureString;
        protected Texture2D texture;
        protected Vector2 position;

        public Vector2 Position {

            set {
                position = value;
            }
        
        }
        protected Vector2 dimensions;
        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                                      (int)dimensions.X, (int)dimensions.Y);
            }
        }
        //public Collider(Vector2 position, Vector2 dimensions, string textureString, ColliderType colliderType)
        //{
        //    this.position = position;
        //    this.dimensions = dimensions;
        //    this.textureString = textureString;
        //    this.colliderType = colliderType;
        //}
        //public Collider() { 
        
        //}

        public Collider(Vector2 position, Vector2 dimensions, string textureString)
        {
            this.position = position;
            this.dimensions = dimensions;
            this.textureString = textureString;
        }
        internal void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(textureString);
        }
        internal void Update(GameTime gameTime)
        {
        }
        internal void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }

        internal abstract void ProcessCollisions(Player player);
        //internal void ProcessCollisions(Player player)
        //{

        //    if (BoundingBox.Intersects(player.BoundingBox))
        //    {
        //        switch (colliderType)
        //        {
        //            case ColliderType.Left:
        //                if (player.Velocity.X > 0) {
        //                    player.MoveHorizontally(0);
        //                }
        //                break;
        //            case ColliderType.Right:
        //                if (player.Velocity.X < 0)
        //                {
        //                    player.MoveHorizontally(0);
        //                }
        //                break;
        //            case ColliderType.Top:
        //                player.Land(BoundingBox);
        //                player.StandOn();
        //                break;
        //            case ColliderType.Bottom:
        //                if (player.Velocity.Y < 0)
        //                {
        //                    player.MoveVertically(0);
        //                }
        //                break;
        //        }
        //    }
        //}
    }
}