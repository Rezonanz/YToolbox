using System.Runtime.CompilerServices;

namespace YellowJelloGames.YExtensions
{
    public static class LongExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long AddPercent(this long value, float percent)
        {
            return value + value.CalculatePercent(percent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long CalculatePercent(this long value, float percent)
        {
            return (long)(value * (percent * 0.01f));
        }
    }
}