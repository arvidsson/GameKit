namespace UnityEngine
{
    public readonly struct SortingLayerSnapshot
    {
        public readonly string Name;
        public readonly int Order;

        public SortingLayerSnapshot(string name, int order)
        {
            Name = name;
            Order = order;
        }
    }

    public static class SpriteRendererExtensions
    {
        /// <summary>
        /// Takes a snapshot of the sprite renderer's sorting layer and sorting order.
        /// </summary>
        public static SortingLayerSnapshot TakeSnapshot(this SpriteRenderer renderer)
        {
            return new SortingLayerSnapshot(
                renderer.sortingLayerName,
                renderer.sortingOrder
            );
        }

        /// <summary>
        /// Restores the sprite renderer's sorting layer and sorting order to the values stored in the snapshot.
        /// </summary>
        public static void Restore(this SpriteRenderer renderer, SortingLayerSnapshot snapshot)
        {
            renderer.sortingLayerName = snapshot.Name;
            renderer.sortingOrder = snapshot.Order;
        }
    }
}
