namespace ServiceBus.DuplicatesDetectorPlugin
{
    /// <summary>
    /// <see cref="DuplicateDetector"/> plugin configuration.
    /// </summary>
    public class DuplicatesDetectorConfiguration
    {
        /// <summary>
        /// Configuration with default duplicate detection strategy.
        /// </summary>
        /// <param name="propertyName">User-property name to use to indicate if message is a duplicate or not.
        /// <remarks>Defaults to "DuplicateDetector.IsDuplicate".</remarks>
        /// </param>
        /// <param name="shouldContinueOnException">Should plugin continue if execution failed or should it throw an exception.
        /// <remarks>Defaults to false to allow further message processing.</remarks>
        /// </param>
        public DuplicatesDetectorConfiguration(string propertyName = "DuplicateDetector.IsDuplicate", bool shouldContinueOnException = false)
        {
            UserPropertyName = propertyName;
            ShouldContinueOnException = shouldContinueOnException;
        }

        internal string UserPropertyName { get; }

        internal bool ShouldContinueOnException { get; }
    }
}