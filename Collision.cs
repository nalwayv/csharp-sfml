using System;
using SFML.System;

namespace csharp_sfml
{
    public class Collision
    {
        public Collision()
        {

        }

        public bool CollideBox(GameData self, GameData other)
        {
            if (!self.IsActive || !other.IsActive)
            {
                return false;
            }

            if (self.Right * self.Scale >= other.Left * other.Scale &&
               self.Left * self.Scale <= other.Right * other.Scale &&
               self.Bottom * self.Scale >= other.Top * other.Scale &&
               self.Top * self.Scale <= other.Bottom * other.Scale)
            {
                return true;
            }

            return false;
        }

        public bool CollideCircle(GameData self, GameData other)
        {
            if (!self.IsActive || !other.IsActive)
            {
                return false;
            }

            var sumR = self.Radius + other.Radius;
            var dis = self.Center - other.Center;

            if (MathHelper.LengthSqu(dis) <= (sumR * sumR))
            {
                return true;
            }

            return false;
        }
    }
}