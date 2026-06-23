# Preferred Patterns

## Core Principles

- **Wrap, don't pass raw strings.** Every textual field has a dedicated single-case union (`Token`, `Id`, `Label`, `Value`, `ValueDetail`, `Unit`, `Link`). Construct these wrappers explicitly so the type system keeps the roles distinct.
- **Treat `Color` as a closed set.** Only `Yellow`, `Green`, `Red`, and `Gray` exist; pattern matches over `Color` can be exhaustive.
- **Optional fields mean "absent", not "empty".** `Item` and `DetailItem` use `option` for non-required parts (label, unit, status, color, link, detail). Use `None` to omit; reserve `emptyValue` for a deliberately blank required `Value`.
- **Records are immutable.** Build new values with `{ existing with Field = ... }` rather than expecting mutation.

## Recommended API Usage

- Create detail rows through `Profiler.Detail.createItem label value`, which fills every optional field with `None`. Layer optional data on top with the pipeline builders instead of restating the whole record.
- `Profiler.Detail.addColor` and `Profiler.Detail.addLink` each take a value and a `DetailItem` and return an updated `DetailItem`, so they chain naturally with `|>`. See `examples.md` → Realistic Example.
- Reuse `Profiler.emptyValue` wherever a blank `Value` is intentional rather than allocating `Value ""` repeatedly.
- Wrap an `Item list` in `Toolbar` to form the top-level structure.

## Error Handling

This library defines types only; it performs no validation, raises no exceptions, and returns no result types. Any validation (e.g. ensuring a `Link` is a well-formed URL) belongs to the calling client/server code.

## Composition

- Prefer the builder pipeline `createItem |> addColor |> addLink` to assemble a `DetailItem` incrementally; it reads top-to-bottom and avoids repeating untouched fields. See `examples.md` → Realistic Example.
- Build an `Item` as a record literal, supplying its `Detail` as a list of composed `DetailItem` values, then collect items into a `Toolbar`. See `examples.md` → Integration Example.

## Integration with Other Libraries

These types are the shared contract between the profiler client and server. Produce values on one side and consume the identical types on the other; do not redefine parallel types locally. The only external dependency is `FSharp.Core`, which keeps the package Fable-compatible — the same source is consumed from .NET and from Fable projects.

## Naming Conventions

- Access everything qualified: `Profiler.Item`, `Profiler.Detail.createItem`, etc. The module is `[<RequireQualifiedAccess>]`.
- Match the existing single-case union convention (`Value "…"`, `Label "…"`) when constructing values.

## Testing Recommendations

- Rely on `Label`'s custom equality: two labels differing only by surrounding whitespace or letter case compare equal. Assert this behavior explicitly when it matters. See `examples.md` → Test Example.
- `Label` has `NoComparison`; tests must not sort or order labels.
- Because the package is pure data, tests exercise construction, equality, and the `Detail` builders rather than side effects.
