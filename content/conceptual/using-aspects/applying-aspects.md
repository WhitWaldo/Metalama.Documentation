---
uid: applying-aspects
---

# Applying Aspects to Code

There are three ways to apply an aspect to code:

* Declaratively, with a custom attribute
* Programmatically, with a fabric
* Programmatically, from another aspect

## Options 1. Declaratively, with a custom attribute

If the aspect derives from `System.Attribute`, you can apply it to your code as a normal custom attribute.


## Option 2. Programmatically, with a fabric

(TODO: document better)

Thanks to fabrics, you can add aspects to a large number of declarations without using custom attributes. You can add a fabric to a project, to a namespace, or to a type:

* For a project-level fabric, add a type implementing the <xref:Metalama.Framework.Fabrics.ProjectFabric> class anywhere in the project, and implement the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> method.
* For a namespace-level fabric, add a type implementing the <xref:Metalama.Framework.Fabrics.NamespaceFabric> class in the desired namespace, and implement the <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> method. 
* For a type-level fabric, add a nested type implementing the <xref:Metalama.Framework.Fabrics.TypeFabric> class in the target type, and implement the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

The `Amend` methods accept a parameter of type <xref:Metalama.Framework.Fabrics.IAmender`1>, which allows you to add aspects by accessing the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound*?text=amender.Outbound> property, selecting targets thanks to the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*>  methods, and finally calling the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> method.

### Example: project fabric

The following example shows how to add a `Log` aspect to all methods in the current project.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric.cs name="Project Fabric"]

### Example: type fabric fabric

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/TypeFabric.cs name="Type Fabric"]

## Option 3. Programmatically, from another aspect

If you're an aspect author and your aspect requires another aspect to work, it is better if your aspect adds it automatically where it needs it. See <xref:child-aspects> for details.