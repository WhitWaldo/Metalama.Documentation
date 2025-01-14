---
uid: exposing-configuration
level: 300
---
# Exposing configuration

Some complex and widely-used aspects need a central, project-wide way to configure their compile-time behavior.

There are two complementary configuration mechanisms: MSBuild properties and the configuration API.

## Benefits

* **Central options of aspects**. When you provide a configuration API, the whole project can be configured from a single place. Without a configuration API, the aspect user must supply the configuration whenever a custom attribute is used.

* **Debug/Release-aware options**. Without a configuration API, setting options according to the `Debug`/`Release` build configuration can be impractical.

* **Run-time performance**. When decisions are taken at compile time and optimal run-time code is generated accordingly, the run-time execution of your app is faster.


## Consuming MSBuild properties

The simplest way for an aspect to accept a configuration property is to read an MSBuild property using the <xref:Metalama.Framework.Project.IProject.TryGetProperty*?text=IProject.TryGetProperty> method. MSBuild properties are not visible to aspects by default: you must instruct MSBuild to pass it to the compiler using the `CompilerVisibleProperty` item.

We recommend the following approach to consume a configuration property:

1. Create a file named `YourProject.targets` (the actual name of the file does not matter but the extension is important):

    ```xml
    <Project>
        <ItemGroup>
            <CompilerVisibleProperty Include="YourProperty" />
        </ItemGroup>
    </Project>
    ```


2. Include `YourProject.targets` in your project and mark it for inclusion under the `build` directory of your NuGet package. This ensures that the property will be visible to the aspect for all projects referencing your package. Your `csproj` file should look like this:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <!-- ... -->
        <ItemGroup>
            <None Include="YourProject.targets">
                <Pack>true</Pack>
                <PackagePath>build</PackagePath>
            </None>
        </ItemGroup>
        <!-- ... -->
    </Project>
    ```

3. To configure the aspect, users should set this property in the `csproj` file, like in the following snippet:

   ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <!-- ... -->
        <PropertyGroup>
            <YourProperty>TheValue</YourProperty>
        </PropertyGroup>
        <!-- ... -->
    </Project>
    ```

     > [!WARNING]
     > Neither line breaks nor semicolons are allowed in values of compiler-visible properties: they will cause your aspect to receive an incorrect value.


### Example

In the following example, the `Log` aspect reads the default category from the MSBuild property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ConsumingProperty.cs name="Consuming Property"]


## Exposing a configuration API

For more complex aspects, a set of MSBuild properties may not be sufficient. Instead, you can build a configuration API that your users will call from their project fabrics.

To create a configuration API:

1. Create a class that derives from <xref:Metalama.Framework.Project.ProjectExtension> and has a default constructor.
2. Optionally, override the <xref:Metalama.Framework.Project.ProjectExtension.Initialize*> method, which receives the <xref:Metalama.Framework.Project.IProject>.
3. In your aspect code, call the [IProject.Extension\<T>()](xref:Metalama.Framework.Project.IProject.Extension*) method, where `T` is your configuration class, to get the configuration object.
4. Optionally, create an extension method to the <xref:Metalama.Framework.Project.IProject> type to expose your configuration API, so that it is more discoverable. The class must be annotated with `[CompileTime]`.
5. To configure your aspect, users should implement a project fabric and access your configuration API using this extension method.

### Example

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.cs name="Consuming Property"]

