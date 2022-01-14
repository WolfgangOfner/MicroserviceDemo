using System;
using System.Threading.Tasks;
using KedaDemo.Messaging.Receiver;
using KedaDemo.Messaging.Writer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KedaDemoApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ServiceBusProcessingController : ControllerBase
    {
        private readonly IServiceBusMessageReceiver _serviceBusMessageReceiver;
        private readonly IServiceBusMessageWriter _serviceBusMessageWriter;

        public ServiceBusProcessingController(IServiceBusMessageReceiver serviceBusMessageReceiver, IServiceBusMessageWriter serviceBusMessageWriter)
        {
            _serviceBusMessageReceiver = serviceBusMessageReceiver;
            _serviceBusMessageWriter = serviceBusMessageWriter;
        }

        /// <summary>
        ///     Action to start processing the queue items.
        /// </summary>
        /// <returns>Returns HTTP Code indicating success or failure of the operation</returns>
        /// <response code="200">Returned when all items are processed.</response>
        /// <response code="400">Returned if something went wrong wile reading items from the queue.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<int>> ProcessQueueItems()
        {
            var messagesReceived = 0;

            try
            {
                messagesReceived = await _serviceBusMessageReceiver.ReceiveMessagesFromKedaQueue();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(messagesReceived);
        }

        /// <summary>
        ///     Action to add new messages to the queue.
        /// </summary>
        /// <returns>Returns HTTP Code indicating success or failure of the operation</returns>
        /// <response code="200">Returned when all items are processed.</response>
        /// <response code="400">Returned if something went wrong wile reading items from the queue.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateQueueItems(int numberOfQueueItems)
        {
            try
            {
                await _serviceBusMessageWriter.WriteMessagesToKedaQueue(numberOfQueueItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}