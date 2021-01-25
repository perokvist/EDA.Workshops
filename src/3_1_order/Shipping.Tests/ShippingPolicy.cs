namespace Shipping.Tests
{
    public class ShippingPolicy
    {
        public static ICommand When(PaymentReceived @event, Order state) => Ship(state);
        public static ICommand When(GoodsPicked @event, Order state) => Ship(state);

        private static ICommand Ship(Order state)
           => null;
    }

    public class Order
    {
        public bool Paid;
        public bool Packed;

        public Order When(IEvent @event) => this;

        public Order When(PaymentReceived @event) => this;
        public Order When(GoodsPicked @event) => this;
    }
}
