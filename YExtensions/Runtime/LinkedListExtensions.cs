using System.Collections.Generic;

namespace YellowJelloGames.YExtensions
{
    public static class LinkedListExtensions
    {
        public static int IndexOf<T>(this LinkedList<T> source, T item)
        {
            int i = 0;
            var node = source.First;
            while (node != null)
            {
                if (item.Equals(node.Value)) return i;

                node = node.Next;
                ++i;
            }
            
            return -1;
        }
        
        public static LinkedList<T> RemoveAllNext<T>(this LinkedList<T> source, LinkedListNode<T> node)
        {
            while (node != null)
            {
                var next = node.Next;
                source.Remove(node);
                node = next;
            }

            return source;
        }
    }
}