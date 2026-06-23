# Examples

All code for this skill lives here. Examples are ordered from simplest to most complete; each is self-contained.

## Basic Example — wrapping values

Construct the single-case wrappers and a simple `Status`.

```fsharp
open Alma.Profiler.Common

let token = Profiler.Token "session-abc"
let itemId = Profiler.Id "node-1"
let label = Profiler.Label "Latency"
let value = Profiler.Value "42"
let unit = Profiler.Unit "ms"
let link = Profiler.Link "https://example.test/metrics"

let status: Profiler.Status =
    { Color = Some Profiler.Green
      Value = Profiler.Value "ok" }
```

## Realistic Example — composing a DetailItem

Start from `Detail.createItem` and layer optional data with the pipeline builders.

```fsharp
open Alma.Profiler.Common

let detail: Profiler.DetailItem =
    Profiler.Detail.createItem (Profiler.Label "Endpoint") (Profiler.Value "200")
    |> Profiler.Detail.addColor Profiler.Green
    |> Profiler.Detail.addLink (Profiler.Link "https://example.test/web-api")
```

## Integration Example — building an Item and a Toolbar

Assemble composed detail rows into an `Item`, then collect items into a `Toolbar`.

```fsharp
open Alma.Profiler.Common

let buildItem id label value status details : Profiler.Item =
    { Id = id
      Label = Some label
      Value = value
      ItemColor = Some Profiler.Yellow
      Unit = Some (Profiler.Unit "ms")
      StatusIcon = Some status
      Detail = details }

let serviceAItem =
    buildItem
        (Profiler.Id "service-a")
        (Profiler.Label "ServiceA")
        (Profiler.Value "120")
        { Color = Some Profiler.Green; Value = Profiler.Value "ok" }
        [ Profiler.Detail.createItem (Profiler.Label "Calls") (Profiler.Value "37")
          Profiler.Detail.createItem (Profiler.Label "Errors") (Profiler.Value "0")
          |> Profiler.Detail.addColor Profiler.Red ]

let workerItem =
    buildItem
        (Profiler.Id "worker")
        (Profiler.Label "Worker")
        Profiler.emptyValue
        { Color = Some Profiler.Gray; Value = Profiler.emptyValue }
        []

let toolbar = Profiler.Toolbar [ serviceAItem; workerItem ]
```

## Test Example — case-insensitive Label equality

`Label` compares trimmed and lowercased, and has no ordering.

```fsharp
open Alma.Profiler.Common

let labelsMatch =
    Profiler.Label "  Latency  " = Profiler.Label "latency"   // true

let labelsDiffer =
    Profiler.Label "Latency" = Profiler.Label "Throughput"    // false
```

## Full Workflow Example — produce on one side, consume on the other

The same `Toolbar` type is produced by a server and read by a client.

```fsharp
open Alma.Profiler.Common

// Producer side (e.g. WebApi)
let produceToolbar () : Profiler.Toolbar =
    let item =
        { Id = Profiler.Id "example-api"
          Label = Some (Profiler.Label "ExampleApi")
          Value = Profiler.Value "5"
          ItemColor = None
          Unit = Some (Profiler.Unit "rps")
          StatusIcon = Some { Color = Some Profiler.Green; Value = Profiler.Value "ok" }
          Detail =
            [ Profiler.Detail.createItem (Profiler.Label "Uptime") (Profiler.Value "99.9")
              |> Profiler.Detail.addColor Profiler.Green
              |> Profiler.Detail.addLink (Profiler.Link "https://example.test/status") ] }
    Profiler.Toolbar [ item ]

// Consumer side (e.g. DemoSystem client)
let summarize (Profiler.Toolbar items) =
    items
    |> List.map (fun item ->
        let (Profiler.Id id) = item.Id
        let (Profiler.Value v) = item.Value
        id, v, item.Detail.Length)
```
