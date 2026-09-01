using VirtoCommerce.PaymentModel;

namespace VirtoCommerce.Payment.CCAvenue
{
    public class CCAvenuePaymentMethod : PaymentMethod
    {
        public CCAvenuePaymentMethod() : base("CCAvenue")
        {
            Name = "CCAvenue";
            Description = "CCAvenue Payment Gateway";
            LogoUrl = "https://cdn.virtocommerce.com/payment/ccavenue.png";
            Code = "CCAvenue";
        }

        public override PaymentMethodType PaymentMethodType => PaymentMethodType.PreparedForm;
        public override PaymentMethodGroupType PaymentMethodGroupType => PaymentMethodGroupType.BankCard;
    }
}
