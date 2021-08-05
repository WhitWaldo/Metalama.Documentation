﻿using System;
using Caravela.Framework.Aspects;

namespace Caravela.Documentation.SampleCode.AspectFramework.Testing
{
    public class SimpleLogAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            Console.WriteLine($"Entering {meta.Target.Method.ToDisplayString()}");

            try
            {
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine($"Leaving {meta.Target.Method.ToDisplayString()}");
            }
        }
    }

}
