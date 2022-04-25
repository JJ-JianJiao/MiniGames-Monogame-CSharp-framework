using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class ProjectileCannonBall:Projectile
    {
        new protected const float Speed = 500;
        protected const string Texture = "CannonBall";
        internal override void Initialize(Vector2 positions, Rectangle gameBoundingBox, Vector2 direction, Vector2 initialVelocity)
        {
            base.Initialize(positions, gameBoundingBox, direction, initialVelocity);
            velocity += direction * Speed;
        }

        internal override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(Texture);
        }
    }
}
