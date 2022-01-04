using System;
using System.Collections.Generic;

namespace Metalama.Documentation.SampleCode.AspectFramework.ConvertToRunTime
{
    internal class TargetCode
    {
        [ConvertToRunTimeAspect]
        private void Method(string a, int c, DateTime e)
        {
            var parameterNames = new List<string> { "a", "c", "e" };
            var buildTime = new Guid(331430378, 17141, 18214, 137, 77, 85, 4, 6, 53, 121, 120);
            var parameterType = typeof(string);
            return;
        }
    }
}
