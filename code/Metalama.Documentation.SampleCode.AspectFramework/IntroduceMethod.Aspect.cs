﻿using Metalama.Framework.Aspects;
using System.Runtime.CompilerServices;

namespace Doc.IntroduceMethod;

internal class ToStringAttribute : TypeAspect
{
    [Introduce( WhenExists = OverrideStrategy.Override )]
    public string ToString() => $"{this.GetType().Name} Id={RuntimeHelpers.GetHashCode( meta.This )}";
}