using System;
using NUnit.Framework;
using WodiLib.Common;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Common
{
    [TestFixture]
    public class CommonEventIdTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [TestCase(-1, true)]
        [TestCase(0, false)]
        [TestCase(9999, false)]
        [TestCase(10000, true)]
        public static void ConstructorIntTest(int value, bool isError)
        {
            var errorOccured = false;
            try
            {
                var _ = new CommonEventId(value);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(0)]
        [TestCase(9999)]
        public static void ToIntTest(int value)
        {
            var instance = new CommonEventId(value);

            var intValue = instance.ToInt();

            // セットした値と取得した値が一致すること
            Assert.AreEqual(intValue, value);
        }

        [TestCase(-1, true)]
        [TestCase(0, false)]
        [TestCase(9999, false)]
        [TestCase(10000, true)]
        public static void CastFromIntTest(int value, bool isError)
        {
            CommonEventId instance = null;
            var errorOccured = false;
            try
            {
                instance = (CommonEventId)value;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (errorOccured) return;

            // キャストした結果が一致すること
            Assert.AreEqual((int)instance, value);
        }

        [TestCase(0)]
        [TestCase(9999)]
        public static void CastToIntTest(int value)
        {
            var castValue = 0;

            var instance = new CommonEventId(value);

            var errorOccured = false;
            try
            {
                castValue = (int) instance;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 元の値と一致すること
            Assert.AreEqual(castValue, value);
        }

        private static readonly object[] EqualTestCaseSource =
        {
            new object[] {0, 0, true},
            new object[] {0, 243, false},
        };

        [TestCaseSource(nameof(EqualTestCaseSource))]
        public static void OperatorEqualTestA(int left, int right, bool isEqual)
        {
            var leftIndex = (CommonEventId) left;
            var rightIndex = (CommonEventId) right;
            Assert.AreEqual(leftIndex == rightIndex, isEqual);
        }

        [TestCaseSource(nameof(EqualTestCaseSource))]
        public static void OperatorEqualTestB(int left, int right, bool isEqual)
        {
            var leftIndex = (CommonEventId) left;
            Assert.AreEqual(leftIndex == right, isEqual);
        }

        [TestCaseSource(nameof(EqualTestCaseSource))]
        public static void OperatorNotEqualTestA(int left, int right, bool isEqual)
        {
            var leftIndex = (CommonEventId) left;
            var rightIndex = (CommonEventId) right;
            Assert.AreEqual(leftIndex != rightIndex, !isEqual);
        }

        [TestCaseSource(nameof(EqualTestCaseSource))]
        public static void OperatorNotEqualTestB(int left, int right, bool isEqual)
        {
            var leftIndex = (CommonEventId) left;
            Assert.AreEqual(leftIndex != right, !isEqual);
        }

        [TestCaseSource(nameof(EqualTestCaseSource))]
        public static void OperatorEqualsTestA(int left, int right, bool isEqual)
        {
            var leftIndex = (CommonEventId) left;
            var rightIndex = (CommonEventId) right;
            Assert.AreEqual(leftIndex.Equals(rightIndex), isEqual);
        }

        [TestCaseSource(nameof(EqualTestCaseSource))]
        public static void OperatorEqualsTestB(int left, int right, bool isEqual)
        {
            var leftIndex = (CommonEventId) left;
            Assert.AreEqual(leftIndex.Equals(right), isEqual);
        }
    }
}