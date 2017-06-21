open System.Collections.Generic

type JValue =
    | JString of string
    | JNumber of decimal
    | JBoolean of bool
    | JNull

type JToken =
    | JValue of JValue
    | JArray of JToken list
    | JObject of (string * JToken) list

// transforms
//      { x: 1, y: { z: 2 } }
// into:
//      [["x", 1], ["y", [["z", 2]]]

let rec objectsToArrays(jToken) =
    match jToken with
    | JValue(_) -> jToken // leave values alone
    | JArray(tokens) ->
        let transformedTokens =
            [
                for token in tokens do
                    yield objectsToArrays(token)
            ]
        JArray(transformedTokens)
    | JObject(fields) ->
        let transformedFields =
            [
                for name, value in fields do
                    yield JArray([ JValue(JString name); objectsToArrays(value) ])
            ]
        JArray(transformedFields)
