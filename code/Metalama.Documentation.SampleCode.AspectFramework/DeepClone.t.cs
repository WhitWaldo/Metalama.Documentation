using System;
namespace Doc.DeepClone
{
  internal class ManuallyCloneable : ICloneable
  {
    public object Clone()
    {
      return new ManuallyCloneable();
    }
  }
  [DeepClone]
  internal class AutomaticallyCloneable : ICloneable
  {
    private int _a;
    private ManuallyCloneable? _b;
    private AutomaticallyCloneable? _c;
    public virtual AutomaticallyCloneable Clone()
    {
      var clone = ((AutomaticallyCloneable)base.MemberwiseClone())!;
      clone._b = (ManuallyCloneable? )this._b?.Clone()!;
      clone._c = this._c?.Clone()!;
      return clone;
    }
    object ICloneable.Clone()
    {
      return Clone();
    }
  }
  internal class DerivedCloneable : AutomaticallyCloneable
  {
    private string? _d;
    public override DerivedCloneable Clone()
    {
      var clone = ((DerivedCloneable)base.Clone())!;
      return clone;
    }
  }
}