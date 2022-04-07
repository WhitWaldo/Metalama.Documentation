---
uid: fabrics-aspects
---

# Adding Aspects in Bulk from a Fabrics

You can use a fabric to programmatically add aspects to any declaration that is "under" the target of the fabric. Thanks to fabrics, you do not need to add aspects one by one using custom attributes.
* From a type fabric, you can add aspects to any member of this type or to the type itself.
* From a namespace fabric, you can add aspects to any type of that namespace, or to any member of one of these types.
* From a project fabric, you can add aspects to any type or member of that project.

To add an an aspect from a fabric:

1. Create a fabric class and add a <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> method.

2. Call one of the following methods from <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>:
   * To select type members (methods, fields, nested types, ...), use the <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.WithTargetMembers*?text=amender.WithTargetMembers> method and provide a lambda expression that select the relevent type members, or
   * To select the type itself, use <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.WithTarget*?text=amender.WithTarget>.

    The reason of this design is that the <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.WithTargetMembers*> method will not only select members declared in source code, but also members introduced by other aspects and that are unknown when your  <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method is executed.


3. Chain the call to <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.WithTargetMembers*> or  <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.WithTarget*> with a call to thr  <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> method.

## Example: adding an aspect to all methods in a project

In the following example, a type fabric adds a logging aspect to all public methods in the type.

[!include[Type Fabric Adding Aspects](../../code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric.cs)]


## Example: adding an aspect from a type fabric

In the following example, a type fabric adds a logging aspect to all public methods in the type.

[!include[Type Fabric Adding Aspects](../../code/Metalama.Documentation.SampleCode.AspectFramework/TypeFabric.cs)]