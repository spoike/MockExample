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
        private Mock<IOrderPrinter> _mockedOrderPrinter;
        private Mock<IOrderView> _mockedOrderView;
            
        [SetUp]
        public void SetUp()
        {
            _orderHandler = new OrderHandler();
            _mockedOrderRepository = new Mock<IOrderRepository>();
            _mockedOrderPrinter = new Mock<IOrderPrinter>();
            _mockedOrderView = new Mock<IOrderView>();

            _orderHandler.OrderRepository = _mockedOrderRepository.Object;
            _orderHandler.OrderPrinter = _mockedOrderPrinter.Object;
            _orderHandler.OrderView = _mockedOrderView.Object;
        }

        private Order GivenOrderWithRows()
        {
            var order = _orderHandler.NewOrder();
            order.OrderRows.Add(new OrderRow() {Amount = 1, ArticleName = "Wrench"});
            return order;
        }

        private Order GivenOrderWithoutRows()
        {
            var order = _orderHandler.NewOrder();
            return order;
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
            var order = GivenOrderWithRows();

            // ASSERT
            Assert.That(order, Is.Not.SameAs(_orderHandler.NewOrder()));
        }

        [Test]
        public void WhenOrderIsSubmittedShouldSaveItToDatabase()
        {
            // ARRANGE
            var order = GivenOrderWithRows();

            // ACT
            _orderHandler.Submit(order);

            // ASSERT
            _mockedOrderRepository.Verify(orderRepo => orderRepo.Store(order));
        }

        [Test]
        public void WhenOrderIsSubmittedShouldSendToPrinter()
        {
            // ARRANGE
            var order = GivenOrderWithRows();

            // ACT
            _orderHandler.Submit(order);

            // ASSERT
            _mockedOrderPrinter.Verify(orderPrinter => orderPrinter.Print(order));
        }

        [Test]
        public void WhenOrderIsSubmittedShouldShowReceipt()
        {
            // ARRANGE
            var order = GivenOrderWithRows();

            // ACT
            _orderHandler.Submit(order);

            // ASSERT
            _mockedOrderView.Verify(orderView => orderView.ShowReceipt(order));
        }

        [Test]
        public void WhenEmptyOrderIsSubmittedShouldAlertThatTheOrderIsEmpty()
        {
            // ARRANGE
            var order = GivenOrderWithoutRows();

            // ACT
            _orderHandler.Submit(order);

            // ASSERT
            _mockedOrderView.Verify(view => view.ShowReceipt(order), Times.Never());
            _mockedOrderView.Verify(view => view.AlertIsEmpty());
        }

    }
}
