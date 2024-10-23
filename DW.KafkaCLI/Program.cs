// See https://aka.ms/new-console-template for more information
using CommandLine;
using Confluent.Kafka;
using DW.KafkaCLI;

var parser = new Parser(with => with.IgnoreUnknownArguments = true);
var cliArgs = Environment.GetCommandLineArgs();
var options = parser.ParseArguments<KafkaCLIOptions>(cliArgs).Value;
var client = new AdminClientBuilder(new AdminClientConfig()
{
    BootstrapServers = options.BootstrapServers,
}).Build();

switch (options.Action.ToLowerInvariant())
{
    case "consume":
        var consumerResult = parser.ParseArguments<ConsumerActionOptions>(cliArgs);
        consumerResult.Errors.ToList()
            .ForEach(x => Console.WriteLine(x.Tag));
        var consumerOptions = consumerResult.Value;
        await Consume(consumerOptions);
        break;
    case "produce":
        var producerResult = parser.ParseArguments<ProducerActionOptions>(cliArgs);
        producerResult.Errors.ToList()
            .ForEach(x => Console.WriteLine(x.Tag));
        var producerOptions = producerResult.Value;
        await Produce(producerOptions);
        break;
    default:
        throw new ArgumentException($"Указаное действие '{options.Action}' не найдено", nameof(options.Action));
}

async Task Consume(ConsumerActionOptions options)
{
    if (string.IsNullOrWhiteSpace(options.ConsumerGroupId))
    {
        options.ConsumerGroupId = Guid.NewGuid().ToString();
    }

    Console.WriteLine($"Выбран режим чтения топика {options.Topic}");
    var consumer = new ConsumerBuilder<Ignore, string>(new ConsumerConfig()
    {
        BootstrapServers = options.BootstrapServers,
        GroupId = options.ConsumerGroupId,
        AutoOffsetReset = AutoOffsetReset.Earliest,
    }).Build();

    consumer.Subscribe(options.Topic);

    Console.WriteLine($"Консьюмер подписал, ожидание сообщения...");
    while (true)
    {
        var message = consumer.Consume();
        Console.WriteLine($"offset '{message.Offset.Value}'; key - '{message.Message.Key}'; value - '{message.Message.Value}'");
    }
}

async Task Produce(ProducerActionOptions options)
{
    Console.WriteLine($"Выбран режим записи в топик {options.Topic}");
    var producer = new ProducerBuilder<string, string>(new ConsumerConfig()
    {
        BootstrapServers = options.BootstrapServers,
    }).Build();

    var message = new Message<string, string>();
    message.Value = options.Value;
    message.Key = options.Key;

    producer.Produce(options.Topic, message);
    Console.WriteLine($"Сообщение успешно отправлено");
}