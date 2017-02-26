using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Testura.Code.Extensions;

namespace Testura.Code.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [TestCase((int)1, true, TestName = "IsNumeric_WhenHavingAInt_ShouldReturnTrue")]
        [TestCase((sbyte)1, true, TestName = "IsNumeric_WhenHavingASbyte_ShouldReturnTrue")]
        [TestCase((byte)1, true, TestName = "IsNumeric_WhenHavingAByte_ShouldReturnTrue")]
        [TestCase((short)1, true, TestName = "IsNumeric_WhenHavingAShort_ShouldReturnTrue")]
        [TestCase((ushort)1, true, TestName = "IsNumeric_WhenHavingAUShort_ShouldReturnTrue")]
        [TestCase((uint)1, true, TestName = "IsNumeric_WhenHavingAUint_ShouldReturnTrue")]
        [TestCase((long)1, true, TestName = "IsNumeric_WhenHavingALong_ShouldReturnTrue")]
        [TestCase((ulong)1, true, TestName = "IsNumeric_WhenHavingAULong_ShouldReturnTrue")]
        [TestCase((float)1, true, TestName = "IsNumeric_WhenHavingAFloat_ShouldReturnTrue")]
        [TestCase((double)1, true, TestName = "IsNumeric_WhenHavingADouble_ShouldReturnTrue")]
        [TestCase("test", false, TestName = "IsNumeric_WhenHavingAString_ShouldReturnFalse")]
        public void IsNumeric(object obj, bool expected)
        {
            Assert.AreEqual(expected, obj.IsNumeric());
        }
    }
}
