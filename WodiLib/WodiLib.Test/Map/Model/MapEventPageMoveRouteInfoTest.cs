using System;
using NUnit.Framework;
using WodiLib.Event;
using WodiLib.Map;

namespace WodiLib.Test.Map
{
    [TestFixture]
    public class MapEventPageMoveRouteInfoTest
    {
        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void AnimateSpeedSetTest(bool isNull, bool isError)
        {
            var instance = new MapEventPageMoveRouteInfo();
            var errorOccured = false;
            try
            {
                instance.AnimateSpeed = isNull ? null : AnimateSpeed.Middle;
            }
            catch (Exception)
            {
                errorOccured = true;
            }
    
            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void MoveSpeedSetTest(bool isNull, bool isError)
        {
            var instance = new MapEventPageMoveRouteInfo();
            var errorOccured = false;
            try
            {
                instance.MoveSpeed = isNull ? null : MoveSpeed.Fast;
            }
            catch (Exception)
            {
                errorOccured = true;
            }
    
            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }
        
        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void MoveFrequencySetTest(bool isNull, bool isError)
        {
            var instance = new MapEventPageMoveRouteInfo();
            var errorOccured = false;
            try
            {
                instance.MoveFrequency = isNull ? null : MoveFrequency.Long;
            }
            catch (Exception)
            {
                errorOccured = true;
            }
    
            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void MoveTypeSetTest(bool isNull, bool isError)
        {
            var instance = new MapEventPageMoveRouteInfo();
            var errorOccured = false;
            try
            {
                instance.MoveType = isNull ? null : MoveType.Not;
            }
            catch (Exception)
            {
                errorOccured = true;
            }
    
            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, false)]
        [TestCase(true, true, true)]
        public static void CustomMoveRouteSetTest(bool isRouteCustom, bool isSetNull, bool isError)
        {
            var instance = new MapEventPageMoveRouteInfo();
            instance.MoveType = isRouteCustom ? MoveType.Custom : MoveType.Not;
            var errorOccured = false;
            try
            {
                instance.CustomMoveRoute = isSetNull ? null : new ActionEntry();
            }
            catch (Exception)
            {
                errorOccured = true;
            }
    
            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }
    }
}