using System.Collections.Generic;
using UnityEngine;

namespace YellowJelloGames.YExtensions
{
    public static class TransformExtensions
    {
        public static void Clear(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                if (Application.isPlaying)
                {
                    Object.Destroy(transform.GetChild(i).gameObject);
                }
                else
                {
                    Object.DestroyImmediate(transform.GetChild(i).gameObject);
                }
            }
        }
        
        public static void ChangeLayersRecursively(this Transform transform, string layerName)
        {
            transform.gameObject.layer = LayerMask.NameToLayer(layerName);
            foreach (Transform child in transform)
            {
                child.ChangeLayersRecursively(layerName);
            }
        }

        public static void ChangeLayersRecursively(this Transform transform, int layerIndex)
        {
            transform.gameObject.layer = layerIndex;
            foreach (Transform child in transform)
            {
                child.ChangeLayersRecursively(layerIndex);
            }
        }
        
        public static bool TryGetComponentInParent<T>(this Transform context, out T target) where T : class
        {
            for (var ancestor = context; ancestor; ancestor = ancestor.parent)
            {
                if (ancestor.gameObject.activeSelf && ancestor.TryGetComponent(out target))
                {
                    return true;
                }
            }

            target = default;
            return false;
        }
        
        public static bool TryGetComponentInChildren<T>(this Transform context, out T target,
            bool includeInactive = false) where T : class
        {
            if (context.TryGetComponent(out target)) return true;

            for (int i = 0, iMax = context.childCount; i < iMax; ++i)
            {
                var child = context.GetChild(i);
                if ((includeInactive || child.gameObject.activeSelf) && child.TryGetComponentInChildren(out target))
                {
                    return true;
                }
            }

            return false;
        }
        
        public static bool TryGetComponentInHierarchy<T>(this Transform context, out T target,
            bool includeInactive = false) where T : class
        {
            for (var ancestor = context; ancestor; ancestor = ancestor.parent)
            {
                if (ancestor.gameObject.activeSelf && ancestor.TryGetComponent(out target))
                {
                    return true;
                }
            }

            for (int i = 0, iMax = context.childCount; i < iMax; ++i)
            {
                var child = context.GetChild(i);
                if ((includeInactive || child.gameObject.activeSelf) && child.TryGetComponentInChildren(out target))
                {
                    return true;
                }
            }

            target = default;
            return false;
        }

        public static bool TryGetComponentInChildrenTopLevel<T>(this Transform context, out T found,
            bool includeInactive = false) where T : Component
        {
            for (int i = 0, iMax = context.childCount; i < iMax; ++i)
            {
                var child = context.GetChild(i);
                if ((includeInactive || child.gameObject.activeSelf) && child.TryGetComponent(out found))
                {
                    return true;
                }
            }

            found = default;
            return false;
        }

        public static T GetComponentInChildrenTopLevel<T>(this Transform context, bool includeInactive = false)
        {
            for (int i = 0, iMax = context.childCount; i < iMax; ++i)
            {
                var child = context.GetChild(i);
                if ((includeInactive || child.gameObject.activeSelf) && child.TryGetComponent<T>(out var found))
                {
                    return found;
                }
            }

            return default;
        }

        public static List<T> GetComponentsInChildrenTopLevel<T>(this Transform context, bool includeInactive = false)
        {
            var list = new List<T>();
            context.GetComponentsInChildrenTopLevelNonAlloc(list, includeInactive);
            return list;
        }

        public static void GetComponentsInChildrenTopLevelNonAlloc<T>(this Transform context, List<T> list, bool includeInactive = false)
        {
            for (int i = 0, iMax = context.childCount; i < iMax; ++i)
            {
                var child = context.GetChild(i);
                if ((includeInactive || child.gameObject.activeSelf) && child.TryGetComponent<T>(out var found))
                {
                    list.Add(found);
                }
            }
        }
    }
}