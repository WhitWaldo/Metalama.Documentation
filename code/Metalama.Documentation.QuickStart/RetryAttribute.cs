﻿using Metalama.Framework.Aspects;
using System;
using System.Threading;

namespace DebugDemo
{
    internal class RetryAttribute : OverrideMethodAspect
    {
        public int MaxAttempts { get; set; } = 5;

        public override dynamic? OverrideMethod()
        {
            for (var i = 0; ; i++)
            {
                try
                {
                    return meta.Proceed();
                }
                catch (Exception e) when (i < this.MaxAttempts)
                {
                    Console.WriteLine($"{e.Message}. Retrying in 100 ms.");
                    Thread.Sleep(100);
                }
            }
        }
    }
}