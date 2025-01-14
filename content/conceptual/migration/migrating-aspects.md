---
uid: migrating-aspects
---

# Migrating custom aspects to Metalama

## Step 0. Learn Metalama on some sandbox project

Porting your PostSharp aspects to Metalama should not be your first contact with Metalama. We suggest that you learn the new product on a new project. Even better: write a prototype of aspects in a sandbox project without PostSharp. You will probably be unable to run your application or even run all your unit tests while migrating aspects, so it is far better to do the learning in a sandbox project.

## Step 1. Create a list of aspects and assess their importance

Go through all projects and identify all used aspects.

Determine the importance of these aspects:

1. aspects required for unit tests to succeed,
2. aspects required for the application to run,
3. aspects required in production but not in a development environment, such as logging, profiling, or caching.

Then sort the aspects by importance. You will later use this list to decide how to implement the aspects.

### Identify uses of multicasting

For each aspect, determine if _attribute multicasting_ is used, and how. All PostSharp aspects implicitly inherit from the `MulticastAttribute`, so all aspects _can_ be multicast, but not all actually are. To determine for which aspects the multicasting feature is used, do a "Find in Files" for the following substrings: `AttributeTarget` or `AttributeExclude`.

If you only find that an aspect is multicast from assembly-level custom attributes, it does not mean that your Metalama aspect will have to implement multicasting. Indeed, it is more elegant to use fabrics than assembly-level multicasting. See <xref:fabrics> for details. If the source code uses multicasting from classes or structs, your Metalama aspect must implement multicasting.

## Step 2. Verify that it's a good time to migrate

Read <xref:when-migrate> and verify that Metalama supports the target frameworks of your projects. Chances are that they are unless the projects still target obsolete platforms.

Verify in <xref:migration-feature-status> if your projects are using a feature of PostSharp that has not been ported to Metalama. If your project uses ready-made aspects from the `PostSharp.Patterns` namespace, verify that these aspects have been ported.

## Step 3. Verify that your projects are in a clean state

You will know that you will be done with the migration when your projects compile without any new warnings and your unit tests will be green. So, before you start, make sure that your projects are all in a clean state:

* Create a new branch for this work in your source repository.
* Verify that the projects build without error. If possible, address any warnings in your code.
* Verify that all unit tests are successful.
* When your projects are free of errors, warnings, and failed tests, commit and push your branch.

## Step 4. Change PostSharp package references to Metalama.Migration and check errors

In all projects, replace all references to the `PostSharp` package with references to the `Metalama.Migration` package. You can probably do this with a "Replace in Files" operation. However, if you use  `packages.config`, consider migrating your projects to the new `PackageReference` format. If you cannot, use the NuGet Package Manager UI, uninstall the `PostSharp` package and install the `Metalama.Migration` package.

The `Metalama.Migration` package contains the public API of PostSharp, but not its implementation. Instead, it contains `[Obsolete]` annotations with indications about the equivalent class or method in Metalama.

If you build these projects at this point, you will see a lot of warnings.

If your code uses APIs that have not been ported to PostSharp yet, you will also see errors. If you see errors, think if you can find a workaround. If not, you may have to delay the migration effort.

Do not run your application or your unit tests at this point. It is not going to work until you port all critical aspects.

## Step 5. Create a separate project for aspect tests

Since your current test base is probably going to be broken for a long time now -- until all your critical aspects will be ported -- it's a good idea to set up an infrastructure where you will be able to test your aspects one by one.

For details, see <xref:aspect-testing>.

## Step 6. Implement the individual aspects

Take the list you created in step 1 and start from the most essential aspects.

Metalama is not a mere update of PostSharp, it has an entirely different architecture and approach. Therefore, you will need to rewrite every single aspect from scratch.

To determine the Metalama equivalent to any PostSharp API, you can get indications from the `[Obsolete]` warnings reported by the `Metalama.Migration` package on your code. We do not attempt to write here a tutorial about porting a specific aspect to Metalama because, if you have gone through Step 0 as we suggested, you should already know that at this point. As we mentioned, it is better to learn Metalama _before_ doing the actual migration work.

We strongly recommend writing aspect tests for each aspect that you are implementing.

If you have identified in Step 1 that the aspect should support multicasting, see <xref:migrating-multicasting>.

## Step 7. Migrate usages of multicasting

The best way to migrate assembly multicasting is to use a project fabric as described in <xref:fabrics-adding-aspects>.

For type-level multicasting, if you have built multicasting into your Metalama aspects as described in <xref:migrating-multicasting>, it should be enough to do a big "Replace in Files" to replace the namespace `PostSharp.Extensibility` to `Metalama.Extensions.Multicast`.

## Step 8. Migrate configuration

If you have configuration or multicasting in a PostSharp file like `postsharp.config` or `MyProject.psproj`, they should be migrated to project fabrics as described in <xref:fabrics-configuration>.

For details, see <xref:migrating-configuration>.

## Step 9. Last project clean up

We recommend that you address all warnings at this point.

If your Metalama aspects have been properly implemented, all your tests should now be green and the application should be running as before.

When this is the case, replace the reference to the `Metalama.Migration` package to `Metalama.Framework`.

You are done.
