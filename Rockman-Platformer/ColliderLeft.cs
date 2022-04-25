﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06_Platformer
{
    internal class ColliderLeft:Collider
    {
        public ColliderLeft(Vector2 position, Vector2 dimensions, string textureString) : base(position, dimensions, textureString)
        {
        }

        internal override void ProcessCollisions(Player player)
        {

            if (BoundingBox.Intersects(player.BoundingBox))
            {
                if (player.Velocity.X > 0)
                {
                    player.MoveHorizontally(0);
                }
            }
        }
    }
}
