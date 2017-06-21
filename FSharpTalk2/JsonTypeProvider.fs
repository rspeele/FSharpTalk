module JsonTypeProviderExample
open System
open System.IO
open FSharp.Data

type CouponDuration =
    | Once
    | Forever
    | Repeating of durationInMonths : int

type CouponDiscount =
    | AmountOff of currency : string * amount : decimal
    | PercentOff of percent : decimal

type Coupon =
    {
        Id : string
        Discount : CouponDiscount
        Duration : CouponDuration
        Created : DateTime
    }

type private CouponJson =
    JsonProvider<"""[
        {
            "id": "id1",
            "created": 1497858536,
            "duration": "once",
            "percent_off": 50.5
        },
        {
            "id": "id2",
            "amount_off": 1.5,
            "created": 1497858536,
            "currency": "usd",
            "duration": "repeating",
            "duration_in_months": 2
        }
    ]""", SampleIsList = true>

let couponFromJson(jsonText : string) =
    let json = CouponJson.Parse(jsonText)

    let discount =
        match json.Currency, json.AmountOff, json.PercentOff with
        | Some(currency), Some(amountOff), None ->
            AmountOff(currency, amountOff)
        | None, None, Some(percentOff) ->
            PercentOff(percentOff)
        | _ -> failwith "Invalid discount"

    let duration =
        match json.Duration, json.DurationInMonths with
        | "repeating", Some(months) ->
            Repeating(months)
        | "once", None -> Once
        | "forever", None -> Forever
        | _ -> failwith "Invalid duration"

    {
        Id = json.Id
        Discount = discount
        Duration = duration
        Created = DateTimeOffset.FromUnixTimeSeconds(int64(json.Created)).UtcDateTime
    }

let couponToJson(coupon : Coupon) =
    let currency, amountOff, percentOff =
        match coupon.Discount with
        | AmountOff(currency, amountOff) ->
            Some(currency), Some(amountOff), None
        | PercentOff(percent) ->
            None, None, Some(percent)

    let duration, durationInMonths =
        match coupon.Duration with
        | Once -> "once", None
        | Forever -> "forever", None
        | Repeating(durationInMonths) -> "repeating", Some(durationInMonths)

    let json =
          CouponJson.Root
            ( id = coupon.Id
            , amountOff = amountOff
            , percentOff = percentOff
            , currency = currency
            , duration = duration
            , durationInMonths = durationInMonths
            , created = int(DateTimeOffset(coupon.Created).ToUnixTimeSeconds())
            )
    json.JsonValue.ToString()