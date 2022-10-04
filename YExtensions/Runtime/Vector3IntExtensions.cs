using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YellowJelloGames.YExtensions
{
    public static class Vector3IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int XZ(this Vector3Int v) => new(v.x, 0, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int XY(this Vector3Int v) => new(v.x, v.y, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int YZ(this Vector3Int v) => new(0, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int WithX(this Vector3Int a, Vector3Int b) => new(b.x, a.y, a.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int WithX(this Vector3Int v, int x) => new(x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int WithY(this Vector3Int a, Vector3Int b) => new(a.x, b.y, a.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int WithY(this Vector3Int v, int y) => new(v.x, y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int WithZ(this Vector3Int a, Vector3Int b) => new(a.x, a.y, b.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int WithZ(this Vector3Int v, int z) => new(v.x, v.y, z);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Shift(this Vector3Int v, int x, int y, int z = 0) => new(v.x + x, v.y + y, v.z + z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToVector3(this Vector3Int v) => new Vector3(v.x, v.y, v.z);

        public static void FlipHorizontal(this IList<Vector3Int> source, IList<Vector3Int> targetOut)
        {
            for (int i = 0, iMax = source.Count; i < iMax; ++i)
            {
                targetOut[i] = new Vector3Int(-1 * source[i].x, source[i].y, source[i].z);
            }
        }

        public static void FlipVertical(this IList<Vector3Int> source, IList<Vector3Int> targetOut)
        {
            for (int i = 0, iMax = source.Count; i < iMax; ++i)
            {
                targetOut[i] = new Vector3Int(source[i].x, -1 * source[i].y, source[i].z);
            }
        }
    }
}