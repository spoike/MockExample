using NUnit.Framework;

namespace MockExample.BL.Tests
{
    [TestFixture]
    public class OrderHandlerTest
    {

        private OrderHandler _orderHandler;

        [SetUp]
        public void SetUp()
        {
            _orderHandler = new OrderHandler();
        }

        [Test]
        public void WhenCreatingNewOrderShouldNotReturnNull()
        {
            // ARRANGE

            // ACT
            var order = _orderHandler.NewOrder();

            // ASSERT
            Assert.That(order, Is.Not.Null);
        }

        [Test]
        public void WhenCreatingNewOrderShouldNotReturnTwiceTheSame()
        {
            // ARRANGE

            // ACT
            var order = _orderHandler.NewOrder();

            // ASSERT
            Assert.That(order, Is.Not.SameAs(_orderHandler.NewOrder()));
        }

        [Test]
        public void WhenOrderIsSubmittedShouldSaveItToDatabase()
        {
            // ARRANGE
            var order = _orderHandler.NewOrder();
            // Arrange the orderhandler to use a mock of IOrderRepository

            // ACT
            // Submit order through _orderHandler

            // ASSERT
            // Assert that OrderHandler object has saved it to the database
            // via the IOrderRepository interface
        }

    }
}
