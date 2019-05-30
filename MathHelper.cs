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

        public static float Angle(Vector2f vec)
        {
            // cos-1(v.x / ||v||)
            // var angle = MathF.Acos(vec.X/ LengthSqrt(vec));

            // sin-1(v.y / ||v||)
            // var angle = MathF.Asin(vec.Y/ LengthSqrt(vec));

            // tan-1(v.y / v.x)
            return MathF.Atan2(vec.Y, vec.X);
        }

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

        public static Vector3f Cross(Vector2f a, Vector2f b)
        {
            var a3 = new Vector3f(a.X, a.Y, 0);
            var b3 = new Vector3f(b.X, b.Y, 0);

            var cx = a3.Y * b3.Z - a3.Z * b3.Y;
            var cy = a3.Z * b3.X - a3.X * b3.Z;
            var cz = a3.X * b3.Y - a3.Y * b3.X;

            return new Vector3f(cx, cy, cz);
        }

        public static Vector2f Projection(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(
                (Dot(v1, v2) / LengthSqu(v2)) * v2.X,
                (Dot(v1, v2) / LengthSqu(v2)) * v2.Y);
        }

        public static Vector2f EdgeNormal(Vector2f v, bool leftHandNormal = false)
        {
            // left Hand Normal
            if (leftHandNormal)
            {
                return new Vector2f(v.Y, -v.X);
            }

            // right Hand Normal
            return new Vector2f(-v.Y, v.X);
        }

        public static float AngleBetweenVectors(Vector2f a, Vector2f b)
        {
            //               ^ ^
            // angle = cos-1(a.b) 
            return MathF.Acos(Dot(Normalize(a), Normalize(b))); //radians

            // angle  = cos-1(a.b / ||A|| * ||B||)
            // return MathF.Acos(Dot(a, b) / LengthSqrt(a) * LengthSqrt(b));
        }

        public static Vector2f RotateVector2f(Vector2f v, float radians, bool clockwise = false)
        {
            if (clockwise)
            {
                radians = TwoPI - radians;
            }

            var xVal = v.X * MathF.Cos(radians) - v.Y * MathF.Sin(radians);
            var yVal = v.X * MathF.Sin(radians) + v.Y * MathF.Cos(radians);

            return new Vector2f(xVal, yVal);
        }

        public static Vector2f LookAt2D(Vector2f position, Vector2f focus)
        {
            var clockwise = false;
            var dirN = Normalize(focus - position);
            var ang = AngleBetweenVectors(position, dirN);

            if (Cross(position, dirN).Z < 0)
            {
                clockwise = true;
            }

            var newDir = RotateVector2f(position, ang, clockwise);

            return newDir; 
        }
    }
}