using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YellowJelloGames.YExtensions
{
    public static class Vector2Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WithX(this Vector2 a, Vector2 b) => new(b.x, a.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WithX(this Vector2 v, float x) => new(x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WithY(this Vector2 a, Vector2 b) => new(a.x, b.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WithY(this Vector2 v, float y) => new(v.x, y);
        
        public static Vector2 GetCentroid(IList<Vector2> points)
        {
            float area = 0f;
            float centerX = 0f;
            float centerY = 0f;

            for (int i = 0, j = points.Count - 1; i < points.Count; j = i++)
            {
                float temp = points[i].x * points[j].y - points[j].x * points[i].y;
                area += temp;

                centerX += (points[i].x + points[j].x) * temp;
                centerY += (points[i].y + points[j].y) * temp;
            }

            // Avoid division by zero
            if (area.AlmostZero()) return Vector2.zero;

            area *= 3f;
            return new Vector2(centerX / area, centerY / area);
        }
    }
}