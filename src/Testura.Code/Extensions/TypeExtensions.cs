#pragma warning disable 1591

namespace Testura.Code.Extensions;

public static class TypeExtensions
{
    /// <summary>
    /// Examine if type is a collection.
    /// </summary>
    /// <param name="type">Type to examine.</param>
    /// <returns><c>true</c> if it's a collection, otherwise <c>false</c> .</returns>
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

    /// <summary>
    /// >Examine if a type is a ICollection.
    /// </summary>
    /// <param name="typeReference">Type to examine..</param>
    /// <returns><c>true</c> it's a ICollection, otherwise <c>false</c>.</returns>
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