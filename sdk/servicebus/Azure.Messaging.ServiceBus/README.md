# Azure Service Bus client library for .NET

Azure Service Bus allows you to build applications that take advantage of asynchronous messaging patterns using a highly-reliable service to broker messages between producers and consumers. Azure Service Bus provides flexible, brokered messaging between client and server, along with structured first-in, first-out (FIFO) messaging, and publish/subscribe capabilities with complex routing. If you would like to know more about Azure Service Bus, you may wish to review: [What is Azure Service Bus?](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview)

Use the client library for Azure Service Bus to:

- Transfer business data: leverage messaging for durable exchange of information, such as sales or purchase orders, journals, or inventory movements.

- Decouple applications: improve reliability and scalability of applications and services, relieving senders and receivers of the need to be online at the same time.

- Control how messages are processed: support traditional competing consumers for messages using queues or allow each consumer their own instance of a message using topics and subscriptions.

- Implement complex workflows: message sessions support scenarios that require message ordering or message deferral.

[Source code](.) | [Package (NuGet)](https://www.nuget.org/packages/Azure.Messaging.ServiceBus/) | [API reference documentation](https://azure.github.io/azure-sdk-for-net/servicebus.html) | [Product documentation](https://docs.microsoft.com/en-us/azure/service-bus/)

## Getting started

### Prerequisites

- **Microsoft Azure Subscription:** To use Azure services, including Azure Service Bus, you'll need a subscription. If you do not have an existing Azure account, you may sign up for a free trial or use your MSDN subscriber benefits when you [create an account](https://account.windowsazure.com/Home/Index).

- **Service Bus namespace:** To interact with Azure Service Bus, you'll also need to have a namespace available. If you are not familiar with creating Azure resources, you may wish to follow the step-by-step guide for [creating a Service Bus namespace using the Azure portal](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-create-namespace-portal). There, you can also find detailed instructions for using the Azure CLI, Azure PowerShell, or Azure Resource Manager (ARM) templates to create a Service bus entity.

- **C# 8.0:** The Azure Service Bus client library makes use of new features that were introduced in C# 8.0.  You can still use the library with older versions of C#, but some of its functionality won't be available.  In order to enable these features, you need to [target .NET Core 3.0](https://docs.microsoft.com/en-us/dotnet/standard/frameworks#how-to-specify-target-frameworks) or [specify the language version](https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/configure-language-version#override-a-default) you want to use (8.0 or above).  If you are using Visual Studio, versions prior to Visual Studio 2019 are not compatible with the tools needed to build C# 8.0 projects.  Visual Studio 2019, including the free Community edition, can be downloaded [here](https://visualstudio.microsoft.com/vs/).

  **Important Note:** The use of C# 8.0 is mandatory to run the [examples](#examples) and the [samples](#next-steps) without modification.  You can still run the samples if you decide to tweak them.

To quickly create the needed Service Bus resources in Azure and to receive a connection string for them, you can deploy our sample template by clicking:

[![](http://azuredeploy.net/deploybutton.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure%2Fazure-sdk-for-net%2Fmaster%2Fsdk%2Fservicebus%2FAzure.Messaging.ServiceBus%2Fassets%2Fsamples-azure-deploy.json)

### Install the package

Install the Azure Service Bus client library for .NET with [NuGet](https://www.nuget.org/):

```PowerShell
dotnet add package Azure.Messaging.ServiceBus --version 7.0.0-preview.1
```

### Authenticating the client

For the Service Bus client library to interact with a queue or topic, it will need to understand how to connect and authorize with it.  The easiest means for doing so is to use a connection string, which is created automatically when creating a Service Bus namespace.  If you aren't familiar with shared access policies in Azure, you may wish to follow the step-by-step guide to [get a Service Bus connection string](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-topics-subscriptions-portal#get-the-connection-string).

Once you have a connection string, you can authenticate your client with it.
```C# Snippet:ServiceBusAuthConnString
// Create a ServiceBusClient that will authenticate using a connection string
string connectionString = "<connection_string>";
ServiceBusClient client = new ServiceBusClient(connectionString);
```

To see how to authenticate using Azure.Identity, view this [example](#authenticating-with-azureidentity).

## Key concepts

Once you've initialized a `ServiceBusClient`, you can interact with the primary resource types within a Service Bus Namespace, of which multiple can exist and on which actual message transmission takes place, the namespace often serving as an application container:

* [Queue](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview#queues): Allows for Sending and Receiving of messages. Often used for point-to-point communication.

* [Topic](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview#topics): As opposed to Queues, Topics are better suited to publish/subscribe scenarios. A topic can be sent to, but requires a subscription, of which there can be multiple in parallel, to consume from.

* [Subscription](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-queues-topics-subscriptions#topics-and-subscriptions): The mechanism to consume from a Topic. Each subscription is independent, and receives a copy of each message sent to the topic. Rules and Filters can be used to tailor which messages are received by a specific subscription.

For more information about these resources, see [What is Azure Service Bus?](https://docs.microsoft.com/azure/service-bus-messaging/service-bus-messaging-overview).

To interact with these resources, one should be familiar with the following SDK concepts:

- A **Service Bus client** is the primary interface for developers interacting with the Service Bus client library. It serves as the gateway from which all interaction with the library will occur.

- A **Service Bus sender** is scoped to a particular queue or topic, and is created using the Service Bus client. The sender allows you to send messages to a queue or topic. It also allows for scheduling messages to be available for delivery at a specified date.

- A **Service Bus receiver** is scoped to a particular queue or subscription, and is created using the Service Bus client. The receiver allows you to receive messages from a queue or subscription. It also allows the messages to be settled. There are four ways of  settling messages:
  * Complete - causes the message to be deleted from the queue or topic.
  * Abandon - releases the receiver's lock on the message allowing for the message to be received by other receivers.
  * Defer - defers the message from being received by normal means. In order to receive deferred messages, the sequence number of the message needs to be retained.
  * DeadLetter - moves the message to the Dead Letter queue. This will prevent the message from being received again. In order to receive messages from the Dead Letter queue, a receiver scoped to the Dead Letter queue is needed.

- A **Service Bus session receiver** is scoped to a particular session-enabled queue or subscription, and is created using the Service Bus client. The session receiver is almost identical to the standard receiver, with the difference being that session management operations are exposed which only apply to session-enabled entities. These operations include getting and setting session state, as well as renewing session locks.

- A **Service Bus processor** is scoped to a particular queue or subscription, and is created using the Service Bus client. The processor allows you to configure an event handler to run for every message that is received. It also allows for specifying an exception handler that will run any time an exception is thrown while a message is being received and processed by the processor. The event handler args will provide the ability to settle a received message.

- A **Service Bus session processor** is scoped to a particular session-enabled queue or subscription, and is created using the Service Bus client. The session processor is almost identical to the standard processor, with the difference being that session management operations are exposed which only apply to session-enabled entities.

For more concepts and deeper discussion, see: [Service Bus Advanced Features](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview#advanced-features).

## Examples

### Sending and receiving a message

Message sending is performed using the `ServiceBusSender`. Receiving is performed using the 
`ServiceBusReceiver`.

```C# Snippet:ServiceBusSendAndReceive
string connectionString = "<connection_string>";
string queueName = "<queue_name>";
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(connectionString);

// create the sender
ServiceBusSender sender = client.CreateSender(queueName);

// create a message that we can send
ServiceBusMessage message = new ServiceBusMessage(Encoding.Default.GetBytes("Hello world!"));

// send the message
await sender.SendAsync(message);

// create a receiver that we can use to receive the message
ServiceBusReceiver receiver = client.CreateReceiver(queueName);

// the received message is a different type as it contains some service set properties
ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveAsync();

// get the message body as a string
string body = Encoding.Default.GetString(receivedMessage.Body.ToArray());
Console.WriteLine(body);
```

### Sending and receiving a batch of messages

We can send several messages at once using a `ServiceBusMessageBatch`. 

```C# Snippet:ServiceBusSendAndReceiveBatch
string connectionString = "<connection_string>";
string queueName = "<queue_name>";
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(connectionString);

// create the sender
ServiceBusSender sender = client.CreateSender(queueName);

// create a message batch that we can send
ServiceBusMessageBatch messageBatch = await sender.CreateBatchAsync();
messageBatch.TryAdd(new ServiceBusMessage(Encoding.UTF8.GetBytes("First")));
messageBatch.TryAdd(new ServiceBusMessage(Encoding.UTF8.GetBytes("Second")));

// send the message batch
await sender.SendAsync(messageBatch);

// create a receiver that we can use to receive the messages
ServiceBusReceiver receiver = client.CreateReceiver(queueName);

// the received message is a different type as it contains some service set properties
IList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveBatchAsync(maxMessages: 2);

foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
{
    // get the message body as a string
    string body = Encoding.Default.GetString(receivedMessage.Body.ToArray());
    Console.WriteLine(body);
}
```

### Complete a message

```C# Snippet:ServiceBusCompleteMessage
string connectionString = "<connection_string>";
string queueName = "<queue_name>";
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(connectionString);

// create the sender
ServiceBusSender sender = client.CreateSender(queueName);

// create a message that we can send
ServiceBusMessage message = new ServiceBusMessage(Encoding.Default.GetBytes("Hello world!"));

// send the message
await sender.SendAsync(message);

// create a receiver that we can use to receive and settle the message
ServiceBusReceiver receiver = client.CreateReceiver(queueName);

// the received message is a different type as it contains some service set properties
ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveAsync();

// complete the message, thereby deleting it from the service
await receiver.CompleteAsync(receivedMessage);
```

### Abandon a message

```C# Snippet:ServiceBusAbandonMessage
ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveAsync();

// abandon the message, thereby releasing the lock and allowing it to be received again by this or other receivers
await receiver.AbandonAsync(receivedMessage);
```

### Defer a message

```C# Snippet:ServiceBusDeferMessage
ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveAsync();

// defer the message, thereby preventing the message from being received again without using
// the received deferred message API.
await receiver.DeferAsync(receivedMessage);

// receive the deferred message by specifying the service set sequence number of the original
// received message
ServiceBusReceivedMessage deferredMessage = await receiver.ReceiveDeferredMessageAsync(receivedMessage.SequenceNumber);
```

### Dead letter a message

```C# Snippet:ServiceBusDeadLetterMessage
ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveAsync();

// deadletter the message, thereby preventing the message from being received again without receiving from the dead letter queue.
await receiver.DeadLetterAsync(receivedMessage);

// receive the dead lettered message with receiver scoped to the dead letter queue.
ServiceBusReceiver dlqReceiver = client.CreateDeadLetterReceiver(queueName);
ServiceBusReceivedMessage dlqMessage = await dlqReceiver.ReceiveAsync();
```

### Using the Processor

The `ServiceBusProcessor` offers automatic completion of processed messages, automatic message lock renewal, and concurrent execution of user specified event handlers.

```C# Snippet:ServiceBusProcessMessages
string connectionString = "<connection_string>";
string queueName = "<queue_name>";
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(connectionString);

// create the sender
ServiceBusSender sender = client.CreateSender(queueName);

// create a message batch that we can send
ServiceBusMessageBatch messageBatch = await sender.CreateBatchAsync();
messageBatch.TryAdd(new ServiceBusMessage(Encoding.UTF8.GetBytes("First")));
messageBatch.TryAdd(new ServiceBusMessage(Encoding.UTF8.GetBytes("Second")));

// send the message batch
await sender.SendAsync(messageBatch);

// get the options to use for configuring the processor
var options = new ServiceBusProcessorOptions
{
    // By default after the message handler returns, the processor will complete the message
    // If I want more fine-grained control over settlement, I can set this to false.
    AutoComplete = false,

    // I can also allow for multi-threading
    MaxConcurrentCalls = 2
};

// create a processor that we can use to process the messages
ServiceBusProcessor processor = client.CreateProcessor(queueName, options);

// since the message handler will run in a background thread, in order to prevent
// this sample from terminating immediately, we can use a task completion source that
// we complete from within the message handler.
TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = Encoding.Default.GetString(args.Message.Body.ToArray());
    Console.WriteLine(body);

    // we can evaluate application logic and use that to determine how to settle the message.
    await args.CompleteAsync(args.Message);
    tcs.SetResult(true);
}

Task ErrorHandler(ProcessErrorEventArgs args)
{
    // the error source tells me at what point in the processing an error occurred
    Console.WriteLine(args.ErrorSource);
    // the fully qualified namespace is available
    Console.WriteLine(args.FullyQualifiedNamespace);
    // as well as the entity path
    Console.WriteLine(args.EntityPath);
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}
await processor.StartProcessingAsync();

// await our task completion source task so that the message handler will be invoked at least once.
await tcs.Task;

// stop processing once the task completion source was completed.
await processor.StopProcessingAsync();
```

### Authenticating with Azure.Identity

The [Azure Identity library](https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/identity/Azure.Identity/README.md) provides easy Azure Active Directory support for authentication.
```C# Snippet:ServiceBusAuthAAD
// Create a ServiceBusClient that will authenticate through Active Directory
string fullyQualifiedNamespace = "yournamespace.servicebus.windows.net";
ServiceBusClient client = new ServiceBusClient(fullyQualifiedNamespace, new DefaultAzureCredential());
```

### Working with Sessions

[Sessions](https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-sessions) provide a mechanism for grouping related messages. In order to use sessions, you need to be working with a session-enabled entity.

- [Sending and receiving session messages](./samples/Sample03_SendReceiveSessions.md)
- [Using the session processor](./samples/Sample05_SessionProcessor.md)

## Troubleshooting

### Service Bus Exception

A `ServiceBusException` is triggered when an operation specific to Service Bus has encountered an issue, including both errors within the service and specific to the client.  The exception includes some contextual information to assist in understanding the context of the error and its relative severity.  These are:

- `IsTransient` : This identifies whether or not the exception is considered recoverable.  In the case where it was deemed transient, the appropriate retry policy has already been applied and retries were unsuccessful.

- `Reason` : Provides a set of well-known reasons for the failure that help to categorize and clarify the root cause.  These are intended to allow for applying exception filtering and other logic where inspecting the text of an exception message wouldn't be ideal.   Some key failure reasons are:

  - **Client Closed** : This occurs when an operation has been requested on a Service Bus client that has already been closed or disposed of. It is recommended to check the application code and ensure that objects from the Service Bus client library are created and closed/disposed in the intended scope.  

  - **Service Timeout** : This indicates that the Service Bus service did not respond to an operation within the expected amount of time.  This may have been caused by a transient network issue or service problem.  The Service Bus service may or may not have successfully completed the request; the status is not known.  It is recommended to attempt to verify the current state and retry if necessary.  

  - **Message Lock Lost** : This can occur if the processing takes longer than the lock duration specified at the entity level for a message. If this error occurs consistently, it may be worth increasing the message lock duration. Otherwise, callers can renew the message lock while they are processing the message to ensure that this error doesn't occur.
  
  - **Messaging Entity Not Found**: A Service Bus resource, such as a queue, topic, or subscription could not be found by the Service Bus service. This may indicate that it has been deleted from the service or that there is an issue with the Service Bus service itself.
  
Reacting to a specific failure reason for the `ServiceBusException` can be accomplished in several ways, such as by applying an exception filter clause as part of the `catch` block:
```csharp
try
{
    // Receive messages using the receiver client
}
catch (ServiceBusExceptions ex) where 
    (ex.Reason == ServiceBusException.FailureReason.ServiceTimeout)
{
    // Take action based on a service timeout
}
```
  
### Other exceptions

For detailed information about the failures represented by the `ServiceBusException` and other exceptions that may occur, please refer to [Service Bus messaging exceptions](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-exceptions).

You can also easily [enable console logging](https://github.com/Azure/azure-sdk-for-net/blob/master/sdk/core/Azure.Core/samples/Diagnostics.md#logging) if you want to dig
deeper into the requests you're making against the service.

## Next steps

Beyond the introductory scenarios discussed, the Azure Service Bus client library offers support for additional scenarios to help take advantage of the full feature set of the Azure Service Bus service. In order to help explore some of these scenarios, the Service Bus client library offers a project of samples to serve as an illustration for common scenarios. Please see the samples [README](./samples/README.md) for details.

## Contributing  

This project welcomes contributions and suggestions. Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

Please see our [contributing guide](./CONTRIBUTING.md) for more information.

![Impressions](https://azure-sdk-impressions.azurewebsites.net/api/impressions/azure-sdk-for-net%2Fsdk%2Fservicebus%2FAzure.Messaging.ServiceBus%2FREADME.png)
