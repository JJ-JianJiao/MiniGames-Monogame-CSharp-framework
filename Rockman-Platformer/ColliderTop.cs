using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06_Platformer
{
    internal class ColliderTop : Collider
    {
        //public ColliderTop() {

        //}

        public ColliderTop(Vector2 position, Vector2 dimensions, string textureString) :base(position, dimensions, textureString) { 
        
        } 

        internal override void ProcessCollisions(Player player)
        {

            if (BoundingBox.Intersects(player.BoundingBox))
            {               
                player.Land(BoundingBox);
                player.StandOn();
            }
        }
    }
}
