    using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TopDownShooter
{
    internal class ShipNonPlayer:GameBot
    {
        public override void Update(GameTime gameTime, ShipPlayer goodGuyShip)
        {
            float egt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime, goodGuyShip);
            switch (state)
            {
                case State.Alive:
                    accelerations = seek(goodGuyShip);
                    updateMovementVars(egt);
                    //just set the orientation to point to wherever we are going
                    orientation = (float)Math.Atan2((double)-velocity.X, (double)velocity.Y);
                    orientation %= 2 * MathHelper.Pi;
                    break;
            }
        }

        public override void Draw(SpriteBatch drawingSurface)
        {
            switch (state)
            {
                case State.Alive:
                    drawingSurface.Draw(texture, position, null, Color.Red, orientation, new Vector2(this.dimensions.X / 2, dimensions.Y / 2), 1, SpriteEffects.None, 0);
                    break;
                case State.Dying:
                    break;
                case State.Dead:
                    break;
            }
            foreach (ProjectileCannonBall projectile in projectiles)
            {
                projectile.Draw(drawingSurface);
            }
        }

        protected SteeringOutput seek(ShipPlayer goodGuyShip)
        {
            float timeToTarget = 1f;
            int satisfactionRadius = 200;

            SteeringOutput arriveSteering = new SteeringOutput();
            arriveSteering.linear = goodGuyShip.Position - Position;

            //have the NPS intelligently wrap around the world instead of turning around
            //when the player ship wraps around
            if (Math.Abs(arriveSteering.linear.X) > gameBoundingBox.Width / 2)
            {
                //if the PS is more than half a world away, point away from it
                //instead of towards it in order to chase it by wrapping around
                arriveSteering.linear.X *= -1;


                float Xadjustment = gameBoundingBox.Width / 2;
                //we want to remove the absolute value of Xadjustment, so change
                //its sign based on X's sign
                if (arriveSteering.linear.X < 0)
                {
                    Xadjustment *= -1;
                }
                //unless we apply the adjustment, the NPS will speed up
                //because it thinks that it is really far from the PS
                arriveSteering.linear = new Vector2((arriveSteering.linear.X - Xadjustment), arriveSteering.linear.Y);
            }
            else
            {
                //only stop moving if we aren't wrapping around. 
                //Otherwise the NPS just stops moving altogether sometimes
                if (arriveSteering.linear.Length() < satisfactionRadius)
                {
                    arriveSteering.linear.X = 0f;
                }
            }
            if (Math.Abs(arriveSteering.linear.Y) > gameBoundingBox.Height / 2)
            {
                arriveSteering.linear.Y *= -1;

                float Yadjustment = gameBoundingBox.Height / 2;
                if (arriveSteering.linear.Y < 0)
                {
                    Yadjustment *= -1;
                }
                //unless we apply the adjustment, the NPS will speed up
                //because it thinks that it is really far from the PS
                arriveSteering.linear = new Vector2(arriveSteering.linear.X, (arriveSteering.linear.Y - Yadjustment));
            }
            else
            {
                if (arriveSteering.linear.Length() < satisfactionRadius)
                {
                    arriveSteering.linear.Y = 0f;
                }
            }
            arriveSteering.linear /= timeToTarget;
            return arriveSteering;
        }
    }
}
