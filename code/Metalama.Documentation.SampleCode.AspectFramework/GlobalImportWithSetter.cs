﻿
using System;
using System.Collections.Generic;

namespace Doc.GlobalImportWithSetter
{
    internal class Foo
    {
        [Import]
        private IFormatProvider? _formatProvider;
    }

    internal class ServiceLocator : IServiceProvider
    {
        private static readonly ServiceLocator _instance = new();
        private readonly Dictionary<Type, object> _services = new();

        public static IServiceProvider ServiceProvider => _instance;

        object? IServiceProvider.GetService( Type serviceType )
        {
            this._services.TryGetValue( serviceType, out var value );

            return value;
        }

        public static void AddService<T>( T service ) where T : class => _instance._services[typeof(T)] = service;
    }
}