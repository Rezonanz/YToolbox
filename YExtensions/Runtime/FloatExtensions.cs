using System.Runtime.CompilerServices;
using UnityEngine;

namespace YellowJelloGames.YExtensions
{
    public static class FloatExtensions
    {
        public const float EPSILON = 0.0001f;
        public const float EPSILON_SQR = EPSILON * EPSILON;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AddPercent(this float value, float percent)
        {
            return value + value.CalculatePercent(percent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculatePercent(this float value, float percent)
        {
            return value * (percent * 0.01f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Remap(this float value, float in1, float in2, float out1, float out2)
        {
            return out1 + (value - in1) * (out2 - out1) / (in2 - in1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AlmostZero(this float v)
        {
            return Mathf.Abs(v) < EPSILON;
        }
    }
}