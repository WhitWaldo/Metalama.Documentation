﻿
using Metalama.Framework.Aspects;
using Metalama.Framework.Project;
using System;

namespace Doc.AspectConfiguration
{
    // Options for the [Log] aspects.
    public class LoggingOptions : ProjectExtension
    {
        private string _defaultCategory = "Default";

        public override void Initialize( IProject project, bool isReadOnly )
        {
            base.Initialize( project, isReadOnly );

            // Optionally, we can initialize the configuration object from properties passed from MSBuild.
            if ( project.TryGetProperty( "DefaultLogProperty", out var propertyValue ) )
            {
                this._defaultCategory = propertyValue;
            }
        }

        public string DefaultCategory
        {
            get => this._defaultCategory;

            set
            {
                if ( this.IsReadOnly )
                {
                    throw new InvalidOperationException();
                }

                this._defaultCategory = value;
            }
        }
    }

    // For convenience, an extension method to access the options.
    [CompileTime]
    public static class LoggingProjectExtensions
    {
        public static LoggingOptions LoggingOptions( this IProject project ) => project.Extension<LoggingOptions>();
    }
}