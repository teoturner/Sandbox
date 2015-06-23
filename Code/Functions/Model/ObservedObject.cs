using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Functions.Model
{
    public class ObservedObject
    {     
        public delegate void SomethingHappenedEventHandler(String foo);

        public event SomethingHappenedEventHandler SomethingHappened;

        public ObservedObject()
        {
        }
    }
}
