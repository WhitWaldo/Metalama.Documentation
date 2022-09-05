﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;

namespace Doc.NotNull
{
    internal class Foo
    {
        public void Method1( [NotNull] string s ) { }

        public void Method2( [NotNull] out string s )
        {
            s = null!;
        }

        [return: NotNull]
        public string Method3()
        {
            return null!;
        }

        [NotNull]
        public string Property { get; set; }
    }

    public class PostConditionFailedException : Exception
    {
        public PostConditionFailedException( string message ) : base( message ) { }
    }
}