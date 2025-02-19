﻿using System;


namespace Doc.UpperCase
{
    public class Shipment
    {
        [UpperCase]
        public string? From;

        [UpperCase]
        public string? To { get; set; }
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
