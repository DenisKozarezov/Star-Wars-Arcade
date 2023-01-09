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
}
