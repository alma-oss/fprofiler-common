# Anti-Patterns

Each entry below is **mistake → why → fix**.

## Common Mistakes

- **Comparing `Label` by its raw inner string** → bypasses the type's custom equality, so values that the library treats as equal (different case or surrounding whitespace) look different → compare `Label` values directly with `=`; the type already normalizes via trim + lowercase.

- **Sorting or ordering `Label` values** → `Label` is declared `NoComparison`, so this fails to compile or forces an unsound workaround → key, group, or deduplicate by equality instead of ordering; if you truly need order, order by some other field.

- **Hand-writing a full `DetailItem` record just to set one optional field** → verbose and easy to get wrong as fields are added → start from `Profiler.Detail.createItem label value` and layer `addColor` / `addLink` via `|>`.

- **Passing bare strings where a wrapper is expected** → defeats the purpose of the single-case unions and lets unrelated values be swapped → always construct the wrapper (`Value "…"`, `Label "…"`, `Link "…"`, etc.).

- **Using `Value ""` to mean "no value"** → conflates an intentional blank with absence and duplicates a shared constant → use `Profiler.emptyValue` for a deliberate blank `Value`, and `None` for genuinely optional fields.

- **Accessing types unqualified (`Item`, `Detail.createItem`)** → the module is `[<RequireQualifiedAccess>]`, so this will not resolve → qualify everything: `Profiler.Item`, `Profiler.Detail.createItem`.

## Do Not Use / Avoid

- **Do not redefine local copies of these types** in client or server code → two divergent definitions break the shared contract → depend on `Alma.Profiler.Common` and use its types on both sides.

- **Avoid adding dependencies through this layer** → extra dependencies can break Fable compatibility → keep usage limited to these types plus `FSharp.Core`.

## Wrong Abstractions

- **Expecting validation, parsing, or rendering helpers here** → the library is type definitions plus a few constructors only; there is no behavior to call → put validation/rendering in the consuming client/server code.

- **Mutating records in place** → records are immutable → derive new values with `{ item with Field = … }`.

## Legacy Usage

- **Assuming a non-`Alma` namespace or pre-net10 target** → the package now lives under the `Alma` namespace and targets current .NET → reference it as `Alma.Profiler.Common` and open `Alma.Profiler.Common`.
