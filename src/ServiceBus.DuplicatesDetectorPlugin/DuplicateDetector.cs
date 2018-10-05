namespace ServiceBus.DuplicatesDetectorPlugin
{
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Core;

    class DuplicateDetector : ServiceBusPlugin
    {
        readonly IDetectDuplicates duplicatesDetector;

        public DuplicateDetector(IDetectDuplicates duplicatesDetector, bool configurationShouldContinueOnException)
        {
            this.duplicatesDetector = duplicatesDetector;
            ShouldContinueOnException = configurationShouldContinueOnException;
        }

        public override string Name { get; } = nameof(DuplicateDetector);

        public override bool ShouldContinueOnException { get; }

        public override Task<Message> AfterMessageReceive(Message message)
        {
            return duplicatesDetector.Detect(message);
        }
    }
}