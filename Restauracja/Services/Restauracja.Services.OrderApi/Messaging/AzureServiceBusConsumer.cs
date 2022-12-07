using Azure.Messaging.ServiceBus;
using Restauracja.MessageBus;
using Restauracja.Services.OrderApi.Model;
using Restauracja.Services.OrderApi.Repositories;
using Restauracja.Services.ShoppingCartApi.Model.Dto;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Restauracja.Services.OrderApi.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;
        private readonly string serviceBusConnectionString;
        private readonly string checkoutMessageTopic;
        private readonly string subscriptionCheckout;
        ServiceBusProcessor checkoutProccessor;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration, IMessageBus messageBus)
        {
            this._orderRepository = orderRepository;
            this._configuration = configuration;
            this._messageBus = messageBus;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            subscriptionCheckout = _configuration.GetValue<string>("SubscriptionCheckout");

            var client = new ServiceBusClient(serviceBusConnectionString);

            checkoutProccessor = client.CreateProcessor(checkoutMessageTopic, subscriptionCheckout);
        }

        public async Task Start()
        {
            checkoutProccessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            checkoutProccessor.ProcessErrorAsync += OnCheckoutMessageError;
            await checkoutProccessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await checkoutProccessor.StopProcessingAsync();
            await checkoutProccessor.DisposeAsync();
        }

        private Task OnCheckoutMessageError(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            Debug.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            CheckoutHeaderDto checkoutHeaderDto = JsonSerializer.Deserialize<CheckoutHeaderDto>(body);

            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDto.UserId,
                FirstName = checkoutHeaderDto.FirstName,
                LastName = checkoutHeaderDto.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDto.CardNumber,
                CouponCode = checkoutHeaderDto.CouponCode,
                CVV = checkoutHeaderDto.CVV,
                DiscountTotal = checkoutHeaderDto.DiscountTotal,
                Email = checkoutHeaderDto.Email,
                ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                PaymentStatus = false,
                Phone = checkoutHeaderDto.Phone,
                PickupDateTime = checkoutHeaderDto.PickupDateTime
            };

            foreach (var detailList in checkoutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = detailList.ProductId,
                    ProductName = detailList.Product.Name,
                    Price = detailList.Product.Price,
                    Count = detailList.Count
                };
                orderHeader.CartTotalItems += detailList.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(orderHeader);

            // Payment request ...
        }
    }
}
