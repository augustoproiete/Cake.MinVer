| README.md |
|:---|

<div align="center">

![Cake.MinVer](assets/cake-minver-logo.png)

</div>

<h1 align="center">Cake.MinVer</h1>
<div align="center">

Cross-platform addin for the [Cake](https://cakebuild.net) build automation system that enables you to use [MinVer](https://github.com/adamralph/minver) for versioning projects using Git tags. The latest version of Cake.MinVer targets .NET 8.0, .NET 7.0, and .NET 6.0, and runs on Windows, Linux, and macOS.

[![NuGet Version](https://img.shields.io/nuget/v/Cake.MinVer.svg?color=blue&style=flat-square)](https://www.nuget.org/packages/Cake.MinVer/) [![Stack Overflow](https://img.shields.io/badge/stack%20overflow-cakebuild-orange.svg?style=flat-square)](http://stackoverflow.com/questions/tagged/cakebuild)

</div>

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Prerequisites

In order to use Cake.MinVer, you will need to install [Cake](https://www.nuget.org/packages/Cake.Tool/) and [MinVer](https://www.nuget.org/packages/minver-cli/) dotnet tools either as [local tools](#using-cakeminver-with-cake-and-minver-installed-as-local-tools) (recommended) or as [global tools](#using-cakeminver-with-cake-and-minver-installed-as-global-tools), and these tools require [.NET Core SDK 2.1.300 or later](https://www.microsoft.com/net/download) and [Git](https://git-scm.com).

## Getting started :rocket:

This addin exposes the functionality of [MinVer](https://github.com/adamralph/minver) to the Cake DSL by being a very thin wrapper around its command line interface; this means that you can use Cake.MinVer in the same way as you would normally use [minver-cli](https://github.com/adamralph/minver#can-i-use-minver-to-version-software-which-is-not-built-using-a-net-sdk-style-project), but with a Cake-friendly interface.

First of all, you need to import Cake.MinVer in your build script by using the [`addin`](http://cakebuild.net/docs/fundamentals/preprocessor-directives) directive:

```csharp
#addin "nuget:?package=Cake.MinVer&version=4.0.0"
```

_Make sure the `&version=` attribute references the [latest version of Cake.MinVer](https://www.nuget.org/packages/Cake.MinVer/) compatible with the Cake runner that you are using. Check the [compatibility table](#compatibility) to see which version of Cake.MinVer to choose_.

Next, call `MinVer()` in order to get the version information using the default settings:

```csharp
#addin "nuget:?package=Cake.MinVer&version=4.0.0"

var version = MinVer();

Task("Example")
    .Does(context =>
{
    context.Information($"Version: {version.Version}");
    context.Information($"Major: {version.Major}");
    context.Information($"Minor: {version.Minor}");
    context.Information($"Patch: {version.Patch}");
    context.Information($"PreRelease: {version.PreRelease}");
    context.Information($"BuildMetadata: {version.BuildMetadata}");
});

RunTarget("Example");
```

### Using Cake.MinVer with Cake and MinVer installed as local tools

Install Cake and MinVer as local tools in your project (one-time setup):

```shell
dotnet new tool-manifest
dotnet tool install cake.tool
dotnet tool install minver-cli
git add .config/dotnet-tools.json
git commit -m "Install Cake & MinVer dotnet tools"
```

Then, before buiding your project (e.g. in your CI server), ensure that Cake and MinVer are available before running your Cake build, by running `dotnet tool restore`:

```shell
dotnet tool restore
dotnet cake
```

### Using Cake.MinVer with Cake and MinVer installed as global tools

Install Cake and MinVer as global tools in your project (one-time setup):

```shell
dotnet tool install --global cake.tool
dotnet tool install --global minver-cli
```

## MinVer properties available to your Cake build script

| Property             | Type     | Description                                 |
| -------------------- | -------- | ------------------------------------------- |
| Version              | `string` | The original, non-normalized version string |
| Major                | `int`    | The major version number                    |
| Minor                | `int`    | The minor version number                    |
| Patch                | `int`    | The patch version number                    |
| PreRelease           | `string` | The pre-release extension                   |
| IsPreRelease         | `bool`   | `true` if `PreRelease` is not null or empty |
| BuildMetadata        | `string` | The build metadata extension                |
| AssemblyVersion      | `string` | `{Major}.0.0.0`                             |
| FileVersion          | `string` | `{Major}.{Minor}.{Patch}.0`                 |
| InformationalVersion | `string` | _same as `Version` above_                   |
| PackageVersion       | `string` | _same as `Version` above_                   |

## MinVer settings you can customize

| Property               | Type                  | Description                                                                               |
| ---------------------- | --------------------- | ----------------------------------------------------------------------------------------- |
| AutoIncrement          | `MinVerAutoIncrement` | The version part to be automatically incremented: `Default`, `Major`, `Minor`, or `Patch` |
| BuildMetadata          | `string`              | The build metadata                                                                        |
| DefaultPreReleasePhase | `string`              | The default pre-release phase                                                             |
| MinimumMajorMinor      | `string`              | The minimum major and minor version                                                       |
| Repo                   | `DirectoryPath`       | The working directory for MinVer to use                                                   |
| TagPrefix              | `string`              | The tag prefix                                                                            |
| Verbosity              | `MinVerVerbosity`     | The verbosity: `Default`, `Error`, `Warn`, `Info`, `Debug`, `Trace`                       |

For more details on how MinVer works,  check its [documentation](https://github.com/adamralph/minver#usage).

### Using Cake.MinVer with custom settings

You can define your settings using an instance of `MinVerSettings`, for example:

```csharp
var settings = new MinVerSettings()
{
    AutoIncrement = MinVerAutoIncrement.Minor,
    DefaultPreReleasePhase = "preview",
    MinimumMajorMinor = "2.5",
    TagPrefix = "v",
    Verbosity = MinVerVerbosity.Trace,
};

var version = MinVer(settings);
```

Alternatively, you can define your settings using Cake's configurator pattern:

```csharp
var version = MinVer(settings => settings
    .WithMinimumMajorMinor("2.5")
    .WithAutoIncrement(MinVerAutoIncrement.Minor)
    .WithDefaultPreReleasePhase("preview")
    .WithMinimumMajorMinor("2.5")
    .WithTagPrefix("v")
    .WithVerbosity(MinVerVerbosity.Trace)
);
```

### Usage Examples

In the [sample](sample/) folder, there are several examples of usage:

- [Default settings](sample/minver.cake)
- Custom settings with `MinVerSettings` instance
  - [Increment the minor version](sample/minver-settings-auto-increment.cake)
  - [Set the build metadata](sample/minver-settings-build-metadata.cake)
  - [Set the default pre-release phase](sample/minver-settings-default-pre-release-phase.cake)
  - [Set the minimum major and minor version](sample/minver-settings-minimum-major-minor.cake)
  - [Set the working directory](sample/minver-settings-repo.cake)
  - [Set the tag prefix](sample/minver-settings-tag-prefix.cake)
  - [Set the verbosity](sample/minver-settings-verbosity.cake)
- Custom settings with configurator pattern
  - [Increment the minor version](sample/minver-configurator-auto-increment.cake)
  - [Set the build metadata](sample/minver-configurator-build-metadata.cake)
  - [Set the default pre-release phase](sample/minver-configurator-default-pre-release-phase.cake)
  - [Set the minimum major and minor version](sample/minver-configurator-minimum-major-minor.cake)
  - [Set the working directory](sample/minver-configurator-repo.cake)
  - [Set the tag prefix](sample/minver-configurator-tag-prefix.cake)
  - [Set the verbosity](sample/minver-configurator-verbosity.cake)

## Compatibility

Cake.MinVer is compatible with all [Cake runners](https://cakebuild.net/docs/running-builds/runners/) that run on .NET 5 or .NET Core 2.1+, and below you can find which version of Cake.MinVer you should use based on the version of the Cake runner you're using.

| Cake runner     | Cake.MinVer       | Cake addin directive                                |
|:---------------:|:-----------------:| --------------------------------------------------- |
| 4.0.0 or higher | 4.0.0 or higher   | `#addin "nuget:?package=Cake.MinVer&version=4.0.0"` |
| 3.0.0           | 3.0.0             | `#addin "nuget:?package=Cake.MinVer&version=3.0.0"` |
| 2.0.0 - 2.3.0   | 2.0.0             | `#addin "nuget:?package=Cake.MinVer&version=2.0.0"` |
| 1.0.0 - 1.3.0   | 1.0.0 - 1.0.1     | `#addin "nuget:?package=Cake.MinVer&version=1.0.1"` |
| 0.33.0 - 0.38.5 | 0.2.0             | `#addin "nuget:?package=Cake.MinVer&version=0.2.0"` |
| < 0.33.0        | _N/A_             | _(not supported)_                                   |

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Release History

Click on the [Releases](https://github.com/cake-contrib/Cake.MinVer/releases) tab on GitHub.

---

_Copyright &copy; 2020-2024 C. Augusto Proiete & Contributors - Provided under the [MIT License](LICENSE)._
