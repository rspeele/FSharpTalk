open System

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

// example: creating a coupon object

let exampleCoupon =
    {
        Id = "example1"
        Discount = AmountOff("USD", 1.00m)
        Duration = Repeating(durationInMonths = 1)
        Created = DateTime.UtcNow
    }

// example: consuming a coupon

let discountedPrice(coupon : Coupon, priceCurrency : string, price : decimal) =
    match coupon.Discount with
    | AmountOff(currency, amount) ->
        if priceCurrency = currency then
            price - amount
        else
            raise(ArgumentException("Currency does not match", "priceCurrency"))
    | PercentOff(percent) ->
        price - (price * percent / 100.0m)