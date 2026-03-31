# AGENTS.md — Alma.Profiler.Common

## Project Purpose

F# library containing common profiler types shared between client and server applications. Provides the shared type definitions used by profiler client and server implementations. Also Fable-compatible. Published as NuGet package `Alma.Profiler.Common`.

## Tech Stack

- **Language:** F# (.NET 10), Fable-compatible
- **Framework:** .NET SDK library
- **Package management:** Paket
- **Build system:** FAKE (F# Make) via `build.sh`
- **Linting:** fsharplint
- **CI/CD:** GitHub Actions
- **Key dependencies:** `FSharp.Core ~> 10.0` (minimal dependency footprint)

## Commands

```bash
# Install dependencies
dotnet tool restore && dotnet paket install

# Build
./build.sh build

# Run tests
./build.sh -t tests

# Lint
dotnet fsharplint lint src/Alma.Fable.Profiler.Common/Alma.Fable.Profiler.Common.fsproj
```

## Project Structure

```
fprofiler-common/
├── src/
│   └── Alma.Fable.Profiler.Common/
│       ├── Alma.Fable.Profiler.Common.fsproj  # Main project (PackageId: Alma.Profiler.Common, v10.0.0)
│       ├── AssemblyInfo.fs                     # Auto-generated
│       ├── Profiler.Common.fs                  # Core profiler types
│       └── paket.references                    # FSharp.Core only
├── build/
│   ├── build.fsproj                            # FAKE build project
│   ├── Build.fs
│   └── AssemblyInfo.fs
├── build.sh                                    # Build entry script
├── paket.dependencies                          # Top-level dependencies
├── fsharplint.json                             # Lint configuration
├── CHANGELOG.md
└── .github/workflows/
    ├── tests.yaml                              # Tests on PRs and nightly
    ├── pr-check.yaml                           # Fixup commit blocker, ShellCheck
    └── publish.yaml                            # NuGet publish on tags
```

## Architecture

Pure type-definition library — a single source file (`Profiler.Common.fs`) containing shared profiler domain types. This is the common contract between profiler client and server implementations.

**Fable compatibility** — the `.fsproj` includes Fable content packaging.

## Build System (FAKE)

Standard library target chain: `Clean → AssemblyInfo → Build → Lint → Tests → Release → Publish`

## CI/CD

- **tests.yaml** — runs on PRs and nightly
- **pr-check.yaml** — blocks fixup commits, runs ShellCheck
- **publish.yaml** — publishes to NuGet on semver tags

## Release Process

1. Increment `<Version>` in `src/Alma.Fable.Profiler.Common/Alma.Fable.Profiler.Common.fsproj`
2. Update `CHANGELOG.md`
3. Commit, tag with version, push

## Conventions

- Minimal dependencies — only `FSharp.Core`
- Source code under `src/Alma.Fable.Profiler.Common/`
- Must remain Fable-compatible
- Types should be plain F# discriminated unions / records

## Pitfalls

- **No tests** — no test project exists currently
- **No Docker** — pure library
- **Fable compatibility** — code must work in both .NET and Fable
- **Source location** — source is in `src/Alma.Fable.Profiler.Common/`, not project root
- **Package naming** — project folder is `Alma.Fable.Profiler.Common` but NuGet PackageId is `Alma.Profiler.Common`
- **Paket, not NuGet CLI** — use `dotnet paket install`
