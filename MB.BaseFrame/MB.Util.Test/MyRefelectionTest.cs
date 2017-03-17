using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MB.Util.Test.EnmuTestA
{
    public enum MyEnum
    {
        TestA,
        TestB
    }

    public class MyCase
    {
        public string MyName {get;set;}
        public MB.Util.Test.EnmuTestA.MyEnum EnumA { get; set; }
    }
}

namespace MB.Util.Test.EnmuTestB
{
    public enum MyEnum
    {
        TestA,
        TestB
    }

    public class MyCase
    {
        public string MyName { get; set; }
        public MB.Util.Test.EnmuTestB.MyEnum EnumA { get; set; }
    }
}

namespace MB.Util.Test
{
    [TestClass]
    public class MyRefelectionTest
    {

        /// <summary>
        /// 0049这个Defect引起的原因是由于
        /// 在使用public T FillModelObject<T>进行CLONE时，两个对象的类型是不一致的，但是类型的数据结构是一致的
        /// 但是由于类型中存在了枚举，枚举的赋值直接通过当前类型的值来赋值，导致了枚举赋值的失败。
        /// 现在就这一情况请进单元测试
        /// </summary>
        [TestMethod]
        public void TestForFillObjectWithBug0049()
        {
            MB.Util.Test.EnmuTestA.MyCase a = new MB.Util.Test.EnmuTestA.MyCase();
            a.MyName = "Test";
            a.EnumA = MB.Util.Test.EnmuTestA.MyEnum.TestA;

            MB.Util.Test.EnmuTestB.MyCase b = MyReflection.Instance.FillModelObject<MB.Util.Test.EnmuTestB.MyCase>(a);
            Assert.AreEqual("Test", b.MyName);
            Assert.AreEqual(MB.Util.Test.EnmuTestB.MyEnum.TestA, b.EnumA);

        }
    }
}
