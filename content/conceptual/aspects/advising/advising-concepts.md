---
uid: advising-concepts
---

# Transforming code: concepts

Aspects can transform the target code by providing _advice_. An advice is a primitive transformation of code. Advice is safely composable: several aspects that do not know about each other can add advice to the same declaration.

> [!NOTE]
> In English, the word _advice_is uncountable, i.e., grammatically plural. The grammatically-correct singular form of _advice_ is _piece of advice_, but using these words in a software engineering text seems very odd. In aspect-oriented programming, _advice_ is a countable concept. Despite the difficulties associated with using uncountable nouns as countable, we sometimes use _an advice_ for the singular form and _advices_ for the plural form, which may be occasionally shocking to some native English speakers. We use other neutral turns of phrases whenever possible unless it would make the phrase much more cumbersome or less understandable.

There are two ways to add advice: _declaratively_ and _imperatively_.

## Declarative advising

The only _declarative advice_ is the _member introduction_ advice marked by the <xref:Metalama.Framework.Aspects.IntroduceAttribute> custom attribute. For each member of the aspect class annotated with `[Introduce]`, the aspect framework will attempt to introduce the member in the target class. For details, see <xref:introducing-members>.

## Imperative advising

_Imperative advice_ is added by implementing the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, thanks to the methods exposed by the <xref:Metalama.Framework.Aspects.IAspectBuilder.Advice> property of the `builder` parameter. See <xref:Metalama.Framework.Advising.IAdviceFactory> for a complete list of methods. In short:

* <xref:Metalama.Framework.Advising.IAdviceFactory.Override*> allows you to replace the implementation of a type member.
* <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceMethod*>, <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceProperty*>, <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceField*> and <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceEvent*> allows your aspect to introduce new members into the target type. See <xref:introducing-members#introducing-members-programmatically> for details.
* <xref:Metalama.Framework.Advising.IAdviceFactory.ImplementInterface*> makes the target type implement an interface. See <xref:implementing-interfaces> for details.

## Template methods

With most kinds of advice, you must provide a _template_ of the member you want to add to the target type. 

Templates are written in standard C# code but mix two kinds of code: _compile-time_ and _run-time_. When some target code is advised, the compile-time part of the corresponding template is _executed_. The output of this execution is the run-time code which is then injected into the source code to make up the _transformed code_.

For details, see <xref:templates>.

