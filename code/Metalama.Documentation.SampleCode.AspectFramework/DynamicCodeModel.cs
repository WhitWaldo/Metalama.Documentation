﻿
using System;
using System.IO;

namespace Doc.DynamicCodeModel
{
    internal class Program
    {
        private TextWriter _logger = Console.Out;

        [Log]
        private void Foo() { }

        private static void Main()
        {
            new Program().Foo();
        }
    }
}