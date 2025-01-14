using System;
namespace Doc.UpperCase
{
  public class Shipment
  {
    private string? _from;
    [UpperCase]
    public string? From
    {
      get
      {
        return _from;
      }
      set
      {
        this._from = value?.ToUpper();
      }
    }
    private string? _to;
    [UpperCase]
    public string? To
    {
      get
      {
        return _to;
      }
      set
      {
        this._to = value?.ToUpper();
      }
    }
  }
  public class UpperCase
  {
    public static void Main(string[] args)
    {
      var package = new Shipment();
      package.From = "lhr";
      package.To = "jfk";
      Console.WriteLine($"Package is booked from {package.From} to {package.To}");
    }
  }
}