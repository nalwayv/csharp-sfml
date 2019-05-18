using System;
using SFML.System;

namespace csharp_sfml
{
    public class MathHelper
    {
        public const float PIOver2 = (MathF.PI / 2.0f);
        public const float PIOver4 = (MathF.PI / 4.0f);
        public const float TwoPI = (MathF.PI * 2.0f);

        public static float Clamp(float value, float min, float max)
        {
            return MathF.Min(MathF.Max(value, min), max);
        }

        public static float Lerp(float norm, float min, float max)
        {
            return (max - min) * norm + min;
        }

        public static float ToDegrees(float radians)
        {
            var val = 57.295779513082320876798154814105f;
            return radians * val;
        }

        public static float ToRadians(float degrees)
        {
            var val = 0.017453292519943295769236907684886f;
            return degrees * val;
        }

        public static float Normalization(float value, float minValue, float maxValue)
        {
            return (value - minValue) / (maxValue - minValue);
        }

        // --- VECTOR

        public static float LengthSqrt(Vector2f vec)
        {
            return MathF.Sqrt((vec.X * vec.X) + (vec.Y * vec.Y));
        }

        public static float LengthSqu(Vector2f vec)
        {
            return (vec.X * vec.X) + (vec.Y * vec.Y);
        }

        public static float DistanceSquared(Vector2f v1, Vector2f v2)
        {
            float a = v2.X - v1.X;
            float b = v2.Y - v1.Y;
            return MathF.Sqrt((a * a) + (b * b));
        }

        public static float Distance(Vector2f v1, Vector2f v2)
        {
            float a = v2.X - v1.X;
            float b = v2.Y - v1.Y;
            return (a * a) + (b * b);
        }

        public static Vector2f Normalize(Vector2f v)
        {
            var len = LengthSqrt(v);

            if (len > 0)
            {
                v *= 1.0f / len;
            }

            return v;
        }

        public static float Dot(Vector2f v1, Vector2f v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

        public static float Cross(Vector2f v1, Vector2f v2)
        {
            return (v1.X * v2.Y) - (v1.Y * v2.X);
        }

        public static Vector2f Projection(Vector2f v1, Vector2f v2)
        {
            return new Vector2f (
                (Dot(v1, v2) / LengthSqu(v2)) * v2.X,
                (Dot(v1, v2) / LengthSqu(v2)) * v2.Y);
        }

        public static Vector2f EdgeNormal(Vector2f v, bool leftHandNormal = false)
        {
            // leftHandNormal
            if (leftHandNormal)
            {
                return new Vector2f(v.Y, -v.X);
            }

            // rightHandNormal
            return new Vector2f(-v.Y, v.X);
        }
    }
}