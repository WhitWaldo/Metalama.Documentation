---
uid: sharing-state-with-advice
level: 400
---

# Sharing state with advice

When you need to share _compile-time_ state between different pieces of advice, or between your implementation of the `BuildAspect` method and the advice, you have a few strategies to choose from.

> [!NOTE]
> When you need to share _run-time_ state with advice, you must choose another strategy. For example, you can introduce a field in the target type and use it from several advice methods.

> [!WARNING]
> **DO NOT share state with an aspect field** if that state depends on the target declaration of the aspect. In the case of inherited aspects, the same instance of the aspect class will be reused for all inherited targets.

## Sharing state with compile-time template parameters

This is the most straightforward way to pass values from your `BuildAspect` method to a template method, but it works only with method templates. For details, see <xref:template-parameters>.

## Sharing state with the Tags property

Compile-time template parameters are unavailable for event, property, or field templates. The most straightforward alternative is to use tags, which are arbitrary name-value pairs.

To define and use tags:

1. In your implementation of the `BuildAspect` method, when adding the advice by calling a method of the <xref:Metalama.Framework.Advising.IAdviceFactory> interface, pass the tags as an anonymous object to the `tags` argument. For instance, `args: new { A = 5, B = "x", C = builder.Target.DeclaringType }`. In this example, `A`, `B`, and `C` are three arbitrary names.

2. In your template method, the tags are available under the `meta.Tags` dictionary. You would, for instance, use the `meta.Tags["A"]` expression to access the tag named `A` that you defined in the previous step.

### Example

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Tags.cs name="Tags"]

## Sharing state with the AspectState property

You can use the <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectState?text=IAspectBuilder.AspectState> property to store any aspect state that depends on the target declaration. This object is exposed on the <xref:Metalama.Framework.Aspects.IAspectInstance.AspectState?text=IAspectInstance.AspectState> property is visible to child aspects and aspects that inherit them.

