# OnionSeed.Helpers.Security

Contains helper methods for security-related functionality.

## Status

| Work in Progress                                                                                                                                                     |                                                                                                                                                            |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/TaffarelJr/OnionSeed.Helpers.Security.svg?logo=github)](https://github.com/TaffarelJr/OnionSeed.Helpers.Security) | [![GitHub issues](https://img.shields.io/github/issues-raw/TaffarelJr/OnionSeed.Helpers.Security.svg?logo=github)](https://github.com/TaffarelJr/OnionSeed.Helpers.Security) |

| Build Status (`master`) |                                                                                                                                                                                          |                                                                                                                                                                                                     |
|-------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Continuous Integration: | [![AppVeyor branch](https://img.shields.io/appveyor/ci/TaffarelJr/onionseed-helpers-security/master.svg?logo=appveyor)](https://ci.appveyor.com/project/TaffarelJr/onionseed-helpers-security)             | [![AppVeyor tests (branch)](https://img.shields.io/appveyor/tests/TaffarelJr/onionseed-helpers-security/master.svg?logo=appveyor)](https://ci.appveyor.com/project/TaffarelJr/onionseed-helpers-security)             |
| Latest Release:         | [![AppVeyor branch](https://img.shields.io/appveyor/ci/TaffarelJr/onionseed-helpers-security-te9hi/master.svg?logo=appveyor)](https://ci.appveyor.com/project/TaffarelJr/onionseed-helpers-security-te9hi) | [![AppVeyor tests (branch)](https://img.shields.io/appveyor/tests/TaffarelJr/onionseed-helpers-security-te9hi/master.svg?logo=appveyor)](https://ci.appveyor.com/project/TaffarelJr/onionseed-helpers-security-te9hi) |

| Published Releases                                                                                                                                    |                                                                                                                                 |
|-------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------|
| [![Nuget](https://img.shields.io/nuget/v/OnionSeed.Helpers.Security.svg?label=latest%20version&logo=nuget)](https://www.nuget.org/packages/OnionSeed.Helpers.Security/) | [![Nuget](https://img.shields.io/nuget/dt/OnionSeed.Helpers.Security.svg?logo=nuget)](https://www.nuget.org/packages/OnionSeed.Helpers.Security/) |

## Build

This solution consists of a [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standardP) class library and an [xUnit](https://xunit.net/) test project. It can be built with any version of [Visual Studio](https://visualstudio.microsoft.com/vs/) (2017+) or [Visual Studio Code](https://code.visualstudio.com/).

The official build/[CI](https://en.wikipedia.org/wiki/Continuous_integration) process is borrowed from another Git repository - [OnionSeed.Build](https://github.com/TaffarelJr/OnionSeed.Build) - and imported here as a Git [submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules). This shared code encapsulates the entire process using [Cake](https://cakebuild.net/) tasks - running them locally produces the same result as running them on a build server (currently [AppVeyor](https://www.appveyor.com/)).

Windows command prompt:
```
powershell .\build.ps1
```

[Powershell](https://docs.microsoft.com/en-us/powershell/scripting/overview?view=powershell-6):
```
.\build.ps1
```

Mac\Linux:
```
./build.sh
```
