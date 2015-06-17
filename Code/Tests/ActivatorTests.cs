using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace Sandbox.Tests
{
    [TestClass]
    public class ActivatorTests
    {
        [TestMethod]
        public void CreateGenericCollectionOfString()
        {
            Object activatedObject = Sandbox.Functions.Activator.CompileDefaultConstructorToInstanceActivator(typeof(Collection<String>))();

            Assert.IsNotNull(activatedObject);
            Assert.IsInstanceOfType(activatedObject, typeof(Collection<String>));
        }

        [TestMethod]
        public void CreateGenericListOfString()
        {
            Object activatedObject = Sandbox.Functions.Activator.CompileDefaultConstructorToInstanceActivator(typeof(List<String>))();

            Assert.IsNotNull(activatedObject);
            Assert.IsInstanceOfType(activatedObject, typeof(List<String>));
        }
    }
}
