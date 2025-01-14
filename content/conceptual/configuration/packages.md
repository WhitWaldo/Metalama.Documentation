---
uid: packages
level: 300
---

# List of NuGet packages

Metalama is composed of many NuGet packages, but some are used only for testing or troubleshooting and will never be included in your public packages.

## Package list

| Package Name | Scenarios | Description |
|--|--|--|
| Metalama.Framework.Redist | Run-Time Execution | The only package required to execute code built with Metalama. It does not contain or reference the assets to _build_ with Metalama. |
| Metalama.Framework | Compiling, Testing, Introspection | The typical top-level package for a project that defines Metalama aspects.
| Metalama.Compiler | Compiling | Replaces Microsoft's C# compiler (Roslyn) with Metalama's own fork.
| Metalama.Compiler.Sdk | Compiling | Defines the extensibility API of `Metalama.Compiler`.
| Metalama.Framework.Sdk | Compiling | Defines the low-level extensibility API of `Metalama.Framework` (extensions using the Roslyn API). |
| Metalama.Testing.AspectTesting | Testing | The top-level package for test projects. References `Metalama.Framework` but inhibits most of its behaviors. See <xref:aspect-testing>. |
| Metalama.Framework.Engine | Testing, Introspection | The principal implementation assembly of Metalama. It is required by the testing and introspection packages. It would be best if you did not use it directly because there is no forward compatibility promise for this package. |
| Metalama.Framework.Introspection | Introspection | Allows querying the code model representing the output of the Metalama process.
| Metalama.Framework.Workspaces | Introspection | Allows any application to load a Visual Studio project or solution and to represent its code model and the Introspection of the Metalama process.

## Package diagrams

### Legend

```mermaid
graph TD

    t[Test and introspection]
    class t testing
    classDef testing fill:orange;

    c[Compile-time]
    class c compileTime
    classDef compileTime fill:yellow;

    r[Run-time]
    class r runTime
    classDef runTime fill:lightgreen;

    classDef framework fill:red;
    f[Core]
    class f framework

    u[Your Code]
    classDef userCode fill:white
    class u userCode

```

### Building, executing and testing

```mermaid
graph BT
    YourCode -- references --> Metalama.Framework
    YourTests -- references --> YourCode
    Metalama.Framework -- references --> Metalama.Framework.Redist
    Metalama.Framework -- references-->  Metalama.Compiler
    Metalama.Framework -- contains --> analyzers((compiler add-ins))
    Metalama.Testing.AspectTesting -- references --> Metalama.Framework.Redist
    Metalama.Testing.AspectTesting -- inhibits --> Metalama.Framework
    Metalama.Testing.AspectTesting -- references--> Metalama.Framework.Engine
    Metalama.Testing.AspectTesting -- references--> xUnit
    Metalama.Framework.Engine -- references --> Metalama.Framework
    YourTests -- references --> Metalama.Testing.AspectTesting
    IDE -- loads --> analyzers
    Metalama.Compiler -- loads --> analyzers

    YourCode[Your Aspects]
    YourTests[Your Aspect Tests]

    Metalama.Compiler -- contains --> compiler((full compiler))

    classDef testing fill:orange;
    class Metalama.Testing.AspectTesting testing;
    class Metalama.Framework.Engine testing;
    class Metalama.Framework.Introspection testing;
    class Metalama.Framework.Workspaces testing;
    class Metalama.LinqPad testing;

    classDef compileTime fill:yellow;
    class Metalama.Compiler compileTime;
    class Metalama.Compiler.Sdk compileTime;
    class Metalama.Framework.Sdk compileTime;

    classDef runTime fill:lightgreen;
    class Metalama.Framework.Redist runTime;

    classDef userCode fill:white;
    class YourCode userCode;
    class YourTests userCode;
    class LinqPad userCode;

    classDef framework fill:red;
    class Metalama.Framework.Redist framework;
    class Metalama.Framework framework;


```

### Introspection

```mermaid
graph BT
    Metalama.Framework.Introspection -- references --> Metalama.Framework
    Metalama.Framework.Workspaces -- references --> Metalama.Framework.Engine
    Metalama.Framework.Workspaces -- references --> Metalama.Framework.Introspection
    Metalama.Framework.Engine -- references --> Metalama.Framework
    Metalama.Framework.Engine -- references --> Roslyn
    Metalama.Framework.Engine -- references --> Metalama.Framework.Introspection
    Metalama.LinqPad -- references --> Metalama.Framework.Workspaces
    LinqPad -- references --> Metalama.LinqPad
    LinqPad -- references --> Metalama.Framework.Workspaces
    YourApp -- references --> Metalama.Framework.Workspaces

    LinqPad[LinqPad Queries]
    YourApp[Your Introspection App]

    classDef testing fill:orange;
    class Metalama.Testing.AspectTesting testing;
    class Metalama.Framework.Engine testing;
    class Metalama.Framework.Introspection testing;
    class Metalama.Framework.Workspaces testing;
    class Metalama.LinqPad testing;

    classDef compileTime fill:yellow;
    class Metalama.Compiler compileTime;
    class Metalama.Compiler.Sdk compileTime;
    class Metalama.Framework.Sdk compileTime;

    classDef userCode fill:white;
    class LinqPad userCode;
    class YourApp userCode;

    classDef framework fill:red;
    class Metalama.Framework framework;


```

### SDK

```mermaid
graph BT

  YourPackage -- contains --> YourCode
  YourPackage -- contains --> YourWeaver

   YourCode -- references --> Metalama.Framework

    Metalama.Compiler -. loads .-> YourWeaver
    YourWeaver -- references --> Metalama.Framework.Sdk


    YourCode[Your Attributes]
    YourWeaver[Your Weavers]
    YourWeaver -- references --> YourCode
    YourPackage[Your Package]

    Metalama.Framework -- references --> Metalama.Framework.Redist
    Metalama.Framework -- references-->  Metalama.Compiler
    Metalama.Compiler.Sdk -- references --> Roslyn
    Metalama.Framework.Sdk -- references --> Metalama.Compiler.Sdk



    classDef testing fill:orange;
    class Metalama.Testing.AspectTesting testing;
    class Metalama.Framework.Engine testing;
    class Metalama.Framework.Introspection testing;
    class Metalama.Framework.Workspaces testing;
    class Metalama.LinqPad testing;

    classDef compileTime fill:yellow;
    class Metalama.Compiler compileTime;
    class Metalama.Compiler.Sdk compileTime;
    class Metalama.Framework.Sdk compileTime;

    classDef runTime fill:lightgreen;
    class Metalama.Framework.Redist runTime;

    classDef userCode fill:white;
    class YourCode userCode;
    class YourWeaver userCode;
    class YourPackage userCode;

    classDef framework fill:red;
    class Metalama.Framework.Redist framework;
    class Metalama.Framework framework;


```
