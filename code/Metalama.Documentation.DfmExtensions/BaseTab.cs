﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal abstract class BaseTab
{
    public string TabId { get; }

    public string FullPath { get; }

    public virtual bool IsEmpty() => !File.Exists( this.FullPath ) || this.IsContentEmpty( File.ReadAllLines( this.FullPath ) );

    protected virtual bool IsContentEmpty( string[] lines ) => lines.All( string.IsNullOrWhiteSpace );

    protected BaseTab( string tabId, string fullPath )
    {
        this.TabId = tabId;
        this.FullPath = fullPath;
    }

    protected string GetProjectDirectory()
    {
        for ( var directory = Path.GetDirectoryName( this.FullPath ); directory != null; directory = Path.GetDirectoryName( directory ) )
        {
            if ( Directory.GetFiles( directory, "*.csproj" ).Any() )
            {
                return directory;
            }
        }

        throw new InvalidOperationException( $"Cannot find the project directory for '{this.FullPath}'." );
    }

    public abstract string GetTabContent( bool fallbackToSource = true );

    protected abstract string TabHeader { get; }

    public void AppendTabHeader( StringBuilder tabHeaders, string tabGroupId )
    {
        tabHeaders.Append( $"<li><a href=\"#tabpanel_{tabGroupId}_{this.TabId}\">{this.TabHeader}</a></li>" );
    }

    public void AppendTabBody( StringBuilder tabBodies, string tabGroupId )
    {
        var content = this.GetTabContent();
        tabBodies.Append( $"<div id=\"tabpanel_{tabGroupId}_{this.TabId}\">{content}</div>" );
    }
}