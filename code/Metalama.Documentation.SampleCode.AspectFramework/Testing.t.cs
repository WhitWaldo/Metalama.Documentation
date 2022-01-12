using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Testing
{
    class SimpleLogTests
    {
        [SimpleLog]
        void MyMethod()
        {
            Console.WriteLine($"Entering SimpleLogTests.MyMethod()");
            try
            {
                Console.WriteLine("Hello, world");
                return;
            }
            finally
            {
                Console.WriteLine($"Leaving SimpleLogTests.MyMethod()");
            }
        }
    }
}