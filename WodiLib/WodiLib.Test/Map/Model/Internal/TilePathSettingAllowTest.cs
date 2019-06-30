using System;
using NUnit.Framework;
using WodiLib.Map;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Map
{
    [TestFixture]
    public class TilePathSettingAllowTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [Test]
        public static void ConstructorTestA()
        {
            var errorOccured = false;
            try
            {
                var _ = new TilePathSettingAllow();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        private static readonly object[] ConstructorTestCaseSource =
        {
            new object[] {new TileCannotPassingFlags(), false},
            new object[] {null, true},
        };

        [TestCaseSource(nameof(ConstructorTestCaseSource))]
        public static void ConstructorTestB(TileCannotPassingFlags cannotPassingFlags, bool isError)
        {
            var errorOccured = false;
            try
            {
                var _ = new TilePathSettingAllow(cannotPassingFlags);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(0x00, false)] // 通行不可なし
        [TestCase(0x02, false)] // 左通行不可
        [TestCase(0x96, false)] // 左・右通行不可
        [TestCase(0x9F, true)] // 全方向不可
        public static void ConstructorTestC(int code, bool isError)
        {
            var errorOccured = false;
            try
            {
                var _ = new TilePathSettingAllow(code);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [Test]
        public static void PathPermissionTest()
        {
            var instance = new TilePathSettingAllow();

            TilePathPermission result = null;

            var errorOccured = false;
            try
            {
                result = instance.PathPermission;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 結果が意図した値と一致すること
            Assert.AreEqual(result, TilePathPermission.Allow);
        }

        [Test]
        public static void ImpassableFlagsTest()
        {
            var instance = new TilePathSettingAllow();

            var errorOccured = false;
            try
            {
                var _ = instance.ImpassableFlags;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生すること
            Assert.IsTrue(errorOccured);
        }

        private static readonly object[] PathOptionTestCaseSource =
        {
            new object[] {TilePathOption.Nothing, false},
            new object[] {null, true},
        };

        [TestCaseSource(nameof(PathOptionTestCaseSource))]
        public static void PathOptionTest(TilePathOption option, bool isError)
        {
            var instance = new TilePathSettingAllow();

            var errorOccured = false;
            try
            {
                instance.PathOption = option;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (errorOccured) return;

            TilePathOption result = null;

            try
            {
                result = instance.PathOption;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 結果が意図した値と一致すること
            Assert.AreEqual(result, option);
        }

        [Test]
        public static void CannotPassingFlagsTest()
        {
            var setValue = new TileCannotPassingFlags(0x02);
            var instance = new TilePathSettingAllow(setValue);

            TileCannotPassingFlags result = null;
            var errorOccured = false;
            try
            {
                result = instance.CannotPassingFlags;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 結果が意図した値であること
            Assert.AreEqual(result, setValue);
        }

        [Test]
        public static void IsCounterTest()
        {
            const bool setValue = true;
            var instance = new TilePathSettingAllow();


            var errorOccured = false;
            try
            {
                instance.IsCounter = setValue;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            var result = false;

            try
            {
                result = instance.IsCounter;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 結果が意図した値と一致すること
            Assert.AreEqual(result, setValue);
        }
    }
}