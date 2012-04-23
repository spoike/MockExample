using NUnit.Framework;

namespace MockExample.BL.Tests
{
    [TestFixture]
    public class OrderHandlerTest
    {
        // Mock implementation
        public class MockedOrderRepository : IOrderRepository
        {
            public bool HasCalledStore = false;

            public Order GivenOrder = null;

            public void Store(Order o)
            {
                HasCalledStore = true;
                GivenOrder = o;
            }
        }

        private OrderHandler _orderHandler;

        private MockedOrderRepository _mockedOrderRepository;

        [SetUp]
        public void SetUp()
        {
            _orderHandler = new OrderHandler();
            _mockedOrderRepository = new MockedOrderRepository();
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
            _orderHandler.OrderRepository = _mockedOrderRepository;

            // ACT
            // Submit order through _orderHandler
            _orderHandler.Submit(order);

            // ASSERT
            // Assert that OrderHandler object has saved it to the database
            // via the IOrderRepository interface
            Assert.That(_mockedOrderRepository.HasCalledStore, Is.True);
            Assert.That(_mockedOrderRepository.GivenOrder, Is.SameAs(order));
        }

    }
}
