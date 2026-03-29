// Title: Vector3D.cs
// Version: 1.0.1
// Date: 2025-06-05



using System;
using UnityEngine;



namespace Rune
{
    public struct Vector3D
    {
        public const double epsilon = 0.00001;

        public double x, y, z;

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new IndexOutOfRangeException("Invalid index of Vector3D.");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new IndexOutOfRangeException("Invalid index of Vector3D.");
                }
            }
        }

        public Vector3D(double x)
        {
            this.x = x;
            this.y = x;
            this.z = x;
        }

        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3D(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3D(Vector4D v4)
        {
            this.x = v4.x;
            this.y = v4.y;
            this.z = v4.z;
        }

        public static Vector3D operator +(Vector3D lhs, Vector3D rhs)
        {
            return new(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3D operator -(Vector3D lhs, Vector3D rhs)
        {
            return new(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector3D operator *(Vector3D lhs, double rhs)
        {
            return new(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        }

        public static Vector3D operator *(double lhs, Vector3D rhs)
        {
            return new(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
        }

        public static Vector3D operator /(Vector3D lhs, double rhs)
        {
            return new(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        }

        public Vector3 ToVector3()
        {
            return new((float)x, (float)y, (float)z);
        }

        public static Vector3D Normalize(Vector3D v)
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

        public static double Dot(Vector3D lhs, Vector3D rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static Vector3D Cross(Vector3D lhs, Vector3D rhs)
        {
            return new(
                lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public static Vector3D Min(Vector3D lhs, Vector3D rhs)
        {
            return new(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y), Math.Min(lhs.z, rhs.z));
        }

        public static Vector3D Max(Vector3D lhs, Vector3D rhs)
        {
            return new(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y), Math.Max(lhs.z, rhs.z));
        }

        public static double Angle(Vector3D from, Vector3D to)
        {
            double num = Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);

            if (num < 1E-15)
            {
                return 0.0;
            }

            double num2 = Math.Clamp(Dot(from, to) / num, -1.0, 1.0);

            return Math.Acos(num2) * 57.29578;
        }

        public double magnitude
        {
            get => Math.Sqrt(x * x + y * y + z * z);
        }

        public double sqrMagnitude
        {
            get => x * x + y * y + z * z;
        }

        public static Vector3D zero
        {
            get => new(0.0, 0.0, 0.0);
        }

        public static Vector3D forward
        {
            get => new(0.0, 0.0, 1.0);
        }

        public static Vector3D back
        {
            get => new(0.0, 0.0, -1.0);
        }

        public static Vector3D up
        {
            get => new(0.0, 1.0, 0.0);
        }

        public static Vector3D down
        {
            get => new(0.0, -1.0, 0.0);
        }

        public static Vector3D left
        {
            get => new(-1.0, 0.0, 0.0);
        }

        public static Vector3D right
        {
            get => new(1.0, 0.0, 0.0);
        }

        public Vector3D normalized
        {
            get => Normalize(this);
        }
    }
}