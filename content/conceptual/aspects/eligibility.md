---
uid: eligibility
level: 300
---

# Defining the eligibility of aspects

Most aspects are designed and implemented for specific kinds of target declarations. For instance, you may decide that your caching aspect will not support `void` methods or methods with `out` or `ref` parameters. As the author of the aspect, it is essential that you make sure that users of your aspect apply it only to the declarations that you expect. Otherwise, the aspect will cause build errors with confusing messages or even incorrect run-time behavior.

## Benefits

Defining the eligibility of an aspect has the following benefits:

* **Predictable behavior**. Applying an aspect to a declaration the aspect was not designed or tested for can be a very confusing experience for your users because of error messages they may not understand. As the author of the aspect, it is your responsibility to ensure that using your aspect is easy and predictable.
* **Standard error messages**. All eligibility error messages are standard. They are easier to understand for aspect users. 
* **Relevant suggestions in the IDE**. The IDE will only propose code action in the refactoring menu for eligible declarations.


## Defining eligibility

To define the eligibility of your aspect, implement or override the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method of the aspect. Use the `builder` parameter, which is of type <xref:Metalama.Framework.Eligibility.IEligibilityBuilder`1>, to specify the requirements of your aspect. For instance, use <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustNotBeAbstract*?text=builder.MustNotBeAbstract()> to require a non-abstract method.

Several predefined eligibility conditions are implemented by the <xref:Metalama.Framework.Eligibility.EligibilityExtensions> static class. You can add a custom eligibility condition by calling <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustSatisfy*> and by providing a lambda expression. This method also expects the user-readable string that should be included in the error message when the user attempts to add the aspect to an ineligible declaration.

>[!NOTE]
> Your implementation of <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> must not reference any instance member of the class. Indeed, this method is called on an instance obtained using `FormatterServices.GetUninitializedObject`, that is, _without invoking the class constructor_.

### Example

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Eligibility.cs name="Eligibility"]

## When to emit custom errors instead?

It may be tempting to add an eligibility condition for every requirement of your aspect instead of emitting a custom error message. However, this may be confusing for the user.

As a rule of thumb, you should use eligibility to define those declarations for which it makes sense to apply the aspect or not and use error messages when the aspect makes sense on the declaration. Still, some contingency may prevent the aspect from being used, and this is where you should report errors.

For details about reporting errors, see <xref:diagnostics>.

### Example 1

Adding a caching aspect to a `void` method does not make sense and should be addressed with eligibility. However, the fact that your aspect does not support methods returning a collection is a limitation caused by your particular implementation and should be reported using a custom error.

### Example 2

Adding a dependency injection aspect to an `int` or `string` field does not make sense, and this condition should be expressed using the eligibility API. However, the fact that your implementation of the aspect requires the field to be non-read-only is a contingency and should be reported as an error.

### Example 3

The following example expands the previous one, reporting custom errors when the target class does not define a field `logger` of type `TextWriter`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/EligibilityAndValidation.cs name="Eligibility and Validation"]

