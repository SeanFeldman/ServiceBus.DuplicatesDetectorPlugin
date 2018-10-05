namespace ServiceBus.DuplicatesDetectorPlugin
{
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Core;

    /// <summary>Service Bus plugin to de-duplicate incoming messages.</summary>
    public static class DuplicateDetectorExtensions
    {
        /// <summary>
        /// Instantiate and register plugin with defaults.
        /// </summary>
        /// <param name="client"><see cref="QueueClient"/>, <see cref="SubscriptionClient"/>, <see cref="QueueClient"/>, <see cref="MessageSender"/>, <see cref="MessageReceiver"/>, or <see cref="SessionClient"/> to register plugin with.</param>
        /// <returns>Registered plugin as <see cref="ServiceBusPlugin"/>.</returns>
        public static ServiceBusPlugin RegisterDuplicateDetectorPlugin(this IClientEntity client)
        {
            var configuration = new DuplicatesDetectorConfiguration();
            var duplicatesDetector = new DefaultDuplicatesDetector(configuration.UserPropertyName);

            return RegisterDuplicateDetectorPlugin(client, configuration, duplicatesDetector);
        }

        /// <summary>
        /// Instantiate and register plugin with defaults.
        /// </summary>
        /// <param name="client"><see cref="QueueClient"/>, <see cref="SubscriptionClient"/>, <see cref="QueueClient"/>, <see cref="MessageSender"/>, <see cref="MessageReceiver"/>, or <see cref="SessionClient"/> to register plugin with.</param>
        /// <param name="configuration">Configuration object.</param>
        /// <param name="duplicatesDetector">Custom duplicates detector strategy.
        /// <remarks>When not supplied, used the default strategy with user-property name supplied via configuration</remarks></param>
        /// <returns>Registered plugin as <see cref="ServiceBusPlugin"/>.</returns>
        public static ServiceBusPlugin RegisterDuplicateDetectorPlugin(this IClientEntity client, DuplicatesDetectorConfiguration configuration, IDetectDuplicates duplicatesDetector = null)
        {
            var duplicatesDetectorToUse = duplicatesDetector ?? new DefaultDuplicatesDetector(configuration.UserPropertyName);
            var plugin = new DuplicateDetector(duplicatesDetectorToUse, configuration.ShouldContinueOnException);

            client.RegisterPlugin(plugin);

            return plugin;
        }
    }
}