---
uid: overriding-members
---
# Overriding Members (Reloaded)

In the section <xref:simple-aspects>, you have learned to override methods, properties, fields and events using a simple object-oriented API. In this section, you will learn how to achieve the same thing using the advising API. This will allow you to modify not only the method that is the immediate target of the aspect, but any method in the type being targeted.

> [!NOTE]
> In this article, we will assume you have learned the techniques explained in <xref:simple-aspects>.

## Overriding methods

To override one or more methods, your aspects needs to implement the <xref:Caravela.Framework.Aspects.IAspect`1.BuildAspect*> method exposed on `builder.AdviceFactory`.

The _first argument_ of `OverrideMethod` is the <xref:Caravela.Framework.Code.IMethod> that you want to override. This method must be in the type being targeted by the current aspect instance.

The _second argument_ of `OverrideMethod` is the name of the template method. This method must exist in the aspect class and, additionally:

* the template method must be annotated with the `[Template]` attribute,
* the template method must have exactly the following signature:

    ```cs
    dynamic? Template()
    ```

### Example: synchronized object

The following aspects wraps all instance methods with a `lock( this )` statement.

> [!NOTE]
> In a production-ready implementation, you should not lock `this` but a private field. You can introduce this field as described in <xref:introducing-members>. A product-ready implementation should also wrap properties.

[!include[Synchronized](../../../code/Caravela.Documentation.SampleCode.AspectFramework/Synchronized.cs)]

## Overriding fields or properties

Overriding a field or a property means overriding its `get` and/or `set` semantic. From the point of view of overriding, fields are transformed into properties of the same name and accessibility.

Before applying the templates, the aspect framework transforms fields and automatic properties into field-backed properties.

> [!WARNING]
> When you override a field, it is no longer possible to reference the field using the `out`, `ref` or `in` keywords. Such cases are currently unsupported and will result in compilation errors. The workaround is to use an intermediate local variable.

There are two approaches to override a field or property: by providing a _property template_, or by providing one or more _accessor templates_.

### Overriding with a property template

This approach is the simplest but it has a few limitations.

Just like for methods, to override one or more fields or properties, your aspects needs to implement the <xref:Caravela.Framework.Aspects.IAspect`1.BuildAspect*> method exposed on `builder.AdviceFactory`.

The _first argument_ of `OverrideFieldOrProperty` is the <xref:Caravela.Framework.Code.IFieldOrProperty> that you want to override. This field or property must be in the type being targeted by the current aspect instance.

The _second argument_ of `OverrideFieldOrProperty` is the name of the template property. This property must exist in the aspect class and, additionally:

* the template property must be annotated with the `[Template]` attribute,
* the template property must be of type `dynamic?`, or a type that is compatible with the type of the overridden property.

### Example: registry-backed class

The following aspects overrides properties so that they are written to and read from the Windows registry.

[!include[Registry Storage](../../../code/Caravela.Documentation.SampleCode.AspectFramework/RegistryStorage.cs)]

### Strongly-typed templates

Instead of providing a dynamically typed template, you can provide a template whose type is compatible with the type of the overridden property. 

### Overriding only one accessor

The property can have a setter, a getter, or both. If one accessor is not specified in the template, the corresponding accessor in the target code will not be overridden.

### Getting or setting the property value

If you have only worked with methods so far, you may be already used to use the `meta.Proceed()` method in your template. This method also works a property template: when called from the getter, it returns the field or property value; when called from the setter, it sets the field or property to the value of the `value` parameter.

If you need to get the property value from the setter, or if you need to set the property value to something else than the `value` parameter, you can do it by getting or setting the `meta.Target.FieldOrProperty.Value` property.

### Example: string normalization

This example illustrates a strongly-typed property template with a single accessor that uses the `meta.Target.FieldOrProperty.Value` expression to access the underlying field or property.

The following aspect can be applied to fields of properties of type `string`. It overrides the setter to trim and lower case the assigned value. 

[!include[Normalize](../../../code/Caravela.Documentation.SampleCode.AspectFramework/Normalize.cs)]

### Overriding with accessor templates

Advising fields or properties with the `OverrideFieldOrProperty` has the following limitations over the use of `OverrideFieldOrPropertyAccessors`:

* You cannot choose a template for each accessor separately.
* You cannot have an `async` or iterator getter template. (Not yet implemented in `OverrideFieldOrPropertyAccessors` anyway.)
* You cannot have generic templates.  (Not yet implemented in `OverrideFieldOrPropertyAccessors` anyway.)

To alleviate these limitations, you can use the method <xref:Caravela.Framework.Aspects.IAdviceFactory.OverrideFieldOrPropertyAccessors*> and provide one or two method templates: a getter template and/or a setter template.

The templates must fulfill the following conditions:

* Both templates must be annotated with the `[Template]` attribute.
* The getter template must be of signature `T Getter()`, where `T` is either `dynamic` or a type compatible with the target field or property.
* The setter template msst be of signature `void Setter(T value)`, where the name `value` of the first parameter is mandatory.

## Overriding events

Overriding events is possible using the <xref:Caravela.Framework.Aspects.IAdviceFactory.OverrideEventAccessors*> method. It follows the same principles than `OverridePropertyAccessors`.

It is possible to override the `add` and `remove` semantics of an event, but not yet the invocation of an event. Therefore, it is of little use and we are skipping the example.