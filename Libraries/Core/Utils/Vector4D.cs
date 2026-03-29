// Title: Vector4D.cs
// Version: 1.0.1
// Date: 2025-06-05



using System;
using UnityEngine;



namespace Rune
{
    public struct Vector4D
    {
        public double x, y, z, w;

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new IndexOutOfRangeException("Invalid index of Vector4d.");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default: throw new IndexOutOfRangeException("Invalid index of Vector4d.");
                }
            }
        }

        public Vector4D(double x)
        {
            this.x = x;
            this.y = x;
            this.z = x;
            this.w = x;
        }

        public Vector4D(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4D(Vector4 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            w = v.w;
        }

        public Vector4 ToVector4()
        {
            return new((float)x, (float)y, (float)z, (float)w);
        }

        public static Vector4D zero
        {
            get => new(0.0, 0.0, 0.0, 0.0);
        }
    }
}