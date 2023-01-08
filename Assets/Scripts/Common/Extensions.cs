using UnityEngine;

namespace Core
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this T[] collection)
        {
            if (collection.Length == 1) return collection[0];

            return collection[UnityEngine.Random.Range(0, collection.Length)];
        }
    }
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float alpha)
        {
            Color result = color;
            result.a = alpha;
            return result;
        }
    }
}
