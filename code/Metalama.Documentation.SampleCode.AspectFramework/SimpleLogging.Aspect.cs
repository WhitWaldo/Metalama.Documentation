﻿using System;
using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SampleCode.AspectFramework.SimpleLogging
{
    public class SimpleLogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
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