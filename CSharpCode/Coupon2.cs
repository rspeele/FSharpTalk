using System;

namespace CSharpCode2
{
    // A second attempt at modeling the Coupon type from
    // https://stripe.com/docs/api#coupon_object

    public class CouponDiscount { }
    public sealed class AmountOffCouponDiscount : CouponDiscount
    {
        public string Currency { get; }
        public decimal Amount { get; }
        public AmountOffCouponDiscount(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }
    }
    public sealed class PercentOffCouponDiscount : CouponDiscount
    {
        public decimal PercentOff { get; }
        public PercentOffCouponDiscount(decimal percentOff)
        {
            PercentOff = percentOff;
        }
    }
    public class Coupon
    {
        public string Id { get; set; }
        // etc...
        public CouponDiscount Discount { get; set; }
        // etc...
    }

    public static class ExampleConsumer
    {
        public static Coupon ExampleCoupon()
            => new Coupon
            {
                Id = "example1",
                // etc.
                Discount = new AmountOffCouponDiscount("USD", 1.0m),
            };

        public static decimal DiscountedPrice
            (CouponDiscount discount, string priceCurrency, decimal price)
        {
            // C# 7 feature: this was much worse just a few months ago.
            switch (discount)
            {
                case PercentOffCouponDiscount percentOff:
                    return price - (price * percentOff.PercentOff / 100.0m);
                case AmountOffCouponDiscount amountOff:
                    if (priceCurrency == amountOff.Currency) {
                        return price - amountOff.Amount;
                    }
                    throw new ArgumentException
                        ("Currency does not match", "priceCurrency");
                // we always have to handle this since somebody could subclass
                // CouponDiscount with an unexpected type
                default:
                    throw new ArgumentException
                        ("Unknown type", nameof(discount));
            }
        }
    }
}
