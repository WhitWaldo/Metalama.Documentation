---
uid: templates
---
# Caravela Template Language

The _Caravela Template Language_ is neither a subset nor a superset of C# but rather a specific way to compile C#. A template can contain _both_ run-time and compile-time code. Every expression or statement in a template is interpreted as having _either_ run-time scope _or_ compile-time scope. Compile-time expressions are initiated by calls to the <xref:Caravela.Framework.Aspects.meta> API.

## Initial example

Before moving forward, let's illustrate this concept with an example. The next aspect writes text to the console before and after the execution of a method, but special care is taken for `out` parameters and `void` methods. This is achieved by a conditional compile-time logic which generates simple run-time code. Compile-time code is highlighted <span class="caravelaClassification_CompileTime">differently</span>, so you can see which part of the code executes at compile time and which executes at run time.

> [!NOTE]
> To benefit from syntax highlighting in Visual Studio, install the PostSharp "Caravela" Tools for Visual Studio (TODO: link)

[!include[Simple Logging](../../code/Caravela.Documentation.SampleCode.AspectFramework/LogParameters.cs)]

***

## Writing compile-time code

### Compile-time expressions

Compile-time expressions are expressions that either contain a call to a compile-time method, or contain a reference to a compile-time local variable.

#### meta API

The entry point of the compile-time API is the <xref:Caravela.Framework.Aspects.meta> static class. This class name is intentionally lower case to convey the sentiment that it is something unusual and gives access to some kind of magic. Actually, the <xref:Caravela.Framework.Aspects.meta> class is the entry point to the meta model and the members of this class can be invoked only in the context of a template.

The <xref:Caravela.Framework.Aspects.meta> exposes to the following members:

- <xref:Caravela.Framework.Aspects.meta.Proceed> invokes the method or accessor being intercepted -- it can be the next aspect or the source implementation.
- <xref:Caravela.Framework.Aspects.meta.Target> gives access to the declaration to which the template is applied.
- <xref:Caravela.Framework.Aspects.IMetaTarget.Parameters?text=meta.Target.Parameters> gives access to the current method or accessor parameters.
- <xref:Caravela.Framework.Aspects.meta.Diagnostics> allows your aspect to report or suppress diagnostics. See <xref:diagnostics> for details.
- <xref:Caravela.Framework.Aspects.meta.This> represents the `this` instance. Together with <xref:Caravela.Framework.Aspects.meta.Base>, <xref:Caravela.Framework.Aspects.meta.ThisStatic> and <xref:Caravela.Framework.Aspects.meta.BaseStatic>, it allows your template to access members of the target class using dynamic code (see below).
- <xref:Caravela.Framework.Aspects.meta.Tags> gives access to an arbitrary dictionary that has been passed to the advice factory method.
- <xref:Caravela.Framework.Aspects.meta.CompileTime*> forces a coerces a neutral expression into a compile-time expression.
- <xref:Caravela.Framework.Aspects.meta.RunTime*> converts the result of a compile-time expression into a run-time value (see below).

### Compile-time local variables

Local variables are run-time by default. To declare a compile-time local variable, you must initialize it to a compile-time value. If you need to initialize the compile-time variable to a literal value such as `0` or `"string"`, use the `meta.CompileTime` method to convert the literal into a compile-time value.

Examples:

- In `var i = 0`, `i` is a run-time variable.
- In `var i = meta.CompileTime(0)`, `i` is a compile-time variable.
- In `var parameters = meta.Target.Parameters`, `parameters` is compile-time variable.

> [!NOTE]
> It is not allowed to assign a compile-time variable from a block whose execution depends on a run-time condition, including:
>
> - a run-time `if`, `else`, `for`, `foreach`, `while`;
> - a `catch` or `finally`.

### Aspect members

Aspect members are compile-time and can be accessed from templates. For instance, an aspect custom attribute and can define a property that can be assigned from user code. This property can be accessed from compile-time code.

There are two exceptions to this rule:

- aspect members whose signature contain a run-time-only type cannot be accessed from a template.
- template members are not considered as compile-time (TODO - specify)

#### Example

The following example shows a simple _Retry_ aspect. The maximum number of attempts can be configured by setting a property of the custom attribute.

[!include[Retry](../../code/Caravela.Documentation.SampleCode.AspectFramework/Retry.cs)]

### Compile-time if

If the condition of an `if` statement is a compile-time expression, the `if` statement will be interpreted at compile-time.

#### Example

In the following example, the aspect prints a different string for static methods than for non-instance ones.

[!include[Compile-Time If](../../code/Caravela.Documentation.SampleCode.AspectFramework/CompileTimeIf.cs)]

### Compile-time foreach

If the expression of a `foreach` statement is a compile-time expression, the `foreach` statement will be interpreted at compile-time.

> [!NOTE]
> It is not allowed to have a compile-time `foreach` inside a block whose execution depends on a run-time condition, including:
>
> - a run-time `if`, `else`, `for`, `foreach`, `while`;
> - a `catch` or `finally`.

#### Example

The following aspect uses a `foreach` loop to print the value of each parameter of the method to which it is applied.

[!include[Compile-Time If](../../code/Caravela.Documentation.SampleCode.AspectFramework/CompileTimeForEach.cs)]

### No compile-time for and while

It is not possible to create compile-time `for` or `while` loops. `goto` statements are forbidden in templates. In these scenarios, you can try to replace the loop with the following construct:

```cs
foreach (int i in meta.CompileTime( Enumerable.Range( 0, n ) ))
```

If the approach above is not possible, you can try to move your logic to a compile-time aspect function (not a template method), have this function return an enumerable, and use the return value in a `foreach` loop in the template method.

### typeof, nameof expressions

`typeof` and `nameof` expressions in compile-time code are always pre-compiled into compile-time expression, which makes it possible for compile-time code to reference run-time types.

### Custom compile-time methods

If you need to move some compile-time logic from the template to a method, you can create a method in the aspect. It will automatically be considered as compile-time.

If you want to share compile-time code between aspects, you can create a compile-time class by marking it with the `[CompileTimeOnly]` custom attribute.

## Writing dynamic run-time code

### Dynamic typing

Templates use the `dynamic` type to represent types that are unknown by the developer of the template. For instance, an aspect may not know in advance the return type of the methods to which it is applied. The return type is represented by the `dynamic` type.

```cs
dynamic? OverrideMethod() 
{ 
    return default;
}
```

All `dynamic` compile-time code are transformed into strongly-typed run-time code. When the template is expanded, `dynamic` variables are transformed into `var` variables. Therefore, all `dynamic` variables must be initialized.

It is not possible, in a template, to generate code that uses `dynamic` typing at run time.

### Converting compile-time values to run-time values

You can use `meta.RunTime( expression )` to convert the result of a compile-time expression into a run-time value. The compile-time expression will be evaluated at compile time, and its result will be converted into _syntax_ that represents that value. Conversions are possible for the following compile-time types:

- Literals;
- Enum values;
- One-dimensional arrays;
- Tuples;
- Reflection objects: <xref:System.Type>, <xref:System.Reflection.MethodInfo>, <xref:System.Reflection.ConstructorInfo>, <xref:System.Reflection.EventInfo>, <xref:System.Reflection.PropertyInfo>, <xref:System.Reflection.FieldInfo>;
- <xref:System.Guid>;
- Generic collections: <xref:System.Collections.Generic.List`1> and <xref:System.Collections.Generic.Dictionary`2>;
- <xref:System.DateTime> and <xref:System.TimeSpan>.

It is not possible to build custom convertors at the moment.

#### Example

[!include[Dynamic](../../code/Caravela.Documentation.SampleCode.AspectFramework/ConvertToRunTime.cs)]

(In the transformed code, the call to `Intrinsics.GetRuntimeTypeHandle` is transformed into a `typeof` later in the compilation process.)

### Dynamic code

The `meta` API exposes some properties of `dynamic` type and some methods returning `dynamic` values. These members are compile-time, but their value represents a _declaration_ that you can dynamically read at run time.
In the case of writable properties, it is also possible to set the value.

Dynamic values are a bit _magic_ because their compile-time value translates into _syntax_ that is injected in the transformed code.

For instance, `meta.Target.Parameters["p"].Value` refers to `p` parameter of the target method and compiles simply into the syntax `p`. It is possible to read this parameter and, if this is an `out` or `ref` parameter, it is also possible to write it.

```cs
// Translates into: Console.WriteLine( "p = " + p );
Console.WriteLine( "p = " + meta.Target.Parameters["p"].Value );


// Translates into: this.MyProperty = 5;
meta.Property.Value = 5;
```

You can also write dynamic code on the left of a dynamic expression:

```cs
// Translates into: this.OnPropertyChanged( "X" );
meta.This.OnPropertyChanged( "X" );
```

You can combine dynamic code and compile-time expressions:

```cs
// Translated into: this.OnPropertyChanged( "MyProperty" );
meta.This.OnPropertyChanged( meta.Property.Name );
```

### Generating calls to the call model

When you have a <xref:Caravela.Framework.Code> representation of a declaration, you may want to access it from your generated run-time code. You can do this by using the `Invokers` property exposed by the <xref:Caravela.Framework.Code.IMethod>, <xref:Caravela.Framework.Code.IFieldOrProperty> or <xref:Caravela.Framework.Code.IEvent> interfaces.

For details, see the documentation of the <xref:Caravela.Framework.Code.Invokers> namespace.

## Debugging templates

See <xref:debugging>.