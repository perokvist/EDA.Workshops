using System;
using Xunit;

namespace Shipping.Tests
{
    public class PolicyTests
    {
        [Fact]
        public void PickedDoesntIssueShip()
        {

            //Given
            var state = new IEvent[] {
            }.Rehydrate<Order>();

            //When
            var cmd = ShippingPolicy.When(new GoodsPicked(), state);

            //Then
            Assert.Null(cmd);
        }

        [Fact]
        public void PaidAndPickedIssueShip()
        {
            //Given
            var state = new IEvent[] {
                new PaymentReceived()
            }.Rehydrate<Order>();

            //When
            var cmd = ShippingPolicy.When(new GoodsPicked(), state);

            //Then
            Assert.NotNull(cmd);
            Assert.IsType<Ship>(cmd);
        }

        [Fact]
        public void PickedAndPaidIssueShip()
        {
            //Given
            var state = new IEvent[] {
                new GoodsPicked()
            }.Rehydrate<Order>();

            //When
            var cmd = ShippingPolicy.When(new PaymentReceived(), state);

            //Then
            Assert.NotNull(cmd);
            Assert.IsType<Ship>(cmd);
        }
    }
}
