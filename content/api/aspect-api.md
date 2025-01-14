---
uid: aspect-api
---

# Aspect API documentation

You will use these namespaces while writing your own aspects or fabrics.


| Namespace                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Framework.Aspects>     | This is the main namespace for creating your own aspects.                                                                                                        |
| <xref:Metalama.Framework.Code>        | This namespace contains the object representation of source code: interfaces that represent types, methods, fields, ...                                                |
| <xref:Metalama.Framework.CodeFixes>   | This namespace allows your aspects to suggest code fixes, accessible at design time from the IDE.                                                            |
| <xref:Metalama.Framework.Diagnostics> | This namespace allows your aspects to report or suppress errors, warnings or information.                                                                           |
| <xref:Metalama.Framework.Eligibility> | This namespace allows your aspects to declare to which declarations they can be validly applied.                                                                    |
| <xref:Metalama.Framework.Fabrics>    | This namespace provides the ability to add aspects or validators to whole projects and namespaces, and allows you to configure aspects.                               |
| <xref:Metalama.Framework.Metrics>        | This namespace allows you to read predefined code metric, and to implement your own metrics. Metrics are useful in validators and LinqPad queries.                   |
| <xref:Metalama.Framework.Project>        | This namespace exposes the object model of the project being processed, as well as the service provider.                                                       |
| <xref:Metalama.Framework.RunTime>     | This namespace contains the classes that are used at run time. All other namespaces are used at compilation time only.                                         |
| <xref:Metalama.Framework.Validation>  | This namespace allows you to build aspects that can validate user code against your own rules. You can validate both the target of the aspect and _references_ to that target. |

