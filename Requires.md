# For using this solution

Under the folder `LocalNuGetPackage` in this solution, place into another Visual Studio solution, build followed by taking the file `ConfigurationLibrary.1.0.1.nupkg` and placing it under `C:\Dotnetland\NuGetLocal`. This is required for the project `EntityFrameworkCoreHelpers`.

**Note** `C:\Dotnetland\NuGetLocal` is just a suggestion.

Make sure to setup a feed[^1] under options in Visual Studio as per below.

![Local](assets/local.png)

[^1]: [Local NuGet package feeds](https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds) are simply hierarchical folder structures on your local network (or even just your own computer) in which you place packages. These feeds can then be used as package sources with all other NuGet operations using the CLI, the Package Manager UI, and the Package Manager Console.