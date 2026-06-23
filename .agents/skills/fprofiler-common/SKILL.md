---
name: fprofiler-common
description: >-
  Use whenever generating or reviewing F# (or Fable) code that builds profiler UI data using the Profiler module from Alma.Profiler.Common — e.g. constructing Item, Toolbar, DetailItem, Status, or Color values, or composing detail items with Detail.createItem, Detail.addColor, and Detail.addLink. Trigger also on mentions of profiler Token/Id/Label/Value/Unit/Link wrappers, the case-insensitive Profiler.Label equality, emptyValue, or sharing profiler types between a client and a server.
---

# F-Profiler-Common

Library: [alma-oss/fprofiler-common](https://github.com/alma-oss/fprofiler-common)
NuGet: `Alma.Profiler.Common`

## Purpose

Provides the shared profiler domain types used by both client and server profiler implementations. It defines a small, Fable-compatible set of value wrappers and records that describe a profiler toolbar — items, their statuses, colors, and detail rows — so producers and consumers agree on one contract. The library contains only type definitions and a few constructor/builder helpers; it has no runtime behavior beyond that.

## When to Use

- Building or rendering a profiler toolbar (`Item`, `Toolbar`) and its detail rows (`DetailItem`).
- Wrapping primitive profiler data in the typed single-case unions (`Token`, `Id`, `Label`, `Value`, `Unit`, `Link`).
- Assigning visual state through `Status` and `Color`.
- Sharing the same profiler type contract between a client project and a server project.

## When NOT to Use

- You need profiler transport, serialization, rendering, or collection logic — that lives in the client/server implementations, not here.
- You need general-purpose domain modeling unrelated to a profiler toolbar.

## Main Concepts

- **`Profiler`** — the single `[<RequireQualifiedAccess>]` module holding every type and helper.
- **`Token` / `Id`** — single-case string wrappers identifying a profiler session and an item.
- **`Label`** — single-case string wrapper with custom equality: compared case-insensitively and whitespace-trimmed; no ordering.
- **`Value` / `ValueDetail` / `Unit` / `Link`** — single-case string wrappers for displayed text, secondary text, unit, and URL.
- **`Color`** — closed set of states: `Yellow`, `Green`, `Red`, `Gray`.
- **`Status`** — optional color plus a `Value` acting as an icon indicator.
- **`DetailItem`** — one detail row: required `Label`/`Value`, optional short label, detail, color, and link.
- **`Detail`** — submodule with `createItem` and the pipeline builders `addLink` and `addColor`.
- **`Item`** — one toolbar entry: `Id`, `Value`, an optional label/unit/status, color, and a list of `DetailItem`.
- **`Toolbar`** — single-case wrapper around an `Item list`.
- **`emptyValue`** — the shared `Value ""` constant.

## Related Libraries

The matching profiler client and server implementations consume these types; this package is their shared dependency. It depends only on `FSharp.Core`.

## Keywords for Search

profiler, toolbar, Profiler module, Item, Toolbar, DetailItem, Status, Color, Label, Token, Id, Value, ValueDetail, Unit, Link, Detail.createItem, addColor, addLink, emptyValue, case-insensitive label equality, single-case union, Fable, FSharp.Core, Alma.Profiler.Common, shared client server types

## Reference Files

For composition principles and recommended API usage, read `references/preferred-patterns.md`. For known pitfalls and incorrect assumptions, read `references/anti-patterns.md`. For worked code examples, read `references/examples.md`.
