using System;

namespace CSharpCode1
{
    // From documentation at:
    // https://stripe.com/docs/api#coupon_object

    public enum CouponDuration
    {
        Once,
        Forever,
        Repeating,
    }
    public class Coupon
    {
        public string Id { get; set; }
        public decimal? AmountOff { get; set; } // if non-null, flat dollar amount to take off
        public decimal? PercentOff { get; set; } // if non-null, percentage to take off

        public DateTime Created { get; set; }
        public string Currency { get; set; } // only valid if amount_off is non-null
        public CouponDuration Duration { get; set; }
        public int? DurationInMonths { get; set; } // only valid if duration is CouponDuration.Repeating

        // etc...
    }
}
