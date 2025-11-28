F-Profiler Common
=================

[![NuGet](https://img.shields.io/nuget/v/Alma.Profiler.Common.svg)](https://www.nuget.org/packages/Alma.Profiler.Common)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Alma.Profiler.Common.svg)](https://www.nuget.org/packages/Alma.Profiler.Common)
[![Tests](https://github.com/alma-oss/fprofiler-common/actions/workflows/tests.yaml/badge.svg)](https://github.com/alma-oss/fprofiler-common/actions/workflows/tests.yaml)

> Library for common profiler types, shared between client and server.

---

## Install

Add following into `paket.references`
```
Alma.Profiler.Common
```

## Release
1. Increment version in `Profiler.Common.fsproj`
2. Update `CHANGELOG.md`
3. Commit new version and tag it

## Development
### Requirements
- [dotnet core](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial)

### Build
```bash
./build.sh build
```

### Tests
```bash
./build.sh -t tests
```
