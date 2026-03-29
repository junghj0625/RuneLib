// Title: Vector2D.cs
// Version: 1.0.1
// Date: 2025-06-05



using System;
using UnityEngine;



namespace Rune
{
    public struct Vector2D
    {
        public const double epsilon = 0.00001;

        public double x, y;

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException("Invalid index of Vector3D.");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException("Invalid index of Vector2D.");
                }
            }
        }

        public Vector2D(double x)
        {
            this.x = x;
            this.y = x;
        }

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2D(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        public Vector2D(Vector3D v3)
        {
            this.x = v3.x;
            this.y = v3.y;
        }

        public Vector2D(Vector4D v4)
        {
            this.x = v4.x;
            this.y = v4.y;
        }

        public static Vector2D operator +(Vector2D lhs, Vector2D rhs)
        {
            return new(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2D operator -(Vector2D lhs, Vector2D rhs)
        {
            return new(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2D operator *(Vector2D lhs, double rhs)
        {
            return new(lhs.x * rhs, lhs.y * rhs);
        }

        public static Vector2D operator *(double lhs, Vector2D rhs)
        {
            return new(lhs * rhs.x, lhs * rhs.y);
        }

        public static Vector2D operator /(Vector2D lhs, double rhs)
        {
            return new(lhs.x / rhs, lhs.y / rhs);
        }

        public Vector2 ToVector2()
        {
            return new((float)x, (float)y);
        }

        public static Vector2D Normalize(Vector2D v)
        {
            double magnitude = v.magnitude;

            if (magnitude > epsilon)
            {
                return v / magnitude;
            }
            else
            {
                return zero;
            }
        }

        public void Normalize()
        {
            this = Normalize(this);
        }

        public static double Dot(Vector2D lhs, Vector2D rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static double Cross(Vector2D lhs, Vector2D rhs)
        {
            return lhs.x * rhs.y - rhs.x * lhs.y;
        }

        public double magnitude
        {
            get => Math.Sqrt(x * x + y * y);
        }

        public double sqrMagnitude
        {
            get => x * x + y * y;
        }

        public static Vector2D zero
        {
            get => new(0.0, 0.0);
        }

        public Vector2D normalized
        {
            get => Normalize(this);
        }
    }
}