using System;
using System.Collections.Generic;
using UnityEngine;



namespace Rune.Utils
{
    public readonly struct Math
    {
        public static float Square(float x)
        {
            return x * x;
        }

        public static double Square(double x)
        {
            return x * x;
        }

        public static float Floor(float x)
        {
            return Mathf.Floor(x);
        }

        public static double Floor(double x)
        {
            return System.Math.Floor(x);
        }

        public static float Ceil(float x)
        {
            return Mathf.Ceil(x);
        }

        public static double Ceil(double x)
        {
            return System.Math.Ceiling(x);
        }

        public static float Abs(float x)
        {
            return x < 0 ? -x : x;
        }

        public static double Abs(double x)
        {
            return System.Math.Abs(x);
        }

        public static Vector2 Abs2(Vector2 v)
        {
            return new Vector2(Abs(v.x), Abs(v.y));
        }

        public static Vector3 Abs3(Vector3 v)
        {
            return new Vector3(Abs(v.x), Abs(v.y), Abs(v.z));
        }

        public static Vector4 Abs4(Vector4 v)
        {
            return new Vector4(Abs(v.x), Abs(v.y), Abs(v.z), Abs(v.w));
        }

        public static float Clamp(float x, float min, float max)
        {
            return Maxf(Minf(x, max), min);
        }

        public static double Clamp(double x, double min, double max)
        {
            return System.Math.Clamp(x, min, max);
        }

        public static Vector2 Clamp2(Vector2 v, float min, float max)
        {
            return new Vector2(Clamp(v.x, min, max), Clamp(v.y, min, max));
        }

        public static Vector2 Clamp2(Vector2 v, Vector2 min, Vector2 max)
        {
            return new Vector2(Clamp(v.x, min.x, max.x), Clamp(v.y, min.y, max.y));
        }

        public static Vector3 Clamp3(Vector3 v, float min, float max)
        {
            return new Vector3(Clamp(v.x, min, max), Clamp(v.y, min, max), Clamp(v.z, min, max));
        }

        public static Vector3 Clamp3(Vector3 v, Vector3 min, Vector3 max)
        {
            return new Vector3(Clamp(v.x, min.x, max.x), Clamp(v.y, min.y, max.y), Clamp(v.z, min.z, max.z));
        }

        public static Vector4 Clamp4(Vector4 v, float min, float max)
        {
            return new Vector4(Clamp(v.x, min, max), Clamp(v.y, min, max), Clamp(v.z, min, max), Clamp(v.w, min, max));
        }

        public static Vector4 Clamp4(Vector4 v, Vector4 min, Vector4 max)
        {
            return new Vector4(Clamp(v.x, min.x, max.x), Clamp(v.y, min.y, max.y), Clamp(v.z, min.z, max.z), Clamp(v.w, min.w, max.w));
        }

        public static int PositiveModulo(int lhs, int rhs)
        {
            if (rhs <= 0)
            {
                throw new ArgumentOutOfRangeException($"{rhs} must be positive.");
            }

            int result = lhs % rhs;

            if (result < 0)
            {
                result += rhs;
            }

            return result;
        }

        public static bool IsPowerOfTwo(int x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }

        public static float Maxf(float x, float y)
        {
            return x > y ? x : y;
        }

        public static float MaxfAbs(float x, float y)
        {
            return Abs(x) > Abs(y) ? Abs(x) : Abs(y);
        }

        public static float Max2(Vector2 v)
        {
            return Maxf(v.x, v.y);
        }

        public static float Max2Abs(Vector2 v)
        {
            return MaxfAbs(v.x, v.y);
        }

        public static float Max3(Vector3 v)
        {
            return Maxf(Maxf(v.x, v.y), v.z);
        }

        public static float Max3Abs(Vector3 v)
        {
            return MaxfAbs(MaxfAbs(v.x, v.y), v.z);
        }

        public static float Max4(Vector4 v)
        {
            return Maxf(Maxf(Maxf(v.x, v.y), v.z), v.w);
        }

        public static float Max4Abs(Vector4 v)
        {
            return MaxfAbs(MaxfAbs(MaxfAbs(v.x, v.y), v.z), v.w);
        }

        public static float Minf(float a, float b)
        {
            return a > b ? b : a;
        }

        public static float MinfAbs(float a, float b)
        {
            return Abs(a) > Abs(b) ? Abs(b) : Abs(a);
        }

        public static float Min2(Vector2 v)
        {
            return Minf(v.x, v.y);
        }

        public static float Min2Abs(Vector2 v)
        {
            return MinfAbs(v.x, v.y);
        }

        public static float Min3(Vector3 v)
        {
            return Minf(Minf(v.x, v.y), v.z);
        }

        public static float Min3Abs(Vector3 v)
        {
            return MinfAbs(MinfAbs(v.x, v.y), v.z);
        }

        public static float Min4(Vector4 v)
        {
            return Minf(Minf(Minf(v.x, v.y), v.z), v.w);
        }

        public static float Min4Abs(Vector4 v)
        {
            return MinfAbs(MinfAbs(MinfAbs(v.x, v.y), v.z), v.w);
        }

        public static Vector2 Direction(Vector2 from, Vector2 to)
        {
            return (to - from).normalized;
        }

        public static Vector3 Direction(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }

        public static float Area(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            return UnityEngine.Vector3.Cross(v0 - v1, v0 - v2).magnitude * 0.5f;
        }

        public static float Cross2(Vector2 v0, Vector2 v1)
        {
            return v0.x * v1.y - v1.x * v0.y;
        }

        public static float ColorDistance(Color c0, Color c1)
        {
            return Mathf.Sqrt(Square(c0.r - c1.r) + Square(c0.g - c1.g) + Square(c0.b - c1.b));
        }

        public static float Angle2Rad(Vector2 from, Vector2 to)
        {
            return Mathf.Atan2(from.x * to.y - from.y * to.x, from.x * to.x + from.y * to.y);
        }

        public static float Angle2Deg(Vector2 from, Vector2 to)
        {
            return Angle2Rad(from, to) * Mathf.Rad2Deg;
        }

        public static float HorzAngle(Vector3 direction)
        {
            return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        }

        public static float VertAngle(Vector3 direction)
        {
            direction = direction.normalized;

            return Mathf.Atan2(direction.y, Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z)) * Mathf.Rad2Deg;
        }

        public static Quaternion QuatFromAngles(float horzAngle, float vertAngle)
        {
            Quaternion q0 = Quaternion.AngleAxis(horzAngle, UnityEngine.Vector3.up);
            Quaternion q1 = Quaternion.AngleAxis(vertAngle, q0 * UnityEngine.Vector3.left) * q0;

            return q1;
        }

        public static Vector2 Vector2(float x)
        {
            return new Vector2(x, x);
        }

        public static Vector2 Vector2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        public static Vector2 Vector2(Vector4 v4)
        {
            return new Vector2(v4.x, v4.y);
        }

        public static Vector3 Vector3(float x)
        {
            return new Vector3(x, x, x);
        }

        public static Vector3 Vector3(Vector2 v2, float z = 0)
        {
            return new Vector3(v2.x, v2.y, z);
        }

        public static Vector3 Vector3(float x, Vector2 v2)
        {
            return new Vector3(x, v2.x, v2.y);
        }

        public static Vector3 Vector3(Vector4 v4)
        {
            return new Vector3(v4.x, v4.y, v4.z);
        }

        public static Vector3 Vector3(Color c)
        {
            return new Vector3(c.r, c.g, c.b);
        }

        public static Vector3 Vector3(System.Numerics.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static Vector4 Vector4(float x)
        {
            return new Vector4(x, x, x, x);
        }

        public static Vector4 Vector4(Vector2 v2, float z = 0, float w = 0)
        {
            return new Vector4(v2.x, v2.y, z, w);
        }

        public static Vector4 Vector4(float x, Vector2 v2, float w = 0)
        {
            return new Vector4(x, v2.x, v2.y, w);
        }

        public static Vector4 Vector4(float x, float y, Vector4 v2)
        {
            return new Vector4(x, y, v2.z, v2.w);
        }

        public static Vector4 Vector4(Vector3 v3, float w = 0)
        {
            return new Vector4(v3.x, v3.y, v3.z, w);
        }

        public static Vector4 Vector4(float x, Vector3 v3)
        {
            return new Vector4(x, v3.x, v3.y, v3.z);
        }

        public static Vector2 RG(Color rgb)
        {
            return new Vector2(rgb.r, rgb.g);
        }

        public static Vector2 GB(Color rgb)
        {
            return new Vector2(rgb.g, rgb.b);
        }

        public static Vector2 RB(Color rgb)
        {
            return new Vector2(rgb.r, rgb.b);
        }

        public static Vector3 NormalLH(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            return UnityEngine.Vector3.Cross(v1 - v0, v2 - v0).normalized;
        }

        public static Vector3 NormalRH(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            return UnityEngine.Vector3.Cross(v2 - v0, v1 - v0).normalized;
        }

        public static Vector3 Center(List<Vector3> vs)
        {
            Vector3 min = Vector3(float.MaxValue);
            Vector3 max = Vector3(float.MinValue);

            foreach (var v in vs)
            {
                min = UnityEngine.Vector3.Min(min, v);
                max = UnityEngine.Vector3.Max(max, v);
            }

            return (min + max) * 0.5f;
        }

        public static Vector2 WithX(Vector2 v, float x)
        {
            return new(x, v.y);
        }

        public static Vector2 WithY(Vector2 v, float y)
        {
            return new(v.x, y);
        }

        public static Vector3 WithX(Vector3 v, float x)
        {
            return new(x, v.y, v.z);
        }

        public static Vector3 WithY(Vector3 v, float y)
        {
            return new(v.x, y, v.z);
        }

        public static Vector3 WithZ(Vector3 v, float z)
        {
            return new(v.x, v.y, z);
        }

        public static Vector4 WithX(Vector4 v, float x)
        {
            return new(x, v.y, v.z, v.w);
        }

        public static Vector4 WithY(Vector4 v, float y)
        {
            return new(v.x, y, v.z, v.w);
        }

        public static Vector4 WithZ(Vector4 v, float z)
        {
            return new(v.x, v.y, z, v.w);
        }

        public static Vector4 WithW(Vector4 v, float w)
        {
            return new(v.x, v.y, v.z, w);
        }

        public static Matrix4x4 Add4x4(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            Matrix4x4 m = new Matrix4x4();

            m.SetColumn(0, lhs.GetColumn(0) + rhs.GetColumn(0));
            m.SetColumn(1, lhs.GetColumn(1) + rhs.GetColumn(1));
            m.SetColumn(2, lhs.GetColumn(2) + rhs.GetColumn(2));
            m.SetColumn(3, lhs.GetColumn(3) + rhs.GetColumn(3));

            return m;
        }

        public static Matrix4x4 Mult4x4(Matrix4x4 lhs, float rhs)
        {
            Matrix4x4 m = new Matrix4x4();

            m.SetColumn(0, lhs.GetColumn(0) * rhs);
            m.SetColumn(1, lhs.GetColumn(1) * rhs);
            m.SetColumn(2, lhs.GetColumn(2) * rhs);
            m.SetColumn(3, lhs.GetColumn(3) * rhs);

            return m;
        }

        public static Matrix4x4 Transform(Vector3 rotation, Vector3 translation)
        {
            Matrix4x4 transform =
                Matrix4x4.Rotate(Quaternion.AngleAxis(rotation.z * Mathf.Rad2Deg, new Vector3(0, 0, 1))) *
                Matrix4x4.Rotate(Quaternion.AngleAxis(rotation.y * Mathf.Rad2Deg, new Vector3(0, 1, 0))) *
                Matrix4x4.Rotate(Quaternion.AngleAxis(rotation.x * Mathf.Rad2Deg, new Vector3(1, 0, 0)));

            transform.SetColumn(3, new Vector4(
                translation.x,
                translation.y,
                translation.z,
                1));

            return transform;
        }

        public static Vector3 MatrixToPosition(Matrix4x4 matrix)
        {
            Vector3 position;

            position.x = matrix.m03;
            position.y = matrix.m13;
            position.z = matrix.m23;

            return position;
        }

        public static Quaternion MatrixToRotation(Matrix4x4 matrix)
        {
            Vector3 forward;

            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;

            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        public static Vector3 MatrixToScale(Matrix4x4 matrix)
        {
            Vector3 scale;

            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;

            return scale;
        }

        public static Vector2 RotateDir2(Vector2 direction, float angle)
        {
            return Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward) * direction.normalized;
        }

        public static Vector2 RotateDir3(Vector3 direction, float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis) * direction.normalized;
        }

        public static Color RGBA(float r, Color c)
        {
            return new Color(r, c.r, c.g, c.b);
        }

        public static Color RGBA(Color c, float a)
        {
            return new Color(c.r, c.g, c.b, a);
        }

        public static Color Grayscale(float value)
        {
            return new Color(value, value, value, 1);
        }

        public static Color HexToColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color color))
            {
                return color;
            }
            else
            {
                return Color.white;
            }
        }

        public static string ColorToHex(Color color)
        {
            return "#" + ColorUtility.ToHtmlStringRGBA(color);
        }

        public static float Luminance(Color c)
        {
            return 0.2126f * c.r + 0.7152f * c.g + 0.0722f * c.b;
        }

        public static void MatrixToTransform(Matrix4x4 matrix, Transform transform)
        {
            transform.position = MatrixToPosition(matrix);
            transform.rotation = MatrixToRotation(matrix);
            transform.localScale = MatrixToScale(matrix);
        }

        public static T MaxValue<T>(List<T> list, Func<T, float> score)
        {
            return MaxValue(list, score, delegate { return true; });
        }

        public static T MaxValue<T>(List<T> list, Func<T, float> score, Func<T, bool> valid)
        {
            T maxT = default(T);

            float maxValue = float.MinValue;

            foreach (var t in list)
            {
                if (!valid(t))
                {
                    continue;
                }

                float s = score(t);

                if (s > maxValue)
                {
                    maxValue = s;
                    maxT = t;
                }
            }

            return maxT;
        }

        public static T MaxValue<T>(SortedSet<T> list, Func<T, float> score, Func<T, bool> valid)
        {
            T maxT = default(T);

            float maxValue = float.MinValue;

            foreach (var t in list)
            {
                if (!valid(t))
                {
                    continue;
                }

                float s = score(t);

                if (s > maxValue)
                {
                    maxValue = s;
                    maxT = t;
                }
            }

            return maxT;
        }

        public static int MaxValueIndex<T>(List<T> list, Func<int, float> score)
        {
            return MaxValueIndex(list, score, delegate { return true; });
        }

        public static int MaxValueIndex<T>(List<T> list, Func<int, float> score, Func<int, bool> valid)
        {
            int maxIndex = -1;

            float maxValue = float.MinValue;

            for (int i = 0, n = list.Count; i < n; i++)
            {
                if (!valid(i))
                {
                    continue;
                }

                float s = score(i);

                if (s > maxValue)
                {
                    maxValue = s;
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        public static bool IsIntersectLines2(Vector2 p0, Vector2 p1, Vector2 q0, Vector2 q1)
        {
            Vector2 r = p1 - p0;
            Vector2 s = q1 - q0;
            Vector2 pq = q0 - p0;

            float rCrossS = Cross2(r, s);
            float pqCrossR = Cross2(pq, r);
            float pqCrossS = Cross2(pq, s);

            Vector2 intersection;


            // Parallel
            if (rCrossS == 0)
            {
                intersection = UnityEngine.Vector2.zero;

                return false;
            }


            // Compute intersection
            float t = pqCrossS / rCrossS;
            float u = pqCrossR / rCrossS;


            // Check the intersection is in the lines
            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
            {
                intersection = p0 + t * r;

                return true;
            }


            // No intersection
            intersection = UnityEngine.Vector2.zero;

            return false;
        }

        public static bool IsIntersectLines2D(Vector2D p0, Vector2D p1, Vector2D q0, Vector2D q1)
        {
            Vector2D r = p1 - p0;
            Vector2D s = q1 - q0;
            Vector2D pq = q0 - p0;

            double rCrossS = Vector2D.Cross(r, s);
            double pqCrossR = Vector2D.Cross(pq, r);
            double pqCrossS = Vector2D.Cross(pq, s);

            Vector2D intersection;


            // Parallel
            if (rCrossS == 0)
            {
                intersection = Vector2D.zero;

                return false;
            }


            // Compute intersection
            double t = pqCrossS / rCrossS;
            double u = pqCrossR / rCrossS;


            // Check the intersection is in the lines
            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
            {
                intersection = p0 + t * r;

                return true;
            }


            // No intersection
            intersection = Vector2D.zero;

            return false;
        }

        public static float IntersectionRayLine2(Vector2 rayOrigin, Vector2 rayDirection, Vector2 p0, Vector2 p1)
        {
            Vector2 p = rayOrigin;
            Vector2 q = p0;
            Vector2 r = rayDirection;
            Vector2 s = p1 - p0;

            Vector2 pq = q - p;

            float c = Cross2(r, s);

            if (c == 0) return -1;

            float t = Cross2(pq, s) / c;
            float u = Cross2(pq, r) / c;

            return (t > 0 && u > 0 && u < 1) ? u : -1;
        }

        public static double IntersectionRayLine2D(Vector2D rayOrigin, Vector2D rayDirection, Vector2D p0, Vector2D p1)
        {
            Vector2D p = rayOrigin;
            Vector2D q = p0;
            Vector2D r = rayDirection;
            Vector2D s = p1 - p0;

            Vector2D pq = q - p;

            double c = Vector2D.Cross(r, s);

            if (c == 0) return -1;

            double t = Vector2D.Cross(pq, s) / c;
            double u = Vector2D.Cross(pq, r) / c;

            return (t > 0 && u > 0 && u < 1) ? u : -1;
        }

        public static float PerspectiveToOrthographicSize(float distance, float fov)
        {
            return distance * Mathf.Tan(fov * Mathf.Deg2Rad * 0.5f);
        }

        public static float OrthographicToPerspectiveDistance(float size, float fov)
        {
            return size / Mathf.Tan(fov * Mathf.Deg2Rad * 0.5f);
        }

        public static List<int> ConvexHull(List<Vector2> points)
        {
            /* Graham scan */



            int vib = 0;

            float my = float.MaxValue;

            for (int v = 0, vn = points.Count; v < vn; v++)
            {
                if (points[v].y < my)
                {
                    my = points[v].y;

                    vib = v;
                }
            }



            List<int> vis = new List<int>(points.Count);

            List<float> angles = new List<float>(points.Count);

            for (int p = 0, pn = points.Count; p < pn; p++)
            {
                vis.Add(p);

                if (p == vib)
                {
                    angles.Add(-0.000001f);
                }
                else
                {
                    Vector2 p0 = points[vib];
                    Vector2 p1 = points[p];

                    Vector2 d0 = new Vector2(1, 0);
                    Vector2 d1 = (p1 - p0).normalized;

                    angles.Add(Mathf.Acos(UnityEngine.Vector2.Dot(d0, d1)));
                }
            }

            vis.Sort(delegate (int i0, int i1) { return angles[i0].CompareTo(angles[i1]); });




            List<int> ch = new List<int>
            {
                vis[0],
                vis[1],
            };

            for (int k = 2, kn = points.Count; k <= kn; k++)
            {
                int v0 = ch[ch.Count - 2];
                int v1 = ch[ch.Count - 1];
                int v2 = vis[k % kn];

                Vector2 p0 = points[v0];
                Vector2 p1 = points[v1];
                Vector2 p2 = points[v2];

                Vector2 e0 = p1 - p0;
                Vector2 e1 = p2 - p0;

                Vector3 d0 = new Vector3(e0.x, e0.y, 0).normalized;
                Vector3 d1 = new Vector3(e1.x, e1.y, 0).normalized;

                if (UnityEngine.Vector3.Cross(d0, d1).z > 0)
                {
                    ch.Add(v2);
                }
                else
                {
                    ch[ch.Count - 1] = v2;
                }
            }



            return ch;
        }

        public static List<int> ConvexHullD(List<Vector2D> points)
        {
            /* Graham scan */



            int vib = 0;

            double my = double.MaxValue;

            for (int v = 0, vn = points.Count; v < vn; v++)
            {
                if (points[v].y < my)
                {
                    my = points[v].y;

                    vib = v;
                }
            }



            List<int> vis = new List<int>(points.Count);

            List<double> angles = new List<double>(points.Count);

            for (int p = 0, pn = points.Count; p < pn; p++)
            {
                vis.Add(p);

                if (p == vib)
                {
                    angles.Add(-0.000001);
                }
                else
                {
                    Vector2D p0 = points[vib];
                    Vector2D p1 = points[p];

                    Vector2D d0 = new Vector2D(1, 0);
                    Vector2D d1 = (p1 - p0).normalized;

                    angles.Add(System.Math.Acos(Vector2D.Dot(d0, d1)));
                }
            }

            vis.Sort(delegate (int i0, int i1) { return angles[i0].CompareTo(angles[i1]); });




            List<int> ch = new List<int>
            {
                vis[0],
                vis[1],
            };

            for (int k = 2, kn = points.Count; k <= kn; k++)
            {
                int v0 = ch[ch.Count - 2];
                int v1 = ch[ch.Count - 1];
                int v2 = vis[k % kn];

                Vector2D p0 = points[v0];
                Vector2D p1 = points[v1];
                Vector2D p2 = points[v2];

                Vector2D e0 = p1 - p0;
                Vector2D e1 = p2 - p0;

                Vector3D d0 = new Vector3D(e0.x, e0.y, 0).normalized;
                Vector3D d1 = new Vector3D(e1.x, e1.y, 0).normalized;

                if (Vector3D.Cross(d0, d1).z > 0)
                {
                    ch.Add(v2);
                }
                else
                {
                    ch[ch.Count - 1] = v2;
                }
            }



            return ch;
        }

        public static bool IsWithin(int value, int minInclusive, int maxExclusive)
        {
            return value >= minInclusive && value < maxExclusive;
        }

        public static bool IsWithin(int index, int count)
        {
            return IsWithin(index, 0, count);
        }

        public static bool IsWithin<T>(int index, List<T> list)
        {
            return IsWithin(index, list.Count);
        }

        public static bool IsZero(float value, float epsilon = 0.000001f)
        {
            return Mathf.Abs(value) < epsilon;
        }

        public static bool IsDegenerateTriangle(Vector3 v0, Vector3 v1, Vector3 v2, float epsilon = 0.000001f)
        {
            return Area(v0, v1, v2) < epsilon;
        }

        public static int SnapToInt(float x, int gap)
        {
            return Mathf.RoundToInt(x / gap) * gap;
        }

        public static Vector2Int SnapToInt(Vector2 v2, int gap)
        {
            return new(SnapToInt(v2.x, gap), SnapToInt(v2.y, gap));
        }

        public static Vector3Int SnapToInt(Vector3 v3, int gap)
        {
            return new(SnapToInt(v3.x, gap), SnapToInt(v3.y, gap), SnapToInt(v3.z, gap));
        }

        public static string ToMMSSFF(float seconds)
        {
            if (seconds < 0f) seconds = 0f;

            int cs = Mathf.FloorToInt(seconds * 100f);

            int mm = cs / 6000;
            int ss = (cs / 100) % 60;
            int ff = cs % 100;

            return $"{mm:00}:{ss:00}.{ff:00}";
        }



        public static readonly double radToDegD = 180.0 / System.Math.PI;
        public static readonly double degToRadD = System.Math.PI / 180.0;
    }
}