﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Linq;
using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

[Export( typeof(IDfmEngineCustomizer) )]
public class DfmEngineCustomizer : IDfmEngineCustomizer
{
    public void Customize( DfmEngineBuilder builder, IReadOnlyDictionary<string, object> parameters )
    {
        var includePosition = builder.BlockRules.Select( ( r, i ) => ( r, i ) ).Single( x => x.r.Name == "DfmIncludeBlock" ).i;

        builder.BlockRules = builder.BlockRules.InsertRange( includePosition, new IMarkdownRule[] { new SampleTokenRule() } );
    }
}