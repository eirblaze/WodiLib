using System;
using NUnit.Framework;
using WodiLib.Map;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Map
{
    [TestFixture]
    public class TilePathSettingDenyTest
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
                var _ = new TilePathSettingDeny();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        [Test]
        public static void PathPermissionTest()
        {
            var instance = new TilePathSettingDeny();

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
            Assert.AreEqual(result, TilePathPermission.Deny);
        }

        [Test]
        public static void ImpassableFlagsTest()
        {
            var instance = new TilePathSettingDeny();

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
            var instance = new TilePathSettingDeny();

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
            var instance = new TilePathSettingDeny();

            var errorOccured = false;
            try
            {
                var _ = instance.CannotPassingFlags;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生すること
            Assert.IsTrue(errorOccured);
        }

        [Test]
        public static void IsCounterTest()
        {
            const bool setValue = true;
            var instance = new TilePathSettingDeny();


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