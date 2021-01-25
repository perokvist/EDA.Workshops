using System.Collections.Generic;

namespace Shipping.Tests
{
    public class GoodsShipped : IEvent
    {
        public string SourceId => "shipping.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public class PaymentReceived : IEvent
    {
        public string SourceId => "payment.order";
        public IDictionary<string, string> Meta { get; set; }
    }

    public class GoodsPicked : IEvent
    {
        public string SourceId => "warehouse.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public class CompletePayment : ICommand
    { }

    public class CompletePacking : ICommand
    { }

    public class PaymentComplete : IEvent
    {
        public string SourceId => "shipping.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public class PackingComplete : IEvent
    {
        public string SourceId => "shipping.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public interface ICommand
    { }

    public interface IEvent
    {
        string SourceId { get; }
        IDictionary<string, string> Meta { get; }
    }
}
