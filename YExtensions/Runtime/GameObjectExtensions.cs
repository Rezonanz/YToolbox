using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YellowJelloGames.YExtensions
{
    public static class GameObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(GameObject gameObject)
        {
#if UNITY_EDITOR
            Object.DestroyImmediate(gameObject);
#else
            Object.Destroy(gameObject);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyAndClear(this IList<GameObject> source)
        {
            for (int i = source.Count - 1; i >= 0; i--)
            {
                Destroy(source[i]);
            }
            
            source.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParent<T>(this GameObject context, out T target) where T : class
        {
            return context.transform.TryGetComponentInParent(out target);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInChildren<T>(this GameObject context, out T target) where T : class
        {
            return context.transform.TryGetComponentInChildren(out target);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInHierarchy<T>(this GameObject context, out T target) where T : class
        {
            return context.transform.TryGetComponentInHierarchy(out target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInChildrenTopLevel<T>(this GameObject context, out T component)
            where T : Component
        {
            return context.transform.TryGetComponentInChildrenTopLevel(out component);
        }

        public static LayerMask CollisionMask(this GameObject gameObject)
        {
            int mask = 0;

            for (int i = 0; i < 32; ++i)
            {
                if (!Physics.GetIgnoreLayerCollision(gameObject.layer, i))
                {
                    mask |= 1 << i;
                }
            }

            return mask;
        }
    }
}