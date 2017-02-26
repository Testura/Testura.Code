using System;

namespace Testura.Code.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsCollection(this Type type)
        {
            var name = type.Name;
            return name.StartsWith("List") ||
                   name.StartsWith("Collection") ||
                   name.StartsWith("Dictionary") ||
                   name.StartsWith("Queue") ||
                   name.StartsWith("Stack") ||
                   name.StartsWith("LinkedList") ||
                   name.StartsWith("ObservableCollection") ||
                   name.StartsWith("SortedList") ||
                   name.StartsWith("HashSet");
        }

        public static bool IsICollection(this Type typeReference)
        {
            var name = typeReference.Name;
            return name.StartsWith("IList") ||
                   name.StartsWith("ICollection") ||
                   name.StartsWith("IDictionary") ||
                   name.StartsWith("IQueue") ||
                   name.StartsWith("IStack") ||
                   name.StartsWith("ILinkedList") ||
                   name.StartsWith("IObservableCollection") ||
                   name.StartsWith("ISortedList") ||
                   name.StartsWith("IHashSet");
        }
    }
}
