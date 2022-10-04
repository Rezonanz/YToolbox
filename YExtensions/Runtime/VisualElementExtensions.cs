using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace YellowJelloGames.YExtensions
{
    public static class VisualElementExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnLeftClick(this VisualElement el, Action action)
        {
            el.RegisterCallback<MouseDownEvent>(e =>
            {
                if (e.button == 0) action?.Invoke();
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnableClassToggle(this VisualElement el, string classNameA, string classNameB,
            bool enable)
        {
            el.RemoveFromClassList(classNameA);
            el.RemoveFromClassList(classNameB);

            string className = enable ? classNameA : classNameB;
            el.AddToClassList(className);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddClasses(this VisualElement el, string classNames)
        {
            if (string.IsNullOrEmpty(classNames)) return;

            foreach (string className in classNames.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                el.AddToClassList(className);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement Create(string classNames)
        {
            var element = new VisualElement();
            element.AddClasses(classNames);
            return element;
        }

        public static VisualElement Create(params string[] classNames)
        {
            var element = new VisualElement();
            foreach (string className in classNames) element.AddToClassList(className);

            return element;
        }
    }
}