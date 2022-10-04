using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YellowJelloGames.YExtensions
{
    public static class Vector3Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XZ(this Vector3 v) => new(v.x, 0f, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XY(this Vector3 v) => new(v.x, v.y, 0f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YZ(this Vector3 v) => new(0, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithX(this Vector3 a, Vector3 b) => new(b.x, a.y, a.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithY(this Vector3 a, Vector3 b) => new(a.x, b.y, a.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithZ(this Vector3 a, Vector3 b) => new(a.x, a.y, b.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);

        /// <summary>
        /// Inverts a scale vector by dividing 1 by each component
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ScaleDown(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector3 ProjectOntoPlane(this Vector3 vector, Vector3 planeNormal)
        {
            return vector - Vector3.Dot(vector, planeNormal) * planeNormal;
        }

        public static Vector3 RelativeTo(this Vector3 direction, Transform transform)
        {
            return Quaternion.LookRotation(transform.forward) * direction;
        }

        public static Vector3 RelativeToPlanar(this Vector3 direction, Transform transform)
        {
            var forward = transform.forward;

            var up = Vector3.up;
            forward = forward.ProjectOntoPlane(up);

            if (forward.AlmostZero())
            {
                forward = Vector3.ProjectOnPlane(transform.up, up);
            }

            return Quaternion.LookRotation(forward) * direction;
        }

        public static bool AlmostZero(this Vector3 v)
        {
            return v.sqrMagnitude < FloatExtensions.EPSILON_SQR;
        }

        public static float DotNormalized(this Vector3 vector, Vector3 direction)
        {
            if (direction.sqrMagnitude > 1)
            {
                direction.Normalize();
            }

            return Vector3.Dot(vector, direction);
        }

        public static Vector3 RemoveDot(this Vector3 vector, Vector3 direction)
        {
            if (direction.sqrMagnitude > 1)
            {
                direction.Normalize();
            }

            float amount = Vector3.Dot(vector, direction);
            vector -= direction * amount;
            return vector;
        }

        public static float GetAnglesFromDir(this Vector3 position, Vector3 dir)
        {
            var forwardLimitPos = position + dir;
            float srcAngles =
                Mathf.Rad2Deg * Mathf.Atan2(forwardLimitPos.z - position.z, forwardLimitPos.x - position.x);

            return srcAngles;
        }

        public static Vector3 Avg(this IList<Vector3> source)
        {
            var avg = Vector3.zero;
            int total = source.Count;
            for (int i = 0; i < total; i++)
            {
                avg += source[i];
            }

            return avg / total;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrDistance(Vector3 x, Vector3 y)
        {
            return (y - x).sqrMagnitude;
        }
    }
}