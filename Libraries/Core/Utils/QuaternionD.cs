// Title: QuaternionD.cs
// Version: 1.0.1
// Date: 2025-06-05



using System;



namespace Rune
{
    public struct QuaternionD
    {
        public QuaternionD(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

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

                    default: throw new IndexOutOfRangeException("Invalid index of QuaternionD.");
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

                    default: throw new IndexOutOfRangeException("Invalid index of QuaternionD.");
                }
            }
        }

        public static QuaternionD operator *(QuaternionD lhs, QuaternionD rhs)
        {
            return new QuaternionD(
                lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
                lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }

        public static Vector3D operator *(QuaternionD q, Vector3D p)
        {
            double x = q.x * 2.0;
            double y = q.y * 2.0;
            double z = q.z * 2.0;
            double xx = q.x * x;
            double yy = q.y * y;
            double zz = q.z * z;
            double xy = q.x * y;
            double xz = q.x * z;
            double yz = q.y * z;
            double wx = q.w * x;
            double wy = q.w * y;
            double wz = q.w * z;

            Vector3D result;

            result.x = (1.0 - (yy + zz)) * p.x + (xy - wz) * p.y + (xz + wy) * p.z;
            result.y = (xy + wz) * p.x + (1.0 - (xx + zz)) * p.y + (yz - wx) * p.z;
            result.z = (xz - wy) * p.x + (yz + wx) * p.y + (1.0 - (xx + yy)) * p.z;

            return result;
        }

        public static QuaternionD Normalize(QuaternionD q)
        {
            QuaternionD clone = new(q.x, q.y, q.z, q.w);

            clone.Normalize();

            return clone;
        }

        public static QuaternionD Inverse(QuaternionD q)
        {
            double sqrLength = q.sqrLength;

            if (sqrLength > epsilon)
            {
                double i = 1.0 / sqrLength;

                return new QuaternionD(q.x * -i, q.y * -i, q.z * -i, q.w * i);
            }
            else
            {
                return q;
            }
        }

        public static QuaternionD AngleAxis(double angle, Vector3D axis)
        {
            if (axis.sqrMagnitude == 0.0) return identity;

            QuaternionD result;

            double h = angle * Utils.Math.degToRadD * 0.5;
            double s = Math.Sin(h);
            double c = Math.Cos(h);

            axis.Normalize();

            result.x = axis.x * s;
            result.y = axis.y * s;
            result.z = axis.z * s;
            result.w = c;

            return result.normalized;
        }

        public static QuaternionD LookRotation(Vector3D forward, Vector3D up)
        {
            forward.Normalize();

            up.Normalize();

            Vector3D v0 = forward;
            Vector3D v1 = Vector3D.Cross(up, v0).normalized;
            Vector3D v2 = Vector3D.Cross(v0, v1).normalized;

            double m00 = v1.x;
            double m01 = v1.y;
            double m02 = v1.z;
            double m10 = v2.x;
            double m11 = v2.y;
            double m12 = v2.z;
            double m20 = v0.x;
            double m21 = v0.y;
            double m22 = v0.z;

            double num8 = m00 + m11 + m22;

            QuaternionD quaternion = new();

            if (num8 > 0.0)
            {
                double num = Math.Sqrt(num8 + 1.0);

                quaternion.w = num * 0.5;

                num = 0.5 / num;

                quaternion.x = (m12 - m21) * num;
                quaternion.y = (m20 - m02) * num;
                quaternion.z = (m01 - m10) * num;

                return quaternion.normalized;
            }
            if (m00 >= m11 && m00 >= m22)
            {
                double num7 = Math.Sqrt(1.0 + m00 - m11 - m22);
                double num4 = 0.5 / num7;

                quaternion.x = 0.5 * num7;
                quaternion.y = (m01 + m10) * num4;
                quaternion.z = (m02 + m20) * num4;
                quaternion.w = (m12 - m21) * num4;

                return quaternion.normalized;
            }
            if (m11 > m22)
            {
                double num6 = Math.Sqrt(1.0 + m11 - m00 - m22);
                double num3 = 0.5 / num6;

                quaternion.x = (m10 + m01) * num3;
                quaternion.y = 0.5 * num6;
                quaternion.z = (m21 + m12) * num3;
                quaternion.w = (m20 - m02) * num3;

                return quaternion.normalized;
            }

            double num5 = Math.Sqrt(1.0 + m22 - m00 - m11);
            double num2 = 0.5 / num5;

            quaternion.x = (m20 + m02) * num2;
            quaternion.y = (m21 + m12) * num2;
            quaternion.z = 0.5 * num5;
            quaternion.w = (m01 - m10) * num2;

            return quaternion.normalized;
        }

        public void Normalize()
        {
            double len = length;

            if (len > epsilon)
            {
                double scale = 1.0 / length;

                x *= scale;
                y *= scale;
                z *= scale;
                w *= scale;
            }
            else
            {
                this = identity;
            }
        }

        public static QuaternionD identity
        {
            get => new QuaternionD(0.0, 0.0, 0.0, 1.0);
        }

        public double length
        {
            get => Math.Sqrt(x * x + y * y + z * z + w * w);
        }

        public double sqrLength
        {
            get => x * x + y * y + z * z + w * w;
        }

        public QuaternionD normalized
        {
            get => Normalize(this);
        }



        public const double epsilon = 0.000001;

        public double x, y, z, w;
    }
}