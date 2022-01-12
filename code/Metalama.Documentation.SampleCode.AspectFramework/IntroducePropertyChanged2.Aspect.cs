﻿using System.ComponentModel;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Metalama.Documentation.SampleCode.AspectFramework.IntroducePropertyChanged2
{
    internal class IntroducePropertyChangedAspect : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var eventBuilder = builder.Advices.IntroduceEvent(
                builder.Target,
                nameof(PropertyChanged));

            builder.Advices.IntroduceMethod(
                builder.Target,
                nameof(OnPropertyChanged),
                tags: new () {  ["event"] = eventBuilder });
        }


        [Template]
        public event PropertyChangedEventHandler? PropertyChanged;

        [Template]
        protected virtual void OnPropertyChanged( string propertyName )
        {
            ((IEvent) meta.Tags["event"]!).Invokers.Final.Raise(
                meta.This, 
                meta.This, new PropertyChangedEventArgs(propertyName));
        }
    }
}