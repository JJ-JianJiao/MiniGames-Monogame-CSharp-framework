using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab06_Platformer
{
    public class Platform
    {
        protected Vector2 position;
        protected Vector2 dimensions;
        protected Texture2D _texture;

        protected bool isMove = false;
        protected int xMoveMax;
        protected float currentMoveMent = 0;
        protected int speed = 20;
        protected int yMoveMax;


        public Texture2D Texture {
            set {
                _texture = value;
            }
        }

        internal ColliderTop colliderTop;
        internal ColliderRight colliderRight;
        internal ColliderBottom colliderBottom;
        internal ColliderLeft colliderLeft;


        public Platform(Vector2 position, Vector2 dimension, bool isMove = false, int xMoveMax = 0, int yMoveMax = 0) {
            //colliderTop = new Collider(
            //    new Vector2(position.X + 3, position.Y),
            //    new Vector2(dimension.X - 6, 1),
            //    "ColliderTop", Collider.ColliderType.Top);
            //colliderRight = new Collider(
            //    new Vector2(position.X + dimension.X -1, position.Y +1),
            //    new Vector2(1,dimension.Y - 2),
            //    "ColliderRight", Collider.ColliderType.Right);
            //colliderBottom = new Collider(
            //    new Vector2(position.X + 3, position.Y + dimension.Y),
            //    new Vector2(dimension.X - 6, 1),
            //    "ColliderBottom", Collider.ColliderType.Bottom);
            //colliderLeft = new Collider(
            //    new Vector2(position.X, position.Y + 1),
            //    new Vector2(1, dimension.Y - 2),
            //    "ColliderTop", Collider.ColliderType.Left);
            colliderTop = new ColliderTop(
                new Vector2(position.X + 3, position.Y),
                new Vector2(dimension.X - 6, 1),
                "ColliderTop");
            colliderRight = new ColliderRight(
                new Vector2(position.X + dimension.X - 1, position.Y + 1),
                new Vector2(1, dimension.Y - 2),
                "ColliderRight");
            colliderBottom = new ColliderBottom(
                new Vector2(position.X + 3, position.Y + dimension.Y),
                new Vector2(dimension.X - 6, 1),
                "ColliderBottom");
            colliderLeft = new ColliderLeft(
                new Vector2(position.X, position.Y + 1),
                new Vector2(1, dimension.Y - 2),
                "ColliderTop");
            this.position = position;
            this.dimensions = dimension;
            //this.texture = texture;
            this.isMove = isMove;
            this.xMoveMax = xMoveMax;
            this.yMoveMax = yMoveMax;
        }

        internal void LoadContent(ContentManager content) {
            colliderTop.LoadContent(content);
            colliderRight.LoadContent(content);
            colliderBottom.LoadContent(content);
            colliderLeft.LoadContent(content);
        }


        internal void Update(GameTime gameTime)
        {
            if (this.isMove) {
                if (xMoveMax > 0 && yMoveMax == 0) {
                    currentMoveMent += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    colliderTop.Position = new Vector2(this.position.X + 3, this.position.Y);
                    colliderRight.Position = new Vector2(this.position.X + this.dimensions.X - 1, this.position.Y + 1);
                    colliderBottom.Position = new Vector2(this.position.X + 3, this.position.Y + this.dimensions.Y);
                    colliderLeft.Position = new Vector2(this.position.X, this.position.Y + 1);
                    if (currentMoveMent >= xMoveMax)
                    {
                        speed *= -1;
                    }
                    else if (currentMoveMent < 0) {
                        speed *= -1;
                    }
                }

                if (xMoveMax == 0 && yMoveMax > 0) {
                    currentMoveMent += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    colliderTop.Position = new Vector2(this.position.X + 3, this.position.Y);
                    colliderRight.Position = new Vector2(this.position.X + this.dimensions.X - 1, this.position.Y + 1);
                    colliderBottom.Position = new Vector2(this.position.X + 3, this.position.Y + this.dimensions.Y);
                    colliderLeft.Position = new Vector2(this.position.X, this.position.Y + 1);
                    if (currentMoveMent >= yMoveMax)
                    {
                        speed *= -1;
                    }
                    else if (currentMoveMent < 0)
                    {
                        speed *= -1;
                    }
                }
            
            }
        }



        internal void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, position, Color.White);
            //spriteBatch.Draw(_texture, position, new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y), Color.Red);
            spriteBatch.Draw(_texture, new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y), Color.Red);
            //colliderTop.Draw(spriteBatch);
            //colliderRight.Draw(spriteBatch);
            //colliderBottom.Draw(spriteBatch);
            //colliderLeft.Draw(spriteBatch);
        }

        internal void ProcessCollisions(Player player) {
            colliderTop.ProcessCollisions(player);
            colliderRight.ProcessCollisions(player);
            colliderBottom.ProcessCollisions(player);
            colliderLeft.ProcessCollisions(player);
        }
        internal void ProcessCollisionsTopAndBottom(Player player)
        {
            colliderTop.ProcessCollisions(player);
            colliderBottom.ProcessCollisions(player);
        }

        internal void ProcessCollisionsLeftAndRight(Player player)
        {
            colliderRight.ProcessCollisions(player);
            colliderLeft.ProcessCollisions(player);
        }
    }
}
