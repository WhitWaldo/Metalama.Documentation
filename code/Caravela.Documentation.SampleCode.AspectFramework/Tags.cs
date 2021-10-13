﻿using System;

namespace Caravela.Documentation.SampleCode.AspectFramework.Tags
{
    class TargetCode
    {
        [TagsAspect]
        void Method(int a, int b)
        {
            Console.WriteLine($"Method({a}, {b})");
        }
    }
}
