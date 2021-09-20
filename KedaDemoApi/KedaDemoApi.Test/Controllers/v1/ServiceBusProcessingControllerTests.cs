using System;
using System.Net;
using FakeItEasy;
using FluentAssertions;
using KedaDemo.Messaging.Receiver;
using KedaDemo.Messaging.Writer;
using KedaDemoApi.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace KedaDemoApi.Test.Controllers.v1
{
    public class ServiceBusProcessingControllerTests
    {
        private readonly ServiceBusProcessingController _testee;
        private readonly IServiceBusMessageReceiver _serviceBusMessageReceiver;
        private readonly IServiceBusMessageWriter _serviceBusMessageWriter;

        public ServiceBusProcessingControllerTests()
        {
            _serviceBusMessageReceiver = A.Fake<IServiceBusMessageReceiver>();
            _serviceBusMessageWriter = A.Fake<IServiceBusMessageWriter>();
            _testee = new ServiceBusProcessingController(_serviceBusMessageReceiver, _serviceBusMessageWriter);
        }

        [Theory]
        [InlineData("Could not read messages from Service Bus")]
        public async void ProcessQueueItems_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _serviceBusMessageReceiver.ReceiveMessagesFromKedaQueue()).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.ProcessQueueItems();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }


        [Theory]
        [InlineData("Something went wrong")]
        public async void CreateQueueItems_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _serviceBusMessageWriter.WriteMessagesToKedaQueue(A<int>._)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.ProcessQueueItems();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void CreateQueueItems_ShouldReturnHttpOk()
        {
            var result = await _testee.ProcessQueueItems();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.OK);
        }

        [Fact]
        public async void ProcessQueueItems_ShouldReturnHttpOk()
        {
            A.CallTo(() => _serviceBusMessageReceiver.ReceiveMessagesFromKedaQueue()).Returns(5);

            var result = await _testee.ProcessQueueItems();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.OK);
        }

        [Fact]
        public async void ProcessQueueItems_ShouldReturnNumberOfProcessedItems()
        {
            A.CallTo(() => _serviceBusMessageReceiver.ReceiveMessagesFromKedaQueue()).Returns(5);

            var result = await _testee.ProcessQueueItems();

            ((OkObjectResult) result.Result).Value.Should().Be(5);
        }
    }
}