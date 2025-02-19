---
uid: fabrics-configuration
---

# Configuring aspects with fabrics

Some aspect libraries may expose a compile-time configuration API that influences how the aspects generate code.

You can set the configuration of aspect libraries in project fabrics or transitive project fabrics.

The following example demonstrates a <xref:Metalama.Framework.Fabrics.ProjectFabric>  that registers a dependency injection framework.


```cs
public class Fabric : ProjectFabric
{
public override void AmendProject( IProjectAmender amender )
{
    amender.Project.DependencyInjectionOptions().RegisterFramework( new LoggerDependencyInjectionFramework() );
}
}
```

