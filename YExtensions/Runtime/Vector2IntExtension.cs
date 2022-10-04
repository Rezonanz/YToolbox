using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YellowJelloGames.YExtensions
{
    public static class Vector2IntExtension
    {
        public static Vector2Int Normalized(this Vector2Int vector)
        {
            int magnitude = Mathf.RoundToInt(vector.magnitude);
            if (magnitude <= 0)
            {
                return Vector2Int.zero;
            }

            var newVector = new Vector2Int(vector.x, vector.y) / magnitude;
            if (newVector.x != 0 || newVector.y != 0)
            {
                return newVector;
            }

            return new Vector2Int(Math.Sign(vector.x), Math.Sign(vector.y));
        }

        public static int ManhattanDistance(Vector2Int a, Vector2Int b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public static double EuclideanDistance(Vector2Int a, Vector2Int b)
        {
            return Math.Sqrt((int)(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2)));
        }
        
        public static int MaxDistance(Vector2Int a, Vector2Int b)
        {
            return Math.Max(Math.Abs(a.x - b.x), Math.Abs(a.y - b.y));
        }

        public static Vector2Int GetRandomInsideRange(this Vector2Int center, float minRadius, float maxRadius)
        {
            return GetRandomInsideRange(center, Random.Range(minRadius, maxRadius));
        }

        public static Vector2Int GetRandomInsideRange(this Vector2Int center, float radius)
        {
            float angle = 2 * Mathf.PI * Random.value;
            
            return new Vector2Int(
                Mathf.RoundToInt(center.x + radius * Mathf.Cos(angle)),
                Mathf.RoundToInt(center.y + radius * Mathf.Sin(angle))
            );
        }

        public static bool IsInsideCircle(this Vector2Int point, Vector2Int center, float radius)
        {
            return (point.x - center.x) * (point.x - center.x) + (point.y - center.y) * (point.y - center.y) <=
                   radius * radius;
        }
        
        public static void GetAdjacentVectorsNonAlloc(this Vector2Int v, IList<Vector2Int> positions)
        {
            positions.Add(new Vector2Int(v.x, v.y + 1)); // ↑
            positions.Add(new Vector2Int(v.x - 1, v.y)); // ←
            positions.Add(new Vector2Int(v.x + 1, v.y)); // → 
            positions.Add(new Vector2Int(v.x, v.y - 1)); // ↓
        }
        
        public static void GetAdjacentAndDiagonalVectorsNonAlloc(this Vector2Int v, IList<Vector2Int> positions)
        {
            positions.Add(new Vector2Int(v.x - 1, v.y + 1)); // ↖
            positions.Add(new Vector2Int(v.x, v.y + 1));       // ↑
            positions.Add(new Vector2Int(v.x + 1, v.y + 1)); // ↗
            positions.Add(new Vector2Int(v.x - 1, v.y));       // ←
            positions.Add(new Vector2Int(v.x + 1, v.y));       // →
            positions.Add(new Vector2Int(v.x - 1, v.y - 1)); // ↙
            positions.Add(new Vector2Int(v.x, v.y - 1));       // ↓
            positions.Add(new Vector2Int(v.x + 1, v.y - 1)); // ↘
        }
        
        public static void GetAdjacentAndDiagonalVectorsNonAlloc(IList<Vector2Int> positions)
        {
            positions.Add(new Vector2Int(-1, 1)); // ↖
            positions.Add(new Vector2Int(0, 1)); // ↑
            positions.Add(new Vector2Int(1, 1)); // ↗
            positions.Add(new Vector2Int(-1, 0)); // ←
            positions.Add(new Vector2Int(1, 0)); // →
            positions.Add(new Vector2Int(-1, -1)); // ↙
            positions.Add(new Vector2Int(0, -1)); // ↓
            positions.Add(new Vector2Int(1, -1)); // ↘
        }

        public static Vector2Int Rotate(this Vector2Int v, float angle)
        {
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            return new Vector2Int(
                Mathf.RoundToInt(v.x * cos - v.y * sin),
                Mathf.RoundToInt(v.x * sin + v.y * cos)
            );
        }
    }
}