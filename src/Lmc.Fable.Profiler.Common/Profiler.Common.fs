namespace Lmc.Profiler.Common

[<RequireQualifiedAccess>]
module Profiler =
    type Token = Token of string

    type Id = Id of string
    type Label = Label of string
    type Value = Value of string
    type ValueDetail = ValueDetail of string
    type Unit = Unit of string
    type Link = Link of string

    type Color =
        | Yellow
        | Green
        | Red
        | Gray

    type Status = {
        Color: Color option
        Value: Value    // todo - this should be something like Icon
    }

    type DetailItem = {
        ShortLabel: Label option
        Label: Label
        Detail: ValueDetail option
        Value: Value
        Color: Color option
        Link: Link option
    }

    [<RequireQualifiedAccess>]
    module Detail =
        let createItem label value =
            {
                ShortLabel = None
                Label = label
                Detail = None
                Value = value
                Color = None
                Link = None
            }

        let addLink: Link -> DetailItem -> DetailItem = fun link item -> { item with Link = Some link }
        let addColor: Color -> DetailItem -> DetailItem = fun color item -> { item with Color = Some color }

    type Item = {
        Id: Id
        Label: Label option
        Value: Value
        ItemColor: Color option
        Unit: Unit option
        StatusIcon: Status option
        Detail: DetailItem list
    }

    type Toolbar = Toolbar of Item list

    let emptyValue = Value ""
