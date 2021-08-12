---
uid: sdk
---

# Caravela.Framework.Sdk

## Introduction

Caravela.Framework.Sdk offers direct access to Caravela's underlying code-modifying capabilities through [Roslyn-based APIs](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/compiler-api-model). 

Unlike Caravela.Framework, our high-level API, aspects built with Caravela.Framework.Sdk must be in their own project, separate from
the code they transform. Caravela.Framework.Sdk is much more complex and unsafe than Caravela.Framework and is not meant for "everyday" use.
We expect that most developers will use Caravela.Framework.

## Maturity

Caravela.Framework.Sdk is in preview, but it is mostly feature complete.

## Implementing an aspect

### Step 1. Define the public interface of your aspect (a custom attribute)

1. Create an "interface" project (it must target .NET Standard 2.0).
2. Add a reference to the _Caravela.Framework_ package (but not  _Caravela.Framework.Sdk_).
3. Define a custom attribute like this:

    ```cs
    [AttributeUsage(AttributeTargets.Assembly)]
    public class VirtuosityAspect : Attribute, IAspect { }
    ```

### Step 2. Create the weaver for this project

1. Create a project that targets .NET Standard 2.0 and name it with the `.Weaver` suffix (by convention).
2. Add a reference the _Caravela.Framework.Sdk_ package.
3. Add a reference to the _first_ project project. In the `<ProjectReference>` in your csproj file, additionally specify `PrivateAssets="all"`.
3. Add a class that implements the following interface:

    ```c#
    public interface IAspectWeaver
    {
        CSharpCompilation Transform(AspectWeaverContext context);
    }
    ```

    where the `AspectWeaverContext` is defined as:

    ```c#
    public sealed class AspectWeaverContext
    {
        public CSharpCompilation Compilation { get; }
        public IReadOnlyList<AspectInstance> AspectInstances { get; }
        public IDiagnosticSink Diagnostics { get; }
    }
    ```

    An implementation of this interface receives a Roslyn compilation and can modify it in any way using Roslyn syntax and semantic APIs. It also receives information about where its associated attribute has been applied (called "aspect instances"). And it can produce diagnostics (errors and warnings) if it has been used incorrectly.

    Note that because Caravela replaces the compiler used to build your code, but not the one used by the IDE, any modifications made here will not affect code completion.


4. Add these custom attributes to your class: 
   
   ```cs
    [CompilerPlugin, AspectWeaver(aspectType: typeof(VirtuosityAspect))]
    ```


For instance:

```cs
[CompilerPlugin, AspectWeaver(aspectType: typeof(VirtuosityAspect))]
class VirtuosityWeaver : IAspectWeaver
{
    public CSharpCompilation Transform(AspectWeaverContext context)
    {
        // Details skipped.
    }
}
```

### Step 3. Use your aspect

In a _third_ project:

1. Reference the _first_ project (the one defining the custom attribute). Add `OutputItemType="Analyzer" ReferenceOutputAssembly="false"` to its `<ProjectReference>` in the csproj file.
2. Reference the _second_ project (the one defining the weaver). Add `OutputItemType="Analyzer"` to its `<ProjectReference>`.
3. Reference the _Caravela.Framework_ package.
4. Use the aspect by applying the attribute:

    ```c#
    [assembly: VirtuosityAspect]
    ```

## Packaging your aspect with its weaver

Packing a weaver project that was created using the steps above will produce a package that contains all parts of the aspect, including its dependencies (simplifying step 3 above), but with a name ending with `.Weaver`.

To fix this:

1. Specify `<PackageId>` without the `.Weaver` suffix inside a `<PropertyGroup>` in the csproj of the second project.
2. Specify `<PackageId>` with a `.Redist` suffix inside a `<PropertyGroup>` in the csproj of the first project.
3. (Optional) Add `<IsPackable>false</IsPackable>` to a `<PropertyGroup>` in the csproj of the first project, to prevent you from creating a package that would contain just the attribute.

## Examples

Available examples of Caravela.Framework.Sdk weavers are:

* [Caravela.Open.Virtuosity](https://github.com/postsharp/Caravela.Open.Virtuosity): makes all possible methods in a project `virtual`
* [Caravela.Open.AutoCancellationToken](https://github.com/postsharp/Caravela.Open.AutoCancellationToken): automatically propagates `CancellationToken` parameter
* [Caravela.Open.DependencyEmbedder](https://github.com/postsharp/Caravela.Open.DependencyEmbedder): bundles .NET Framework applications into a single executable file

The Caravela.Open.Virtuosity repository contains very little logic, so it can be used as a template for your own weavers.
