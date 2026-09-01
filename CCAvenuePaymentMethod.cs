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

        // Hosted form flow (redirect to CCAvenue)
        public override PaymentMethodType PaymentMethodType => PaymentMethodType.PreparedForm;

        // Grouped under bank card payments
        public override PaymentMethodGroupType PaymentMethodGroupType => PaymentMethodGroupType.BankCard;
    }
}
