using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Testura.Code.Extensions;

namespace Testura.Code.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [TestCase(typeof(List<string>), true, TestName = "IsCollection_WhenHavingAList_ShouldReturnTrue")]
        [TestCase(typeof(Collection<string>), true, TestName = "IsCollection_WhenHavingACollection_ShouldReturnTrue")]
        [TestCase(typeof(Dictionary<string, string>), true, TestName = "IsCollection_WhenHavingADictionary_ShouldReturnTrue")]
        [TestCase(typeof(Queue<string>), true, TestName = "IsCollection_WhenHavingAQueue_ShouldReturnTrue")]
        [TestCase(typeof(Stack<string>), true, TestName = "IsCollection_WhenHavingAStack_ShouldReturnTrue")]
        [TestCase(typeof(LinkedList<string>), true, TestName = "IsCollection_WhenHavingALinkedList_ShouldReturnTrue")]
        [TestCase(typeof(SortedList<string, int>), true, TestName = "IsCollection_WhenHavingASortedList_ShouldReturnTrue")]
        [TestCase(typeof(HashSet<string>), true, TestName = "IsCollection_WhenHavingAHashSet_ShouldReturnTrue")]
        [TestCase(typeof(int), false, TestName = "IsCollection_WhenHavingInt_ShouldReturnfalse")]
        public void IsCollection(Type type, bool expects)
        {
            Assert.AreEqual(expects, type.IsCollection());
        }

        [TestCase(typeof(IList<string>), true, TestName = "IsICollection_WhenHavingAIList_ShouldReturnTrue")]
        [TestCase(typeof(ICollection<string>), true, TestName = "IsICollection_WhenHavingAICollection_ShouldReturnTrue")]
        [TestCase(typeof(IDictionary<string, string>), true, TestName = "IsICollection_WhenHavingAIDictionary_ShouldReturnTrue")]
        [TestCase(typeof(List<string>), false, TestName = "IsICollection_WhenHavingAList_ShouldReturnFalse")]
        [TestCase(typeof(int), false, TestName = "IsICollection_WhenHavingAInt_ShouldReturnFalse")]
        public void IsICollection(Type type, bool expects)
        {
            Assert.AreEqual(expects, type.IsICollection());
        }

    }
}
