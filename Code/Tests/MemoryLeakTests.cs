using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sandbox.Functions;
using Sandbox.Functions.Model;

namespace Sandbox.Tests
{
    [TestClass]
    public class MemoryLeakTests
    {
        [TestMethod]
        public void CreateUncollectibleObject()
        {
            var collectibleObject = new ObservedObject();

            WeakReference reference = CreateUncollectibleObject(collectibleObject);

            // Service should have gone out of scope about now, 
            // so the garbage collector can clean it up
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.IsNotNull(reference.Target);
        }

        [TestMethod]
        public void CreateUncollectibleObjectAndReleaseEventHandler()
        {
            var collectibleObject = new ObservedObject();

            WeakReference reference = CreateUncollectibleObjectAndRemoveEventHandler(collectibleObject);

            // Service should have gone out of scope about now, 
            // so the garbage collector can clean it up
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.IsNull(reference.Target);
        }

        private WeakReference CreateUncollectibleObject(ObservedObject collectibleObject)
        {
            var uncollectibleObject = new ObserverObject();
            collectibleObject.SomethingHappened += uncollectibleObject.SomethingHappened;

            return new WeakReference(uncollectibleObject, true);
        }

        private WeakReference CreateUncollectibleObjectAndRemoveEventHandler(ObservedObject collectibleObject)
        {
            var uncollectibleObject = new ObserverObject();
            collectibleObject.SomethingHappened += uncollectibleObject.SomethingHappened;

            WeakReference weakReference = new WeakReference(uncollectibleObject, true);
            
            collectibleObject.SomethingHappened -= uncollectibleObject.SomethingHappened;

            return weakReference;
        }
    }
}
