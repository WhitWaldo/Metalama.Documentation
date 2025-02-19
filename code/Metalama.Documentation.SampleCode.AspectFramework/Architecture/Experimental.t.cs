// Warning LAMA0900 on `ExperimentalApi.Foo`: `The 'ExperimentalApi' type is experimental.`
using Metalama.Extensions.Architecture.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Doc.Architecture.Experimental
{
  [Experimental]
  internal static class ExperimentalApi
  {
    public static void Foo()
    {
    }
    public static void Bar()
    {
      // This call is allowed because we are within the experimental class.
      Foo();
    }
  }
  internal static class ProductionCode
  {
    public static void Dummy()
    {
      // This call is reported.
      ExperimentalApi.Foo();
    }
  }
}