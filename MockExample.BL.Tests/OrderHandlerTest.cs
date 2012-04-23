using Moq;
using NUnit.Framework;

namespace MockExample.BL.Tests
{
    [TestFixture]
    public class OrderHandlerTest
    {

        // SUT
        private OrderHandler _orderHandler;

        // Mocks
        private Mock<IOrderRepository> _mockedOrderRepository;
            
        [SetUp]
        public void SetUp()
        {
            _orderHandler = new OrderHandler();
            _mockedOrderRepository = new Mock<IOrderRepository>();
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
            _orderHandler.OrderRepository = _mockedOrderRepository.Object;

            // ACT
            // Submit order through _orderHandler
            _orderHandler.Submit(order);

            // ASSERT
            // Assert that OrderHandler object has saved it to the database
            // via the IOrderRepository interface
            _mockedOrderRepository.Verify(orderRepo => orderRepo.Store(order));
        }

    }
}
