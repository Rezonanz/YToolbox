using System;

namespace YellowJelloGames.YExtensions
{
    public static class EnumExtensions
    {
        public static T[] GetValues<T>() where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}