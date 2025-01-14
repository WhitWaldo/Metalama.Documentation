---
uid: applying-live-templates
level: 100
---

# Using live templates

Aspects, as you already know, modify the code during compilation, but let your source code intact. Live templates, by contrast, transform your source code, within your editor. After you apply a live template to a declaration, you can edit the code that has been generated. Live templates are a one-off. Unlike aspects, if the author of the live template modifies the code generation rules after you applied the template to your source code, your source code will not be modified.

You can use live templates from the refactoring menu, also named the _lightbulb_ or _screwdriver_ menu.


## Step 1. Add a project or package reference

Like aspects, live templates are declared in projects or NuGet packages that you add as references to your project.

The first step is to add the aspect library to your project using a `<ProjectReference>` or a `<PackageReference>`. This step brings the aspect to the refactoring menu. You can remove the reference after the operation if needed.

> [!NOTE]
> If you only use live templates from this project, consider using the `PrivateAssets="all"` option so reference does not impact other projects consuming your project.

## Step 2. Apply the live template

1. Position the caret on the name of the declaration on which you want to apply the aspect.
2. Click on the lightbulb or refactoring icon, and choose to _Apply live template_.
    ![Screenshot](images/LiveTemplate1.png)

3. Choose the aspect that you want to apply
    ![Screenshot](images/LiveTemplate2.png)

> [!NOTE]
> The author of the live template can customize the appearance of the live template in the refactoring menu. It may be under a different menu item than _Apply live template_.

